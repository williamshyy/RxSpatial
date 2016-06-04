using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace Performance
{
    public partial class Form1 : Form
    {
        private Microsoft.Office.Interop.Excel.Application xlApp;
        private Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
        private Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        private void transparentToolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void GetDataButton_Click_1(object sender, EventArgs e)
        {
            xlApp = new Microsoft.Office.Interop.Excel.Application();

           // xlApp.Visible = true;
            if (xlApp == null)
                MessageBox.Show("1");
            xlWorkBook = xlApp.Workbooks.Open("C:/Users/willi_000/Downloads/Summary.xlsx", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            if (xlWorkBook == null)
                MessageBox.Show("2");
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            if (xlWorkSheet == null)
                MessageBox.Show("3");
            //MessageBox.Show(xlWorkSheet.get_Range("A1", "A1").Value2.ToString());
            //Chart xlChart
            // Now create the chart.
           /* ChartObjects chartObjs = (ChartObjects)xlWorkSheet .ChartObjects(Type.Missing);
            ChartObject chartObj = chartObjs.Add(100, 20, 300, 300);
            Chart xlChart = chartObj.Chart;*/
            ChartObjects chartObjs = (ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);
            ChartObject chartObj = chartObjs.Add(100, 20, 300, 300);
            Chart xlChart = chartObj.Chart;
            int[,] v1 = { { 1, 2, 5, 10 }, { 2, 4, 10, 20 }, { 3, 6, 15, 30 } };
            Range rg;
            rg = xlWorkSheet.get_Range("A2", "D4");
            rg.Value2 = v1;

            xlChart.ChartType = XlChartType.xlXYScatterSmoothNoMarkers;
            xlChart.SetSourceData(rg, Type.Missing); 
          /*  int nRows = 25;
            int nColumns = 25;
            string upperLeftCell = "B3";
            int endRowNumber = System.Int32.Parse(upperLeftCell.Substring(1))
                + nRows - 1;
            char endColumnLetter = System.Convert.ToChar(
                Convert.ToInt32(upperLeftCell[0]) + nColumns - 1);
            string upperRightCell = System.String.Format("{0}{1}",
                endColumnLetter, System.Int32.Parse(upperLeftCell.Substring(1)));
            string lowerRightCell = System.String.Format("{0}{1}",
                endColumnLetter, endRowNumber);

            // Send single dimensional array to Excel:
            Range rg1 = xlWorkSheet.get_Range("B2", "B5");
            double[] xarray = new double[nColumns];
            xlWorkSheet.Cells[1, 1] = "Data for surface chart";
            for (int i = 0; i < xarray.Length; i++)
            {
                xarray[i] = -3.0f + i * 0.25f;
                xlWorkSheet.Cells[i + 3, 1] = xarray[i];
                xlWorkSheet.Cells[2, 2 + i] = xarray[i];
            }

            Range rg = xlWorkSheet.get_Range(upperLeftCell, lowerRightCell);
            rg.Value2 = AddData(nRows, nColumns);

            Range chartRange = xlWorkSheet.get_Range("A2", lowerRightCell);
            xlChart.SetSourceData(chartRange, Type.Missing);
            xlChart.ChartType = XlChartType.xlLine;

            // Customize axes:
            Axis xAxis = (Axis)xlChart.Axes(XlAxisType.xlCategory,
                XlAxisGroup.xlPrimary);
            xAxis.HasTitle = true;
            xAxis.AxisTitle.Text = "X Axis";

            Axis yAxis = (Axis)xlChart.Axes(XlAxisType.xlSeriesAxis,
                XlAxisGroup.xlPrimary);
            yAxis.HasTitle = true;
            yAxis.AxisTitle.Text = "Y Axis";

            // Add title:
            xlChart.HasTitle = true;
            xlChart.ChartTitle.Text = "Peak Function";

            // Remove legend:
            xlChart.HasLegend = false;*/
            xlChart.Export("C:/Users/willi_000/Downloads/sample.png", "PNG", false);
            PictureBox pb1 = new PictureBox();
            pb1.Image = Image.FromFile("C:/Users/willi_000/Downloads/sample.png");
            pb1.Location = new System.Drawing.Point(100, 100);
            pb1.Size = new Size(500, 500);
            this.Controls.Add(pb1);
        }

        private double[,] AddData(int nRows, int nColumns)
        {
            double[,] dataArray = new double[nRows, nColumns];
            double[] xarray = new double[nColumns];
            for (int i = 0; i < xarray.Length; i++)
            {
                xarray[i] = -3.0f + i * 0.25f;
            }
            double[] yarray = xarray;

            for (int i = 0; i < dataArray.GetLength(0); i++)
            {
                for (int j = 0; j < dataArray.GetLength(1); j++)
                {
                    dataArray[i, j] = 3 * Math.Pow((1 - xarray[i]), 2)
                        * Math.Exp(-xarray[i] * xarray[i] -
                        (yarray[j] + 1) * (yarray[j] + 1)) -
                        10 * (0.2 * xarray[i] - Math.Pow(xarray[i], 3) -
                        Math.Pow(yarray[j], 5)) *
                        Math.Exp(-xarray[i] * xarray[i] - yarray[j] * yarray[j])
                        - 1 / 3 * Math.Exp(-(xarray[i] + 1) * (xarray[i] + 1) -
                        yarray[j] * yarray[j]);
                }
            }
            return dataArray;
        }
    }

}
