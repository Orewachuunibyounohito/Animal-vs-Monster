// Refactoring Playlist: https://www.youtube.com/playlist?list=PLv3bW4BDh6I8tg1LSJoB7Ioz64s8Bcufz
// By: ITsLifeOverAll, https://github.com/ITsLifeOverAll

using System;
using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;
using System.Drawing;
using Refactoring;
using UnityEditor;
using UnityEngine;
// using System.Reflection.Metadata.Ecma335;

namespace UglyTrivia
{
    public class Game
    {
        private const int WRONG_ANSWER = 7;
        private const int PLACES_COUNT = 12;
        private const int MAX_PLAYER_COUNT = 6;

        private List<Player>   _players = new ();
        private int            _currentPlayer = 0;
        private Dice           dice;
        private QuestionSystem question;

        public bool   hasWinner;
        public Player CurrentPlayer => _players[_currentPlayer];


        public Game(params string[] playerNames)
        {
            foreach(var name in playerNames){
                Add(name);
            }
            dice     = new Dice();
            question = new QuestionSystem();
        }

        public void Running(){
            Roll().MoveToNewPlace().GetQuestion().AnswerQuestion().IsGameOver().NextPlayer();
            InfoSystem.DisplayInfo(true);
        }

        public bool IsPlayable()
        {
            return _players.Count >= 2;
        }

        private Game Roll()
        {
            dice.RollingDice();
            InfoSystem.Add(CurrentPlayer.Name + " is the current player");
            InfoSystem.Add("They have rolled a " + dice.Point);
            return this;
        }

        private Game MoveToNewPlace()
        {
            if (CurrentPlayer.InPenaltyBox)
            {
                if(!IsReleased()){ return this; }

                InfoSystem.Add(CurrentPlayer.Name + " is getting out of the penalty box");
            }

            CurrentPlayer.Places += dice.Point;
            CurrentPlayer.Places %= PLACES_COUNT;
            InfoSystem.Add(CurrentPlayer.Name
                                    + "'s new location is "
                                    + CurrentPlayer.Places);
            return this;
        }

        private Game GetQuestion()
        {
            if(CurrentPlayer.InPenaltyBox){ return this; }

            question.TakeQuestion(CurrentCategory());
            return this;
        }

        private Game AnswerQuestion(){
            if(CurrentPlayer.InPenaltyBox){ return this; }
            
            var ans = UnityEngine.Random.Range(0, 9);
            if(ans == WRONG_ANSWER) { WrongAnswer(); }
            else                    { WasCorrectlyAnswered(); }
            return this;
        }

        private Game IsGameOver()
        {
            hasWinner = CurrentPlayer.Purses == 6;
            if(hasWinner){ InfoSystem.Add($"----- Winner {CurrentPlayer.Name} -----"); }
            return this;
        }

        private void NextPlayer(){
            if(hasWinner){ return ; }

            _currentPlayer++;
            _currentPlayer %= _players.Count;

            InfoSystem.Add("");
        }
        
        private bool Add(string playerName)
        {
            if(_players.Count == MAX_PLAYER_COUNT) { return false; }

            _players.Add(new Player(playerName));

            InfoSystem.Add(playerName + " was added");
            InfoSystem.Add("They are player number " + _players.Count);
            return true;
        }

        private bool IsReleased()
        {
            bool isReleasable = dice.Point % 2 != 0;
            if(isReleasable){
                CurrentPlayer.InPenaltyBox = false;
            }else{
                CurrentPlayer.InPenaltyBox = true;
                InfoSystem.Add(CurrentPlayer.Name + " is not getting out of the penalty box");
            }
            return isReleasable;
        }

        private QuestionCategory CurrentCategory(){
            var category = CurrentPlayer.Places switch
            {
                var places when (places % 4) == 0 => QuestionCategory.Pop,
                var places when (places % 4) == 1 => QuestionCategory.Science,
                var places when (places % 4) == 2 => QuestionCategory.Sports,
                var places when (places % 4) == 3 => QuestionCategory.Rock,
                _ => throw new Exception("Invalid category")
            };
            return category;
        }

        private void WasCorrectlyAnswered()
        {
            if (CurrentPlayer.InPenaltyBox){ return ; }

            InfoSystem.Add("Answer was correct!!!!");
            CurrentPlayer.Purses++;
            InfoSystem.Add(CurrentPlayer.Name
                            + " now has "
                            + CurrentPlayer.Purses
                            + " Gold Coins.");
        }

        private void WrongAnswer()
        {
            InfoSystem.Add("Question was incorrectly answered");
            InfoSystem.Add(CurrentPlayer.Name + " was sent to the penalty box");
            CurrentPlayer.InPenaltyBox = true;
        }
    }
}
