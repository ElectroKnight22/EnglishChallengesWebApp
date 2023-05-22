using System.Diagnostics.CodeAnalysis;

namespace EnglishChallengesWebApp.Resources.Model
{
    public class Question
    {
        public string CorrectAnswer { get; set; } = string.Empty;
        public string WrongAnswer1 { get; set; } = string.Empty;
        public string WrongAnswer2 { get; set; } = string.Empty;
        public string Prompt { get; set; } = "Please choose the best answer";
        public string ImageSource { get; set; } = string.Empty;
        public Guid Guid { get; set; } = Guid.Empty;

        public override bool Equals([AllowNull] object obj)
        {
            return obj is Question question &&
                   Guid == question.Guid;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Guid);
        }
    }
}