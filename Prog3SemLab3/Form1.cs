using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Symbolics;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Double.Solvers;

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
                double[] xValue = new double[xyPointsGridView.RowCount];
                double[] yValue = new double[xyPointsGridView.RowCount];
                for (int point = 0; point < xyPointsGridView.RowCount - 1; ++point)
                {
                    xValue[point] = double.Parse(xyPointsGridView[0, point].Value.ToString());
                    yValue[point] = double.Parse(xyPointsGridView[1, point].Value.ToString());
                }
                PointsDraw(xValue, yValue);
                var coefsContainer = CoefFinding(xValue, yValue);
                await Task.Run(() => LineDraw(coefsContainer, minX(xValue), maxX(xValue)));
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



        private void PointsDraw(double[] xValue, double[] yValue)
        {
            chart.Series[0].Points.Clear();
            for (int point = 0; point < xValue.Length; ++point)
            {
                chart.Series[0].Points.AddXY(xValue[point], yValue[point]);
            }
        }



        private void LineDraw(List<Vector<double>> coefs, double start, double end)
        {
            Action action = () =>
            {
                chart.Series[1].Points.Clear();
                chart.Series[2].Points.Clear();
            };
            Invoke(action);
            string linearFuncString = "x*" + coefs[0][0].ToString() + "+" + coefs[0][1].ToString();
            string sqrFuncString = "x^2*" + coefs[1][0].ToString() + "+x*" + coefs[1][1].ToString() + "+" + coefs[1][2].ToString();
            Expression linearFun = Infix.ParseOrThrow(linearFuncString.Replace(',', '.'));
            Expression sqrFunc = Infix.ParseOrThrow(sqrFuncString.Replace(',', '.'));

            double step = (end - start) / 1000;
            while (start < end)
            {
                action = () => {
                    chart.Series[1].Points.AddXY(start, FuncValue(start, linearFun));
                    chart.Series[2].Points.AddXY(start, FuncValue(start, sqrFunc));
                };
                Invoke(action);
                start += step;
            }
        }



        private double FuncValue(double point, Expression func)
        {
            Dictionary<string, FloatingPoint> symbol = new Dictionary<string, FloatingPoint>()
            {
                { "x", point }
            };
            return MathNet.Symbolics.Evaluate.Evaluate(symbol, func).RealValue;
        }



        private List<Vector<double>> CoefFinding(double[] xValue, double[] yValue)
        {
            double sumY = 0;
            double sumX = 0;
            double sumSqrX = 0;
            double sumThrX = 0;
            double sumQuadX = 0;
            double sumXY = 0;
            double sumSqrXY = 0;

            for (int point = 0; point < xValue.Length; ++point)
            {
                sumY += yValue[point];
                sumX += xValue[point];
                sumSqrX += Math.Pow(xValue[point], 2);
                sumThrX += Math.Pow(xValue[point], 3);
                sumQuadX += Math.Pow(xValue[point], 4);
                sumXY += xValue[point] * yValue[point];
                sumSqrXY += Math.Pow(xValue[point], 2) * yValue[point];
            }

            Matrix<double> matrix = DenseMatrix.OfArray(new double[,] {
                {sumQuadX, sumThrX, sumSqrX},
                {sumThrX, sumSqrX, sumX},
                {sumSqrX, sumX, xValue.Length - 1}});
            Vector<double> answers = Vector<double>.Build.Dense(new double[] { sumSqrXY, sumXY, sumY });

            Vector<double> sqrCoefs = matrix.SolveIterative(answers, new MlkBiCgStab());
            Vector<double> linearCoefs = matrix.SubMatrix(1, 2, 1, 2).SolveIterative(answers.SubVector(1, 2), new MlkBiCgStab());

            List<Vector<double>> coefsContainer = new List<Vector<double>> { linearCoefs, sqrCoefs };
            return coefsContainer;
        }
        

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
