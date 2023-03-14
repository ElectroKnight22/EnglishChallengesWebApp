﻿using Toolbelt.Blazor.SpeechSynthesis;
using EnglishChallengesWebApp.Resources.Interface;
using Microsoft.AspNetCore.Components;
using CurrieTechnologies.Razor.SweetAlert2;

namespace EnglishChallengesWebApp.Resources.Model
{
    public abstract class LevelDisplay : Level, ILevelDisplay
    {
        [Inject]
        protected SpeechSynthesis? Speaker { get; set; }

        public string LevelTitle { get; set; } = string.Empty;
        public int QuestionNumber { get; set; }
        public int Score { get; set; }
        public int Attempts { get; set; }
        public bool IsAnsweringDisabled { get; set; } = false;

        public Question CurrentQuestion { get; set; } = new();
        public List<string> AnswerTexts { get; set; } = new();


        protected override async Task OnInitializedAsync()
        {
            for (int i = 0; i < 10; i++)
            {
                AnswerTexts.Add("loading...");
            }
            await LoadQuestionSet();
            await Update(true);

        }
        public async Task Update(bool shouldUpdate)
        {
            UpdateLevelTitle();
            if (shouldUpdate)
            {
                if (QuestionList.Count == 0)
                {
                    await PromptReplay();
                }
                else
                {
                    LoadNextQuestion();
                    UpdateShownQuestion();
                }
            }
        }
        public void UpdateScore(bool isCorrect)
        {
            if (isCorrect)
            {
                Score++;
                QuestionNumber++;
            }
            Attempts++;
            LevelTitle = $"Level {LevelNumber} Question {QuestionNumber} - {QuestionList.Count} left. Attempts: {Attempts}. Score: {Score}.";
        }

        public void UpdateShownQuestion()
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
        }
        public void UpdateLevelTitle()
        {
            try
            {
                LevelTitle = $"Level {LevelNumber} Question {QuestionNumber} - {QuestionList.Count} left. Attempts: {Attempts}. Score: {Score}.";
            }
            catch { throw new Exception("LevelTitle label has not been created."); }
        }
        public void LoadNextQuestion()
        {
            Random rnd = new();
            List<int> answerIndexes = new() { 0, 1, 2 };
            var shuffledIndex = answerIndexes.OrderBy(a => rnd.Next()).ToList();

            CurrentQuestion = QuestionList.First();
            QuestionList.Remove(CurrentQuestion);
        }
        public virtual async Task ChooseAnswer(int buttonNumber)
        {
            if (CurrentQuestion != null)
            {
                IsAnsweringDisabled = true;
                bool isCorrect = CheckAnswer(buttonNumber);
                bool shouldUpdate = isCorrect;
                await Update(shouldUpdate);
                IsAnsweringDisabled = false;
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
        public async Task ResetLevel()
        {
            QuestionNumber = 0;
            Score = 0;
            Attempts = 0;
            await LoadQuestionSet();
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
}