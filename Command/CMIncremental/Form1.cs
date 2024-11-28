using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CMIncremental
{
    public partial class Form1 : Form
    {
        List<IShape> items = new List<IShape>();
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
            new Command(() => this.items = new List<IShape>()).Execute();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (items.Find((i) => i.IsMyPoint(e.X, e.Y)) == null)
            {
                int r = rnd.Next(20, 50);
                new Command(() => this.items.Add(new Circle(e.X, e.Y, r))).Execute();
            }
            else
            {
                new Command(() => this.items.Remove(this.items.Find((i) => i.IsMyPoint(e.X, e.Y)))).Execute();
            }
            Refresh();
        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            new Command(() => this.items.Clear()).Execute();
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var i in items)
                i.Draw(e.Graphics);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CM.Instance.Undo();
            Refresh();
        }
    }
}
