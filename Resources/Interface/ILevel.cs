using EnglishChallengesWebApp.Resources.Model;

namespace EnglishChallengesWebApp.Resources.Interface
{
    public interface ILevel
    {
        int LevelNumber { get; }
        string LevelType { get; }
        int QuestionSetId { get; set; }
        HashSet<Question> QuestionHashSet { get; set; }
        List<Question> QuestionList { get; set; }
    }
}