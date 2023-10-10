using Postgrest.Attributes;
using Postgrest.Models;
using System.Diagnostics.CodeAnalysis;

namespace EnglishChallengesWebApp.Resources.Model
{
    [Table("folders")]
    public class Folder : BaseModel
    {
        [PrimaryKey("id", false)] public int Id { get; set; }
        [Column("name")] public string Name { get; set; } = string.Empty;
        [Column("level_type")] public string LevelType { get; set; } = string.Empty;
        [Column("level_number")] public int LevelNumber { get; set; } = 0;
        [Column("parent_id")] public int ParentFolderId { get; set; } = 0;
        //[Column("question_sets")] public HashSet<QuestionSet> QuestionSets { get; set; } = new();

    }
}
