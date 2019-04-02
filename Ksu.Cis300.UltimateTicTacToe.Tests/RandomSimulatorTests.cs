/* RandomSimulatorTests.cs
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
    [TestFixture]
    public class RandomSimulatorTests
    {
        /// <summary>
        /// Tests that a random simulation on a game won by the first player returns 1.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestAWonByFirst()
        {
            UltimateBoard b = Games.GetBoard(Games.XWins, Games.XWins.Length); // Play the game until X wins.
            Assert.That(RandomSimulator.Simulate(b), Is.EqualTo(1));
        }

        /// <summary>
        /// Tests that a random simulation on a game won by the second player returns 1.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestAWonBySecond()
        {
            UltimateBoard b = Games.GetBoard(Games.OWins, Games.OWins.Length); // Play the game until O wins.
            Assert.That(RandomSimulator.Simulate(b), Is.EqualTo(1));
        }

        /// <summary>
        /// Tests that a random simulation on a drawn game returns 0.5.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestADrawnGame()
        {
            UltimateBoard b = Games.GetBoard(Games.Draw, Games.Draw.Length); // Play the game to the end.
            Assert.That(RandomSimulator.Simulate(b), Is.EqualTo(0.5f));
        }

        /// <summary>
        /// Tests that a random simulation on a game whose only available play wins the game
        /// returns 0.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestBWinNextTurn()
        {
            UltimateBoard b = Games.GetBoard(Games.Draw, 53);
            b.Play((1, 0, 1, 0));
            b.Play((1, 0, 0, 0));
            b.Play((0, 1, 2, 0));
            b.Play((1, 0, 1, 2));
            b.Play((0, 2, 1, 0)); // Now the only available play wins the game.
            Assert.That(RandomSimulator.Simulate(b), Is.EqualTo(0));
        }

        /// <summary>
        /// Test the random simulator on a game with two legal plays. Player.First
        /// has just played, leaving Player.Second two choices. (1, 0, 0, 2) blocks
        /// Player.First's only possible win, guaranteeing a draw. If Player.Second
        /// plays (0, 2, 1, 0), however, Player.First's only possible response, 
        /// (1, 0, 0, 2), wins. Over a large number of simulations, therefore, the 
        /// average score for Player.First should be around 0.75.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestCTwoChoices()
        {
            UltimateBoard b = Games.GetBoard(Games.Draw, 53);
            b.Play((1, 0, 1, 0));
            b.Play((1, 0, 0, 0));
            b.Play((0, 1, 2, 0));
            b.Play((1, 0, 1, 2));
            b.Play((0, 2, 2, 0));
            b.Play((0, 2, 0, 1));
            float sum = 0;
            for (int i = 0; i < 1000; i++)
            {
                sum += RandomSimulator.Simulate(new UltimateBoard(b));
            }
            Assert.That(sum / 1000, Is.GreaterThan(0.7f).And.LessThan(0.8f));
        }

        /// <summary>
        /// Tests the random simulator on a board with three legal plays. Player.Second
        /// has just played, leaving Player.First three choices. (0, 2, 0, 1) leads to the same
        /// board position as in the above test. (0, 2, 1, 0) leads to a position with only one
        /// legal move, (1, 0, 0, 2), which guarantees a draw. The third play, (1, 0, 0, 2), wins 
        /// for Player.First. Thus, from the perspective of Player.Second, the average score over 
        /// a large number of simulations should be about 0.25.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestDThreeChoices()
        {
            UltimateBoard b = Games.GetBoard(Games.Draw, 53);
            b.Play((1, 0, 1, 0));
            b.Play((1, 0, 0, 0));
            b.Play((0, 1, 2, 0));
            b.Play((1, 0, 1, 2));
            b.Play((0, 2, 2, 0));
            float sum = 0;
            for (int i = 0; i < 1000; i++)
            {
                sum += RandomSimulator.Simulate(new UltimateBoard(b));
            }
            Assert.That(sum / 1000, Is.GreaterThan(0.2f).And.LessThan(0.3f));
        }

        /// <summary>
        /// Tests the random simulator on a larger tree. The score, on average, should
        /// be about 13/16, or 0.8125.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestELargerTree()
        {
            UltimateBoard b = Games.GetBoard(Games.Draw, 53);
            b.Play((1, 0, 1, 0));
            b.Play((1, 0, 0, 0));
            b.Play((0, 1, 2, 0));
            b.Play((1, 0, 1, 2));
            float sum = 0;
            for (int i = 0; i < 1000; i++)
            {
                sum += RandomSimulator.Simulate(new UltimateBoard(b));
            }
            Assert.That(sum / 1000, Is.GreaterThan(0.76).And.LessThan(0.86));
        }
    }
}
