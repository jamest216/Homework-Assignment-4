/* UltimateBoardTests.cs
 * Author: Rod Howell
 */
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.UltimateTicTacToe.Tests
{
    /// <summary>
    /// Unit tests for the UltimateBoard class.
    /// </summary>
    [TestFixture]
    public class UltimateBoardTests
    {
        /// <summary>
        /// Tests that a new game isn't over.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestANewGameNotOver()
        {
            UltimateBoard b = new UltimateBoard();
            Assert.That(b.IsOver, Is.False);
        }

        /// <summary>
        /// Tests that a new game isn't won.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestANewGameNotWon()
        {
            UltimateBoard b = new UltimateBoard();
            Assert.That(b.IsWon, Is.False);
        }

        /// <summary>
        /// Tests that all plays are available for a new game.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestAAllPlaysInitiallyAvailable()
        {
            List<(int, int, int, int)> allPlays = new List<(int, int, int, int)>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int m = 0; m < 3; m++)
                    {
                        for (int n = 0; n < 3; n++)
                        {
                            allPlays.Add((i, j, m, n)); // Add each square on the board.
                        }
                    }
                }
            }
            UltimateBoard b = new UltimateBoard();
            Assert.That(b.GetAvailablePlays(), Is.EquivalentTo(allPlays));
        }

        /// <summary>
        /// Tests that after the first play, GetAvailablePlays returns the correct plays.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestBPlaysAvailableAfterFirst()
        {
            UltimateBoard b = new UltimateBoard();
            b.Play((0, 0, 0, 2));
            List<(int, int, int, int)> avail = new List<(int, int, int, int)>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    avail.Add((0, 2, i, j)); // Add all squares on small board (0, 2);
                }
            }
            Assert.That(b.GetAvailablePlays(), Is.EquivalentTo(avail));
        }

        /// <summary>
        /// Tests that GetAvailablePlays returns the correct plays after the first play is to
        /// a square forcing the second player to play on the same board.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestBPlaysAvailableAfterFirstSameBoard()
        {
            UltimateBoard b = new UltimateBoard();
            b.Play((1, 2, 1, 2));
            (int, int, int, int)[] avail =
            {
                (1, 2, 0, 0),
                (1, 2, 0, 1),
                (1, 2, 0, 2),
                (1, 2, 1, 0),
                (1, 2, 1, 1),
                (1, 2, 2, 0),
                (1, 2, 2, 1),
                (1, 2, 2, 2)
            };
            Assert.That(b.GetAvailablePlays(), Is.EquivalentTo(avail));
        }

        /// <summary>
        /// Tests that IsOver is still false after a sequence of plays that doesn't finish the game.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestCNotOverBeforeLastPlay()
        {
            UltimateBoard b = Games.GetBoard(Games.Draw, Games.Draw.Length - 1); // Play all but the last play.
            Assert.That(b.IsOver, Is.False);
        }

        /// <summary>
        /// Tests that IsWon is still false after a sequence of plays that doesn't finish the game.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestCNotWonBeforeLastPlay()
        {
            UltimateBoard b = Games.GetBoard(Games.XWins, Games.XWins.Length - 1); // Play all but the last play.
            Assert.That(b.IsWon, Is.False);
        }

        /// <summary>
        /// Tests that IsOver is true after the game is drawn.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestCDrawIsOver()
        {
            UltimateBoard b = Games.GetBoard(Games.Draw, Games.Draw.Length); // Play the entire game.
            Assert.That(b.IsOver, Is.True);
        }

        /// <summary>
        /// Tests that IsWon is false after the game is drawn.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestCDrawNotWon()
        {
            UltimateBoard b = Games.GetBoard(Games.Draw, Games.Draw.Length); // Play the entire game.
            Assert.That(b.IsWon, Is.False);
        }

        /// <summary>
        /// Tests that IsOver is true after a game is won.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestCWinIsOver()
        {
            UltimateBoard b = Games.GetBoard(Games.OWins, Games.OWins.Length); // Play the entire game.
            Assert.That(b.IsOver, Is.True);
        }

        /// <summary>
        /// Tests that IsWon is true after a game is won.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestCWinIsWon()
        {
            UltimateBoard b = Games.GetBoard(Games.OWins, Games.OWins.Length); // Play the entire game.
            Assert.That(b.IsWon, Is.True);
        }

        /// <summary>
        /// Tests that GetAvailablePlays returns the correct plays after a play that allows
        /// plays to multiple boards. The summary board at this point should look like:
        /// 
        ///  X |   |
        /// ---+---+---
        ///  O | O | X
        /// ---+---+---
        ///  X | X | O
        /// 
        /// where 'X' denotes the first player and 'O' denotes the second player.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestDAvailablePlaysMultipleBoards()
        {
            UltimateBoard b = Games.GetBoard(Games.Draw, 55);
            (int, int, int, int)[] avail =  // The correct available plays
            {
                (0, 1, 1, 1),
                (0, 1, 2, 0),
                (0, 2, 0, 1),
                (0, 2, 1, 0),
                (0, 2, 2, 0)
            };
            Assert.That(b.GetAvailablePlays(), Is.EquivalentTo(avail));
        }

        [Test, Timeout(1000)]
        public void TestEAvailablePlaysCopyOfNewBoard()
        {
            List<(int, int, int, int)> allPlays = new List<(int, int, int, int)>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int m = 0; m < 3; m++)
                    {
                        for (int n = 0; n < 3; n++)
                        {
                            allPlays.Add((i, j, m, n)); // Add each square on the board.
                        }
                    }
                }
            }
            UltimateBoard b = new UltimateBoard();
            UltimateBoard copy = new UltimateBoard(b);
            Assert.That(copy.GetAvailablePlays(), Is.EquivalentTo(allPlays));
        }

        /// <summary>
        /// Tests that a copy of the board after a sequence of plays gives the same available plays.
        /// The summary board at this point should look like:
        /// 
        ///  X |   |
        /// ---+---+---
        ///  O | O | X
        /// ---+---+---
        ///  X | X | O
        /// 
        /// where 'X' denotes the first player and 'O' denotes the second player.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestEAvailablePlaysAfterNonemptyCopy()
        {
            UltimateBoard b = Games.GetBoard(Games.Draw, 55);
            (int, int, int, int)[] avail =  // The correct available plays
            {
                (0, 1, 1, 1),
                (0, 1, 2, 0),
                (0, 2, 0, 1),
                (0, 2, 1, 0),
                (0, 2, 2, 0)
            };
            UltimateBoard copy = new UltimateBoard(b);
            Assert.That(copy.GetAvailablePlays(), Is.EquivalentTo(avail));
        }

        /// <summary>
        /// Tests that when a copy is made of a board with the first player to play,
        /// it is still the first player's turn. The square played after the copy is
        /// made will block if the first player plays, but will win the game is if the
        /// second player plays.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestECorrectFirstPlayerAfterCopy()
        {
            UltimateBoard b = Games.GetBoard(Games.OWins, 42);
            UltimateBoard copy = new UltimateBoard(b);
            copy.Play((2, 1, 0, 1));
            Assert.That(copy.IsWon, Is.False);
        }

        /// <summary>
        /// Tests that when a copy is made of a board with the second player to play,
        /// it is still the second player's turn. The square played the second play after the copy is
        /// made will block if the first player plays, but will win the game is if the
        /// second player plays.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestECorrectSecondPlayerAfterCopy()
        {
            UltimateBoard b = Games.GetBoard(Games.OWins, 41);
            UltimateBoard copy = new UltimateBoard(b);
            copy.Play((1, 0, 0, 1));
            copy.Play((2, 1, 0, 1));
            Assert.That(copy.IsWon, Is.False);
        }

        /// <summary>
        /// Tests that making a play on a copy doesn't change the small board in the original.
        /// If this is the first test fails for this class, and all tests of the TicTacToeBoard
        /// class pass, the small boards probably are not copies - both UltimateBoards contain references
        /// to the same small boards.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestFCopyCopiesSmallBoard()
        {
            UltimateBoard b = Games.GetBoard(Games.OWins, 42);
            UltimateBoard copy = new UltimateBoard(b);
            copy.Play((2, 1, 0, 1));
            Assert.That(b.GetAvailablePlays(), Contains.Item((2, 1, 0, 1)));
        }

        /// <summary>
        /// Tests that making a play on a copy doesn't change the summary board in the original.
        /// If this is the first test that fails for this class, and all tests for the TicTacToeBoard
        /// class pass, then the summary boards probably are not copies - both UltimateBoards contain
        /// references to the same summary boards.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestFCopyCopiesSummaryBoard()
        {
            UltimateBoard b = Games.GetBoard(Games.OWins, 38);
            UltimateBoard copy = new UltimateBoard(b);
            copy.Play((0, 2, 2, 2));
            Assert.That(b.GetAvailablePlays(), Contains.Item((0, 2, 0, 0)));
        }
    }
}
