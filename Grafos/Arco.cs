using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafos
{
    class Arco
    {
        public Vertice nDestino;
        //Guarda el vértice al que se dirige el arco

        public int peso;
        //Guarda el peso del arco

        public float grosorFlecha;
        //Establece el grosor de la flecha del arco

        public Color color;
        //Almacena el color de la flecha

        public Arco(Vertice destino, int peso)
        {
            //Constructor de la clase Arco que toma un destino y el peso
            this.nDestino = destino;
            this.peso = peso;
            this.grosorFlecha = 2;
            this.color = Color.FromArgb(239, 98, 108);
        }
        public Arco(Vertice destino) : this(destino, 1)
        {
            //Constructor de la clase Arco que solo toma un destino
            this.nDestino = destino;
        }
    }
}
