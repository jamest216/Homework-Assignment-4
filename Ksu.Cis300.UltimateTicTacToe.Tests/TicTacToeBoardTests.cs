/* TicTacToeBoardTests.cs
 * Author: Rod Howell
 */
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.UltimateTicTacToe.Tests
{
    /// <summary>
    /// Unit tests for the TicTacToeBoard class.
    /// </summary>
    [TestFixture]
    public class TicTacToeBoardTests
    {
        /// <summary>
        /// Tests that a new game isn't over.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestANewGameNotOver()
        {
            TicTacToeBoard b = new TicTacToeBoard();
            Assert.That(b.IsOver, Is.False);
        }

        /// <summary>
        /// Tests that a new game isn't won.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestANewGameNotWon()
        {
            TicTacToeBoard b = new TicTacToeBoard();
            Assert.That(b.IsWon, Is.False);
        }

        /// <summary>
        /// Tests that GetAvailablePlays works correctly for an empty board.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestAAvailablePlaysEmptyBoard()
        {
            TicTacToeBoard b = new TicTacToeBoard();
            List<(int, int, int, int)> allPlays = new List<(int, int, int, int)>();
            allPlays.Add((0, 0, 0, 0)); // Something in the list initially.
            int majorRow = 1;
            int majorCol = 2;
            for (int i = 0; i < 3; i++) // Adds all squares, using the given values for majorRow and MajorCol
            {
                for (int j = 0; j < 3; j++)
                {
                    allPlays.Add((majorRow, majorCol, i, j));
                }
            }
            List<(int, int, int, int)> avail = new List<(int, int, int, int)>();
            avail.Add((0, 0, 0, 0)); // The initial list content
            b.GetAvailablePlays(avail, majorRow, majorCol);
            Assert.That(avail, Is.EquivalentTo(allPlays)); // avail and allPlays should contain the same elements
        }

        /// <summary>
        /// Tests that making 8 plays without getting 3 in a row doesn't end the game.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestB8PlaysNotOver()
        {
            TicTacToeBoard board = new TicTacToeBoard();
            board.Play(Player.First, 1, 1);
            board.Play(Player.Second, 0, 0);
            board.Play(Player.First, 0, 2);
            board.Play(Player.Second, 2, 0);
            board.Play(Player.First, 1, 0);
            board.Play(Player.Second, 1, 2);
            board.Play(Player.First, 0, 1);
            board.Play(Player.Second, 2, 1);
            Assert.That(board.IsOver, Is.False);
        }

        /// <summary>
        /// Tests that making 8 plays without getting 3 in a row doesn't win the game.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestB8PlaysNotWon()
        {
            TicTacToeBoard board = new TicTacToeBoard();
            board.Play(Player.First, 1, 1);
            board.Play(Player.Second, 0, 0);
            board.Play(Player.First, 0, 2);
            board.Play(Player.Second, 2, 0);
            board.Play(Player.First, 1, 0);
            board.Play(Player.Second, 1, 2);
            board.Play(Player.First, 0, 1);
            board.Play(Player.Second, 2, 1);
            Assert.That(board.IsWon, Is.False);
        }

        /// <summary>
        /// Makes 8 plays and checks that GetAvailablePlays returns the only available play.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestB8PlaysGetAvailable()
        {
            TicTacToeBoard board = new TicTacToeBoard();
            board.Play(Player.First, 1, 1);
            board.Play(Player.Second, 0, 0);
            board.Play(Player.First, 0, 2);
            board.Play(Player.Second, 2, 0);
            board.Play(Player.Draw, 1, 0);
            board.Play(Player.Second, 1, 2);
            board.Play(Player.First, 0, 1);
            board.Play(Player.Second, 2, 1);
            (int, int, int, int)[] correct = { (0, 1, 2, 2) }; // (2, 2) is the only empty square
            List<(int, int, int, int)> avail = new List<(int, int, int, int)>();
            board.GetAvailablePlays(avail, 0, 1);
            Assert.That(avail, Is.EquivalentTo(correct));
        }

        /// <summary>
        /// Tests that a win is recorded if 3 in a row are played horizontally.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestCHorizontalWin()
        {
            TicTacToeBoard board = new TicTacToeBoard();
            board.Play(Player.First, 1, 1);
            board.Play(Player.First, 1, 0);
            board.Play(Player.First, 1, 2);
            Assert.That(board.IsWon, Is.True);
        }

        /// <summary>
        /// Tests that the game is over after 3 in a row are played horizontally.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestCHorizontalOver()
        {
            TicTacToeBoard board = new TicTacToeBoard();
            board.Play(Player.Second, 2, 2);
            board.Play(Player.Second, 2, 0);
            board.Play(Player.Second, 2, 1);
            Assert.That(board.IsOver, Is.True);
        }

        /// <summary>
        /// Tests that the game is won after 3 in a row are played vertically.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestCVerticalWon()
        {
            TicTacToeBoard b = new TicTacToeBoard();
            b.Play(Player.Second, 0, 0);
            b.Play(Player.Second, 2, 0);
            b.Play(Player.Second, 1, 0);
            Assert.That(b.IsWon, Is.True);
        }

        /// <summary>
        /// Tests that the game is over after 3 in a row are played vertically.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestCVerticalOver()
        {
            TicTacToeBoard b = new TicTacToeBoard();
            b.Play(Player.First, 0, 1);
            b.Play(Player.First, 1, 1);
            b.Play(Player.First, 2, 1);
            Assert.That(b.IsOver, Is.True);
        }

        /// <summary>
        /// Tests that the game is won after 3 in a row are played on the major diagonal.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestCMajorDiagonalWon()
        {
            TicTacToeBoard b = new TicTacToeBoard();
            b.Play(Player.First, 0, 0);
            b.Play(Player.First, 1, 1);
            b.Play(Player.First, 2, 2);
            Assert.That(b.IsWon, Is.True);
        }

        /// <summary>
        /// Tests that the game is over after 3 in a row are played on the major diagonal.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestCMajorDiagonalOver()
        {
            TicTacToeBoard b = new TicTacToeBoard();
            b.Play(Player.Second, 1, 1);
            b.Play(Player.Second, 0, 0);
            b.Play(Player.Second, 2, 2);
            Assert.That(b.IsOver, Is.True);
        }

        /// <summary>
        /// Tests that the game is won after 3 in a row are played on the minor diagonal.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestCMinorDiagonalWon()
        {
            TicTacToeBoard b = new TicTacToeBoard();
            b.Play(Player.First, 0, 2);
            b.Play(Player.First, 1, 1);
            b.Play(Player.First, 2, 0);
            Assert.That(b.IsWon, Is.True);
        }

        /// <summary>
        /// Tests that the game is over after 3 in a row are played on the minor diagonal.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestCMinorDiagonalOver()
        {
            TicTacToeBoard b = new TicTacToeBoard();
            b.Play(Player.Second, 1, 1);
            b.Play(Player.Second, 0, 2);
            b.Play(Player.Second, 2, 0);
            Assert.That(b.IsOver, Is.True);
        }

        /// <summary>
        /// Tests that the game is not won if 3 draws in a row are played horizontally.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestD3DrawsHorizontalNotWon()
        {
            TicTacToeBoard b = new TicTacToeBoard();
            b.Play(Player.Draw, 0, 0);
            b.Play(Player.Draw, 0, 1);
            b.Play(Player.Draw, 0, 2);
            Assert.That(b.IsWon, Is.False);
        }

        /// <summary>
        /// Tests that the game is not over if 3 draws in a row are played horizontally.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestD3DrawsHorizontalNotOver()
        {
            TicTacToeBoard b = new TicTacToeBoard();
            b.Play(Player.Draw, 0, 0);
            b.Play(Player.Draw, 0, 1);
            b.Play(Player.Draw, 0, 2);
            Assert.That(b.IsOver, Is.False);
        }

        /// <summary>
        /// Tests that the game is not won if 3 draws in a row are played vertically.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestD3DrawsVerticalNotWon()
        {
            TicTacToeBoard b = new TicTacToeBoard();
            b.Play(Player.Draw, 1, 1);
            b.Play(Player.Draw, 0, 1);
            b.Play(Player.Draw, 2, 1);
            Assert.That(b.IsWon, Is.False);
        }

        /// <summary>
        /// Tests that the game is not over if 3 draws in a row are played vertically.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestD3DrawsVerticalNotOver()
        {
            TicTacToeBoard b = new TicTacToeBoard();
            b.Play(Player.Draw, 1, 1);
            b.Play(Player.Draw, 0, 1);
            b.Play(Player.Draw, 2, 1);
            Assert.That(b.IsOver, Is.False);
        }

        /// <summary>
        /// Tests that the game is not won if 3 draws in a row are played on the major diagonal.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestD3DrawsMajorDiagonalNotWon()
        {
            TicTacToeBoard b = new TicTacToeBoard();
            b.Play(Player.Draw, 1, 1);
            b.Play(Player.Draw, 0, 0);
            b.Play(Player.Draw, 2, 2);
            Assert.That(b.IsWon, Is.False);
        }

        /// <summary>
        /// Tests that the game is not over if 3 draws in a row are played on the major diagonal.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestD3DrawsMajorDiagonalNotOver()
        {
            TicTacToeBoard b = new TicTacToeBoard();
            b.Play(Player.Draw, 1, 1);
            b.Play(Player.Draw, 0, 0);
            b.Play(Player.Draw, 2, 2);
            Assert.That(b.IsOver, Is.False);
        }

        /// <summary>
        /// Tests that the game is not won if 3 draws in a row are played on the minor diagonal.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestD3DrawsMinorDiagonalNotWon()
        {
            TicTacToeBoard b = new TicTacToeBoard();
            b.Play(Player.Draw, 1, 1);
            b.Play(Player.Draw, 0, 2);
            b.Play(Player.Draw, 2, 0);
            Assert.That(b.IsWon, Is.False);
        }

        /// <summary>
        /// Tests that the game is not over if 3 draws in a row are played on the minor diagonal.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestD3DrawsMinorDiagonalNotOver()
        {
            TicTacToeBoard b = new TicTacToeBoard();
            b.Play(Player.Draw, 1, 1);
            b.Play(Player.Draw, 0, 2);
            b.Play(Player.Draw, 2, 0);
            Assert.That(b.IsOver, Is.False);
        }

        /// <summary>
        /// Tests that a full board without 3 in a row in over but not won.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestEDrawIsOverAndNotWon()
        {
            TicTacToeBoard board = new TicTacToeBoard();
            board.Play(Player.First, 1, 1);
            board.Play(Player.Second, 0, 0);
            board.Play(Player.Draw, 0, 2);
            board.Play(Player.Second, 2, 0);
            board.Play(Player.First, 1, 0);
            board.Play(Player.Draw, 1, 2);
            board.Play(Player.First, 0, 1);
            board.Play(Player.Second, 2, 1);
            board.Play(Player.First, 2, 2);
            Assert.Multiple(() =>
            {
                Assert.That(board.IsOver, Is.True);
                Assert.That(board.IsWon, Is.False);
            });
        }

        /// <summary>
        /// Tests that copying a board preserves a false value for IsWon.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestFCopiesNotWon()
        {
            TicTacToeBoard b = new TicTacToeBoard();
            Assert.That(new TicTacToeBoard(b).IsWon, Is.False);
        }

        /// <summary>
        /// Tests that copying a board preserves a true value for IsWon.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestFCopiesWon()
        {
            TicTacToeBoard b = new TicTacToeBoard();
            b.Play(Player.First, 0, 0);
            b.Play(Player.First, 1, 1);
            b.Play(Player.First, 2, 2);
            Assert.That(new TicTacToeBoard(b).IsWon, Is.True);
        }

        /// <summary>
        /// Tests that copying a board preserves a false value for IsOver.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestFCopiesNotOver()
        {
            TicTacToeBoard b = new TicTacToeBoard();
            Assert.That(new TicTacToeBoard(b).IsOver, Is.False);
        }

        /// <summary>
        /// Tests that copying a board preserves a true value for IsOver.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestFCopiesOver()
        {
            TicTacToeBoard b = new TicTacToeBoard();
            b.Play(Player.Second, 1, 0);
            b.Play(Player.Second, 1, 1);
            b.Play(Player.Second, 1, 2);
            Assert.That(new TicTacToeBoard(b).IsOver, Is.True);
        }

        /// <summary>
        /// Makes 8 plays and copies the board, then checks that GetAvailablePlays returns the only available play.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestG8PlaysCopyGetAvailable()
        {
            TicTacToeBoard board = new TicTacToeBoard();
            board.Play(Player.First, 1, 1);
            board.Play(Player.Second, 0, 0);
            board.Play(Player.First, 0, 2);
            board.Play(Player.Second, 2, 0);
            board.Play(Player.Draw, 1, 0);
            board.Play(Player.Second, 1, 2);
            board.Play(Player.First, 0, 1);
            board.Play(Player.Second, 2, 1);
            (int, int, int, int)[] correct = { (0, 1, 2, 2) }; // (2, 2) is the only empty square
            List<(int, int, int, int)> avail = new List<(int, int, int, int)>();
            new TicTacToeBoard(board).GetAvailablePlays(avail, 0, 1);
            Assert.That(avail, Is.EquivalentTo(correct));
        }

        /// <summary>
        /// Tests that making a play to a copy doesn't change the contents of the original board.
        /// If this is the first failed test, then the 2-dimensional array probably hasn't been copied, but
        /// instead, only a reference to the array has been copied.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestHCopyDoesntReferenceBoard()
        {
            TicTacToeBoard board = new TicTacToeBoard();
            List<(int, int, int, int)> availBefore = new List<(int, int, int, int)>();
            board.GetAvailablePlays(availBefore, 0, 0);
            TicTacToeBoard copy = new TicTacToeBoard(board);
            copy.Play(Player.First, 1, 1);
            List<(int, int, int, int)> availAfter = new List<(int, int, int, int)>();
            board.GetAvailablePlays(availAfter, 0, 0);
            Assert.That(availAfter, Is.EquivalentTo(availBefore));
        }

        /// <summary>
        /// Tests that finishing 3 in a row horizontally on a copy doesn't affect whether
        /// the original board is won. If this is the first failed test, then the array keeping 
        /// track of the number of plays to each row was probably not copied completely; i.e., either
        /// the two boards contain references to the same array of arrays, or the two arrays 
        /// of arrays contain references to the same arrays.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestHCopyDoesntReferenceHorizontalCounts()
        {
            TicTacToeBoard board = new TicTacToeBoard();
            board.Play(Player.Second, 0, 0);
            board.Play(Player.Second, 0, 1);
            TicTacToeBoard copy = new TicTacToeBoard(board);
            copy.Play(Player.Second, 0, 2);
            Assert.That(board.IsWon, Is.False);
        }

        /// <summary>
        /// Tests that finishing 3 in a row vertically on a copy doesn't affect whether
        /// the original board is won. If this is the first failed test, then the array keeping 
        /// track of the number of plays to each column was probably not copied completely; i.e., either
        /// the two boards contain references to the same array of arrays, or the two arrays 
        /// of arrays contain references to the same arrays.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestHCopyDoesntReferenceVerticalCounts()
        {
            TicTacToeBoard board = new TicTacToeBoard();
            board.Play(Player.Second, 0, 0);
            board.Play(Player.Second, 1, 0);
            TicTacToeBoard copy = new TicTacToeBoard(board);
            copy.Play(Player.Second, 2, 0);
            Assert.That(board.IsWon, Is.False);
        }

        /// <summary>
        /// Tests that finishing 3 in a row on the major diagonal on a copy doesn't affect whether
        /// the original board is won. If this is the first failed test, then the array keeping 
        /// track of the number of plays to the major diagonal was probably not copied; i.e., 
        /// the two boards contain references to the same array.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestHCopyDoesntReferenceMajorDiagonalCounts()
        {
            TicTacToeBoard board = new TicTacToeBoard();
            board.Play(Player.Second, 0, 0);
            board.Play(Player.Second, 1, 1);
            TicTacToeBoard copy = new TicTacToeBoard(board);
            copy.Play(Player.Second, 2, 2);
            Assert.That(board.IsWon, Is.False);
        }

        /// <summary>
        /// Tests that finishing 3 in a row on the minor diagonal on a copy doesn't affect whether
        /// the original board is won. If this is the first failed test, then the array keeping 
        /// track of the number of plays to the minor diagonal was probably not copied; i.e., 
        /// the two boards contain references to the same array.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestHCopyDoesntReferenceMinorDiagonalCounts()
        {
            TicTacToeBoard board = new TicTacToeBoard();
            board.Play(Player.Second, 0, 2);
            board.Play(Player.Second, 1, 1);
            TicTacToeBoard copy = new TicTacToeBoard(board);
            copy.Play(Player.Second, 2, 0);
            Assert.That(board.IsWon, Is.False);
        }
    }
}
