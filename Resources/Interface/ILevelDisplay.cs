using EnglishChallengesWebApp.Resources.Model;

namespace EnglishChallengesWebApp.Resources.Interface
{
    public interface ILevelDisplay : ILevel
    {
        string LevelTitle { get; set; }
        int QuestionNumber { get; set; }
        int Score { get; set; }
        int Attempts { get; set; }
        Question CurrentQuestion { get; set; }
        List<string> AnswerTexts { get; set; }

        Task Update(bool shouldUpdate);
        void UpdateScore(bool isCorrect);
        void UpdateShownQuestion();
        void UpdateLevelTitle();
        void LoadNextQuestion();
        Task ChooseAnswer(int buttonNumber);
        bool CheckAnswer(int buttonNumber);
        void GiveHint();
        Task ResetLevel();
        Task SpeakAnswer();
    }
}