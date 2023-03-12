using System.Diagnostics.CodeAnalysis;

namespace EnglishChallengesWebApp.Resources.Model
{
    public class Question
    {
        public string CorrectAnswer { get; set; }
        public string WrongAnswer1 { get; set; }
        public string WrongAnswer2 { get; set; }
        public string Prompt { get; set; }
        public string ImageSource { get; set; }

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