/* Games.cs
 * Author: Rod Howell
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.UltimateTicTacToe.Tests
{
    /// <summary>
    /// Provides support for playing through specific games.
    /// </summary>
    public static class Games
    {
        /// <summary>
        /// The sequence of plays in a game won by the first player.
        /// </summary>
        public static (int, int, int, int)[] XWins =>
            new (int, int, int, int)[]
            {
                (1, 1, 1, 1),
                (1, 1, 0, 2),
                (0, 2, 2, 2),
                (2, 2, 2, 1),
                (2, 1, 1, 0),
                (1, 0, 0, 0),
                (0, 0, 0, 0),
                (0, 0, 1, 2),
                (1, 2, 0, 2),
                (0, 2, 1, 0),
                (1, 0, 2, 1),
                (2, 1, 2, 1),
                (2, 1, 2, 0),
                (2, 0, 0, 0),
                (0, 0, 0, 2),
                (0, 2, 1, 2),
                (1, 2, 1, 2),
                (1, 2, 2, 2),
                (2, 2, 2, 2),
                (2, 2, 0, 1),
                (0, 1, 0, 1),
                (0, 1, 2, 1),
                (2, 1, 0, 0),
                (0, 0, 1, 0),
                (1, 0, 2, 2),
                (2, 2, 1, 1),
                (1, 1, 2, 2),
                (1, 1, 0, 0),
                (0, 0, 0, 1),
                (0, 1, 0, 0),
                (0, 2, 1, 1),
                (1, 1, 0, 1),
                (0, 1, 1, 2),
                (1, 2, 2, 1),
                (0, 2, 0, 0),
                (1, 0, 2, 0),
                (2, 0, 2, 0),
                (2, 0, 0, 2),
                (0, 1, 1, 0),
                (1, 0, 1, 2),
                (1, 2, 2, 0),
                (2, 0, 1, 2),
                (1, 2, 1, 0),
                (1, 0, 1, 1),
                (0, 1, 1, 1)
            };

        /// <summary>
        /// The sequence of moves in a game won by the second player.
        /// </summary>
        public static (int, int, int, int)[] OWins =>
            new (int, int, int, int)[]
            {
                (1, 1, 0, 0),
                (0, 0, 2, 2),
                (2, 2, 0, 1),
                (0, 1, 1, 2),
                (1, 2, 2, 0),
                (2, 0, 2, 1),
                (2, 1, 2, 1),
                (2, 1, 2, 0),
                (2, 0, 0, 1),
                (0, 1, 2, 2),
                (2, 2, 1, 1),
                (1, 1, 2, 0),
                (2, 0, 2, 0),
                (2, 0, 1, 0),
                (1, 0, 2, 0),
                (2, 0, 1, 2),
                (1, 2, 1, 0),
                (1, 0, 0, 0),
                (0, 0, 0, 0),
                (0, 0, 2, 0),
                (2, 0, 1, 1),
                (1, 1, 2, 2),
                (2, 2, 2, 1),
                (2, 1, 0, 0),
                (0, 0, 0, 1),
                (0, 1, 0, 2),
                (0, 2, 2, 0),
                (2, 0, 0, 2),
                (0, 2, 1, 0),
                (1, 0, 1, 0),
                (1, 0, 1, 2),
                (1, 2, 0, 0),
                (0, 0, 0, 2),
                (0, 2, 0, 2),
                (0, 2, 1, 2),
                (1, 2, 0, 2),
                (0, 2, 2, 1),
                (2, 1, 0, 2),
                (0, 2, 2, 2),
                (1, 1, 2, 1),
                (2, 1, 1, 0),
                (1, 0, 0, 1),
                (2, 1, 0, 1),
                (2, 1, 1, 1)
            };

        /// <summary>
        /// The sequence of moves in a drawn game.
        /// </summary>
        public static (int, int, int, int)[] Draw =>
            new (int, int, int, int)[]
            {
                (1, 1, 1, 1),
                (1, 1, 1, 2),
                (1, 2, 1, 2),
                (1, 2, 0, 2),
                (0, 2, 0, 2),
                (0, 2, 0, 0),
                (0, 0, 2, 2),
                (2, 2, 2, 1),
                (2, 1, 2, 0),
                (2, 0, 0, 0),
                (0, 0, 1, 1),
                (1, 1, 0, 2),
                (0, 2, 2, 2),
                (2, 2, 0, 1),
                (0, 1, 0, 0),
                (0, 0, 0, 0),
                (0, 0, 2, 0),
                (2, 0, 2, 0),
                (2, 0, 1, 0),
                (1, 0, 2, 2),
                (2, 2, 1, 0),
                (1, 0, 2, 0),
                (2, 0, 1, 2),
                (1, 2, 2, 0),
                (2, 0, 2, 2),
                (2, 2, 2, 0),
                (2, 0, 0, 2),
                (0, 2, 1, 2),
                (1, 2, 2, 1),
                (2, 1, 2, 1),
                (2, 1, 0, 1),
                (0, 1, 2, 2),
                (2, 2, 2, 2),
                (2, 2, 1, 1),
                (1, 1, 2, 2),
                (1, 1, 0, 0),
                (0, 0, 2, 1),
                (2, 1, 2, 2),
                (1, 0, 2, 1),
                (2, 1, 1, 2),
                (1, 2, 1, 1),
                (1, 1, 0, 1),
                (0, 1, 0, 2),
                (0, 2, 2, 1),
                (2, 1, 0, 2),
                (0, 2, 1, 1),
                (1, 0, 0, 1),
                (0, 1, 2, 1),
                (2, 1, 0, 0),
                (1, 0, 1, 1),
                (1, 2, 0, 1),
                (0, 1, 0, 1),
                (0, 1, 1, 2),
                (1, 0, 0, 0),
                (0, 1, 1, 0),
                (0, 1, 1, 1),
                (0, 2, 0, 1),
                (0, 2, 1, 0)
            };

        /// <summary>
        /// Gets the board resulting from playing the given number of plays from the
        /// given sequence of plays.
        /// </summary>
        /// <param name="plays">The sequence of plays.</param>
        /// <param name="n">The number of plays to make from the sequence.</param>
        /// <returns>The board that results from making the specified plays.</returns>
        public static UltimateBoard GetBoard((int, int, int, int)[] plays, int n)
        {
            UltimateBoard b = new UltimateBoard();
            for (int i = 0; i < n; i++)
            {
                b.Play(plays[i]);
            }
            return b;
        }
    }
}
