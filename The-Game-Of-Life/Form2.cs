using System;
using System.Windows.Forms;

namespace The_Game_Of_Life
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            BoardLength.SelectedText = "0";
            StartPercentage.SelectedText = "0";
            NumberOfCycles.SelectedText = "0";
        }

        public static int Length { get; set; }
        public static int Percentage { get; set; }
        public static int Cycle { get; set; }


        private void Button_OK_Click(object sender, EventArgs e)
        {
            try
            {
                if (BoardLength.Text == "" || StartPercentage.Text == "" || NumberOfCycles.Text == "")
                {
                    MessageBox.Show("You must provide a value for all fields.");
                    return;
                }
                else if (int.Parse(BoardLength.Text) <= 0 || int.Parse(StartPercentage.Text) <= 0 || int.Parse(NumberOfCycles.Text) <= 0)
                {
                    MessageBox.Show("Each value must be above 0.");
                    return;
                }
                else if (int.Parse(StartPercentage.Text) >= 100)
                {
                    MessageBox.Show("You cannot exceed 100%.");
                    return;
                }
                else
                {
                    Length = int.Parse(BoardLength.Text);
                    Percentage = int.Parse(StartPercentage.Text);
                    Cycle = int.Parse(NumberOfCycles.Text);

                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Input data is in wrong format.");
                return;
            }


        }

    }
}
