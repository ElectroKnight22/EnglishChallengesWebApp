using Toolbelt.Blazor.SpeechSynthesis;
using EnglishChallengesWebApp.Resources.Interface;
using Microsoft.AspNetCore.Components;
using CurrieTechnologies.Razor.SweetAlert2;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;

namespace EnglishChallengesWebApp.Resources.Model
{
    public abstract class LevelDisplay : Level, ILevelDisplay
    {
        [Inject]
        protected SpeechSynthesis Speaker { get; set; } = default!;
        [Inject]
        protected NavigationManager NavMan { get; set; } = default!;
        public string LevelTitle { get; set; } = string.Empty;
        public int QuestionNumber { get; set; }
        public int Score { get; set; }
        public int Attempts { get; set; }
        public bool IsAnsweringDisabled { get; set; } = false;

        public Question CurrentQuestion { get; set; } = new();
        public List<string> AnswerTexts { get; set; } = new();
        public string Prompt { get; set; } = string.Empty;

        IEnumerable<SpeechSynthesisVoice> Voices = default!;

        protected override async Task OnInitializedAsync()
        {
            for (int i = 0; i < 10; i++)
            {
                AnswerTexts.Add("loading...");
            }
            await ResetLevel();
        }
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Voices = await Speaker.GetVoicesAsync();
                StateHasChanged();
            }
        }
        public async Task Update(bool shouldNext)
        {
            UpdateLevelTitle();
            StateHasChanged();
            if (shouldNext && QuestionList.Count == 0)
            {
                await PromptEnd();

            }
            UpdateLevelTitle();
        }
        public void UpdateScore(bool shouldNext)
        {
            if (shouldNext)
            {
                Score++;
                if (QuestionList.Count != 0)
                {
                    QuestionNumber++;
                }
            }
            Attempts++;
        }
        public void UpdateLevelTitle()
        {
            try
            {
                LevelTitle = $"== Level {LevelNumber} Question {QuestionNumber} == {QuestionList.Count} left. Attempts: {Attempts}. Score: {Score}.";
            }
            catch { throw new Exception("LevelTitle label has not been created."); }
        }
        public void UpdateShownChoices()
        {
            Random rnd = new();
            List<int> answerIndexes = new() { 0, 1, 2 };
            var shuffledIndex = answerIndexes.OrderBy(a => rnd.Next()).ToList();

            AnswerTexts = new();

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
            Prompt = CurrentQuestion.Prompt ?? "Please select the best answer";
        }
        public void LoadNextQuestion()
        {
            IsAnsweringDisabled = false;
            Random rnd = new();
            List<int> answerIndexes = new() { 0, 1, 2 };
            var shuffledIndex = answerIndexes.OrderBy(a => rnd.Next()).ToList();

            CurrentQuestion = QuestionList.First();
            QuestionList.Remove(CurrentQuestion);
            UpdateShownChoices();
        }
        public virtual async Task ChooseAnswer(string choice)
        {
            bool isCorrect;
            if (CurrentQuestion != null)
            {
                IsAnsweringDisabled = true;
                isCorrect = choice == CurrentQuestion.CorrectAnswer;
                if (!isCorrect) { IsAnsweringDisabled = false; };
                await SpeakString(choice);
                UpdateScore(isCorrect);
                await Update(isCorrect);
            }
        }
        public async void GiveHint()
        {
            if (CurrentQuestion != null)
            {
                await SpeakString(CurrentQuestion.CorrectAnswer);
            }
        }
        public async Task ResetLevel()
        {
            QuestionNumber = 1;
            Score = 0;
            Attempts = 0;
            bool hasQuestions = await LoadQuestionSet();
            if (!hasQuestions)
            {
                await Swal.FireAsync(
                  "No questions found!",
                  "Returning to level selection.",
                  SweetAlertIcon.Error
                  );

                NavMan.NavigateTo($"Selecting/{FolderId}/{LevelNumber}/{LevelType}");
            }
            else
            {
                await Update(true);
                LoadNextQuestion();
            }
        }
        public async Task PromptReplay()
        {
            SweetAlertResult replayLevel = await Swal.FireAsync(new SweetAlertOptions
            {
                Title = "Reset the Level?",
                Text = "A new attempt at these questions.",
                ShowCancelButton = true,
                ConfirmButtonText = "Reset",
                CancelButtonText = "Leave",
            });

            if (!string.IsNullOrEmpty(replayLevel.Value))
            {
                await Swal.FireAsync(
                  "Level Reset.",
                  "Better luck this time!",
                  SweetAlertIcon.Info
                  );
                await ResetLevel();
            }
            else
            {
                NavMan.NavigateTo($"Selecting/{FolderId}/{LevelNumber}/{LevelType}");
            }
        }

        protected async Task PromptEnd()
        {
            string levelResultString = "Your Score: " + Score.ToString() + " / " + Attempts.ToString();
            int score = (int)Math.Ceiling((decimal)Score / Attempts * 100);
            var swalIcon = score >= 80 ? SweetAlertIcon.Success
                         : score >= 60 ? SweetAlertIcon.Warning
                         : SweetAlertIcon.Error;

            await Swal.FireAsync(new SweetAlertOptions
            {
                Title = "Level Complete!",
                Html = levelResultString,
                Icon = swalIcon,
                ConfirmButtonText = "Confirm",
            });

            await PromptReplay();
        }
        public async Task SpeakString(string choice)
        {
            var utterancet = new SpeechSynthesisUtterance()
            {
                Text = choice,
                Lang = "en-US",
                Voice = Voices.FirstOrDefault(v => v.Name.Contains("Jenny") || v.Name.Contains("Samantha"))

            };
            await Speaker.SpeakAsync(utterancet); // 👈 Speak with "Jenny"'s voice!
        }
    }
}