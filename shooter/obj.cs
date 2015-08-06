using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace shooter
{    
    class Obj
    {
        protected int x, y, status;
        public int d;
        internal double Ox, Oy;     
        internal double Vx, Vy;
        protected Color cl;
        protected int max_x, min_x, max_y, min_y;
        internal bool shooted;
        protected bool erased;
        public Obj()
        {           
            x = 0; y = 0;                             
            status = 3;
            Vx = 3; Vy = 2;
            shooted = false;
            erased = false;
            d = 1;
            Ox = x + d / 2;
            Oy = y + d / 2;
        }
        public void MoveNext()
        {
            if (!shooted)
            {
                x = x + (int)(Math.Round(Vx));
                y = y + (int)(Math.Round(Vy));
            }
        }
        public virtual void Draw(Graphics g)
        {            
            g.FillRectangle(Brushes.Black, x, y, 1, 1);
        }
        public virtual void Erase(Graphics g)
        {
            g.FillRectangle(Brushes.White, x, y, 1, 1);
        }
        public  void Clash(ref Obj a) 
        {
            if (!shooted && !a.shooted)
            {
                Ox = x + d / 2;
                Oy = y + d / 2;
                double h;
                h = Math.Abs(Math.Sqrt((Ox - a.Ox) * (Ox - a.Ox) + (Oy - a.Oy) * (Oy - a.Oy)));
                if (h <= (double)((d + a.d) / 2)) //проверяет ударяется ли шар с другим шаром
                {
                    Vx = -Vx;
                    Vy = -Vy;
                    a.Vx = -a.Vx;
                    a.Vy = -a.Vy;
                }
            }
        }
        internal void Repulsion(int _w, int _h)
        {
            if ((max_x >= _w && (max_y >= _h || min_y <= 0)) || (min_x <= 0 && (max_y >= _h || min_y <= 0)))
                status = 0;
            else
            {
                if (max_y >= _h || min_y <= 0)
                    status = 1;
                else
                {
                    if (max_x >= _w || min_x <= 0)
                        status = 2;
                    else
                        status = 3;
                }

            }
            if (status == 0)
            {
                Vx = (-1) * Vx;
                Vy = (-1) * Vy;
            }
            else
                if (status == 1)
                    Vy = (-1) * Vy;
                else
                    if (status == 2)
                        Vx = (-1) * Vx;
        }
        public virtual void Step (int w, int h)
        {           
            max_x = x; min_x = x;
            min_y = y; max_y = y ;
            Repulsion(w, h);
            
        }
        public  int Shooted(int m_x, int m_y)
        {
            if (!shooted)
            {
                Ox = x + d / 2;
                Oy = y + d / 2;
                double h;
                h = Math.Abs(Math.Sqrt((Ox - m_x) * (Ox - m_x) + (Oy - m_y) * (Oy - m_y)));
                if (h <= (double)(d / 2))
                {
                    shooted = true;
                    return 1;
                }
            }
            return 0;
        }        
    }
    class Obj_circle: Obj
    {
        
        public Obj_circle(): base(){}
        public Obj_circle(int _x, int _y, double _Vx, double _Vy, Color _cl, int _d)
        {
            d = _d ;
            x = _x; y = _y;
            Vx = _Vx; Vy = _Vy;            
            cl = _cl;
            Ox = x +d / 2;
            Oy = y +d / 2;        
            status = 3;
            shooted = false;
            erased = false;
        }
        public override void Draw(Graphics g)
        {
            if (!shooted)
            {                
                Pen blackPen = new Pen(cl, 3);
                Rectangle rect = new Rectangle(x, y, d, d);
                g.DrawEllipse(blackPen, rect);
                g.FillEllipse(new SolidBrush(cl), rect);
                erased = false;
            }
        }
        public override void Erase(Graphics g)
        {
            if (!erased)
            {
                Pen eraser = new Pen(Color.White, 3);
                Rectangle rect = new Rectangle(x, y, d, d);
                g.DrawEllipse(eraser, rect);
                g.FillEllipse(new SolidBrush(Color.White), rect);
                erased = true;
            }
        }
        /*public override void Clash(ref Obj a)
        {
            

        } */       
        public override void Step(int w, int h )
        {
            if (!shooted)
            {
                Ox = x + d / 2;
                Oy = y + d / 2;
                max_x = x + d; min_x = x;
                min_y = y; max_y = y + d;
                Repulsion(w, h);
            }

        }
       
    }
    class Obj_square : Obj
    {
               
        public Obj_square(): base(){}
        public Obj_square(int _x, int _y, double _Vx, double _Vy, Color _cl, int _d)
        {
            d = _d ;
            x = _x; y = _y;
            Vx = _Vx; Vy = _Vy;            
            cl = _cl;
            Ox = x +d / 2;
            Oy = y +d / 2;        
            status = 3;
            shooted = false;
            erased = false;
        }
        public override void Draw(Graphics g)
        {
            if (!shooted)
            {                
                Pen blackPen = new Pen(cl, 3);
                Rectangle rect = new Rectangle(x, y, d, d);
                g.DrawRectangle(blackPen, rect);
                g.FillRectangle(new SolidBrush(cl), rect);
                erased = false;
            }
        }
        public override void Erase(Graphics g)
        {
            if (!erased)
            {
                Pen whitePen = new Pen(Color.White, 3);
                Rectangle rect = new Rectangle(x, y, d, d);
                g.DrawRectangle(whitePen, rect);
                g.FillRectangle(new SolidBrush(Color.White), rect);
                erased = true;
            }
        }
        /*public override void Clash(ref Obj a)
        {
            if (!shooted && !a.shooted)
            {
                Ox = x + d / 2;
                Oy = y + d / 2;
                double h;
                h = Math.Abs(Math.Sqrt((Ox - a.Ox) * (Ox - a.Ox) + (Oy - a.Oy) * (Oy - a.Oy)));
                if (h <= (double)((d + a.d) / 2)) //проверяет ударяется ли квадрат с другим кругом
                {
                    Vx = -Vx;
                    Vy = -Vy;
                    a.Vx = -a.Vx;
                    a.Vy = -a.Vy;
                }
            }

        }*/       
        public override void Step(int w, int h)
        {
            if (!shooted)
            {
                Ox = x + d / 2;
                Oy = y + d / 2;
                max_x = x + d; min_x = x;
                min_y = y; max_y = y + d;
                Repulsion(w, h);
            }
        }
       
    }
    class Obj_star : Obj
    {
        private Point[] points1 = new Point[3];
        private Point[] points2 = new Point[3];
        public Obj_star() : base() { }
        public Obj_star(int _x, int _y, double _Vx, double _Vy, Color _cl, int _d)
        {
            d = _d;
            x = _x; y = _y;
            Vx = _Vx; Vy = _Vy;
            cl = _cl;
            Ox = x + d / 2;
            Oy = y + d / 2;
            status = 3;
            shooted = false;
            erased = false;
        }
        public override void Draw(Graphics g)
        {
            if (!shooted)
            {                
                Pen newPen = new Pen(cl, 3);
                points1[0].X = x + d/2; points1[0].Y = y;
                points1[1].X = x + d; points1[1].Y = y + d;
                points1[2].X = x; points1[2].Y = y + d;
                g.DrawPolygon(newPen, points1);
                g.FillPolygon(new SolidBrush(cl), points1);

                points2[0].X = x; points2[0].Y = y+d/3;
                points2[1].X = x + d; points2[1].Y = y+d/3;
                points2[2].X = x + d/2; points2[2].Y = y + d+d/3;
                g.DrawPolygon(newPen, points2);
                g.FillPolygon(new SolidBrush(cl), points2);

                erased = false;
            }
        }
        public override void Erase(Graphics g)
        {
            if (!erased)
            {
                
                Pen whitePen = new Pen(Color.White, 3);                
                g.DrawPolygon(whitePen, points1);
                g.FillPolygon(new SolidBrush(Color.White), points1);
                g.DrawPolygon(whitePen, points2);
                g.FillPolygon(new SolidBrush(Color.White), points2);
                erased = true;
            }
        }
        /*public override void Clash(ref Obj a)
        {
            if (!shooted && !a.shooted)
            {
                Ox = x + d / 2;
                Oy = y + d / 2;
                double h;
                h = Math.Abs(Math.Sqrt((Ox - a.Ox) * (Ox - a.Ox) + (Oy - a.Oy) * (Oy - a.Oy)));
                if (h <= (double)((d + a.d) / 2)) //проверяет ударяется ли квадрат с другим кругом
                {
                    Vx = -Vx;
                    Vy = -Vy;
                    a.Vx = -a.Vx;
                    a.Vy = -a.Vy;
                }
            }

        }*/
        public override void Step(int w, int h)
        {
            if (!shooted)
            {
                Ox = x + d / 2;
                Oy = y + d / 2;
                max_x = x + d; min_x = x;
                min_y = y; max_y = y + d;
                Repulsion(w, h);
            }
        }

    }
}
