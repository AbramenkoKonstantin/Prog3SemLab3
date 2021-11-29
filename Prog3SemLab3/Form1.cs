using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Symbolics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Double.Solvers;
using Microsoft.Office.Interop.Excel;
using System.Linq;
using System.IO;
using Microsoft.VisualBasic;

namespace Prog3SemLab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        async private void LaunchBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Point[] points = new Point[0];
                for (int index = 0; index < xyPointsGridView.RowCount - 1; ++index)
                {
                    double xCellValue = double.Parse(xyPointsGridView.Rows[index].Cells[0].Value.ToString());
                    double yCellValue = double.Parse(xyPointsGridView.Rows[index].Cells[1].Value.ToString());
                    points = points.Append(new Point(xCellValue, yCellValue)).ToArray();
                }

                double minX = points[0].X;
                double maxX = points[0].X;
                foreach (Point point in points)
                {
                    minX = point.X < minX ? point.X : minX;
                    maxX = point.X > maxX ? point.X : maxX;
                }

                PointsDraw(points);
                await Task.Run(() =>
                {
                    LineDraw(LinearCoefsFinding(points), minX, maxX);
                    CurveDraw(QuadraticCoefsFinding(points), minX, maxX);
                });
            }
            catch
            {

            }
        }


        private double minX(double[] xValue)
        {
            double minX = xValue[0];
            for (int point = 0; point < xValue.Length; ++point)
            {
                if (xValue[point] < minX)
                {
                    minX = xValue[point];
                }
            }
            return minX;
        }


        private double maxX(double[] xValue)
        {
            double maxX = xValue[0];
            for (int point = 0; point < xValue.Length; ++point)
            {
                if (xValue[point] > maxX)
                {
                    maxX = xValue[point];
                }
            }
            return maxX;
        }

        private void PointsDraw(Point[] points)
        {
            var pointsSeria = chart.Series[0];
            pointsSeria.Points.Clear();
            for (int index = 0; index < points.Length; ++index)
            {
                pointsSeria.Points.AddXY(points[index].X, points[index].Y);
            }
        }

        private void LineDraw(Vector<double> coefsVector, double lowerBorder, double upperBorder)
        {
            var lineSeria = chart.Series[1];
            Invoke((System.Action)(() => lineSeria.Points.Clear()));

            string linearFuncString = coefsVector[0].ToString() + "*x" + "+" + coefsVector[1].ToString();
            Expression func = Infix.ParseOrThrow(linearFuncString.Replace(',', '.'));

            double x = lowerBorder;
            double step = (upperBorder - lowerBorder) / 500;

            while (x < upperBorder)
            {
                Invoke((System.Action)(() => lineSeria.Points.AddXY(x, FuncValue(x, func))));
                x += step;
            }
        }


        private void CurveDraw(Vector<double> coefsVector, double lowerBorder, double upperBorder)
        {
            var curveSeria = chart.Series[2];
            Invoke((System.Action)(() => curveSeria.Points.Clear()));

            string funcString = coefsVector[0].ToString() + "*x^2+" + coefsVector[1].ToString() + "*x" + "+" + coefsVector[2].ToString();
            Expression func = Infix.ParseOrThrow(funcString.Replace(',', '.'));

            double x = lowerBorder;
            double step = (upperBorder - lowerBorder) / 500;

            while (x < upperBorder)
            {
                Invoke((System.Action)(() => curveSeria.Points.AddXY(x, FuncValue(x, func))));
                x += step;
            }
        }


        private double FuncValue(double point, Expression func)
        {
            Dictionary<string, FloatingPoint> symbol = new Dictionary<string, FloatingPoint>()
            {
                { "x", point }
            };
            return Evaluate.Evaluate(symbol, func).RealValue;
        }


        private Vector<double> LinearCoefsFinding(Point[] points)
        {
            Matrix coefMatrix = DenseMatrix.OfArray(new double[2, 2]
            {
                {0, 0},
                {0, 0}
            });
            Vector<double> answersVector = Vector<double>.Build.Dense(new double[2] { 0, 0 });

            for (int index = 0; index < points.Length; ++index)
            {
                answersVector[0] += points[index].X * points[index].Y;
                answersVector[1] += points[index].Y;
                coefMatrix[0, 0] += Math.Pow(points[index].X, 2);
                coefMatrix[0, 1] = coefMatrix[1, 0] += points[index].X;
            }
            coefMatrix[1, 1] = points.Length;

            return coefMatrix.SolveIterative(answersVector, new MlkBiCgStab());
        }

        private Vector<double> QuadraticCoefsFinding(Point[] points)
        {
            Matrix coefMatrix = DenseMatrix.OfArray(new double[3, 3]
{
                { 0, 0, 0 },
                { 0, 0, 0 },
                { 0, 0, 0 }
});
            Vector<double> answersVector = Vector<double>.Build.Dense(new double[3] { 0, 0, 0 });

            for (int index = 0; index < points.Length; ++index)
            {
                answersVector[0] += Math.Pow(points[index].X, 2) * points[index].Y;
                answersVector[1] += points[index].X * points[index].Y;
                answersVector[2] += points[index].Y;
                coefMatrix[0, 0] += Math.Pow(points[index].X, 4);
                coefMatrix[0, 1] = coefMatrix[1, 0] += Math.Pow(points[index].X, 3);
                coefMatrix[0, 2] = coefMatrix[1, 1] = coefMatrix[2, 0] += Math.Pow(points[index].X, 2);
                coefMatrix[1, 2] = coefMatrix[2, 1] += points[index].X;
            }
            coefMatrix[2, 2] = points.Length;

            return coefMatrix.SolveIterative(answersVector, new MlkBiCgStab());
        }


        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void excel_Click(object sender, EventArgs e)
        {
            xyPointsGridView.Rows.Clear();
            using (OpenFileDialog excelFile = new OpenFileDialog())
            {
                DialogResult result = excelFile.ShowDialog();
                if (result == DialogResult.OK && Path.GetExtension(excelFile.FileName) == ".xlsx")
                {
                    Microsoft.Office.Interop.Excel.Application objExcel = new Microsoft.Office.Interop.Excel.Application();
                    Workbook objWorkBook = objExcel.Workbooks.Open(excelFile.FileName);
                    Worksheet objWorkSheet = (Worksheet)objWorkBook.Sheets[1];

                    string[] xColumn = ReadColumn(objWorkSheet, 1);
                    string[] yColumn = ReadColumn(objWorkSheet, 2);

                    for (int index = 0; index < xColumn.Length; ++index)
                    {
                        xyPointsGridView.Rows.Add();
                        var xCell = xyPointsGridView.Rows[index].Cells[0];
                        var yCell = xyPointsGridView.Rows[index].Cells[1];
                        xCell.Value = xColumn[index];
                        yCell.Value = yColumn[index];
                    }

                    objWorkBook.Close();
                    objExcel.Quit();
                }
                else if (result == DialogResult.OK)
                {
                    MessageBox.Show("Ошибка!", "Неверное расширение файла.");
                }
            }
        }

        private string[] ReadColumn(Worksheet objWorkSheet, int columnIndex)
        {
            Range range = objWorkSheet.UsedRange.Columns[columnIndex];
            Array cellsValues = (Array)range.Cells.Value2;
            return cellsValues.OfType<object>().Select(o => o.ToString()).ToArray();
        }
    }
}