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

    ILevelDisplay questionPage { get; set; }

    void Update(bool shouldUpdate);

    void ChooseAnswer(object sender, EventArgs e);

    void InitializeQuestionSet();

    void LoadQuestion();
    Task UpdateScore(bool isCorrect);
    Task<bool> CheckAnswer();
    void ClickedHint(object sender, EventArgs e);
    void ResetLevel(object sender, EventArgs e);
    void UpdateLevelTitle();
    void UpdateShownQuestion();
    void UpdateQuestionImage();
    Task SpeakAnswer();
}
