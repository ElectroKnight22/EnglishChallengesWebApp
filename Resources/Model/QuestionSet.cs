using Postgrest.Attributes;
using Postgrest.Models;

namespace EnglishChallengesWebApp.Resources.Model
{
    [Table("question_sets")]
    public class QuestionSet : BaseModel
    {
        [PrimaryKey("id", false)] public int Id { get; set; }
        [Column("name")] public string Name { get; set; }
        [Column("level_type")] public string LevelType { get; set; }
        [Column("level_number")] public int LevelNumber { get; set; }
        [Column("last_modified")] public string LastModifiedDate { get; set; }
        [Column("is_published")] public bool IsPublished { get; set; } = false;

        [Column("questions_json")] public HashSet<Question> Questions { get; set; }

        public override bool Equals(object obj)
        {
            return obj is QuestionSet qSet &&
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
}