/**
 * RandomSimulator.cs
 * Author: James Thomas
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.UltimateTicTacToe
{
    public class RandomSimulator
    {
        /// <summary>
        /// sets a random object
        /// </summary>
        private static Random _randNum = new Random();

        /// <summary>
        /// simulates randomly
        /// </summary>
        /// <param name="x">board to simulate</param>
        /// <returns>returns a float</returns>
        public static float Simulate(UltimateBoard x)
        {
            if (x.IsOver == true)
            {
                if (x.IsWon == true)
                {
                    return 1;
                }
                else
                {
                    return 0.5f;
                }
            }
            else
            {
                List<(int, int, int, int)> temp = x.GetAvailablePlays();
                int randPlay = _randNum.Next(temp.Count);             
                x.Play(temp[randPlay]);
                float num = Simulate(x);
                return (1 - num);
            }
        }
    }
}
