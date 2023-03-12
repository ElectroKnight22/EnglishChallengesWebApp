using System.Text.Json;

public class QuestionSet
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LevelType { get; set; }
    public int LevelNumber { get; set; }
    public string LastModifiedDate { get; set; }
    public bool IsPublished { get; set; } = false;

    public HashSet<Question> Questions { get; set; }


    //public QuestionSet(string title, string date, int num, string type)
    //{
    //    this.Name = title;
    //    this.LastModifiedDate = date;
    //    this.LevelNumber = num;
    //    this.LevelType = type;
    //    Questions = new();
    //}
    //note: Supabase implementation does not allow constructors with parameters.

    private JsonSerializerOptions jsonOptions = new JsonSerializerOptions
    {
        WriteIndented = true
    };

    public void ToJson()
    {
        string json = JsonSerializer.Serialize(Questions, jsonOptions);
    }


    public override bool Equals(object obj)
    {
        var qSet = obj as QuestionSet;
        return qSet != null &&
               Id == qSet.Id &&
               Name == qSet.Name &&
               LevelNumber == qSet.LevelNumber &&
               LevelType == qSet.LevelType;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, LevelNumber, LevelType);
    }
}


