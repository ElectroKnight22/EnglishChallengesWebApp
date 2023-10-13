using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using EnglishChallengesWebApp.Resources.Interface;

namespace EnglishChallengesWebApp.Resources.Model
{
    public class Level: SupabaseClient, ILevel
    {
        [Inject]
        protected SweetAlertService Swal { get; set; } = default!;

        [Parameter]
        public int FolderId { get; set; }
        [Parameter]
        public int LevelNumber { get; set; }
        [Parameter]
        public string LevelType { get; set; } = string.Empty;
        [Parameter]
        public int QuestionSetId { get; set; }
        public HashSet<Question> QuestionHashSet { get; set; } = new();
        public List<Question> QuestionList { get; set; } = new();

        protected async Task<bool> LoadQuestionSet()
        {
            var result = await Supabase.From<QuestionSet>().Where(x => x.Id == QuestionSetId).Select(x => new object[] { x.Questions?? new() }).Single();
            if (result?.Questions != null)
            {
                QuestionHashSet = result.Questions;
                foreach (Question question in QuestionHashSet)
                {
                    if (question.Guid ==  Guid.Empty || question.Guid == null) question.Guid = Guid.NewGuid();
                }
                QuestionList = QuestionHashSet.OrderBy(x => Guid.NewGuid()).ToList();
                return true;
            }
            return false;
        }
    }
}
