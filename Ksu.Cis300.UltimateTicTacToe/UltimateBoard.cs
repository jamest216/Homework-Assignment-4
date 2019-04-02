/**
 * UltimateBoard.cs
 * Author: James Thomas
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.UltimateTicTacToe
{
    public class UltimateBoard
    {
        /// <summary>
        /// 9 smaller boards
        /// </summary>
        private TicTacToeBoard[,] _smallerBoards = new TicTacToeBoard[3, 3];

        /// <summary>
        /// the ultimate major board
        /// </summary>
        private TicTacToeBoard _largerBoard = new TicTacToeBoard();

        /// <summary>
        /// sets game = to a new game
        /// </summary>
        private bool _newGame = true;

        /// <summary>
        /// the last play made lcoation
        /// </summary>
        private (int, int, int, int) _lastPlayMade;

        /// <summary>
        /// the player currently playing
        /// </summary>
        private Player _player = Player.First;

        /// <summary>
        /// bool to see if game is won
        /// </summary>
        public bool IsWon
        {

            get
            {
                return _largerBoard.IsWon;
            }
        }

        /// <summary>
        /// bool to see if game is over
        /// </summary>
        public bool IsOver
        {
            get
            {
                return _largerBoard.IsOver;
            }
        }

        /// <summary>
        /// sets smaller boards = to new
        /// </summary>
        public UltimateBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _smallerBoards[i, j] = new TicTacToeBoard();
                }
            }

        }

        /// <summary>
        /// copies the boards info
        /// </summary>
        /// <param name="x"></param>
        public UltimateBoard(UltimateBoard x)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _smallerBoards[i, j] = new TicTacToeBoard(x._smallerBoards[i, j]);
                }
            }
            TicTacToeBoard temp = new TicTacToeBoard(x._largerBoard);

            _largerBoard = temp;

            _newGame = x._newGame;

            _lastPlayMade = x._lastPlayMade;

            _player = x._player;
        }

        /// <summary>
        /// gets avaiable plays on board
        /// </summary>
        /// <returns>a list of available plays</returns>
        public List<(int, int, int, int)> GetAvailablePlays()
        {
            List<(int, int, int, int)> temp = new List<(int, int, int, int)>();

            if (_newGame == false && _smallerBoards[_lastPlayMade.Item3, _lastPlayMade.Item4].IsOver == false)
            {
                _smallerBoards[_lastPlayMade.Item3, _lastPlayMade.Item4].GetAvailablePlays(temp, _lastPlayMade.Item3, _lastPlayMade.Item4);
               
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (_smallerBoards[i, j].IsOver == false)
                        {
                            _smallerBoards[i, j].GetAvailablePlays(temp, i, j);
                        }
                    }
                }          
            }
            return temp;
        }

        /// <summary>
        /// plays on a specific location
        /// </summary>
        /// <param name="locationOfPlay">location of the last play</param>
        public void Play((int, int, int, int) locationOfPlay)
        {
            TicTacToeBoard t = _smallerBoards[locationOfPlay.Item1, locationOfPlay.Item2];
            t.Play(_player, locationOfPlay.Item3, locationOfPlay.Item4);
            if (t.IsOver)
            {
                if (t.IsWon)
                {
                    _largerBoard.Play(_player, locationOfPlay.Item1, locationOfPlay.Item2);
                }
                else
                {
                    _largerBoard.Play(Player.Draw, locationOfPlay.Item1, locationOfPlay.Item2);
                }
            }
            _newGame = false;
            _lastPlayMade = locationOfPlay;
            if (_player == Player.First)
            {
                _player = Player.Second;
            }
            else
            {
                _player = Player.First;
            }
        }
    }
}
