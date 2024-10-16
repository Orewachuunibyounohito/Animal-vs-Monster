// Refactoring Playlist: https://www.youtube.com/playlist?list=PLv3bW4BDh6I8tg1LSJoB7Ioz64s8Bcufz
// By: ITsLifeOverAll, https://github.com/ITsLifeOverAll

namespace UglyTrivia
{
    public class Dice
    {
        public int Point{ get; set; }

        public void RollingDice() => Point = UnityEngine.Random.Range(1, 7);
    }
}
