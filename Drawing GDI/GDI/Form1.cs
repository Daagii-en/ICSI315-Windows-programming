namespace GDI
{
    public partial class Form1 : Form
    {
        Pen GridPen;
        Pen AxesPen;
        List<PointF> pointFs = new List<PointF>();
        public Form1()
        {
            InitializeComponent();
            GridPen = new Pen(Color.LightGray, 1.8F);
            AxesPen = new Pen(Color.Black, 2.0F);
            this.DoubleBuffered = true;
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.DrawLine(MaroonPen, new Point(00, 00), new Point(panel1.Height, panel1.Width));
            /*for (int i = panel1.Width / 20; i < panel1.Width; i += panel1.Width / 20)
            {
                e.Graphics.DrawLine(GridPen, new Point(i, 0), new Point(i, panel1.Height));
            }*/
            for (int i = 0; i <= panel1.Width; i += panel1.Width / 20)
            {
                e.Graphics.DrawLine(GridPen, new Point(i, 0), new Point(i, panel1.Height));
            }
            for (int i = 0; i <= panel1.Height; i += panel1.Height / 20)
            {
                e.Graphics.DrawLine(GridPen, new Point(0, i), new Point(panel1.Width, i));
            }
            e.Graphics.DrawLine(AxesPen, new Point(panel1.Width / 2, 0), new Point(panel1.Width / 2, panel1.Height));
            e.Graphics.DrawLine(AxesPen, new Point(0, panel1.Height / 2), new Point(panel1.Width, panel1.Height / 2));
            if (textBox2.Text == "" || textBox3.Text == "")
            {
                return;
            }
            //
            Graphics g = this.panel1.CreateGraphics();
            float a = float.Parse(textBox2.Text);
            float x = float.Parse(textBox3.Text);
            float realx = 0;
            for (float i = 0; i < panel1.Width; i = i + (float)(0.1))
            {
                float y = a * realx * realx;
                //
                g.FillRectangle(Brushes.Red, panel1.Width / 2 + realx, panel1.Height / 2 - y / (panel1.Height / 20), 1, 1);
                g.FillRectangle(Brushes.Red, panel1.Width / 2 - realx, panel1.Height / 2 - y / (panel1.Height / 20), 1, 1);
                realx += (float)0.1;
                if (panel1.Height / 2 + a * x * x > panel1.Height) { return; }
                if (panel1.Width / 2 + x > panel1.Width) { return; }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Refresh();
        }
    }
}