/* GameTreeNodeTests.cs
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
    /// Unit tests for the GameTreeNode class.
    /// </summary>
    [TestFixture]
    public class GameTreeNodeTests
    {
        /// <summary>
        /// Tests that the 1-parameter constructor correctly stores the play.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestAConstructor()
        {
            GameTreeNode t = new GameTreeNode((0, 1, 2, 0));
            Assert.That(t.Play, Is.EqualTo((0, 1, 2, 0)));
        }

        /// <summary>
        /// Tests that children are correctly added and retrieved. The board position
        /// used has four available plays.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestBAddChildren()
        {
            UltimateBoard b = Games.GetBoard(Games.XWins, 43);
            GameTreeNode t = new GameTreeNode(Games.XWins[42]);
            t.AddChildren(b);
            List<(int, int, int, int)> avail = b.GetAvailablePlays();
            List<(int, int, int, int)> children = new List<(int, int, int, int)>();
            foreach ((int, int, int, int) play in avail)
            {
                children.Add(t.GetChild(play).Play); // Get the specified child and retrieve its play
            }
            Assert.That(children, Is.EquivalentTo(avail));
        }

        /// <summary>
        /// Tests that a simulation on a position won for the first player returns 1.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestCSimulateXWin()
        {
            UltimateBoard b = Games.GetBoard(Games.XWins, Games.XWins.Length);
            GameTreeNode t = new GameTreeNode(Games.XWins[Games.XWins.Length - 1]);
            Assert.That(t.Simulate(b), Is.EqualTo(1));
        }

        /// <summary>
        /// Tests that a simulation on a position won for the second player returns 1.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestCSimulateOWin()
        {
            UltimateBoard b = Games.GetBoard(Games.OWins, Games.OWins.Length);
            GameTreeNode t = new GameTreeNode(Games.OWins[Games.OWins.Length - 1]);
            Assert.That(t.Simulate(b), Is.EqualTo(1));
        }

        /// <summary>
        /// Tests that a simulation on a drawn position returns 0.5.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestCSimulateDraw()
        {
            UltimateBoard b = Games.GetBoard(Games.Draw, Games.Draw.Length);
            GameTreeNode t = new GameTreeNode(Games.Draw[Games.Draw.Length - 1]);
            Assert.That(t.Simulate(b), Is.EqualTo(0.5f));
        }

        /// <summary>
        /// Tests that the first simulation on a node does a random simulation 
        /// without adding children to that node.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestDFirstSimulation()
        {
            UltimateBoard b = Games.GetBoard(Games.Draw, 53);
            b.Play((1, 0, 1, 0));
            b.Play((1, 0, 0, 0));
            b.Play((0, 1, 2, 0));
            b.Play((1, 0, 1, 2));
            b.Play((0, 2, 0, 1)); // This board position has only one available play.
            GameTreeNode t = new GameTreeNode((0, 2, 0, 1));
            float score = t.Simulate(b);
            GameTreeNode child = null;
            try
            {
                child = t.GetChild((1, 0, 0, 2)); // The only possible child.
            }
            catch (Exception)
            {
                // Ignore any exception.
            }
            Assert.Multiple(() =>
            {
                Assert.That(score, Is.EqualTo(0)); // The only play wins for the opponent.
                Assert.That(child, Is.Null); // Check that no child was retrieved.
            });
        }

        /// <summary>
        /// Tests that the second simulation adds the children and does a correct simulation
        /// on that child.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestESecondSimulation()
        {
            UltimateBoard b = Games.GetBoard(Games.Draw, 53);
            b.Play((1, 0, 1, 0));
            b.Play((1, 0, 0, 0));
            b.Play((0, 1, 2, 0));
            b.Play((1, 0, 1, 2));
            b.Play((0, 2, 0, 1)); // This board position has only one available play.
            GameTreeNode t = new GameTreeNode((0, 2, 0, 1));
            t.Simulate(new UltimateBoard(b)); // The first simulation should not add a child (see above).
            float score = t.Simulate(new UltimateBoard(b)); // The second simulation should add the child.
            Assert.Multiple(() =>
            {
                Assert.That(score, Is.EqualTo(0)); // The only play wins for the opponent.
                Assert.That(t.GetChild((1, 0, 0, 2)).Play, Is.EqualTo((1, 0, 0, 2))); // Check that the child was retrieved.
            });
        }

        /// <summary>
        /// Tests that each child is simulated once before its children are added.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestFSimulateAllChildrenOnce()
        {
            UltimateBoard b = Games.GetBoard(Games.Draw, 53);
            b.Play((1, 0, 1, 0));
            b.Play((1, 0, 0, 0));
            b.Play((0, 1, 2, 0));
            b.Play((1, 0, 1, 2)); // This board position has four available plays.
            GameTreeNode t = new GameTreeNode((1, 0, 1, 2));
            for (int i = 0; i < 5; i++)
            {
                // The second iteration adds the four children and does a simulation on one
                // of them. The next three should each do a simulation on a different child 
                // without adding any grandchildren.
                t.Simulate(new UltimateBoard(b));
            }
            List<(int, int, int, int)> avail = b.GetAvailablePlays();
            StringBuilder results = new StringBuilder();
            foreach ((int, int, int, int) play in avail)
            {
                GameTreeNode grandchild = null;
                try
                {
                    grandchild = t.GetChild(play).GetChild((1, 0, 0, 2)); // (1, 0, 0, 2) is an available response to each available play.
                    results.Append(grandchild.Play.ToString());
                }
                catch // The above will throw an exception if the grandchild hasn't been added.
                {
                    results.Append("OK;");
                }
            }
            Assert.That(results.ToString(), Is.EqualTo("OK;OK;OK;OK;")); // t should not contain any of the grandchildren.
        }

        /// <summary>
        /// Tests that after the children have been added, the first simulation on each child returns the correct score.
        /// The order that the children are chosen is implementation-dependent; hence, we just check that each correct score
        /// is returned, with the order being unimportant.  The tree used has two children, and from each of these children,
        /// only one play is available. These two plays each end the game with different results.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestGGetChildForSimulationFirstTimeThrough()
        {
            UltimateBoard b = Games.GetBoard(Games.Draw, 53);
            b.Play((1, 0, 1, 0));
            b.Play((1, 0, 0, 0));
            b.Play((0, 1, 2, 0));
            b.Play((1, 0, 1, 2));
            b.Play((0, 2, 2, 0));
            b.Play((0, 2, 0, 1));
            GameTreeNode t = new GameTreeNode((0, 2, 0, 1));
            t.Simulate(new UltimateBoard(b)); // A random simulation.
            float[] scores = new float[2];

            scores[0] = t.Simulate(new UltimateBoard(b));
            // The above adds the two children, (0, 2, 1, 0) and (1, 0, 0, 2), and does a random simulation on one of them.
            // The child chosen might be either, depending on the implementation. In each case, there is only one possible 
            // continuation. If (0, 2, 1, 0) is chosen, the value returned will be 1; otherwise, the value will be 0.5.

            scores[1] = t.Simulate(new UltimateBoard(b));
            // The above does a random simulation on the other child; hence, the value returned should be either 1 or 0.5,
            // whichever value wasn't returned by the previous simulation.

            float[] correct = { 1, 0.5f };
        }

        /// <summary>
        /// Tests that after each child has been simulated once, subsequent simulations each select the correct child.
        /// The tree used has two children, and from each of these children, only one play is available. These two plays each 
        /// end the game with different results. Thus, the score returned indicates which child was chosen. Note that because
        /// the score returned is from the perspective of the player who just played, lower scores will tend to be returned
        /// more often (i.e., the more promising plays for the player about to play are simulated more often).
        /// </summary>
        [Test, Timeout(1000)]
        public void TestHGetChildForSimulationUsingFormula()
        {
            UltimateBoard b = Games.GetBoard(Games.Draw, 53);
            b.Play((1, 0, 1, 0));
            b.Play((1, 0, 0, 0));
            b.Play((0, 1, 2, 0));
            b.Play((1, 0, 1, 2));
            b.Play((0, 2, 2, 0));
            b.Play((0, 2, 0, 1));
            GameTreeNode t = new GameTreeNode((0, 2, 0, 1));
            t.Simulate(new UltimateBoard(b)); // A random simulation.

            t.Simulate(new UltimateBoard(b));
            // The above adds the two children, (0, 2, 1, 0) and (1, 0, 0, 2), and does a random simulation on one of them.

            t.Simulate(new UltimateBoard(b));
            // The above does a random simulation on the other child.

            // At this point, the results of any further simulations are determined entirely by whichever child maximizes
            // the formula. The results should therefore be the same for any correct implementation.
            float[] scores = new float[10];
            for (int i = 0; i < scores.Length; i++)
            {
                scores[i] = t.Simulate(new UltimateBoard(b));
            }
            float[] correct = { 0.5f, 0.5f, 1, 0.5f, 0.5f, 1, 0.5f, 0.5f, 0.5f, 0.5f };
            Assert.That(scores, Is.EqualTo(correct)); // The same values in the same order
        }

        /// <summary>
        /// Tests that after enough simulations, the best child is chosen. In this test, only two plays are possible.
        /// From (0, 2, 1, 0), the opponent's only legal play wins the game. From (1, 0, 0, 2), the opponent's only 
        /// legal play draws. The latter play should therefore be chosen.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestIGetBestChild()
        {
            UltimateBoard b = Games.GetBoard(Games.Draw, 53);
            b.Play((1, 0, 1, 0));
            b.Play((1, 0, 0, 0));
            b.Play((0, 1, 2, 0));
            b.Play((1, 0, 1, 2));
            b.Play((0, 2, 2, 0));
            b.Play((0, 2, 0, 1));
            GameTreeNode t = new GameTreeNode((0, 2, 0, 1));
            for (int i = 0; i < 100; i++)
            {
                t.Simulate(new UltimateBoard(b));
            }
            Assert.That(t.GetBestChild().Play, Is.EqualTo((1, 0, 0, 2)));
        }

        /// <summary>
        /// Tests that after enough simulations, the best child is chosen. In this test, the tree is slightly larger
        /// than in the above test. There are four legal plays. From (0, 2, 0, 1) and (0, 2, 1, 0), the only legal
        /// response wins for the opponent. From (1, 0, 0, 2), every continuation leads to a draw. From (0, 2, 2, 0),
        /// some continuations draw, and others win for the opponent; however, one of the responses wins immediately
        /// for the opponent. (1, 0, 0, 2) should therefore be chosen.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestJGetBestChildLargerTree()
        {
            UltimateBoard b = Games.GetBoard(Games.Draw, 53);
            b.Play((1, 0, 1, 0));
            b.Play((1, 0, 0, 0));
            b.Play((0, 1, 2, 0));
            b.Play((1, 0, 1, 2));
            GameTreeNode t = new GameTreeNode((1, 0, 1, 2));
            for (int i = 0; i < 100; i++)
            {
                t.Simulate(new UltimateBoard(b));
            }
            Assert.That(t.GetBestChild().Play, Is.EqualTo((1, 0, 0, 2)));
        }
    }
}
