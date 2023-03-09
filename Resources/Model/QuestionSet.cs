public class QuestionSet
{
    public string Title { get; set; }
    public string LastModifiedDate { get; set; }
    public int LevelNumber { get; set; }
    public string LevelType { get; set; }
    public HashSet<Question> Questions { get; set; }


    public QuestionSet(string title, string date, int num, string type)
    {
        this.Title = title;
        this.LastModifiedDate = date;
        this.LevelNumber = num;
        this.LevelType = type;
        Questions = new();
    }

    public override bool Equals(object obj)
    {
        var qSet = obj as QuestionSet;
        return qSet != null &&
               Title == qSet.Title &&
               LevelNumber == qSet.LevelNumber &&
               LevelType == qSet.LevelType;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Title, LevelNumber, LevelType);
    }
}
