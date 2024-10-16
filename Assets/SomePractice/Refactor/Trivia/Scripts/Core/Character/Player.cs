// Refactoring Playlist: https://www.youtube.com/playlist?list=PLv3bW4BDh6I8tg1LSJoB7Ioz64s8Bcufz
// By: ITsLifeOverAll, https://github.com/ITsLifeOverAll

using System;

namespace UglyTrivia
{
    public class Player
    {
        public string Name{ get; set; }
        public int Places{ get; set; }
        public int Purses{ get; set; }

        public bool InPenaltyBox{ get; set; }
        public QuestionCategory Category{
            get{
                var category = Places switch
                {
                    var places when (places % 4) == 0 => QuestionCategory.Pop,
                    var places when (places % 4) == 1 => QuestionCategory.Science,
                    var places when (places % 4) == 2 => QuestionCategory.Sports,
                    var places when (places % 4) == 3 => QuestionCategory.Rock,
                    _ => throw new Exception("Invalid category")
                };
                return category;
            }
        }

        public Player(string name){
            Name         = name;
            Places       = 0;
            Purses       = 0;
            InPenaltyBox = false;
        }
    }
}
