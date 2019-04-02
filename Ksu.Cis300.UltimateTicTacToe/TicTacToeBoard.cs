/**
 * TicTacToeBoard.cs
 * Author: James Thomas
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.UltimateTicTacToe
{
    public class TicTacToeBoard
    {

        /// <summary>
        /// instance of one 3 by 3 game board
        /// </summary>
        private Player[,] _board = new Player[3, 3];

        /// <summary>
        /// the number of plays to the board
        /// </summary>
        private int _numberOfPlays;

        /// <summary>
        /// keeps track of how many times each of the two players has played to each of the three rows
        /// </summary>
        private int[][] _numberOfRows = new int[3][];
            
        /// <summary>
        /// keeps track of how many times each of the two players has played to each of the three columns
        /// </summary>
        private int[][] _numberOfColumns = new int[3][];

        /// <summary>
        /// keeps track of how many times each of the two players has played to the major diagonal
        /// </summary>
        private int[] _majorDiagonal = new int[2];

        /// <summary>
        /// keeps track of how many times each of the two players has played to the minor diagonal
        /// </summary>
        private int[] _minorDiagonal = new int[2];

        /// <summary>
        /// indicating whether one of the plays has won or not
        /// </summary>
        public bool IsWon { get; set; }

        /// <summary>
        /// indicating whether the game is over or not
        /// </summary>
        public bool IsOver { get; set; }


        /// <summary>
        /// constructs an empty board
        /// </summary>
        public TicTacToeBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _board[i, j] = Player.None;
                }
                _numberOfColumns[i] = new int[2];
                _numberOfRows[i] = new int[2];
            }
        }

        /// <summary>
        /// used to construct a copy of the given board and all of its contents and properties
        /// </summary>
        /// <param name="x"></param>
        public TicTacToeBoard(TicTacToeBoard x)
        {
            Array.Copy(x._board, _board, x._board.Length);
            x._majorDiagonal.CopyTo(_majorDiagonal, 0);
            x._minorDiagonal.CopyTo(_minorDiagonal, 0);
            for (int i = 0; i < 3; i++)
            {
                _numberOfRows[i] = (int[])x._numberOfRows[i].Clone();
                _numberOfColumns[i] = (int[])x._numberOfColumns[i].Clone();
            }

            _numberOfPlays = x._numberOfPlays;
            IsOver = x.IsOver;
            IsWon = x.IsWon;
        }

        /// <summary>
        /// finds the avaiable plays on the board
        /// </summary>
        /// <param name="l">list of locations to be added to</param>
        /// <param name="row">given row</param>
        /// <param name="col">given column</param>
        public void GetAvailablePlays(List<(int, int, int, int)> l, int row, int col)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (_board[i, j] == Player.None)
                    {
                        l.Add((row, col, i, j));
                    }
                }
            }
        }

        /// <summary>
        /// update the number of plays made to a given path
        /// </summary>
        /// <param name="numPlays">number of plays each player has made to some path in the board</param>
        /// <param name="p">the player making a play to this path</param>
        private void PlayTo(int[] numPlays, Player p)
        {
            numPlays[(int)p]++;

            if (numPlays[(int)p] == 3)
            {
                IsOver = true;
                IsWon = true;
            }
        }

        /// <summary>
        /// the individual play the player makes 
        /// </summary>
        /// <param name="p">the Player that made the play</param>
        /// <param name="row">row at whcih play was made</param>
        /// <param name="col">column at which the play was made</param>
        public void Play(Player p, int row, int col)
        {
            _board[row, col] = p;
            _numberOfPlays++;

            if (_numberOfPlays == 9)
            {
                IsOver = true;
            }

            if (p == Player.First || p == Player.Second)
            {
                PlayTo(_numberOfRows[row], p);
                PlayTo(_numberOfColumns[col], p);
                if (row == col)
                {
                    PlayTo(_majorDiagonal, p);
                }
                if ((row + col) == 2)
                {
                    PlayTo(_minorDiagonal, p);
                }
            }
        }
    }
}
