using System;
using System.Windows.Forms;

namespace The_Game_Of_Life
{
    public partial class InputForm : Form
    {
        public InputForm()
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
                bool startPercentageErrorParse = int.TryParse(StartPercentage.Text, out int parsedStartPercentage);
                bool boardLengthErrorParse = int.TryParse(BoardLength.Text, out int parsedBoardLength);
                bool numberOfCyclesErrorParse = int.TryParse(NumberOfCycles.Text, out int parsedNumberOfCycles);

                if (!startPercentageErrorParse || !boardLengthErrorParse || !numberOfCyclesErrorParse)
                {
                    MessageBox.Show("All fields must be integers");
                    return;
                }

                if (parsedStartPercentage <= 0 || parsedStartPercentage > 100)
                {
                    MessageBox.Show("Percentage must be between 0 and 100");
                    return;
                }

                if (parsedBoardLength <= 0 || parsedBoardLength > 10)
                {
                    MessageBox.Show("Length must be between 0 and 10");
                    return;
                }

                if (parsedNumberOfCycles <= 0 || parsedNumberOfCycles > 100)
                {
                    MessageBox.Show("Number of cycles must be between 0 and 100");
                    return;
                }

                Length = parsedBoardLength;
                Percentage = parsedStartPercentage;
                Cycle = parsedNumberOfCycles;

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
