using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main
{
    public class Serpiente
    {
        List<Posicion> cola { get; set; } // No es readonly porque vamos añadiendo elementos a la cola. 
        public Direccion direccion { get; set; }
        public int puntos { get; set; }
        public bool estaViva { get; set; } 

        public Serpiente(int x, int y)
        {
            Posicion posicionInicial = new Posicion(x, y);
            cola = new List<Posicion>() { posicionInicial };
            direccion = Direccion.Abajo;

            puntos = 0;
            estaViva = true;
        }

        public void DibujarSerpiente()
        {
            foreach (Posicion posicion in cola)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Util.DibujarPosicion(posicion.X, posicion.Y, "x");
                Console.ResetColor();
            }
        }

        public void ComprobarMorir(Tablero tablero)
        {
            // Si primera posición de la cola esta en la cola. 
            Posicion primeraPosicion = cola.First();
            estaViva = !((cola.Count(a => a.X == primeraPosicion.X && a.Y == primeraPosicion.Y) > 1)
                || CabezaEstaEnPared(tablero, primeraPosicion));
        }

        // Si la primera posición está en cualquiera de los muros, morimos. 
        private bool CabezaEstaEnPared(Tablero tablero, Posicion primeraPosicion)
        {
            return primeraPosicion.Y == 0 || primeraPosicion.Y == tablero.Altura 
                || primeraPosicion.X == 0 || primeraPosicion.X == tablero.Anchura;
        }

        public void Moverse(bool haComido)
        {
            // Se crea una nueva cola, y no se pone a 0 la que ya tenemos es porque hay que hacer un bucle.
            List<Posicion> nuevaCola = new  List<Posicion>();  
            nuevaCola.Add(ObteberNuevaPrimeraPosicion());
            nuevaCola.AddRange(cola);

            if (!haComido)
            {
                nuevaCola.Remove(nuevaCola.Last());
            }

            cola = nuevaCola;
        }

        public bool PosicionEnCola(int x, int y)
        {
            return cola.Any(a => a.X == x && a.Y == y); // Esto nos dice si el nuevo caramelo es valido o no. 
        }

        private Posicion ObteberNuevaPrimeraPosicion() // private porque solo se accede desde moverse.
        {
            int x = cola.First().X;
            int y = cola.First().Y;

            switch (direccion)
            {
                case Direccion.Abajo:
                    y += 1;
                    break;
                case Direccion.Arriba:
                    y -= 1;
                    break;
                case Direccion.Derecha:
                    x += 1;
                    break;
                case Direccion.Izquierda:
                    x -= 1;
                    break;
            }
            return new Posicion(x, y);
        }

        public bool comerCaramelo (Caramelo caramelo, Tablero tablero)
        {
            if (PosicionEnCola(caramelo.posicion.X, caramelo.posicion.Y))
            {
                puntos += 10; // sumamos puntos
                tablero.contieneCaramelo = false; // Quitar el caramelo o generar uno nuevo. 
                return true; 
            }
            return false;
        }
    }
}
