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
        private Random rnd = new Random();
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
                    MainTimer.Tick += new EventHandler(Timer_Tick);

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


            int NumberOfCells = InputForm.Length * InputForm.Length;
            int NumberOfRandomCells = (InputForm.Percentage * NumberOfCells) / 100;

            RandomNumbers = new List<int>();

            int Temp = 0;
            while (NumberOfRandomCells > 0)
            {
                Temp = rnd.Next(0, NumberOfCells + 1);
                if (!RandomNumbers.Contains(Temp))
                {
                    RandomNumbers.Add(Temp);
                    NumberOfRandomCells--;

                }

            }

            int m = 1;
            for (int i = 0; i < InputForm.Length; i++)
            {
                for (int j = 0; j < InputForm.Length; j++)
                {
                    if (RandomNumbers.Contains(m))
                    {
                        MatrixOfCells[i, j] = 1;
                    }
                    else
                    {
                        MatrixOfCells[i, j] = 0;
                    }
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
                        Visible = true
                    };
                    Cells[i, j].Name = string.Format("Board {0}, {1}", i, j);

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
                Location = new Point(150, 17),
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

            CycleNumber.Text = "Cycle no. " + Counter + ".";
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
            int[,] MatrixOfCellsTemp = new int[InputForm.Length, InputForm.Length];
            int AliveNeighbours = 0;


            for (int i = 0; i < InputForm.Length; i++)
            {
                for (int j = 0; j < InputForm.Length; j++)
                {
                    AliveNeighbours = 0;

                    AliveNeighbours = Compute(i, j);

                    if (MatrixOfCells[i, j] == 0 && AliveNeighbours == 3)
                    {
                        MatrixOfCellsTemp[i, j] = 1;
                    }
                    else if (MatrixOfCells[i, j] == 1 && (AliveNeighbours == 2 || AliveNeighbours == 3))
                    {
                        MatrixOfCellsTemp[i, j] = 1;
                    }
                    else
                    {
                        MatrixOfCellsTemp[i, j] = 0;
                    }

                }
            }


            for (int i = 0; i < InputForm.Length; i++)
            {
                for (int j = 0; j < InputForm.Length; j++)
                {
                    MatrixOfCells[i, j] = MatrixOfCellsTemp[i, j];

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
            int Alive = 0;
            for (int i = Row - 1; i <= Row + 1; i++)
            {
                for (int j = Column - 1; j <= Column + 1; j++)
                {
                    if (!(Row == i && Column == j))
                    {
                        if (i >= 0 && i < InputForm.Length && j >= 0 && j < InputForm.Length)
                        {
                            if (MatrixOfCells[i, j] == 1)
                            {
                                Alive++;
                            }
                        }
                    }
                }
            }

            return Alive;
        }





    }
}
