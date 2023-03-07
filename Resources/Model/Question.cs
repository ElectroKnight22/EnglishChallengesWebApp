namespace EnglishChallengesWebApp.Resources.Model
{
    public class Question
    {
        public string ImageSource { get; set; }
        public int Number { get; set; }
        public string CorrectAnswer { get; set; }
        public string WrongAnswer1 { get; set; }
        public string WrongAnswer2 { get; set; }
        public string Prompt { get; set; }
        public QuestionSet QuestionSet { get; set; }

        public override bool Equals(object obj)
        {
            var question = obj as Question;
            return question != null &&
                   CorrectAnswer == question.CorrectAnswer &&
                   WrongAnswer1 == question.WrongAnswer1 &&
                   WrongAnswer2 == question.WrongAnswer2 &&
                   Prompt == question.Prompt &&
                   QuestionSet == question.QuestionSet;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CorrectAnswer, WrongAnswer1, WrongAnswer2, Prompt, QuestionSet);
        }
    }
}