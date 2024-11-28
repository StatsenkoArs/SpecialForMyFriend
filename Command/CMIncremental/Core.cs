using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CMIncremental
{
    public interface IShape
    {
        void Draw(Graphics g);

        bool IsMyPoint(int x, int y);
    }

    public class Circle : IShape
    {
        int x, y, r;

        public Circle(int x, int y, int r) => (this.x, this.y, this.r) = (x, y, r);

        public void Draw(Graphics g)
        {
            g.FillEllipse(Brushes.Red, x - r / 2, y - r / 2, r, r);
            g.DrawEllipse(Pens.Black, x - r / 2, y - r / 2, r, r);
        }

        public bool IsMyPoint(int px, int py)
        {
            return (x - px) * (x - px) + (y - py) * (y - py) < r * r;
        }
    }
}
