using Microsoft.AspNetCore.Components;

public abstract class LevelDisplay : ComponentBase /*ILevelDisplay*/
{
    public int LevelNumber { get; set; }
    public string LevelType { get; set; }
    public string LevelTitle { get; set; }
    public int questionNumber { get; set; }
    public int score { get; set; }
    public int attempts { get; set; }
}
//    public List<Question> CurrentQuestionSet { get; set; }
//    public Question CurrentQuestion { get; set; }
//    //public QuestionImage QuestionImage { get; set; }

//    //public FormattedString ImageAttribution { get; set; }
//    public ILevelDisplay questionPage { get; set; }
//    List<Question> ILevelDisplay.CurrentQuestionSet { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
//    Question ILevelDisplay.CurrentQuestion { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
//    ILevelDisplay ILevelDisplay.questionPage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

//    public LevelDisplay(int LevelNumber, string LevelType)
//    {
//        this.LevelNumber = LevelNumber;
//        this.LevelType = LevelType;
//        InitializeQuestionSet();
//        //QuestionImage = new();
//    }

//    public void Update(bool shouldUpdate)
//    {
//        UpdateLevelTitle();
//        if (shouldUpdate)
//        {
//            if (CurrentQuestionSet.Count == 0)
//            {
//                //await PromptReplay();
//            }
//            else
//            {
//                LoadQuestion();
//                //await QuestionImage.Initialize(CurrentQuestion.CorrectAnswer);
//                //GetFormattedImageAttribution();

//                //todo
//                UpdateShownQuestion();
//                UpdateQuestionImage();
//            }
//        }
//        UpdateLevelTitle();
//    }

//    private async Task PromptReplay()
//    {
//        //var replayLevel = await JSRuntime.InvokeVoidAsync("alert", "Hello, world!");
//        //if (replayLevel) ResetLevel(this, null);
//        //else await Application.Current.MainPage.Navigation.PopAsync();

//        //todo
//    }

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


//    public void LoadQuestion()
//    {
//        Random rnd = new Random();
//        List<int> answerIndexes = new List<int> { 0, 1, 2 };
//        var shuffledIndex = answerIndexes.OrderBy(a => rnd.Next()).ToList();

//        CurrentQuestion = CurrentQuestionSet.First();
//        CurrentQuestionSet.Remove(CurrentQuestion);
//        LevelTitle = $"Level {LevelNumber.ToString()} Question {questionNumber.ToString()} - {CurrentQuestionSet.Count} left. Attempts: {attempts}. Score: {score}.";
//    }


//    public virtual async Task<bool> CheckAnswer()
//    {
//        bool isCorrect = true; //todo
//        await UpdateScore(isCorrect);
//        return isCorrect;
//    }

//    public async Task UpdateScore(bool isCorrect)
//    {
//        if (isCorrect)
//        {
//            score++;
//            questionNumber++;
//        }
//        attempts++;
//        LevelTitle = $"Level {LevelNumber.ToString()} Question {questionNumber.ToString()} - {CurrentQuestionSet.Count} left. Attempts: {attempts}. Score: {score}.";
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

//    public async void ClickedHint(object sender, EventArgs e)
//    {
//        //Button button = sender as Button;
//        //await Task.Yield();
//        //button.IsEnabled = false;
//        //await SpeakAnswer();
//        //button.IsEnabled = true;

//        //todo
//    }

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
