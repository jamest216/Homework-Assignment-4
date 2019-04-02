/**
 * GameTreeNode.cs
 * Author: James Thomas
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.UltimateTicTacToe
{
    public class GameTreeNode
    {
        /// <summary>
        /// holds the given play
        /// </summary>
        public (int, int, int, int) Play { get; }

        /// <summary>
        /// holds children of current node
        /// </summary>
        private GameTreeNode[] _children = null;

        /// <summary>
        /// total simulations
        /// </summary>
        private int _simulations;

        /// <summary>
        /// simulations the current child has been put
        /// </summary>
        private int _childrenSimulations;

        /// <summary>
        /// total score of play
        /// </summary>
        private float _score;

        /// <summary>
        /// empty constructor no param calls
        /// </summary>
        public GameTreeNode()
        {

        }

        /// <summary>
        /// sets the play = to the current play
        /// </summary>
        /// <param name="play"></param>
        public GameTreeNode((int, int, int, int) play)
        {
            Play = play;
        }

        /// <summary>
        /// updates score given f
        /// </summary>
        /// <param name="f">score to add</param>
        /// <returns>the score added</returns>
        private float UpdateScore(float f)
        {
            _score = _score + f;
            _simulations++;
            return f;
        }

        /// <summary>
        /// get a specific child for simulation
        /// </summary>
        /// <returns>a child for simulation</returns>
        private GameTreeNode GetChildForSimulation()
        {
            if(_childrenSimulations < _children.Length){
                GameTreeNode currentNode = _children[_childrenSimulations];
                _childrenSimulations++;
                return currentNode;
            }
            else
            {
                float MaxNum = -1;
                double x = (2 * Math.Log(_simulations));
                GameTreeNode best = null;
                foreach(GameTreeNode temp in _children)
                {
                    float result = (float)((temp._score / temp._simulations) + (Math.Sqrt(x / temp._simulations)));
                    if(result > MaxNum)
                    {
                        best = temp;
                        MaxNum = result;
                    }
                }
                return best;
            }
        }

        /// <summary>
        /// adds children to the current node
        /// </summary>
        /// <param name="x">the baord whos children to be added</param>
        public void AddChildren(UltimateBoard x)
        {
            List<(int, int, int, int)> temp = x.GetAvailablePlays();
            _children = new GameTreeNode[temp.Count];

            for(int i = 0; i < _children.Length; i++)
            {
                _children[i] = new GameTreeNode(temp[i]);
            }
        }

        /// <summary>
        /// simulates the current play on a board
        /// </summary>
        /// <param name="x">board to be simulated</param>
        /// <returns>returns an updated score</returns>
        public float Simulate(UltimateBoard x)
        {
            if (_simulations == 0)
            {
                return UpdateScore(RandomSimulator.Simulate(x));
            }
            else if (x.IsOver == true)
            {
                if(x.IsWon == true)
                {
                    return UpdateScore(1);
                }
                else
                {
                    return UpdateScore(.5f);
                }
            }
            else
            {
                if (_children == null)
                {
                    AddChildren(x);
                }
                GameTreeNode temp = GetChildForSimulation();
                x.Play(temp.Play);
                return UpdateScore(1 - temp.Simulate(x));
            }
        }

        /// <summary>
        /// gets the bets child for simulation
        /// </summary>
        /// <returns>returns best child for simulation</returns>
        public GameTreeNode GetBestChild()
        {
            int max = 0;
            GameTreeNode temp = null;

            for (int i = 0; i < _children.Length; i++)
            {
                temp = _children[i];
                if (_children[i]._simulations > max)
                {
                    temp = _children[i];
                    max = _children[i]._simulations;
                }
            }
            return temp;
        }

        /// <summary>
        /// gets a child 
        /// </summary>
        /// <param name="play">a play giving its location on board</param>
        /// <returns>child thats equal to the play</returns>
        public GameTreeNode GetChild((int, int, int, int) play)
        {
            GameTreeNode temp = null;
            for (int i = 0; i < _children.Length; i++)
            {
                if (_children[i].Play.Equals(play))
                {
                    temp = _children[i];
                }
            }
            return temp;
        }
    }
}
