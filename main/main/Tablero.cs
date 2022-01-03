using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main
{
    public class Tablero
    {
        public readonly int Altura; // readonly porque solo se asigna una vez. 
        public readonly int Anchura;
        public bool contieneCaramelo;

        public Tablero( int altura, int anchura)
        {
            Altura  = altura;
            Anchura = anchura;
            contieneCaramelo = false;
        }

        // for desde 0 hasta la anchura
        public void DibujarTablero()
        {
            for (int i = 0; i <= Altura; i++)
            {
                Util.DibujarPosicion(Anchura, i, "#");
                Util.DibujarPosicion(0, i, "#");
            }

            for (int i = 0; i <= Anchura; i++)
            {
                Util.DibujarPosicion(i, 0, "#");
                Util.DibujarPosicion(i, Altura, "#");
            }
        }
    }
}
