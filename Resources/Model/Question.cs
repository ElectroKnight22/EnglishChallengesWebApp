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

        public override bool Equals([AllowNull] object obj)
        {
            return obj is Question question &&
                   CorrectAnswer == question.CorrectAnswer &&
                   WrongAnswer1 == question.WrongAnswer1 &&
                   WrongAnswer2 == question.WrongAnswer2 &&
                   Prompt == question.Prompt;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CorrectAnswer, WrongAnswer1, WrongAnswer2, Prompt);
        }
    }
}