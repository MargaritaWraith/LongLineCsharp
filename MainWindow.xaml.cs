using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;


namespace LongLine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void OnClick(object sender, EventArgs e)
        {
            var Zn = new Complex(double.Parse(RnEdit.Text), double.Parse(XnEdit.Text));
            //MessageBox.Show($"Zн = {Zn.Real} + j{Zn.Imaginary}");

            var LongLine = new LLine(150, 0, 150, 10);
            var (z, Uz, Iz) = LongLine.Solve(1, 2);


            var UzGraph = new LineSeries();

            for (int i = 0; i < z.Length; i++)
            {
                UzGraph.Points.Add(new DataPoint(z[i], Uz[i].Magnitude));
            }

            UzGraph.Color = OxyColors.Red;
            UzGraph.StrokeThickness = 2;

            var model = new PlotModel();
            model.Title = "U(z)";
            model.Series.Add(UzGraph);
            model.Axes.Add(new LinearAxis
            {
                Title = "z, м",
                Position = AxisPosition.Bottom,
                Minimum = 0,
                Maximum = z.Max(),
                MajorGridlineThickness = 1,
                MinorGridlineThickness = 0.5,
                MajorGridlineColor = OxyColors.Green,
                MinorGridlineColor = OxyColors.Green,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dash,
            });
            model.Axes.Add(new LinearAxis
            {
                Title = "|U(z)|, В",
                Position = AxisPosition.Left,
                Minimum = 0,
                Maximum = 1.2 * Uz.Max(u => u.Magnitude),
                MajorGridlineThickness = 1,
                MinorGridlineThickness = 0.5,
                MajorGridlineColor = OxyColors.Green,
                MinorGridlineColor = OxyColors.Green,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dash,
            });

            PlotUz.Model = model;
            

        }
    }
}
