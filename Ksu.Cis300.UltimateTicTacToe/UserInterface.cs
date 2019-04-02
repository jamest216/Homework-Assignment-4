/**
 * UserInterface.cs
 * Author: James Thomas
 * */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ksu.Cis300.UltimateTicTacToe
{
    public partial class UserInterface : Form
    {
        /// <summary>
        /// initializes board
        /// </summary>
        public UserInterface()
        {
            InitializeComponent();
            InitializeBoard();
        }

        /// <summary>
        /// current game position
        /// </summary>
        private UltimateBoard _currentGamePosition = new UltimateBoard();

        /// <summary>
        /// portion of the tree we can access and play
        /// </summary>
        private GameTreeNode _portionOfGameTree = new GameTreeNode();

        /// <summary>
        /// symbol for human 
        /// </summary>
        private string _SymbolPlayer;

        /// <summary>
        /// symbol for computer
        /// </summary>
        private string _SymbolOComputer;

        /// <summary>
        /// adds a control onto the UI
        /// </summary>
        /// <param name="c">control to ve added</param>
        /// <param name="containsControl">panel to be added to</param>
        /// <param name="w">width of panel</param>
        /// <param name="h">height of panel</param>
        /// <param name="m">margin of panel</param>
        private void AddControl(Control c, FlowLayoutPanel containsControl, int w, int h, int m)
        {
            c.Size = new Size(w,h);
            c.Margin = new Padding(m);
            containsControl.Controls.Add(c);
        }

        /// <summary>
        /// /initializes the board
        /// </summary>
        private void InitializeBoard()
        {
            for(int i = 0; i < 3; i++)
            {
                FlowLayoutPanel BigRow = new FlowLayoutPanel();
                AddControl(BigRow, uxFlowLayoutPanel, uxFlowLayoutPanel.Width , uxFlowLayoutPanel.Height / 3, 0);
                for(int j = 0; j < 3; j++)
                {
                    FlowLayoutPanel BigCol = new FlowLayoutPanel();
                    AddControl(BigCol, BigRow, BigRow.Width / 3 - 4, BigRow.Height - 4, 2);
                    for (int m = 0; m < 3; m++)
                    {
                        FlowLayoutPanel SmallRow = new FlowLayoutPanel();
                        AddControl(SmallRow, BigCol, BigCol.Width, BigCol.Height / 3, 0);
                        for (int n = 0; n < 3; n++)
                        {
                            Button b = new Button();

                            AddControl(b, SmallRow, SmallRow.Width / 3, SmallRow.Height, 0);
                            b.Click += ButtonClick;
                            b.Tag = (i, j, m, n);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// disables all the buttons on UI
        /// </summary>
        private void DisableAllButtons()
        {
            foreach (Control one in uxFlowLayoutPanel.Controls)
            {
                foreach (Control two in one.Controls)
                {
                    foreach (Control three in two.Controls)
                    {
                        foreach (Control four in three.Controls)
                        {
                            four.Enabled = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// defines whether the game is over 
        /// </summary>
        /// <param name="player">player being used for</param>
        /// <returns>a bool if game is over</returns>
        private bool GameIsover(string player)
        {
            if(_currentGamePosition.IsOver == true)
            {
                DisableAllButtons();
                if(_currentGamePosition.IsWon == true)
                {
                    uxTextBox.Text = player + " win!";
                    return true;
                }
                else
                {
                    uxTextBox.Text = "The game is a draw";
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        ///  the computers play
        /// </summary>
        private void ComputerPlay()
        {
            DisableAllButtons();
            uxTextBox.Text = "My Turn.";
            Update();

            for(int i = 0; i < 25000; i++)
            {
                UltimateBoard temp = new UltimateBoard(_currentGamePosition);
                _portionOfGameTree.Simulate(temp);
            }

            GameTreeNode t = _portionOfGameTree.GetBestChild();
            (int, int, int, int) p = t.Play;
            _currentGamePosition.Play(p);
            uxFlowLayoutPanel.Controls[p.Item1].Controls[p.Item2].Controls[p.Item3].Controls[p.Item4].Text = _SymbolOComputer;

            if(!GameIsover("I"))
            {
                _portionOfGameTree = t;
                
                foreach((int,int,int,int) loc in _currentGamePosition.GetAvailablePlays())
                {
                    uxFlowLayoutPanel.Controls[loc.Item1].Controls[loc.Item2].Controls[loc.Item3].Controls[loc.Item4].Enabled = true;
                }
                uxTextBox.Text = "Your Turn.";          
            }
        }

        /// <summary>
        /// loads the board using a message box
        /// </summary>
        /// <param name="sender">object being held</param>
        /// <param name="e">event handle for the click event</param>
        private void UserInterface_Load(object sender, EventArgs e)
        {
            if(MessageBox.Show("Would you like to play first? ", "First Play", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _SymbolPlayer = "X";
                _SymbolOComputer = "O";

                _portionOfGameTree.AddChildren(_currentGamePosition);
                uxTextBox.Text = "Your Turn.";
            }
            else
            {
                _SymbolPlayer = "O";
                _SymbolOComputer = "X";
                ComputerPlay();
            }
        }

        /// <summary>
        /// Each button click event
        /// </summary>
        /// <param name="o">the button click</param>
        /// <param name="e">the button click event</param>
        public void ButtonClick(object o, EventArgs e)
        {
            Button b = (Button)o;
            b.Text = _SymbolPlayer;
            (int, int, int, int) loc = ((int,int,int,int))b.Tag;
            _currentGamePosition.Play(loc);
            if(GameIsover("You") != true)
            {
                _portionOfGameTree = _portionOfGameTree.GetChild(loc);
                ComputerPlay();
            }
        }
    }
}
