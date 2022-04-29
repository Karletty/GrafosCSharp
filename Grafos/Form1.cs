using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Grafos
{
    public partial class Simulador : Form
    {
        private Grafo grafo;
        private Vertice nuevoNodo;
        private Vertice nodoOrigen;
        private Vertice nodoDestino;
        enum Control
        {
            SinAccion,
            DibujandoArco,
            DibujandoVertice
        }
        private Control control = Control.SinAccion;
        private FrmVertice ventanaVertice;

        public Simulador()
        {
            InitializeComponent();
            grafo = new Grafo();
            nuevoNodo = null;
            control = Control.SinAccion;
            ventanaVertice = new FrmVertice();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void Pizarra_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                grafo.DibujarGrafo(e.Graphics);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Pizarra_MouseLeave(object sender, EventArgs e)
        {
            Pizarra.Refresh();
        }

        private void nuevoVerticeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevoNodo = new Vertice();
            control = Control.DibujandoVertice;
            ventanaVertice.Visible = true;
        }

        private void Pizarra_MouseUp(object sender, MouseEventArgs e)
        {
            if (control == Control.DibujandoArco)
            {
                if ((nodoDestino = grafo.DetectarPunto(e.Location)) != null && nodoOrigen != nodoDestino)
                {
                    if (grafo.AgregarArco(nodoOrigen, nodoDestino))
                    {
                        int d = 0;
                        nodoOrigen.ListaAdyacencia.Find(v => v.nDestino == nodoDestino).peso = d;
                    }
                }
                control = Control.SinAccion;
                nodoOrigen = null;
                nodoDestino = null;

                Pizarra.Refresh();
            }
        }

        private void Pizarra_MouseMove(object sender, MouseEventArgs e)
        {
            switch (control)
            {
                case Control.DibujandoArco:
                    AdjustableArrowCap bigArrow = new AdjustableArrowCap(4, 4, true);
                    bigArrow.BaseCap = LineCap.Triangle;
                    Pizarra.Refresh();
                    Pizarra.CreateGraphics().DrawLine(new Pen(Brushes.Black, 2)
                    {
                        CustomEndCap = bigArrow
                    }, nodoOrigen.Posicion, e.Location);
                    break;
                case Control.DibujandoVertice:
                    if(nuevoNodo != null)
                    {
                        int posX = e.Location.X;
                        int posY = e.Location.Y;

                        if (posX < nuevoNodo.Dimensiones.Width / 2)
                            posX = nuevoNodo.Dimensiones.Width / 2;
                        else if (posX > Pizarra.Size.Width - nuevoNodo.Dimensiones.Width / 2)
                            posX = Pizarra.Size.Width - nuevoNodo.Dimensiones.Width / 2;

                        if (posY < nuevoNodo.Dimensiones.Height / 2)
                            posY = nuevoNodo.Dimensiones.Height / 2;
                        else if (posY > Pizarra.Size.Height - nuevoNodo.Dimensiones.Width / 2)
                            posY = Pizarra.Size.Height - nuevoNodo.Dimensiones.Width / 2;
                    }
                    break;
            }
        }

        private void Pizarra_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                if ((nodoOrigen = grafo.DetectarPunto(e.Location)) != null)
                    control = Control.DibujandoArco;
                if(nuevoNodo != null && nodoOrigen == null)
                {
                    ventanaVertice.Visible = false;
                    ventanaVertice.control = false;
                    grafo.AgregarVertice(nuevoNodo);
                    ventanaVertice.ShowDialog();

                    if (ventanaVertice.control)
                    {
                        if (grafo.BuscarVertice(ventanaVertice.txtVertice.Text) == null)
                            nuevoNodo.Valor = ventanaVertice.txtVertice.Text;
                        else
                            MessageBox.Show($"El nodo {ventanaVertice.txtVertice.Text} ya existe en el grafo", "Error nuevo nodo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    nuevoNodo = null;
                    control = Control.SinAccion;
                    Pizarra.Refresh();
                }
            }
            if(e.Button == MouseButtons.Right)
            {
                if(control == Control.SinAccion)
                {
                    if ((nodoOrigen = grafo.DetectarPunto(e.Location)) != null)
                        CMSCrearVertice.Text = $"Nodo {nodoOrigen.Valor}";
                    else
                        Pizarra.ContextMenuStrip = this.CMSCrearVertice;
                }
            }
        }
    }
}
