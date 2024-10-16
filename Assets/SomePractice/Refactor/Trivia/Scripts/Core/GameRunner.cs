// Refactoring Playlist: https://www.youtube.com/playlist?list=PLv3bW4BDh6I8tg1LSJoB7Ioz64s8Bcufz
// By: ITsLifeOverAll, https://github.com/ITsLifeOverAll

using System.Collections;
using Refactoring;
using UglyTrivia;
using UnityEngine;

namespace Trivia
{
    public class GameRunner : MonoBehaviour
    {
        private const string INFO_TEXT_PATH = "Canvas/InfoView/Viewport/Content/InfoText";
        Game aGame;
        private void Start()
        {
            InfoSystem.SetTextUI(transform.Find(INFO_TEXT_PATH).GetComponent<TMPro.TextMeshProUGUI>());
            // aGame = new Game("Chet");
            // aGame = new Game("Chet", "Pat", "Sue");
            aGame = new Game("Chet", "Pat", "Sue", "Ray", "Mary", "Wine");
            if(!aGame.IsPlayable()){ Destroy(gameObject); }

            StartCoroutine(GameStart());
        }

        private IEnumerator GameStart()
        {
            do{
                aGame.Running();
                yield return new WaitForSeconds( 1 );
            }while(!aGame.hasWinner);
        }
    }
}

