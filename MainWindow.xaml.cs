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
            if (!double.TryParse(RnEdit.Text, out var Rn) || 
                !double.TryParse(XnEdit.Text, out var Xn) || 
                !double.TryParse(WEdit.Text, out var W) ||
                !double.TryParse(lmdEdit.Text, out var lmd))
            {
                MessageBox.Show("Некорректный ввод значений", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var Zn = new Complex(Rn, Xn);

            var LongLine = new LLine(Rn, Xn, W, lmd);
            var (z, Uz, Iz) = LongLine.Solve(1, 2);


            var UzGraph = new LineSeries();
            var IzGraph = new LineSeries();

            for (int i = 0; i < z.Length; i++)
            {
                UzGraph.Points.Add(new DataPoint(z[i], Uz[i].Magnitude));
                IzGraph.Points.Add(new DataPoint(z[i], Iz[i].Magnitude * 1000));
            }

            UzGraph.Color = OxyColors.Red;
            UzGraph.StrokeThickness = 2;

            var Uz_model = new PlotModel();
            Uz_model.Title = "U(z)";
            Uz_model.Series.Add(UzGraph);
            Uz_model.Axes.Add(new LinearAxis
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
            Uz_model.Axes.Add(new LinearAxis
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

            PlotUz.Model = Uz_model;


            var Iz_model = new PlotModel();
            Iz_model.Title = "I(z)";
            Iz_model.Series.Add(IzGraph);
            Iz_model.Axes.Add(new LinearAxis
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
            Iz_model.Axes.Add(new LinearAxis
            {
                Title = "|I(z)|, мА",
                Position = AxisPosition.Left,
                Minimum = 0,
                Maximum = 1.2 * Iz.Max(u => u.Magnitude) * 1000,
                MajorGridlineThickness = 1,
                MinorGridlineThickness = 0.5,
                MajorGridlineColor = OxyColors.Green,
                MinorGridlineColor = OxyColors.Green,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dash,
            });

            PlotIz.Model = Iz_model;


        }
    }
}
