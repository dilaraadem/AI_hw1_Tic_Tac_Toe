//Dilara Ademoglu
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dilara_Ademoglu_AI_hw2
{
    class MiniMax
    {
        private int maxDepth;
        int turn;
        bool playersturn = false;
        List<int[]> test = new List<int[]>();
        //constructor
        public MiniMax(int pturn, int maxDepth)
        {
            this.turn = pturn;
            this.maxDepth = maxDepth;
        }

        public int[] AImove(Board board) //main function for this class
        {
            int alpha = Int32.MinValue;
            int beta = Int32.MaxValue;
            int[] result = minimaxAl(maxDepth, playersturn, alpha, beta, board); // depth, max turn
            return new int[] { result[1], result[2] };   // row, col
        }

        public List<int[]> generateMoves(Board board) //generates array of int[] that stores x and y coordinates
        {
            List<int[]> nextMoves = new List<int[]>(); // allocate List

            if (board.detectRow())
                return nextMoves;   // return empty list

            // Search for empty cells and add to the List
            for (int row = 0; row < 3; ++row)
            {
                for (int col = 0; col < 3; ++col)
                {
                    if (board.holders[row, col].getValue() == Board.B)
                    {
                        nextMoves.Add(new int[] { row, col });
                    }
                }
            }
            return nextMoves;
        }

        public int getPlayer(bool pturn) //get boolean value and returns players value in int
        {
            int player = 0;
            if (pturn) //selecting turn
            {
                player = 0;
            }
            else
            {
                player = 1;
            }
            return player;
        }

        /* The heuristic evaluation function for the given line of 3 cells
              * Return +100, +10, +1 for 3-, 2-, 1-in-a-line for computer.
              * -100, -10, -1 for 3-, 2-, 1-in-a-line for opponent.
              * 0 otherwise 
              */
        public int evaluateScore(bool pturn, Board board, int x1, int y1, int x2, int y2, int x3, int y3)
        {
            int score = 0;
            int player = 0;
            int opponent = 1;
            if (pturn) //selecting turn
            {
                opponent = 1;
                player = 0;
            }
            else
            {
                player = 1;
                opponent = 0;
            }

            //1 out of 3
            if (board.holders[x1, y1].getValue() == player) // if AI has O on the line +1
            {
                score = 1;
            }
            else if (board.holders[x1, y1].getValue() == opponent)//if opponent has X on the line -1 
                score = -1;

            //2 out of 3
            if (board.holders[x2, y2].getValue() == player) //cell2 is AI
            {
                if (score == 1) //AI has O in the line already
                    score = 10;
                else if (score == -1) //The line already has X on it
                    return 0;
                else //empty line
                    score = 1;
            }
            else if (board.holders[x2, y2].getValue() == opponent) // cell 2 is X
            {
                if (score == -1)// cell1 is X
                {
                    score = -10;
                }
                else if (score == 1) // cell1 is O
                {
                    return 0;
                }
                else // cell1 is empty
                {
                    score = -1;
                }
            }

            // Third cell
            if (board.holders[x3, y3].getValue() == player) //cell 3 is O
            {
                if (score > 0)// cell1 and/or cell2 is O
                {
                    score = 100;
                }
                else if (score < 0)// cell1 and/or cell2 is X
                {
                    return 0;
                }
                else// cell1 and cell2 are empty
                {
                    score = 1;
                }
            }
            else if (board.holders[x3, y3].getValue() == opponent) //cell3 is X
            {
                if (score < 0)// cell1 and/or cell2 is X
                {
                    score = 100;
                }
                else if (score > 1)// cell1 and/or cell2 is O
                {
                    return 0;
                }
                else // cell1 and cell2 are empty
                {
                    score = -1;
                }
            }

            return score;
        }

        public int evalScore(Board board, bool player)
        {
            int score = 0;
            for (int x = 0; x < 3; x++)
            {
                score += evaluateScore(player, board, x, 0, x, 1, x, 2); //evaluate rows
            }
            for (int y = 0; y < 3; y++)
            {
                score += evaluateScore(player, board, 0, y, 1, y, 2, y);  //evaluate cols
            }
            score += evaluateScore(player, board, 0, 0, 1, 1, 2, 2);  // diagonal
            score += evaluateScore(player, board, 0, 2, 1, 1, 2, 0);  // other diagonal

            return score;
        }


        public int[] minimaxAl(int level, bool player, int alpha, int beta, Board board) //algorithm implemenatation with alpha beta pruning
        {

            List<int[]> nextMoves = generateMoves(board);
            int[] storage;
            int score;
            int bestRow = -1;
            int bestCol = -1;
            if (nextMoves.Count == 0 || level == 0 || board.detectRow()) //if board is full or a player won or depth has reached
            {
                score = evalScore(board, player); //calculate heuristics
                return new int[] { score, bestRow, bestCol }; //return score and coordinates(x,y)
            }
            else
            {
                foreach (int[] move in nextMoves) //for each move in available moves
                {
                    if (!player)//player is AI
                    {
                        Board modiBoard = board;
                        modiBoard.holders[move[0], move[1]].setValue(getPlayer(player));
                        bool opponent = !player;
                        storage = minimaxAl(level - 1, opponent, alpha, beta, modiBoard); //recurse function
                        score = storage[0];
                        if (score > alpha)
                        {
                            alpha = score;
                            bestRow = move[0];
                            bestCol = move[1];
                            test.Add(new int[] { score, bestRow, bestCol });
                        }
                        modiBoard.holders[move[0], move[1]].setValue(Board.B);//undo move
                        player = true;
                    }
                    else //player is opponent
                    {
                        Board modiBoard = board; //copy of the board
                        modiBoard.holders[move[0], move[1]].setValue(getPlayer(player)); //set value on the mod.board
                        storage = minimaxAl(level - 1, player, alpha, beta, modiBoard); //recurse function
                        score = storage[0];
                        if (score < beta)
                        {
                            beta = score;
                            bestRow = move[0];
                            bestCol = move[1];
                            test.Add(new int[] { score, bestRow, bestCol });
                        }
                        modiBoard.holders[move[0], move[1]].setValue(Board.B);//undo move
                        player = false;
                    }

                    if (alpha >= beta) //pruning happens here
                        break;
                }
                return new int[] { (player == false) ? (int)alpha : (int)beta, bestRow, bestCol };
            }
        }
    }
}
