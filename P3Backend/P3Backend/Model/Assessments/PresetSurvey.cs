using System.Collections.Generic;
using P3Backend.Model.Questions;

namespace P3Backend.Model
{
    public class PresetSurvey
    {
        public int Id { get; set; }
        public string Theme { get; set; }
        public Question PresetQuestion { get; set; }

        public PresetSurvey()
        {
            //EF
        }

        public PresetSurvey(string theme, Question presetQuestion)
        {
            Theme = theme;
            PresetQuestion = presetQuestion;
        }

        // public void addQuestionsToTheme(string theme, List<Question> q)
        // {
        //     if (QuestionsPerTheme.ContainsKey(theme))
        //     {
        //         QuestionsPerTheme[theme].AddRange(q);
        //     }
        //     else
        //     {
        //         QuestionsPerTheme.Add(theme, q);
        //     }
        // }
    }
}