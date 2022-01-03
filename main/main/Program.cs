using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main
{
    class Program
    {
        static void Main(string[] args)
        {
            Tablero tablero     = new Tablero(20, 20);
            Serpiente serpiente = new Serpiente(10,10);
            Caramelo caramelo   = new Caramelo(0, 0);
            bool haComido       = false;

            do
            {
                Console.Clear();
                tablero.DibujarTablero();

                serpiente.ComprobarMorir(tablero);
                if (serpiente.estaViva)
                {
                    serpiente.Moverse(haComido);
                    // comprobamos si se ha comido el caramelo. 
                    haComido = serpiente.comerCaramelo(caramelo, tablero);

                    // Dibujamos serpiente
                    serpiente.DibujarSerpiente();

                    if (!tablero.contieneCaramelo)
                    {
                        caramelo = Caramelo.CrearCaramelo(serpiente, tablero);
                    }

                    // Dibujamos caramelo
                    caramelo.DibujarCaramelo();

                    // Leemos información por teclado de la dirección.
                    var sw = Stopwatch.StartNew();
                    while (sw.ElapsedMilliseconds <= 250) // durante 250ms estará pendiente de lo que hay dentro del while.
                    {
                        serpiente.direccion = LeerMovimiento(serpiente.direccion);
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Util.DibujarPosicion(tablero.Anchura / 2, tablero.Altura / 2, "GAME OVER");
                    Util.DibujarPosicion(tablero.Anchura / 2, (tablero.Altura / 2)+1, $"Puntuación: {serpiente.puntos}");
                    Console.ResetColor();
                }

            }
            while (serpiente.estaViva);

            Console.ReadKey(); // para salir. 
        }

        static Direccion LeerMovimiento(Direccion movimientoActual)// nos devuelve la dirección a la que vamos a actualizar la serpiente.
        {
            if (Console.KeyAvailable)
            {
                var Key = Console.ReadKey().Key; // con el consoleKeyInfo podemos saber la dirección a la que nos estamos moviendo.

                if (Key == ConsoleKey.UpArrow && movimientoActual != Direccion.Abajo) // esto es porque no podemos ir a la dirección completa opuesta.
                {
                    return Direccion.Arriba;
                }
                else if (Key == ConsoleKey.DownArrow && movimientoActual != Direccion.Arriba)
                {
                    return Direccion.Abajo;
                }
                else if (Key == ConsoleKey.LeftArrow && movimientoActual != Direccion.Derecha)
                {
                    return Direccion.Izquierda;
                }
                else if (Key == ConsoleKey.RightArrow && movimientoActual != Direccion.Izquierda)
                {
                    return Direccion.Derecha;
                }
            }
            return movimientoActual;
        }
    }
}
