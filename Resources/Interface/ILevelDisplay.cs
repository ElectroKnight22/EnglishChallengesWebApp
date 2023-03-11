public interface ILevelDisplay : ILevel
{
    public string LevelTitle { get; set; }
    int questionNumber { get; set; }
    int score { get; set; }
    int attempts { get; set; }
    List<Question> CurrentQuestionSet { get; set; }
    //QuestionImage QuestionImage { get; set; }
    //FormattedString ImageAttribution { get; set; }
    Question CurrentQuestion { get; set; }

    Task Update(bool shouldUpdate);

    Task ChooseAnswer(int buttonNumber);

    void InitializeQuestionSet();
    void InitializeAnswers();

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
