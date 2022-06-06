using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lab
{
    public partial class Lab : Form
    {
        List<Point> points = new List<Point>();
        Graham graham;

        public Lab()
        {
            InitializeComponent();
            chart();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
      
        private void chart()
        {
            chart_points.Series.Clear();
            chart_points.ChartAreas.Clear();

            ChartArea area = new ChartArea("Area");
            Series border = new Series("Border");
            Series triangle = new Series("Triangle");
            Series points = new Series("Points");

            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.Enabled = false;

            border.ChartArea = "Area";
            border.ChartType = SeriesChartType.Line;
            border.BorderWidth = 3;
            border.Color = Color.Black;

            triangle.ChartArea = "Area";
            triangle.ChartType = SeriesChartType.Line; ;
            triangle.BorderWidth = 3;
            triangle.Color = Color.Blue;

            points.ChartArea = "Area";
            points.ChartType = SeriesChartType.Point;
            points.MarkerStyle = MarkerStyle.Circle;
            points.MarkerColor = Color.Red;

            chart_points.ChartAreas.Add(area);
            chart_points.Series.Add(border);
            chart_points.Series.Add(triangle);
            chart_points.Series.Add(points);
        }
    
        private void create_points(int a, int b, int count)
        {
            points.Clear();
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                double x = random.NextDouble();
                double y = random.NextDouble();
                Point point = new Point();
                point.x = (b - a) * x + a;
                point.y = (b - a) * y + a;
                points.Add(point);
            }
        }
    
        private void show_points()
        {
            foreach (Point point in points)
            {
                chart_points.Series[2].Points.AddXY(point.x, point.y);
                chart_points.Series[2].Points[chart_points.Series[2].Points.Count - 1].Label
                    = chart_points.Series[2].Points.Count.ToString();
                chart_points.Series[2].Points[chart_points.Series[2].Points.Count - 1].MarkerSize = 9;
            }
        }
     
        private void show_border()
        {
            List<Point> border = graham.border;
            foreach (Point point in border)
            {
                chart_points.Series[0].Points.AddXY(point.x, point.y);
            }
        }
       
        private void show_triangle()
        {
            chart_points.Series[1].Points.Clear();
            List<Point> triangle = Triangle.triangle(graham.border);
            foreach(Point point in triangle)
            {
                chart_points.Series[1].Points.AddXY(point.x, point.y);
            }
        }
     
        private void create()
        {
            chart();

            graham = new Graham(points);
            if (points.Count > 2)
                points = graham.initial_points;

            show_triangle();
            show_points();
            show_border();
        }
       
        private void buttonCreate_Click(object sender, EventArgs e)
        {
            int count = (int)number.Value;
            int a = 0; int b = count * count;
            create_points(a, b, count);
            create();
        }
    
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            chart_points.Series[0].Points.Clear();

            double x = (double)X.Value;
            double y = (double)Y.Value;
            points.Add(new Point { x = x, y = y });

            chart_points.Series[2].Points.AddXY(x, y);
            chart_points.Series[2].Points[chart_points.Series[2].Points.Count - 1].Label
                = chart_points.Series[2].Points.Count.ToString();
            chart_points.Series[2].Points[chart_points.Series[2].Points.Count - 1].MarkerSize = 8;

            graham = new Graham(points);
            if (points.Count > 2)
                points = graham.initial_points;
            show_triangle();
            show_border();
        }
       
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int number = (int)numer_of_point.Value;
            if (points.Count >= number && 0<number)
            {
                points.RemoveAt(number - 1);
                create();
            }
            else
                MessageBox.Show("There’s no point number " + number);

            create();
        }

        
    }
}
