﻿using EnglishChallengesWebApp.Resources.Interface;
using Microsoft.AspNetCore.Components;

namespace EnglishChallengesWebApp.Resources.Model
{
    public class LevelEditor : ComponentBase, ILevelEditor
    {
        public int LevelNumber => throw new NotImplementedException();

        public string LevelType => throw new NotImplementedException();

        public void AddEntry(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void EditEntries(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void GetQuestionSetFromFile()
        {
            throw new NotImplementedException();
        }

        public void InitializeEntries()
        {
            throw new NotImplementedException();
        }

        public void LoadEntries(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void SaveEntries(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}