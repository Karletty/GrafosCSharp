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
        //Almacena una lista de todos los vértices

        public Grafo()
        {
            //Constructor de un nuevo grafo
            vertices = new List<Vertice>();
        }

        public Vertice AgregarVertice(string valor)
        {
            //Agrega un vértice tomando su valor
            Vertice v = new Vertice(valor);
            vertices.Add(v);
            return v;
        }

        public void AgregarVertice(Vertice nuevo)
        {
            //Agrega un vértice recibiendo un vértice
            vertices.Add(nuevo);
        }

        public Vertice BuscarVertice(string valor)
        {
            //Busca dentro de la lista de vértices si el valor ingresado ya existe
            return vertices.Find(v => v.Valor == valor);
        }

        public bool AgregarArco(string origen, string destino, int peso = 1)
        {
            //Crea un arco a partir de los nombres de los dos lugares y su peso, primero busca si los nombres ya pertenecen a otros y sino llama a la función agregar arco con vertices
            Vertice vOrigen, vDestino;

            if ((vOrigen = vertices.Find(v => v.Valor == origen)) == null)
                throw new Exception($"El nodo {origen} no existe en el grafo");
            if ((vDestino = vertices.Find(v => v.Valor == destino)) == null)
                throw new Exception($"El nodo {destino} no existe en el grafo");
            return AgregarArco(vOrigen, vDestino);
        }

        public bool AgregarArco(Vertice origen, Vertice destino, int peso = 1)
        {
            //Recibe los dos vértices y su peso, busca si en el origen no hay un arco que los conecta y si se cumple entonces crea uno y retorna true, sino retorna false
            if ((origen.ListaAdyacencia.Find(v => v.nDestino == destino)) == null)
            {
                origen.ListaAdyacencia.Add(new Arco(destino, peso));
                return true;
            }
            return false;
        }

        public void DibujarGrafo(Graphics g)
        {
            //Manda a dibujar cada arco de cada vértice y a cada vértice
            foreach (Vertice v in vertices)
                v.DibujarArco(g);

            foreach (Vertice v in vertices)
                v.DibujarVertice(g);
        }

        public Vertice DetectarPunto(Point posMouse)
        {
            //Devuelve un vértice que se encuentra en la posición que se recibe, si no hay retorna null
            foreach (Vertice actual in vertices)
                if (actual.DetectarPunto(posMouse))
                    return actual;
            return null;
        }

        public void Reestablecer(Graphics g)
        {
            //Restablece el grafo actualizando cada vértice y cada arco de estos y luego vuelve a dibujar todo el grafo
            foreach (Vertice v in vertices)
            {
                v.Color = Color.White;
                v.FontColor = Color.Black;
                foreach (Arco arc in v.ListaAdyacencia)
                {
                    arc.grosorFlecha = 1;
                    arc.color = Color.FromArgb(34, 24, 28);
                }
            }

            DibujarGrafo(g);
        }

        public void EliminarVertice(Vertice v)
        {
            foreach (Vertice vertice in vertices)
            {
                List<Arco> arcosConservar = new List<Arco>();
                foreach (Arco arco in vertice.ListaAdyacencia)
                {
                    if (arco.nDestino != v)
                    {
                        arcosConservar.Add(arco);
                    }
                }
                vertice.ListaAdyacencia.Clear();
                vertice.ListaAdyacencia = arcosConservar;
            }
            vertices.Remove(v);
        }
    }
}
