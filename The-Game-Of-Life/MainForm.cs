using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace The_Game_Of_Life
{
    public partial class MainForm : Form
    {
        private readonly Random rnd = new Random();
        private readonly Font standard = new Font("Arial", 16, FontStyle.Bold);

        private Panel gameBoard;
        private int counter = 0;
        private Timer mainTimer;
        private PictureBox[,] cells;
        private int[,] matrixOfCells;
        private Label cycleNumber = new Label();
        private List<int> randomNumbers;

        public MainForm()
        {
            this.InitializeComponent();
        }

        private void New_game_Click(object sender, EventArgs e)
        {
            using (InputForm form2 = new InputForm())
            {
                DialogResult dr = form2.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    if (this.gameBoard != null)
                    {
                        this.gameBoard.Dispose();
                        this.cycleNumber.Dispose();
                        this.randomNumbers.Clear();
                    }

                    this.CreateBoard();

                    this.mainTimer = new Timer
                    {
                        Interval = 1000,
                    };
                    this.mainTimer.Tick += this.Timer_Tick;

                    this.counter = 0;
                    this.mainTimer.Start();
                }
            }
        }

        private void CreateBoard()
        {
            this.gameBoard = new Panel
            {
                Location = new Point(15, 55),
                BackColor = Color.White,
                Size = new Size(InputForm.Length * 50, InputForm.Length * 50),
                BorderStyle = BorderStyle.FixedSingle,
                Name = "Board",
            };
            this.Controls.Add(this.gameBoard);

            this.cells = new PictureBox[InputForm.Length, InputForm.Length];
            this.matrixOfCells = new int[InputForm.Length, InputForm.Length];

            int numberOfCells = InputForm.Length * InputForm.Length;
            int numberOfRandomCells = (InputForm.Percentage * numberOfCells) / 100;

            this.randomNumbers = new List<int>();

            while (numberOfRandomCells > 0)
            {
                int temp = this.rnd.Next(0, numberOfCells + 1);
                if (!this.randomNumbers.Contains(temp))
                {
                    this.randomNumbers.Add(temp);
                    numberOfRandomCells--;
                }
            }

            int m = 1;
            for (int i = 0; i < InputForm.Length; i++)
            {
                for (int j = 0; j < InputForm.Length; j++)
                {
                    this.matrixOfCells[i, j] = this.randomNumbers.Contains(m) ? 1 : 0;
                    m++;
                }
            }

            for (int i = 0; i < InputForm.Length; i++)
            {
                for (int j = 0; j < InputForm.Length; j++)
                {
                    this.cells[i, j] = new PictureBox
                    {
                        Location = new Point(j * 50, i * 50),
                        BackColor = Color.Black,
                        Size = new Size(50, 50),
                        Visible = true,
                        Name = $"Board {i}, {j}",
                    };

                    this.gameBoard.Controls.Add(this.cells[i, j]);
                }
            }

            for (int i = 0; i < InputForm.Length; i++)
            {
                for (int j = 0; j < InputForm.Length; j++)
                {
                    this.cells[i, j].Visible = Convert.ToBoolean(this.matrixOfCells[i, j]);
                }
            }

            this.cycleNumber = new Label
            {
                Location = new Point(300, 17),
                Size = new Size(250, 25),
                Visible = true,
                Font = this.standard,
                Text = "Start",
            };
            this.Controls.Add(this.cycleNumber);

            this.gameBoard.Invalidate();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.counter++;

            this.cycleNumber.Text = $"Cycle no. {this.counter}.";
            this.ExecuteOneCycle();

            if (this.counter >= InputForm.Cycle)
            {
                this.mainTimer.Stop();
                this.cycleNumber.Text += " The End.";
                return;
            }
        }

        private void ExecuteOneCycle()
        {
            int[,] matrixOfCellsTemp = new int[InputForm.Length, InputForm.Length];
            for (int i = 0; i < InputForm.Length; i++)
            {
                for (int j = 0; j < InputForm.Length; j++)
                {
                    int aliveNeighbours = this.Compute(i, j);

                    if (this.matrixOfCells[i, j] == 0 && aliveNeighbours == 3)
                    {
                        matrixOfCellsTemp[i, j] = 1;
                    }
                    else if (this.matrixOfCells[i, j] == 1 && (aliveNeighbours == 2 || aliveNeighbours == 3))
                    {
                        matrixOfCellsTemp[i, j] = 1;
                    }
                    else
                    {
                        matrixOfCellsTemp[i, j] = 0;
                    }
                }
            }

            for (int i = 0; i < InputForm.Length; i++)
            {
                for (int j = 0; j < InputForm.Length; j++)
                {
                    this.matrixOfCells[i, j] = matrixOfCellsTemp[i, j];
                }
            }

            for (int i = 0; i < InputForm.Length; i++)
            {
                for (int j = 0; j < InputForm.Length; j++)
                {
                    this.cells[i, j].Visible = Convert.ToBoolean(this.matrixOfCells[i, j]);
                }
            }

            this.gameBoard.Invalidate();
        }

        private int Compute(int row, int column)
        {
            int alive = 0;
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = column - 1; j <= column + 1; j++)
                {
                    if (!(row == i && column == j) && i >= 0 && i < InputForm.Length && j >= 0 && j < InputForm.Length)
                    {
                        if (this.matrixOfCells[i, j] == 1)
                        {
                            alive++;
                        }
                    }
                }
            }

            return alive;
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            if (this.gameBoard != null)
            {
                this.gameBoard.Dispose();
                this.cycleNumber.Dispose();
                this.randomNumbers.Clear();
            }
            else
            {
                MessageBox.Show("There is nothing to reset.");
                return;
            }

            this.CreateBoard();

            this.mainTimer.Stop();
            this.mainTimer.Start();
            this.counter = 0;
        }
    }
}
