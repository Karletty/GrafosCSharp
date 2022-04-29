using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafos
{
    class Grafo
    {
        public List<Vertice> vertices;

        public Grafo()
        {
            vertices = new List<Vertice>();
        }

        public Vertice AgregarVertice(string valor)
        {
            Vertice v = new Vertice(valor);
            vertices.Add(v);
            return v;
        }

        public void AgregarVertice(Vertice nuevo)
        {
            vertices.Add(nuevo);
        }

        public Vertice BuscarVertice(string valor)
        {
            return vertices.Find(v => v.Valor == valor);
        }

        public bool AgregarArco(string origen, string destino, int peso = 1)
        {
            Vertice vOrigen, vDestino;

            if ((vOrigen = vertices.Find(v => v.Valor == origen)) == null)
                throw new Exception($"El nodo {origen} no existe en el grafo");
            if ((vDestino = vertices.Find(v => v.Valor == destino)) == null)
                throw new Exception($"El nodo {destino} no existe en el grafo");
            return AgregarArco(vOrigen, vDestino);
        }

        public bool AgregarArco(Vertice origen, Vertice destino, int peso = 1)
        {
            if((origen.ListaAdyacencia.Find(v => v.nDestino == destino)) == null)
            {
                origen.ListaAdyacencia.Add(new Arco(destino, peso));
                return true;
            }
            return false;
        }

        public void DibujarGrafo(Graphics g)
        {
            foreach (Vertice v in vertices)
                v.DibujarArco(g);

            foreach (Vertice v in vertices)
                v.DibujarVertice(g);
        }

        public Vertice DetectarPunto(Point posMouse)
        {
            foreach (Vertice actual in vertices)
                if (actual.DetectarPunto(posMouse))
                    return actual;
            return null;
        }

        public void Reestablecer(Graphics g)
        {
            foreach(Vertice v in vertices)
            {
                v.Color = Color.White;
                v.FontColor = Color.Black;
                foreach (Arco arc in v.ListaAdyacencia)
                {
                    arc.grosorFlecha = 1;
                    arc.color = Color.Black;
                }
            }

            DibujarGrafo(g);
        }
    }
}
