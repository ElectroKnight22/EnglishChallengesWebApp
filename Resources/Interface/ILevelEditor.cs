namespace EnglishChallengesWebApp.Resources.Interface
{
    public interface ILevelEditor : ILevel
    {
        void AddEntry(object sender, EventArgs e);
        void EditEntries(object sender, EventArgs e);
        void LoadEntries(object sender, EventArgs e);
        void GetQuestionSetFromFile();
        void InitializeEntries();
        void SaveEntries(object sender, EventArgs e);
    }
}