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



        }

    }
}
