using EnglishChallengesWebApp.Resources.Model;

namespace EnglishChallengesWebApp.Resources.Interface
{
    public interface ILevelDisplay : ILevel
    {
        public string LevelTitle { get; set; }
        int QuestionNumber { get; set; }
        int Score { get; set; }
        int Attempts { get; set; }
        List<Question> CurrentQuestionList { get; set; }
        //QuestionImage QuestionImage { get; set; }
        //FormattedString ImageAttribution { get; set; }
        Question CurrentQuestion { get; set; }

        Task Update(bool shouldUpdate);

        Task ChooseAnswer(int buttonNumber);

        Task InitializeQuestionSet();
        void LoadQuestion();
        void UpdateScore(bool isCorrect);
        bool CheckAnswer(int buttonNumber);
        void GiveHint();
        Task ResetLevel();
        void SetLevelTitle();
        void UpdateShownQuestion();
        void UpdateQuestionImage();
        Task SpeakAnswer();
    }
}