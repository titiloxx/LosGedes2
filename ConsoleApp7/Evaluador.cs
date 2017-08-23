using HoldemHand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7
{
    class Evaluador
    {
        public double probabilidades,cartaAlta,color,escalera,poker,escaleraReal,tripla,
            par,doblePar,escaleraColor,full;

        public double misProbabilidades(string misCartas, string tablero, int numeroEnemigos)
        {
            numeroEnemigos = 1;
        ulong playerMask = Hand.ParseHand(misCartas); // Player Pocket Cards
        ulong board = Hand.ParseHand(tablero);   // Partial Board
        int contadorP1 = 0;
        int contadorEne = 0;
        // Count of total hands examined.
        long count = 0;

        // Iterate through all possible opponent hands
        foreach (ulong opponentMask in Hand.Hands(0UL,
                             board | playerMask, 2))
        {
            // Iterate through all possible boards
            foreach (ulong boardMask in Hand.Hands(board,
                           opponentMask | playerMask, 5))
            {
                // Create a hand value for each player
                uint playerHandValue =
                       Hand.Evaluate(boardMask | playerMask, 7);
                uint opponentHandValue =
                       Hand.Evaluate(boardMask | opponentMask, 7);

                // Calculate Winners
                if (playerHandValue > opponentHandValue)
                {
                    contadorP1++;
                }
                else if (playerHandValue < opponentHandValue)
                {
                    contadorEne++;
                }
                else if (playerHandValue == opponentHandValue)
                {
                    contadorEne++;
                    contadorP1++;
                }
                count++;
            }
               

            }
            double misProbabilidades = (((double)contadorP1) / ((double)count) * 100.0);
            double enemigoProbabilidades = 100 - (((double)contadorP1) / ((double)count) * 100.0);
            misProbabilidades = misProbabilidades * 100 / (misProbabilidades + (numeroEnemigos * enemigoProbabilidades));
            return misProbabilidades;
        }

    }
}
