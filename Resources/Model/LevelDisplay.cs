using Microsoft.AspNetCore.Components;
using Toolbelt.Blazor.SpeechSynthesis;
using CurrieTechnologies.Razor.SweetAlert2;


public abstract class LevelDisplay : ComponentBase, ILevelDisplay
{
    [Parameter]
    public int LevelNumber { get; set; }
    [Parameter]
    public string LevelType { get; set; }
    [Parameter]
    public string LevelTitle { get; set; }
    [Parameter]
    public int questionNumber { get; set; }
    public int score { get; set; }
    public int attempts { get; set; }

    public List<Question> CurrentQuestionSet { get; set; }
    public Question? CurrentQuestion { get; set; }
    //[Parameter]
    //public QuestionImage QuestionImage { get; set; }
    //[Parameter]
    //public FormattedString ImageAttribution { get; set; }
    [Parameter]
    public SpeechSynthesis Speaker { get; set; }

    [Parameter]
    public SweetAlertService Swal { get; set; }

    public bool AnswerButtonDisabled { get; set; } = false;

    public List<string> AnswerTexts = new();

    protected override void OnInitialized()
    {
        InitializeQuestionSet();
        InitializeAnswers();
        Update(true);

    }

    public virtual async Task ChooseAnswer(int buttonNumber)
    {
        if (CurrentQuestion != null)
        {
            await Task.Yield();
            AnswerButtonDisabled = true;
            bool isCorrect = CheckAnswer(buttonNumber);
            bool shouldUpdate = isCorrect;
            await Update(shouldUpdate);
            await Task.Delay(200);
            AnswerButtonDisabled = false;
        }
    }
    public virtual bool CheckAnswer(int buttonNumber)
    {
        bool isCorrect = AnswerTexts[buttonNumber] == CurrentQuestion.CorrectAnswer;
        UpdateScore(isCorrect);
        return isCorrect;
    }

    public async void GiveHint()
    {
        await SpeakAnswer();
    }

    public void InitializeQuestionSet()
    {
        CurrentQuestionSet = new();
        HashSet<Question> currentLevelHash = new();
        foreach (Question question in CurrentQuestionSet)
        {
            currentLevelHash.Add(question);
        }
        CurrentQuestionSet = currentLevelHash.OrderBy(x => Guid.NewGuid()).ToList();
    }

    public void InitializeAnswers()
    {
        AnswerTexts.Add("Correct");
        AnswerTexts.Add("Wrong1");
        AnswerTexts.Add("Wrong2");
    }

    public void LoadQuestion()
    {
        Random rnd = new Random();
        List<int> answerIndexes = new List<int> { 0, 1, 2 };
        var shuffledIndex = answerIndexes.OrderBy(a => rnd.Next()).ToList();

        CurrentQuestion = CurrentQuestionSet.First();
        CurrentQuestionSet.Remove(CurrentQuestion);
        SetLevelTitle();
    }

    public async Task ResetLevel()
    {
        questionNumber = 0;
        score = 0;
        attempts = 0;
        InitializeQuestionSet();
        await Update(true);
    }

    public async Task SpeakAnswer()
    {
        await Speaker.CancelAsync();
        if (CurrentQuestion != null)
        {
            await Speaker.SpeakAsync(CurrentQuestion.CorrectAnswer);
        }
    }

    public async Task Update(bool shouldUpdate)
    {
        SetLevelTitle();
        if (shouldUpdate)
        {
            if (CurrentQuestionSet.Count == 0)
            {
                await PromptReplay();
            }
            else
            {
                LoadQuestion();
                //await QuestionImage.Initialize(CurrentQuestion.CorrectAnswer);
                //GetFormattedImageAttribution();
                UpdateShownQuestion();
                //UpdateQuestionImage();
            }
        }
        SetLevelTitle();
    }

    public void SetLevelTitle()
    {
        try
        {
            LevelTitle = $"Level {LevelNumber.ToString()} Question {questionNumber.ToString()} - {CurrentQuestionSet.Count} left. Attempts: {attempts}. Score: {score}.";
        }
        catch { throw new Exception("LevelTitle label has not been created."); }
    }

    void ILevelDisplay.UpdateQuestionImage()
    {
        throw new NotImplementedException();
    }

    public void UpdateScore(bool isCorrect)
    {
        if (isCorrect)
        {
            score++;
            questionNumber++;
        }
        attempts++;
        LevelTitle = $"Level {LevelNumber.ToString()} Question {questionNumber.ToString()} - {CurrentQuestionSet.Count} left. Attempts: {attempts}. Score: {score}.";
    }

    public void UpdateShownQuestion()
    {
        Random rnd = new Random();
        List<int> answerIndexes = new List<int> { 0, 1, 2 };
        var shuffledIndex = answerIndexes.OrderBy(a => rnd.Next()).ToList();

        foreach (int i in shuffledIndex)
        {
            switch (i)
            {
                case 0:
                    AnswerTexts.Add(CurrentQuestion.CorrectAnswer);
                    break;
                case 1:
                    AnswerTexts.Add(CurrentQuestion.WrongAnswer1);
                    break;
                case 2:
                    AnswerTexts.Add(CurrentQuestion.WrongAnswer2);
                    break;
                default:
                    break;
            }
        }
    }

    public async Task PromptReplay()
    {
        SweetAlertResult replayLevel = await Swal.FireAsync(new SweetAlertOptions
        {
            Title = "No more questions!",
            Text = "Resetting the level.",
            Icon = SweetAlertIcon.Warning,
            ShowCancelButton = true,
            ConfirmButtonText = "OK",
            CancelButtonText = "Cancel",
        });

        if (!string.IsNullOrEmpty(replayLevel.Value))
        {
            await Swal.FireAsync(
              "Level Reset",
              "Another attempt at the questions!",
              SweetAlertIcon.Success
              );
            await ResetLevel();
        }
        else if (replayLevel.Dismiss == DismissReason.Cancel)
        {
            await Swal.FireAsync(
              "Cancelled",
              "You chose to not start over.",
              SweetAlertIcon.Error
              );
        }
        //else await Application.Current.MainPage.Navigation.PopAsync();

        //todo
    }
}




//    public virtual async void ChooseAnswer(object sender, EventArgs e)
//    {
//        //Button button = sender as Button;
//        //await Task.Yield();
//        //button.IsEnabled = false;
//        //bool isCorrect = await CheckAnswer(button);
//        //bool shouldUpdate = isCorrect;
//        //await Update(shouldUpdate);
//        //await Task.Delay(200);
//        //button.IsEnabled = true;

//        //todo
//    }

//    public void InitializeQuestionSet()
//    {
//        CurrentQuestionSet = new();
//        HashSet<Question> currentLevelHash = new();
//        //foreach (Question question in QuestionSet.Questions)
//        //{
//        //    if (question.LevelNumber == this.LevelNumber && question.LevelType == this.LevelType)
//        //    {
//        //        currentLevelHash.Add(question);
//        //    }
//        //}
//        //todo
//        CurrentQuestionSet = currentLevelHash.OrderBy(x => Guid.NewGuid()).ToList();
//    }

//    //public void GetFormattedImageAttribution()
//    //{
//    //    //attribution currently not clickable due to a know MAUI bug from Microsoft. See: https://github.com/dotnet/maui/issues/4734.
//    //    //may have been fixed by: https://github.com/dotnet/maui/tree/fix-4734.
//    //    var fs = new FormattedString();
//    //    var span1 = new Span { Text = "Photo by " };
//    //    var span2 = new Span
//    //    {
//    //        Text = QuestionImage.Author,
//    //        TextDecorations = TextDecorations.Underline
//    //    };
//    //    span2.GestureRecognizers.Add(new TapGestureRecognizer
//    //    {
//    //        Command = new Command(async () =>
//    //        {
//    //            var link = QuestionImage.AuthorUrl;
//    //            await Launcher.OpenAsync(link);
//    //        })
//    //    });
//    //    var span3 = new Span { Text = " on " };
//    //    var span4 = new Span
//    //    {
//    //        Text = "Unsplash",
//    //        TextDecorations = TextDecorations.Underline
//    //    };
//    //    span4.GestureRecognizers.Add(new TapGestureRecognizer
//    //    {
//    //        Command = new Command(() =>
//    //        {
//    //            var link = "https://unsplash.com/?utm_source=your_app_name&utm_medium=referral";
//    //            Launcher.OpenAsync(link);
//    //        })
//    //    });

//    //    fs.Spans.Add(span1);
//    //    fs.Spans.Add(span2);
//    //    fs.Spans.Add(span3);
//    //    fs.Spans.Add(span4);

//    //    ImageAttribution = fs;
//    //}

//    public async void ResetLevel(object sender, EventArgs e)
//    {
//        questionNumber = 0;
//        score = 0;
//        attempts = 0;
//        InitializeQuestionSet();
//        Update(true);
//    }

//    public virtual void LoadOptionText()
//    {
//        throw new NotImplementedException();
//    }

//    public virtual void UpdateLevelTitle()
//    {
//        throw new NotImplementedException();
//    }

//    public virtual void UpdateShownQuestion()
//    {
//        throw new NotImplementedException();
//    }

//    public virtual void UpdateQuestionImage()
//    {
//        throw new NotImplementedException();
//    }

//    public async Task SpeakAnswer()
//    {
//        //await TextToSpeech.Default.SpeakAsync(CurrentQuestion.CorrectAnswer)

//        //todo
//    }

//    public string IntToText(int number)
//    {
//        string[] ones = { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
//        string[] tens = { "", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
//        string[] teens = { "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };

//        if (number == 0)
//        {
//            return "zero";
//        }
//        else if (number < 0)
//        {
//            return "minus " + IntToText(-number);
//        }
//        else if (number < 10)
//        {
//            return ones[number];
//        }
//        else if (number < 20)
//        {
//            return teens[number - 11];
//        }
//        else if (number < 100)
//        {
//            return tens[number / 10] + ((number % 10 > 0) ? " " + IntToText(number % 10) : "");
//        }
//        else if (number < 1000)
//        {
//            return ones[number / 100] + " hundred" + ((number % 100 > 0) ? " and " + IntToText(number % 100) : "");
//        }
//        else if (number < 1000000)
//        {
//            return IntToText(number / 1000) + " thousand" + ((number % 1000 > 0) ? " " + IntToText(number % 1000) : "");
//        }
//        else
//        {
//            return "number too large";
//        }
//    }

//    //public Task<bool> CheckAnswer()
//    //{
//    //    throw new NotImplementedException();
//    //}
//    //todo
//}
