using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoldemHand;
using System.Threading;
using HandHistories.Parser.Parsers.FastParser.PokerStars;
using System.Windows.Forms;

namespace ConsoleApp7
{
    class Program
    {
        static void Main(string[] args)
        {
            Controlador controlador = new Controlador();
            Partida partida = new Partida();
            Bot bot = new Bot();
            Console.WriteLine("Ingrese asiento del gediento: ");
            partida.miAsiento = Convert.ToInt32(Console.ReadLine());
            bool unaVez = true;

            while (true)
            {
                Console.WriteLine("Probabilidades: " + controlador.buscarMisProbabilidades());
                partida.actualizarInfoPartida();
                if (controlador.esNuestroTurno(partida.miAsiento))
                {
                    if (unaVez)
                    {
                        if (controlador.conCuantoIr() == 0)
                        {
                            if (controlador.tengoBuenasCartasInicio() || controlador.buscarMisProbabilidades() > 80)
                            {
                                bot.subir();
                                unaVez = false;
                            }
                            else
                            {
                                bot.pasar();
                                unaVez = false;
                            }
                        }
                        else if (controlador.conCuantoIr() >= partida.ciega * 30)
                        {
                            if (controlador.tengoExcelentesCartasInicio() || controlador.buscarMisProbabilidades() > 95)
                            {
                                bot.allIn();
                                unaVez = false;
                            }
                            else
                            {
                                bot.irse();
                                unaVez = false;
                            }
                        }
                        else if (controlador.conCuantoIr() >= partida.ciega * 10 && controlador.conCuantoIr() < partida.ciega * 30)
                        {
                            if (controlador.tengoBuenasCartasInicio() || controlador.buscarMisProbabilidades() > 80)
                            {
                                bot.igualar();
                                unaVez = false;
                            }
                            else
                            {
                                bot.irse();
                                unaVez = false;
                            }
                        }
                        else if (controlador.conCuantoIr() >= partida.ciega * 5 && controlador.conCuantoIr() < partida.ciega * 10)
                        {
                            if (controlador.tengoBuenasCartasInicio() || controlador.buscarMisProbabilidades() > 70)
                            {
                                bot.igualar();
                                unaVez = false;
                            }
                            else
                            {
                                bot.irse();
                                unaVez = false;
                            }
                        }
                        else
                        {
                            if (controlador.tengoExcelentesCartasInicio() || controlador.buscarMisProbabilidades() > 80)
                            {
                                bot.subir();
                                unaVez = false;
                            }
                            else if (controlador.tengoAceptablesCartasInicio() || controlador.buscarMisProbabilidades() > 50)
                            {
                                bot.igualar();
                                unaVez = false;
                            }
                            else
                            {
                                bot.irse();
                                unaVez = false;
                            }
                        }
                    }
                    
                }
                else
                {
                    unaVez = true;
                }
                Console.Clear();
            }
          
        }
    }
}


