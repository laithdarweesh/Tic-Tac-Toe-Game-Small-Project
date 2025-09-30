using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tic_Tac_Toe_Game_My_Solution.Properties;

namespace Tic_Tac_Toe_Game_My_Solution
{
    public partial class Form1 : Form
    {
        enum enPlayerTurn { Player1, Player2}
        enum enWinner { Player1, Player2, GameInProgress, Draw}

        struct stGameStatus
        {
            public short PlayCount;
            public enWinner Winner;
            public bool GameOver;
        }

        stGameStatus GameStatus;
        enPlayerTurn PlayerTurn = enPlayerTurn.Player1;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color WhiteColor = Color.FromArgb(255, 255, 255, 255);
            Pen pen = new Pen(WhiteColor);
            pen.Width = 15;

            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            //Draw Horizantal Line
            e.Graphics.DrawLine(pen, 400, 300, 1050, 300);
            e.Graphics.DrawLine(pen, 400, 460, 1050, 460);

            //Draw Vertivcal Line
            e.Graphics.DrawLine(pen, 610, 140, 610, 620);
            e.Graphics.DrawLine(pen, 840, 140, 840, 620);

        }

        void EndGame()
        {
            GameStatus.GameOver = true;
            lblTurn.Text = "Game Over";

            switch(GameStatus.Winner)
            {
                case enWinner.Player1:
                    lblWinner.Text = "Player 1";
                    break;

                case enWinner.Player2:
                    lblWinner.Text = "Player 2";
                    break;

                default:
                    lblWinner.Text = "Draw";
                    break;
            }

            MessageBox.Show("Game Over", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DisableButtons();
        }

        bool CheckValues(Button btn1, Button btn2, Button btn3)
        {
            if(btn1.Tag.ToString() != "?" && btn1.Tag.ToString() == btn2.Tag.ToString() && btn1.Tag.ToString() == btn3.Tag.ToString())
            {
                btn1.BackColor = Color.GreenYellow;
                btn2.BackColor = Color.GreenYellow;
                btn3.BackColor = Color.GreenYellow;

                if(btn1.Tag.ToString() == "X")
                {
                    GameStatus.Winner = enWinner.Player1;
                }
                else
                {
                    GameStatus.Winner = enWinner.Player2;
                }

                return true;
            }

            return false;
        }

        void CheckWinner()
        {
            // Check for rows:

            // row 1
            if(CheckValues(btn1, btn2, btn3)) 
            {
                EndGame();
                return;
            }

            // row 2
            if (CheckValues(btn4, btn5, btn6))
            {
                EndGame();
                return;
            }

            // row 3
            if (CheckValues(btn7, btn8, btn9))
            {
                EndGame();
                return;
            }

            // Check for cols:

            // col 1
            if (CheckValues(btn1, btn4, btn7))
            {
                EndGame();
                return;
            }

            // col 2
            if (CheckValues(btn2, btn5, btn8))
            {
                EndGame();
                return;
            }

            // col 3
            if (CheckValues(btn3, btn6, btn9))
            {
                EndGame();
                return;
            }

            // check for diagonal

           // diagonal 1
           if(CheckValues(btn1, btn5, btn9))
            {
                EndGame();
                return;
            }

            // diagonal 2
            if (CheckValues(btn3, btn5, btn7))
            {
                EndGame();
                return;
            }

            if(GameStatus.PlayCount == 9)
            {
                GameStatus.Winner = enWinner.Draw;
                EndGame();
                return;
            }
        }

        void ChangeImage(Button btn)
        {
            if(btn.Tag.ToString() == "?")
            {
                GameStatus.PlayCount++;
                GameStatus.Winner = enWinner.GameInProgress;
                GameStatus.GameOver = false;

                switch(PlayerTurn)
                {
                    case enPlayerTurn.Player1:
                        btn.Image = Resources.X;
                        btn.Tag = "X";
                        lblTurn.Text = "Player 2";
                        PlayerTurn = enPlayerTurn.Player2;
                        CheckWinner();
                        break;

                    case enPlayerTurn.Player2:
                        btn.Image = Resources.O;
                        btn.Tag = "O";
                        lblTurn.Text = "Player 1";
                        PlayerTurn = enPlayerTurn.Player1;
                        CheckWinner();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Wrong Choice", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void DisableButtons()
        {
            btn1.Enabled = false;
            btn2.Enabled = false;
            btn3.Enabled = false;
            btn4.Enabled = false;
            btn5.Enabled = false;
            btn6.Enabled = false;
            btn7.Enabled = false;
            btn8.Enabled = false;
            btn9.Enabled = false;
        }

        void ResetButtons(Button btn)
        {
            btn.Tag = "?";
            btn.Image = Resources.question_mark_96;
            btn.Enabled = true;
            btn.BackColor = Color.Transparent;
        }
        
        void RestartGame()
        {
            ResetButtons(btn1);
            ResetButtons(btn2);
            ResetButtons(btn3);
            ResetButtons(btn4);
            ResetButtons(btn5);
            ResetButtons(btn6);
            ResetButtons(btn7);
            ResetButtons(btn8);
            ResetButtons(btn9);

            PlayerTurn = enPlayerTurn.Player1;
            GameStatus.Winner = enWinner.GameInProgress;
            GameStatus.PlayCount = 0;
            GameStatus.GameOver = false;
            lblTurn.Text = "Player 1";
            lblWinner.Text = "In Progress";
        }

        private void btn_Click(object sender, EventArgs e)
        {
            ChangeImage((Button) sender);
        }

        private void btnRestratGame_Click(object sender, EventArgs e)
        {
            RestartGame();
        }
    }
}
