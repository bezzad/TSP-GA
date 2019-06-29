using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TSP
{
    public partial class EnterNumberRandomPointForm : Form
    {
        public EnterNumberRandomPointForm()
        {
            InitializeComponent();
        }

        public int NumberOfCities
        {
            get { return Convert.ToInt32(numCities.Value); }
        }

        public int NumberSafety
        {
            get { return tSafety.Value; }
        }

        private void tSafety_Scroll(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(tSafety, "The number of replicates for the safe: " + tSafety.Value.ToString() + " times");
        }
    }
}
