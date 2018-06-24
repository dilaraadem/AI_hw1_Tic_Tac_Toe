//Dilara Ademoglu
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Dilara_Ademoglu_AI_hw2
{
    public class Board
    {
        public static int steps = 0; //control number of moves
        private Rectangle[,] slots = new Rectangle[3, 3];
        public Holder[,] holders = new Holder[3, 3];
        public int winner = -1;
        public const int X = 0;
        public const int O = 1;
        public const int B = -1;
        public bool gameOver = false;
        private int turn = X;

        public void initializeBoard() //sets the game
        {
            for (int x = 0; x < 3; x++) //rows
            {
                for (int y = 0; y < 3; y++)//cols
                {
                    slots[x, y] = new Rectangle(x * 166, y * 166, 166, 166);
                    holders[x, y] = new Holder();
                    holders[x, y].setValue(B);
                    holders[x, y].setLoc(new Point(x, y));
                }
            }
        }
    
        public void detectClick(Point loc) //action when clicking
        {
            //checks if the board was clicked not the menu
            if (loc.Y <= 500)
            {
                int x = 0;
                int y = 0;
                //x coordinate
                if (loc.X < 166)
                    x = 0;
                else if (loc.X > 166 && loc.X < 332)
                    x = 1;
                else
                    x = 2;

                //y coordinate
                if (loc.Y < 166)
                    y = 0;
                else if (loc.Y > 166 && loc.Y < 332)
                    y = 1;
                else if (loc.Y > 332 && loc.Y < 498)
                    y = 2;

                move(x, y);
            }
        }
       
        public bool isGameOver() //getter for game over
        {
            return gameOver;
        }

        public void move(int x, int y) //move function for acting
        {
            if (steps % 2 == 0) //X's turn
            {
                if (holders[x, y].getValue() != O)
                {
                    GraphicsPart.drawX(new Point(x, y));
                    holders[x, y].setValue(X);
                    steps++;//with every click increment moves
                    turn = O;
                    if (detectRow())
                    {
                        MessageBox.Show("X has won!");
                        winner = X;
                        reset();
                    }
                }
            }
            else //O's turn
            {
                if (holders[x, y].getValue() != X)
                {
                    GraphicsPart.drawO(new Point(x, y));
                    holders[x, y].setValue(O);
                    steps++;//with every click increment moves
                    turn = X;
                    if (detectRow())
                    {
                        MessageBox.Show("O has won!");
                        winner = O;
                        reset();
                    }
                }
            }

            if (steps == 9 && detectRow() == false) //DRAW
            {
                MessageBox.Show("It's a draw!");
                winner = B;
                reset();
            }
        }

        public bool detectRow() //detect if there is 3 of the same symbol that leads to a win
        {
            // detects horizontal rows
            for (int x = 0; x < 3; x++)
            {
                if (holders[x, 0].getValue() == X && holders[x, 1].getValue() == X && holders[x, 2].getValue() == X)
                {
                    return true;
                }
                else if (holders[x, 0].getValue() == O && holders[x, 1].getValue() == O && holders[x, 2].getValue() == O)
                {
                    return true;
                }
                //detects diagonal rows
                switch (x)
                {
                    case 0:

                        if (holders[x, 0].getValue() == X && holders[x + 1, 1].getValue() == X && holders[x + 2, 2].getValue() == X)
                        {
                            return true;
                        }
                        else if (holders[x, 0].getValue() == O && holders[x + 1, 1].getValue() == O && holders[x + 2, 2].getValue() == O)
                        {
                            return true;
                        }
                        break;

                    case 2:
                        if (holders[x, 0].getValue() == X && holders[x - 1, 1].getValue() == X && holders[x - 2, 2].getValue() == X)
                        {
                            return true;
                        }
                        else if (holders[x, 0].getValue() == O && holders[x - 1, 1].getValue() == O && holders[x - 2, 2].getValue() == O)
                        {
                            return true;
                        }
                        break;
                }
            }
            // detects vertical rows
            for (int y = 0; y < 3; y++)
            {
                if (holders[0, y].getValue() == X && holders[1, y].getValue() == X && holders[2, y].getValue() == X)
                {
                    return true;
                }
                else if (holders[0, y].getValue() == O && holders[1, y].getValue() == O && holders[2, y].getValue() == O)
                {
                    return true;
                }
            }
            return false;
        }

        public void reset() //resets the board
        {
            holders = new Holder[3, 3];
            initializeBoard();
            GraphicsPart.setUpCanvas();
            steps = 0;
            if (getWinner() == X || getWinner() == B)
                turn = X;
            else
                turn = O;
            winner = B;
        }

        public int getTurn() //returns turn
        {
            return turn;
        }
        public int getWinner() //returns winner
        {
            return winner;
        }
    }
}
