using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace The_Game_Of_Life
{
    public partial class MainForm : Form
    {
        private Panel GameBoard;
        private int Counter = 0;
        private readonly Random rnd = new Random();
        private Timer MainTimer;
        private PictureBox[,] Cells;
        private int[,] MatrixOfCells;
        private Label CycleNumber = new Label();
        private Font Standard = new Font("Arial", 16, FontStyle.Bold);
        private List<int> RandomNumbers;

        public MainForm()
        {
            InitializeComponent();
        }

        private void New_game_Click(object sender, EventArgs e)
        {
            using (InputForm form2 = new InputForm())
            {
                DialogResult dr = form2.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    if (GameBoard != null)
                    {
                        GameBoard.Dispose();
                        CycleNumber.Dispose();
                        RandomNumbers.Clear();
                    }

                    Board();

                    MainTimer = new Timer();
                    MainTimer.Interval = 1000;
                    MainTimer.Tick += Timer_Tick;

                    Counter = 0;
                    MainTimer.Start();
                }
            }
        }

        private void Board()
        {
            GameBoard = new Panel
            {
                Location = new Point(15, 55),
                BackColor = Color.White,
                Size = new Size(InputForm.Length * 50, InputForm.Length * 50),
                BorderStyle = BorderStyle.FixedSingle,
                Name = "Board"
            };
            Controls.Add(GameBoard);

            Cells = new PictureBox[InputForm.Length, InputForm.Length];
            MatrixOfCells = new int[InputForm.Length, InputForm.Length];

            int numberOfCells = InputForm.Length * InputForm.Length;
            int numberOfRandomCells = (InputForm.Percentage * numberOfCells) / 100;

            RandomNumbers = new List<int>();

            while (numberOfRandomCells > 0)
            {
                int temp = rnd.Next(0, numberOfCells + 1);
                if (!RandomNumbers.Contains(temp))
                {
                    RandomNumbers.Add(temp);
                    numberOfRandomCells--;
                }
            }

            int m = 1;
            for (int i = 0; i < InputForm.Length; i++)
            {
                for (int j = 0; j < InputForm.Length; j++)
                {
                    MatrixOfCells[i, j] = RandomNumbers.Contains(m) ? 1 : 0;
                    m++;
                }
            }

            for (int i = 0; i < InputForm.Length; i++)
            {
                for (int j = 0; j < InputForm.Length; j++)
                {
                    Cells[i, j] = new PictureBox
                    {
                        Location = new Point(j * 50, i * 50),
                        BackColor = Color.Black,
                        Size = new Size(50, 50),
                        Visible = true,
                        Name = $"Board {i}, {j}"
                    };

                    GameBoard.Controls.Add(Cells[i, j]);
                }
            }

            for (int i = 0; i < InputForm.Length; i++)
            {
                for (int j = 0; j < InputForm.Length; j++)
                {
                    Cells[i, j].Visible = Convert.ToBoolean(MatrixOfCells[i, j]);
                }
            }

            CycleNumber = new Label
            {
                Location = new Point(300, 17),
                Size = new Size(250, 25),
                Visible = true,
                Font = Standard,
                Text = "Start"
            };
            Controls.Add(CycleNumber);

            GameBoard.Invalidate();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Counter++;

            CycleNumber.Text = $"Cycle no. {Counter}.";
            OneCycle();

            if (Counter >= InputForm.Cycle)
            {
                MainTimer.Stop();
                CycleNumber.Text += " The End.";
                return;
            }
        }

        private void OneCycle()
        {
            int[,] matrixOfCellsTemp = new int[InputForm.Length, InputForm.Length];
            for (int i = 0; i < InputForm.Length; i++)
            {
                for (int j = 0; j < InputForm.Length; j++)
                {
                    int aliveNeighbours = Compute(i, j);

                    if (MatrixOfCells[i, j] == 0 && aliveNeighbours == 3)
                    {
                        matrixOfCellsTemp[i, j] = 1;
                    }
                    else if (MatrixOfCells[i, j] == 1 && (aliveNeighbours == 2 || aliveNeighbours == 3))
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
                    MatrixOfCells[i, j] = matrixOfCellsTemp[i, j];
                }
            }

            for (int i = 0; i < InputForm.Length; i++)
            {
                for (int j = 0; j < InputForm.Length; j++)
                {
                    Cells[i, j].Visible = Convert.ToBoolean(MatrixOfCells[i, j]);
                }
            }

            GameBoard.Invalidate();
        }

        private int Compute(int Row, int Column)
        {
            int alive = 0;
            for (int i = Row - 1; i <= Row + 1; i++)
            {
                for (int j = Column - 1; j <= Column + 1; j++)
                {
                    if (!(Row == i && Column == j) && i >= 0 && i < InputForm.Length && j >= 0 && j < InputForm.Length)
                    {
                        if (MatrixOfCells[i, j] == 1)
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
            if (GameBoard != null)
            {
                GameBoard.Dispose();
                CycleNumber.Dispose();
                RandomNumbers.Clear();
            }
            else
            {
                MessageBox.Show("There is nothing to reset.");
                return;
            }

            Board();

            MainTimer.Stop();
            MainTimer.Start();
            Counter = 0;
        }
    }
}
