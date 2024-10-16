// Refactoring Playlist: https://www.youtube.com/playlist?list=PLv3bW4BDh6I8tg1LSJoB7Ioz64s8Bcufz
// By: ITsLifeOverAll, https://github.com/ITsLifeOverAll

using System;
using System.Collections.Generic;
using System.Linq;
using Refactoring;

namespace UglyTrivia
{
    public class QuestionSystem
    {
        private const int MAX_QUESTION_COUNT = 50;
        
        private Dictionary<QuestionCategory, LinkedList<string>> questionDictionary = new ();
        
        public QuestionSystem(){
            var categories = Enum.GetValues(typeof(QuestionCategory)).Cast<QuestionCategory>().ToList();
            foreach(var category in categories){
                questionDictionary.Add(category, new LinkedList<string>());
            }

            for (int i = 0; i < MAX_QUESTION_COUNT; i++){
                questionDictionary[QuestionCategory.Pop].AddLast("Pop Question " + i);
                questionDictionary[QuestionCategory.Science].AddLast("Science Question " + i);
                questionDictionary[QuestionCategory.Sports].AddLast("Sports Question " + i);
                questionDictionary[QuestionCategory.Rock].AddLast("Rock Question " + i);
            }
        }

        public void TakeQuestion(QuestionCategory category){
            InfoSystem.Add("The category is " + category);

            InfoSystem.Add(questionDictionary[category].First.Value);
            questionDictionary[category].RemoveFirst();
        }
    }
}