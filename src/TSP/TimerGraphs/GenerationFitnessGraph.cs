using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace TSP.TimerGraphs
{
    public partial class GenerationFitnessGraph : Form
    {
        public ToolStripMenuItem timerGraphToolStripMenuItem;
        GraphPane myPane;
        public PointPairList[] PPlist;

        public GenerationFitnessGraph()
        {
            InitializeComponent();
            //
            //
            myPane = zgc.GraphPane;
            // Fill the axis background with a color gradient
            myPane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45F);

            // Fill the pane background with a color gradient
            myPane.Fill = new Fill(Color.White, Color.FromArgb(220, 220, 255), 45F);
            // Set the titles and axis labels
            myPane.Title.Text = "TSP Generation-Fitness Chart Line";
            myPane.XAxis.Title.Text = "Generation (Number reproduction of population)";
            myPane.YAxis.Title.Text = "Fitness (Cast of Elite Chromosomes) - Distance";
        }

        private void CreateGraph(ZedGraphControl zgc)
        {
            //
            // clear old coordinates
            //
            myPane.CurveList.Clear();
            zgc.AxisChange();

            // Generate a LightBlue curve with circle symbols, and "My Curve" in the legend
            LineItem CurveS = myPane.AddCurve("Series GA", PPlist[0], Color.LightBlue, SymbolType.Diamond);
            // Generate a PaleVioletRed curve with circle symbols, and "My Curve" in the legend
            LineItem CurveP = myPane.AddCurve("Parallel GA", PPlist[1], Color.PaleVioletRed, SymbolType.Circle);

            float allPointSize = 50F;
            // Fill the area under the curve with a white-red gradient at 45 degrees
            CurveS.Line.Fill = new Fill(Color.Transparent, Color.LightBlue, allPointSize);
            // Make the symbols opaque by filling them with white
            CurveS.Symbol.Fill = new Fill(Color.Transparent);


            // Fill the area under the curve with a white-red gradient at 45 degrees
            CurveP.Line.Fill = new Fill(Color.Transparent, Color.PaleVioletRed, allPointSize);
            // Make the symbols opaque by filling them with white
            CurveP.Symbol.Fill = new Fill(Color.Transparent);


            // Calculate the Axis Scale Ranges
            zgc.AxisChange();
            zgc.Refresh();
        }

        private void TimeGraph_Load(object sender, EventArgs e)
        {
            //
            CreateGraph(zgc);

            timerGraphToolStripMenuItem.Checked = true;
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            // Calculate the Axis Scale Ranges
            zgc.AxisChange();
            zgc.Refresh();
        }

        private void TimeGraph_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerGraphToolStripMenuItem.ShowDropDown();
            timerGraphToolStripMenuItem.Checked = false;
        }
    }
}