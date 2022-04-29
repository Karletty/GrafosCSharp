using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafos
{
    class Vertice
    {
        public string Valor;
        public List<Arco> ListaAdyacencia;
        Dictionary<string, short> _banderas;
        Dictionary<string, short> _banderas_predeterminado;

        static int size = 35;
        Size dimensiones;
        Color colorNodo;
        Color colorFuente;
        Point pos;
        int radio;

        public Color Color
        {
            get { return colorNodo; }
            set { colorNodo = value; }
        }

        public Color FontColor
        {
            get { return colorFuente; }
            set { colorFuente = value; }
        }

        public Point Posicion
        {
            get { return pos; }
            set { pos = value; }
        }

        public Size Dimensiones
        {
            get { return dimensiones; }
            set
            {
                radio = value.Width / 2;
                dimensiones = value;
            }
        }

        public Vertice(string val)
        {
            this.Valor = val;
            this.ListaAdyacencia = new List<Arco>();
            this._banderas = new Dictionary<string, short>();
            this._banderas_predeterminado = new Dictionary<string, short>();
            this.Color = Color.Green;
            this.dimensiones = new Size(size, size);
            this.FontColor = Color.White;
        }
        
        public Vertice() : this("") { }

        public void DibujarVertice(Graphics g)
        {
            SolidBrush b = new SolidBrush(this.colorNodo);
            Rectangle areaNodo = new Rectangle(this.pos.X - radio, this.pos.Y - radio, this.Dimensiones.Width, this.Dimensiones.Height);
            g.FillEllipse(b, areaNodo);
            g.DrawString(this.Valor, new Font("Times New Roman", 14), new SolidBrush(colorFuente), this.pos.X, this.pos.Y, new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            });

            g.DrawEllipse(new Pen(Brushes.Black, (float)1.0), areaNodo);
            b.Dispose();
        }

        public void DibujarArco(Graphics g)
        {
            float distancia;
            int dX, dY;

            foreach (Arco arco in ListaAdyacencia)
            {
                dX = this.pos.X - arco.nDestino.pos.X;
                dY = this.pos.Y - arco.nDestino.pos.Y;

                distancia = (float)Math.Sqrt(dX * dX + dY * dY);

                AdjustableArrowCap bigArrow = new AdjustableArrowCap(4, 4, true);
                bigArrow.BaseCap = System.Drawing.Drawing2D.LineCap.Triangle;

                g.DrawLine(
                    new Pen(new SolidBrush(arco.color), arco.grosorFlecha)
                        {
                            CustomEndCap = bigArrow, Alignment = PenAlignment.Center
                        },
                    pos,
                    new Point(arco.nDestino.Posicion.X + (int)(radio * dX / distancia), arco.nDestino.Posicion.Y + (int)(radio * dY / distancia)));

                g.DrawString(
                    arco.peso.ToString(),
                    new Font("Times New Roman", 12),
                    new SolidBrush(Color.Black),
                    this.pos.X - (int)(dX / 3),
                    this.pos.Y - (int)(dY / 3),
                    new StringFormat()
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Far
                        }
                    );
            }
        }

        public bool DetectarPunto(Point p)
        {
            GraphicsPath posicion = new GraphicsPath();
            posicion.AddEllipse(
                new Rectangle(
                    this.pos.X - this.dimensiones.Width/2,
                    this.pos.Y - this.dimensiones.Height/2,
                    this.dimensiones.Width,
                    this.dimensiones.Height
                    ));
            bool retval = posicion.IsVisible(p);
            posicion.Dispose();
            return retval;
        }

        public string ToString()
        {
            return this.Valor;
        }
    }
}
