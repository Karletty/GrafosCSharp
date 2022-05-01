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
        //Declara las variables globales a utilizar para dibujar arcos y crear los vértices
        private Grafo grafo;
        private Vertice nuevoNodo;
        private Vertice nodoOrigen;
        private Vertice nodoDestino;
        private Vertice nodoEliminar;
        enum Control
        {
            SinAccion,
            DibujandoArco,
            CreandoVertice
        }
        private Control control = Control.SinAccion;
        //Sirve para decirle al programa que acción está ejecutando en ese momento para que todas las funciones actuen según la acción a ejecutar
        private FrmVertice ventanaVertice;

        public Simulador()
        {
            InitializeComponent();
            grafo = new Grafo();
            nuevoNodo = null;
            control = Control.SinAccion;
            ventanaVertice = new FrmVertice();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
        }

        private void Pizarra_Paint(object sender, PaintEventArgs e)
        {
            //Cuando la pizarra se pinta o se refresca dibuja el grafo
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
            //Cuando el mouse deja la pizarra esta se refresca
            Pizarra.Refresh();
        }

        private void nuevoVerticeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Cuando se da click en la opción de crear vértice en el context menu se crea un nuevo nodo y le dice al programa que se está creando un nuevo nodo
            nuevoNodo = new Vertice();
            control = Control.CreandoVertice;
        }

        private void Pizarra_MouseUp(object sender, MouseEventArgs e)
        {

            switch (control)
            {

                case Control.DibujandoArco:
                    if ((nodoDestino = grafo.DetectarPunto(e.Location)) != null && nodoOrigen != nodoDestino)
                    {
                        //Cuando un botón del mouse se levanta, si está dibujando un arco se detecta si el punto donde se levantó es un nodo y si no es el mismo nodo de origen
                        if (grafo.AgregarArco(nodoOrigen, nodoDestino))
                        {
                            int d = 0;
                            nodoOrigen.ListaAdyacencia.Find(v => v.nDestino == nodoDestino).peso = d;
                            //Verifica si el arco se puede agregar y si se agrega añade el nodo destino a la lista de adyacencia del origen
                        }
                    }
                    control = Control.SinAccion;
                    nodoOrigen = null;
                    nodoDestino = null;

                    Pizarra.Refresh();
                    //Le dice al programa que ya no está haciendo nada y se refresca
                    break;
            }
        }

        private void Pizarra_MouseMove(object sender, MouseEventArgs e)
        {
            switch (control)
            {
                case Control.CreandoVertice:
                    if (nuevoNodo != null)
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
                        nuevoNodo.Posicion = new Point(posX, posY);
                        Pizarra.Refresh();
                        nuevoNodo.DibujarVertice(Pizarra.CreateGraphics());
                        //Si se encuentra creando un vértice el dibujo del círculo se moverá con el mouse para ubiar el nodo
                    }
                    break;
                case Control.DibujandoArco:
                    AdjustableArrowCap bigArrow = new AdjustableArrowCap(4, 4, true);
                    bigArrow.BaseCap = LineCap.Triangle;
                    Pizarra.Refresh();
                    Pizarra.CreateGraphics().DrawLine(new Pen(Brushes.Black, 2)
                    {
                        CustomEndCap = bigArrow
                    }, nodoOrigen.Posicion, e.Location);
                    //Si se encuentra creando un arco, se dibujará una flecha que se moverá según como se mueva el mouse
                    break;
            }
        }

        private void Pizarra_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //Cuando se da click al botón izquierdo del mouse
                if ((nodoOrigen = grafo.DetectarPunto(e.Location)) != null)
                    control = Control.DibujandoArco;
                //Si da click en un nodo entonces le dice al programa que se está dibujando un arco
                if (nuevoNodo != null && nodoOrigen == null)
                {
                    ventanaVertice.Visible = false;
                    ventanaVertice.control = false;
                    ventanaVertice.ShowDialog();
                    //Si ya se había dado click a crear un nodo y se da click se abre la ventana del formulario vértice

                    if (ventanaVertice.control)
                    {
                        if (grafo.BuscarVertice(ventanaVertice.txtVertice.Text) == null)
                        {
                            nuevoNodo.Valor = ventanaVertice.txtVertice.Text;
                            grafo.AgregarVertice(nuevoNodo);
                            //Si ya se escribió un valor en el otro formulario y ese vértice no existe aún, asigna el valor al nuevo nodo y lo agrega al grafo
                        }
                        else
                            MessageBox.Show($"El nodo {ventanaVertice.txtVertice.Text} ya existe en el grafo", "Error nuevo nodo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //Si el valor ya existía se avisa al usuario
                    }

                    nuevoNodo = null;
                    control = Control.SinAccion;
                    Pizarra.Refresh();
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                //Si se clickea el botón derecho del mouse
                if (control == Control.SinAccion)
                {
                    if ((nodoOrigen = grafo.DetectarPunto(e.Location)) != null)
                        CMSCrearVertice.Text = $"Nodo {nodoOrigen.Valor}";
                    //Si el lugar donde se clickea es un nodo el menú debe decir el nombre
                    else
                        Pizarra.ContextMenuStrip = this.CMSCrearVertice;
                    //Si no se clickea un nombre se muestra el menú de crear vertice
                }
                if ((nodoEliminar = grafo.DetectarPunto(e.Location)) != null)
                {
                    //Busca si el punto donde se clickeo es un nodo y lo guarda en nodo eliminar
                    CMSEliminarVertice.Items.Clear();
                    CMSEliminarVertice.Items.Add(eliminarVérticeToolStripMenuItem);
                    //Limpia el menú y vuelve a agregar el "Eliminar Vértice"
                    foreach (Arco arco in nodoEliminar.ListaAdyacencia)
                    {
                        //Recorre la lista de adyacencia del nodo al que se le dio click
                        CMSEliminarVertice.Items.Add($"Eliminar arco hacia {arco.nDestino.ToString()}", default(Image), (snd, evt) =>
                        {
                            nodoEliminar.ListaAdyacencia.Remove(arco);
                            Pizarra.Refresh();
                        });
                        //En el menú agrega los textos de eliminar arco que hacia el nodo de destino y si se le da click a esta opción elimina el arco de la lista de adyacencia y refresca
                    }
                    Pizarra.ContextMenuStrip = this.CMSEliminarVertice;
                    //Muestra el menú para eliminar vértice y sus arcos
                }
            }
        }

        private void eliminarVérticeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Si se le da click a la opción de eliminar vértice en el menú se llama a la función del grafo y se refresca la pizarra
            grafo.EliminarVertice(nodoEliminar);
            Pizarra.Refresh();
        }
    }
}
