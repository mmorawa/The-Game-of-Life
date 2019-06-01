using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace The_Game_Of_Life
{
    public partial class Form1 : Form
    {
        private Panel Board;
        private int Counter = 0;
        private Random rnd = new Random();
        private Timer MainTimer;
        private PictureBox[,] Cells;
        private int[,] MatrixOfCells;
        private Label CycleNumber = new Label();
        private Font Standard = new Font("Arial", 16, FontStyle.Bold);
        private List<int> RandomNumbers;

        public Form1()
        {
            InitializeComponent();
        }



        private void New_game_Click(object sender, EventArgs e)
        {
            using (Form2 form2 = new Form2())
            {
                DialogResult dr = form2.ShowDialog();



                if (dr == DialogResult.OK)
                {
                    if (Board != null)
                    {
                        Board.Dispose();
                        CycleNumber.Dispose();
                        RandomNumbers.Clear();
                    }



                }
            }
        }




    }
}
