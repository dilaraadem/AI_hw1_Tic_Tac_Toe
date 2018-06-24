//Dilara Ademoglu
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dilara_Ademoglu_AI_hw2
{
    public partial class Form1 : Form
    {
        public GraphicsPart grapP;
        Board board;
        public int maxDepth;
        static bool gameOn = false;
        int pturn = 1; //by default

        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e) //create the board
        {
            Graphics gr = panel1.CreateGraphics();
            grapP = new GraphicsPart(gr);
            board = new Board();
            board.initializeBoard();
        }
        private void panel1_Click(object sender, EventArgs e) //get click action
        {
            Point mouse = Cursor.Position;
            mouse = panel1.PointToClient(mouse);
            board.detectClick(mouse);
            if (getAI() && Board.steps != 9)
                runMinimax();
        }

        private void Form1_Load(object sender, EventArgs e)
        { }

        private void button1_Click(object sender, EventArgs e) //restarts the game
        {
            board.reset();
        }

        public int calculateDepth() //depending the difficulty level, we pick a depth for game tree
        {
            maxDepth = Int32.Parse(comboBox1.SelectedItem.ToString());
            return maxDepth;
        }

        public static bool getAI() //starts the AI game
        {
            return gameOn;
        }

        private void runMinimax() //running algorithm
        {
            MiniMax mnmx = new MiniMax(pturn, maxDepth);
            int[] move = mnmx.AImove(board);
            board.move(move[0], move[1]);
        }

        private void button2_Click(object sender, EventArgs e) //sets max depth and starts AI game
        {
            gameOn = true;
            if (comboBox1.SelectedItem != null)
                calculateDepth();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        { }
    }
}
