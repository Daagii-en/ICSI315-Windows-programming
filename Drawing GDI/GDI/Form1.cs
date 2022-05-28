namespace GDI
{
    public partial class Form1 : Form
    {
        //GridPen and AxesPen are used to draw lines and axes.
        Pen GridPen;
        Pen AxesPen;
        public Form1()
        {
            InitializeComponent();
            //GridPen and AxesPen are as Pen class objects with Pen.Color and Pen.Width.
            GridPen = new Pen(Color.LightGray, 1.8F);
            AxesPen = new Pen(Color.Black, 2.0F);
            //this.DoubleBuffered = true;
        }
        /// <summary>
        /// Draw line and graphic on the panel1_Paint object 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //this loop draws lines along the horizontal axis of the coordinates
            for (int i = 0; i <= panel1.Width; i += panel1.Width / 20)
            {
                e.Graphics.DrawLine(GridPen, new Point(i, 0), new Point(i, panel1.Height));
            }
            //this loop draws lines along the vertical axis of the coordinates
            for (int i = 0; i <= panel1.Height; i += panel1.Height / 20)
            {
                e.Graphics.DrawLine(GridPen, new Point(0, i), new Point(panel1.Width, i));
            }
            //draws a line along the main axis of the coordinates
            e.Graphics.DrawLine(AxesPen, new Point(panel1.Width / 2, 0), new Point(panel1.Width / 2, panel1.Height));
            e.Graphics.DrawLine(AxesPen, new Point(0, panel1.Height / 2), new Point(panel1.Width, panel1.Height / 2));
            //if a and x are not set, return empty
            if (textBox2.Text == "" || textBox3.Text == "")
            {
                return;
            }
            // Create a g object from the Graphics class
            Graphics g = this.panel1.CreateGraphics();
            Graphics g2 = this.panel1.CreateGraphics();
            // Convert the number a and x entered manually into a float
            float a = float.Parse(textBox2.Text);
            float x = float.Parse(textBox3.Text);
            //pointX draws a graphic with an accuracy of 0.1
            float pointX = 0;
            //This loop is to find the value of y by the values of a and x and draw a graph.
            for (float i = 0; i < panel1.Width; i = i + (float)(0.1))
            {
                float y = a * pointX * pointX;
                float y2 =  pointX;
                //Use a brush to create g objects ( x, y, width, and height.)
                g.FillRectangle(Brushes.Red, panel1.Width / 2 + pointX, panel1.Height / 2 - y / (panel1.Height / 20), 1, 1);
                g.FillRectangle(Brushes.Red, panel1.Width / 2 - pointX, panel1.Height / 2 - y / (panel1.Height / 20), 1, 1);

                g2.FillRectangle(Brushes.GreenYellow, panel1.Width / 2 + pointX, panel1.Height / 2 - y2, 1, 1);
                g2.FillRectangle(Brushes.GreenYellow, panel1.Width / 2 - pointX, panel1.Height / 2 + y2, 1, 1);
                pointX += (float)0.1;
                //if (panel1.Height / 2 + a * x * x > panel1.Height) { return; }
                //if (panel1.Width / 2 + x > panel1.Width) { return; }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Refresh();
        }
    }
}