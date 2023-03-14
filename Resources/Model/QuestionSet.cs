using Postgrest.Attributes;
using Postgrest.Models;
using System.Diagnostics.CodeAnalysis;

namespace EnglishChallengesWebApp.Resources.Model
{
    [Table("question_sets")]
    public class QuestionSet : BaseModel
    {
        [PrimaryKey("id", false)] public int Id { get; set; }
        [Column("name")] public string Name { get; set; } = string.Empty;
        [Column("level_type")] public string LevelType { get; set; } = string.Empty;
        [Column("level_number")] public int LevelNumber { get; set; } = 0;
        [Column("last_modified")] public string? LastModifiedDate { get; set; }
        [Column("is_published")] public bool IsPublished { get; set; } = false;

        [Column("questions_json")] public HashSet<Question>? Questions { get; set; }

        public override bool Equals([AllowNull] object obj)
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