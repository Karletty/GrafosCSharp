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
        public int peso;

        public float grosorFlecha;
        public Color color;

        public Arco(Vertice destino, int peso)
        {
            this.nDestino = destino;
            this.peso = peso;
            this.grosorFlecha = 2;
            this.color = Color.BlueViolet;

        }
    }
}
