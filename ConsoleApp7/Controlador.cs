using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using HoldemHand;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp7
{

    class EnemigosGedientos
    {
        public string cartas, carta1, carta2;//ganadores
        public int ID, pozo, apuesta, totalApostado; //totalApostado es el total de todas las apuestas del enemigo
        public List<int> nivelMentiroso = new List<int>();//ganadores y perdedores
        public double nivelMentirosoPromedio;
        public bool vivo;
        public bool abrio = false;
        public int cuantoGano;
        public bool mostroCartas()
        {
            bool devuelto = (carta1 != "00") && (carta2 != "00") ? true : false;
            return devuelto;
        }
    }


    class Tablero
    {
        public string[] cartas = new string[5];
        public int cantidad;

    }


    class Partida
    {
        Controlador controler = new Controlador();
        public List<Mano> manos = new List<Mano>();
        public List<EnemigosGedientos> enemigos = new List<EnemigosGedientos>();
        public int ciega;
        private Mano mano;
        public int miAsiento;

        public Partida()
        {
            mano = new Mano(miAsiento);
            ciega = controler.buscarCiegaPokerstars();
            
        }
        
        public void actualizarCiega()
        {
            ciega = controler.buscarCiegaPokerstars();
        }

        private void actualizarEnemigos()
        {
            foreach (Mano m in manos)
            {
                if (!m.analizada)
                {

                    m.encontrarGanadores();

                    foreach (EnemigosGedientos ganador in m.ganadores)
                    {
                        if (ganador.mostroCartas())
                        {
                            Hand mano = new Hand(ganador.cartas, m.rondas[m.rondas.Count - 1].tableroViejo);

                            switch (mano.HandTypeValue)
                            {
                                case Hand.HandTypes.HighCard:
                                    if (ganador.totalApostado >= (ciega) * 10)
                                    {
                                        ganador.nivelMentiroso.Add(10);
                                    }
                                    if (ganador.totalApostado >= (ciega) * 5 && ganador.totalApostado < (ciega) * 10)
                                    {
                                        ganador.nivelMentiroso.Add(8);
                                    }
                                    if (ganador.totalApostado >= (ciega) * 2 && ganador.totalApostado < (ciega) * 5)
                                    {
                                        ganador.nivelMentiroso.Add(5);
                                    }
                                    if (ganador.totalApostado > (ciega) * 0 && ganador.totalApostado < (ciega) * 2)
                                    {
                                        ganador.nivelMentiroso.Add(2);
                                    }
                                    break;
                                case Hand.HandTypes.Pair:
                                    if (ganador.totalApostado >= (ciega) * 20)
                                    {
                                        ganador.nivelMentiroso.Add(10);
                                    }
                                    if (ganador.totalApostado >= (ciega) * 10 && ganador.totalApostado < (ciega) * 20)
                                    {
                                        ganador.nivelMentiroso.Add(9);
                                    }
                                    if (ganador.totalApostado >= (ciega) * 5 && ganador.totalApostado < (ciega) * 10)
                                    {
                                        ganador.nivelMentiroso.Add(8);
                                    }
                                    if (ganador.totalApostado >= (ciega) * 2 && ganador.totalApostado < (ciega) * 5)
                                    {
                                        ganador.nivelMentiroso.Add(3);
                                    }
                                    if (ganador.totalApostado > (ciega) * 0 && ganador.totalApostado < (ciega) * 2)
                                    {
                                        ganador.nivelMentiroso.Add(2);
                                    }
                                    break;
                                case Hand.HandTypes.TwoPair:
                                    if (ganador.totalApostado >= (ciega) * 20)
                                    {
                                        ganador.nivelMentiroso.Add(7);
                                    }
                                    if (ganador.totalApostado >= (ciega) * 10 && ganador.totalApostado < (ciega) * 20)
                                    {
                                        ganador.nivelMentiroso.Add(6);
                                    }
                                    if (ganador.totalApostado >= (ciega) * 5 && ganador.totalApostado < (ciega) * 10)
                                    {
                                        ganador.nivelMentiroso.Add(4);
                                    }
                                    if (ganador.totalApostado >= (ciega) * 2 && ganador.totalApostado < (ciega) * 5)
                                    {
                                        ganador.nivelMentiroso.Add(1);
                                    }
                                    if (ganador.totalApostado > (ciega) * 0 && ganador.totalApostado < (ciega) * 2)
                                    {
                                        ganador.nivelMentiroso.Add(1);
                                    }
                                    break;
                                case Hand.HandTypes.Trips:
                                    if (ganador.totalApostado >= (ciega) * 20)
                                    {
                                        ganador.nivelMentiroso.Add(4);
                                    }
                                    if (ganador.totalApostado >= (ciega) * 10 && ganador.totalApostado < (ciega) * 20)
                                    {
                                        ganador.nivelMentiroso.Add(4);
                                    }
                                    if (ganador.totalApostado >= (ciega) * 5 && ganador.totalApostado < (ciega) * 10)
                                    {
                                        ganador.nivelMentiroso.Add(3);
                                    }
                                    if (ganador.totalApostado >= (ciega) * 2 && ganador.totalApostado < (ciega) * 5)
                                    {
                                        ganador.nivelMentiroso.Add(1);
                                    }
                                    if (ganador.totalApostado > (ciega) * 0 && ganador.totalApostado < (ciega) * 2)
                                    {
                                        ganador.nivelMentiroso.Add(1);
                                    }
                                    break;
                                case Hand.HandTypes.Straight:
                                    if (ganador.totalApostado >= (ciega) * 20)
                                    {
                                        ganador.nivelMentiroso.Add(2);
                                    }
                                    if (ganador.totalApostado > (ciega) * 0 && ganador.totalApostado < (ciega) * 20)
                                    {
                                        ganador.nivelMentiroso.Add(1);
                                    }
                                    break;
                                case Hand.HandTypes.Flush:
                                    if (ganador.totalApostado >= (ciega) * 20)
                                    {
                                        ganador.nivelMentiroso.Add(1);
                                    }
                                    if (ganador.totalApostado > (ciega) * 0 && ganador.totalApostado < (ciega) * 20)
                                    {
                                        ganador.nivelMentiroso.Add(1);
                                    }
                                    break;
                                case Hand.HandTypes.FullHouse:
                                    ganador.nivelMentiroso.Add(1);
                                    break;
                                case Hand.HandTypes.FourOfAKind:
                                    ganador.nivelMentiroso.Add(1);
                                    break;
                                case Hand.HandTypes.StraightFlush:
                                    ganador.nivelMentiroso.Add(1);
                                    break;
                                default:
                                    break;
                            }

                        }



                    }

                    foreach (EnemigosGedientos perdedor in m.perdedores)
                    {
                        if (perdedor.mostroCartas())
                        {
                            Hand mano = new Hand(perdedor.cartas, m.rondas[m.rondas.Count - 1].tableroViejo);

                            switch (mano.HandTypeValue)
                            {
                                case Hand.HandTypes.HighCard:
                                    if (perdedor.totalApostado >= (ciega) * 10)
                                    {
                                        perdedor.nivelMentiroso.Add(10);
                                    }
                                    if (perdedor.totalApostado >= (ciega) * 5 && perdedor.totalApostado < (ciega) * 10)
                                    {
                                        perdedor.nivelMentiroso.Add(8);
                                    }
                                    if (perdedor.totalApostado >= (ciega) * 2 && perdedor.totalApostado < (ciega) * 5)
                                    {
                                        perdedor.nivelMentiroso.Add(5);
                                    }
                                    if (perdedor.totalApostado > (ciega) * 0 && perdedor.totalApostado < (ciega) * 2)
                                    {
                                        perdedor.nivelMentiroso.Add(2);
                                    }
                                    break;
                                case Hand.HandTypes.Pair:
                                    if (perdedor.totalApostado >= (ciega) * 20)
                                    {
                                        perdedor.nivelMentiroso.Add(10);
                                    }
                                    if (perdedor.totalApostado >= (ciega) * 10 && perdedor.totalApostado < (ciega) * 20)
                                    {
                                        perdedor.nivelMentiroso.Add(9);
                                    }
                                    if (perdedor.totalApostado >= (ciega) * 5 && perdedor.totalApostado < (ciega) * 10)
                                    {
                                        perdedor.nivelMentiroso.Add(8);
                                    }
                                    if (perdedor.totalApostado >= (ciega) * 2 && perdedor.totalApostado < (ciega) * 5)
                                    {
                                        perdedor.nivelMentiroso.Add(3);
                                    }
                                    if (perdedor.totalApostado > (ciega) * 0 && perdedor.totalApostado < (ciega) * 2)
                                    {
                                        perdedor.nivelMentiroso.Add(2);
                                    }
                                    break;
                                case Hand.HandTypes.TwoPair:
                                    if (perdedor.totalApostado >= (ciega) * 20)
                                    {
                                        perdedor.nivelMentiroso.Add(7);
                                    }
                                    if (perdedor.totalApostado >= (ciega) * 10 && perdedor.totalApostado < (ciega) * 20)
                                    {
                                        perdedor.nivelMentiroso.Add(6);
                                    }
                                    if (perdedor.totalApostado >= (ciega) * 5 && perdedor.totalApostado < (ciega) * 10)
                                    {
                                        perdedor.nivelMentiroso.Add(4);
                                    }
                                    if (perdedor.totalApostado >= (ciega) * 2 && perdedor.totalApostado < (ciega) * 5)
                                    {
                                        perdedor.nivelMentiroso.Add(1);
                                    }
                                    if (perdedor.totalApostado > (ciega) * 0 && perdedor.totalApostado < (ciega) * 2)
                                    {
                                        perdedor.nivelMentiroso.Add(1);
                                    }
                                    break;
                                case Hand.HandTypes.Trips:
                                    if (perdedor.totalApostado >= (ciega) * 20)
                                    {
                                        perdedor.nivelMentiroso.Add(4);
                                    }
                                    if (perdedor.totalApostado >= (ciega) * 10 && perdedor.totalApostado < (ciega) * 20)
                                    {
                                        perdedor.nivelMentiroso.Add(4);
                                    }
                                    if (perdedor.totalApostado >= (ciega) * 5 && perdedor.totalApostado < (ciega) * 10)
                                    {
                                        perdedor.nivelMentiroso.Add(3);
                                    }
                                    if (perdedor.totalApostado >= (ciega) * 2 && perdedor.totalApostado < (ciega) * 5)
                                    {
                                        perdedor.nivelMentiroso.Add(1);
                                    }
                                    if (perdedor.totalApostado > (ciega) * 0 && perdedor.totalApostado < (ciega) * 2)
                                    {
                                        perdedor.nivelMentiroso.Add(1);
                                    }
                                    break;
                                case Hand.HandTypes.Straight:
                                    if (perdedor.totalApostado >= (ciega) * 20)
                                    {
                                        perdedor.nivelMentiroso.Add(2);
                                    }
                                    if (perdedor.totalApostado > (ciega) * 0 && perdedor.totalApostado < (ciega) * 20)
                                    {
                                        perdedor.nivelMentiroso.Add(1);
                                    }
                                    break;
                                case Hand.HandTypes.Flush:
                                    if (perdedor.totalApostado >= (ciega) * 20)
                                    {
                                        perdedor.nivelMentiroso.Add(1);
                                    }
                                    if (perdedor.totalApostado > (ciega) * 0 && perdedor.totalApostado < (ciega) * 20)
                                    {
                                        perdedor.nivelMentiroso.Add(1);
                                    }
                                    break;
                                case Hand.HandTypes.FullHouse:
                                    perdedor.nivelMentiroso.Add(1);
                                    break;
                                case Hand.HandTypes.FourOfAKind:
                                    perdedor.nivelMentiroso.Add(1);
                                    break;
                                case Hand.HandTypes.StraightFlush:
                                    perdedor.nivelMentiroso.Add(1);
                                    break;
                                default:
                                    break;
                            }

                        }



                    }

                    foreach (Ronda ronda in m.rondas)
                    {
                        foreach (EnemigosGedientos gd in ronda.enemigos)
                        {
                            foreach (EnemigosGedientos gdPartida in enemigos)
                            {
                                if (gd.ID == gdPartida.ID)
                                {
                                    int totalMentiras = 0;
                                    for (int i = 0; i < gd.nivelMentiroso.Count; i++) //va el igual?
                                    {
                                        totalMentiras = totalMentiras + gd.nivelMentiroso[i];
                                    }
                                    gdPartida.nivelMentirosoPromedio = totalMentiras / gd.nivelMentiroso.Count;
                                }
                            }
                        }

                    }

                    m.analizada = true;
                }


                // e.nivelMentiroso = evaluarMentiroso(e.);

            }

        }

        public void actualizarInfoPartida()
        {
            #region ConsolaAnalizadora
            /*
            Console.WriteLine("Manos: " + manos.Count);
            Console.WriteLine("Rondas: " + mano.rondas.Count);
            string[] board = controler.buscarTablero().Split(' ');
            int contador = controler.buscarTablero().Split(' ').Count();
            try { 
            Console.Write("Cartas 0: "+mano.rondas[mano.rondas.Count-1].board);
                Console.WriteLine("  Viejo: " + mano.rondas[mano.rondas.Count - 1].tableroViejo);
            Console.WriteLine("Cartas -1: " + mano.rondas[mano.rondas.Count - 2].board);
            }
            catch
            {

            }*/
            #endregion

            actualizarEnemigos();

            mano.actualizarInfoMano();
            if (mano.rondaActual.tableroViejo == "" && mano.rondas.Count > 1)
            {
                manos.Add(mano);
                mano = new Mano(miAsiento);
            }
        }

    }

    class Mano
    {
        public Ronda rondaActual,rondaAnterior;
        private Controlador controler = new Controlador();
        public List<Ronda> rondas = new List<Ronda>();
        public List<EnemigosGedientos> ganadores = new List<EnemigosGedientos>();
        public List<EnemigosGedientos> perdedores = new List<EnemigosGedientos>();
        public List<EnemigosGedientos> enemigos = new List<EnemigosGedientos>();
        int contadorRondas;
        private int miAsiento;
        public Mano(int miAsient)
        {
             rondaActual = new Ronda(miAsiento);
             rondaAnterior = new Ronda(miAsiento);
             contadorRondas = 0;
             miAsiento = miAsient;
            controler.miAsiento = miAsient;
        }

        public bool analizada;

        public void encontrarGanadores()
        {
            totalApostado();
            foreach (EnemigosGedientos gd in enemigos)
            {
                if (gd.cuantoGano != 0)
                {
                    ganadores.Add(gd);
                }
                else if (gd.mostroCartas() == true && gd.cuantoGano == 0)
                {
                    perdedores.Add(gd);
                }
            }

        }

        public void totalApostado()
        {
            foreach (EnemigosGedientos enemigo in rondas[rondas.Count-1].enemigos)
            {
                enemigos.Add(enemigo);
            }

            for (int i = 0; i < enemigos.Count; i++)
            {
                foreach (Ronda ronda in rondas)
                {
                    enemigos[i].totalApostado = enemigos[i].totalApostado + ronda.enemigos[i].apuesta;
                }
            }

        }

        private bool cambiaronApuestas()
        {

            var control = controler.buscarTablero();
            return (rondas[rondas.Count - 1].pozoRonda != controler.buscarPozo()) ? true : false;

        }

        private bool esPrimeraRonda()
        {
            return (rondas.Count == 0) ? true : false;
        }

        public void actualizarInfoMano()//Actualizador de la mano
        {

            if (rondaActual.cambioTablero())
            {
                rondas.Add(rondaActual);
                rondaActual = new Ronda(miAsiento);
            }
            else
            {
                rondaActual.actualizarEnemigoApuestas();
            }
        }

    }

    class Ronda
    {

        Controlador controler = new Controlador();

        public List<EnemigosGedientos> enemigos = new List<EnemigosGedientos>();
        private int miAsiento;
        public int enemigosVivos, pozoRonda;
        public string board,tableroViejo;

        public void actualizarEnemigoApuestas()
        {
            actualizarRonda();
        }

        public bool cambioTablero()
        {

            return (tableroViejo != controler.buscarTablero()) ? true : false;

        }

        public bool esPrimeraRonda()
        {
            return (controler.buscarTablero() == "") ? true : false;
        }

        public void actualizarRonda()
        {
            board = controler.buscarTablero();
            pozoRonda = controler.buscarPozo();
            controler.actualizarEnemigos(ref enemigos);
            enemigosVivos = 0;
            foreach (EnemigosGedientos enemigo in enemigos)
            {
                if (enemigo.vivo)
                {
                    enemigosVivos++;
                }
            }
        }

        public bool alguienMostroCartas()
        {
            bool booleano = false;
            foreach (var item in enemigos)
            {
                if (item.mostroCartas())
                {
                    booleano = true;
                }
            }
            return booleano;
        }

        public Ronda(int miAsient)
        {
            pozoRonda = controler.buscarPozo();
            board = controler.buscarTablero();
            tableroViejo = board;
            enemigos = controler.buscarEnemigos();
            enemigosVivos = 0;
            miAsiento = miAsient;
            controler.miAsiento = miAsient;
            foreach (EnemigosGedientos enemigo in enemigos)
            {
                if (enemigo.vivo == true)
                {
                    enemigosVivos++;
                }
            }
        }

    }

    class Controlador
    {

        public string misprobabilidades;
        Tablero tablero = new Tablero();
        Evaluador val = new Evaluador();
        public int miAsiento;
        List<EnemigosGedientos> posibleYo = new List<EnemigosGedientos>();

        #region declaracionVariables
        int direccionBase = 0;
        int direccionBase2 = 0;
        int offsetBase = 0x00F5A0E0;
        int offsetBase2 = 0x00F56E88;
        string process = "PokerStars";
        #endregion

        public bool esNuestroTurno(int miAsient)
        {
            int dirBaseAsiento = -0x0000031C; //para todos los asientos
            int read;
            int read1A1, read2A1, read3A1, read4A1;
            int read1A2, read2A2, read3A2, read4A2;
            int read1A3, read2A3, read3A3, read4A3;
            int read1A4, read2A4, read3A4, read4A4;
            int read1A5, read2A5, read3A5, read4A5;
            int read1A6, read2A6, read3A6, read4A6;

            VAMemory vam = new VAMemory();
            vam.processName = process;
            Process[] p = Process.GetProcessesByName(process);
            foreach (ProcessModule m in p[0].Modules)

                if (m.ModuleName == "THREADSTACK0")
                {
                    direccionBase = (int)m.BaseAddress;
                }


            switch (miAsient)
            {

                case 0:
                    //read = vam.ReadInt32((IntPtr)direccionBase + dirBaseAsiento);
                    read = 0x0EA4F458;
                    read1A1 = vam.ReadInt32((IntPtr)read + 0x4);
                    read2A1 = vam.ReadInt32((IntPtr)read1A1 + 0x17F0);
                    read3A1 = vam.ReadInt32((IntPtr)read2A1 + 0x10);
                    read4A1 = vam.ReadByte((IntPtr)read3A1 + 0x4);
                    return (read4A1 == 1) ? true : false;


                case 1:
                    read = 0x0A68E020;
                    //read = vam.ReadInt32((IntPtr)read);
                    read1A2 = vam.ReadInt32((IntPtr)read + 0x4);
                    read2A2 = vam.ReadInt32((IntPtr)read1A2 + 0x1A38);
                    read3A2 = vam.ReadInt32((IntPtr)read2A2 + 0x10);
                    read4A2 = vam.ReadByte((IntPtr)read3A2 + 0x4);
                    return (read4A2 == 1) ? true : false;


                case 2:
                    read = 0x0A7EB270;
                    read1A3 = vam.ReadInt32((IntPtr)read + 0x4);
                    read2A3 = vam.ReadInt32((IntPtr)read1A3 + 0x1C80);
                    read3A3 = vam.ReadInt32((IntPtr)read2A3 + 0x10);
                    read4A3 = vam.ReadByte((IntPtr)read3A3 + 0x4);
                    return (read4A3 == 1) ? true : false;


                case 3:
                    read = 0x0B513B68;
                    read1A4 = vam.ReadInt32((IntPtr)read + 0x4);
                    read2A4 = vam.ReadInt32((IntPtr)read1A4 + 0x1EC8);
                    read3A4 = vam.ReadInt32((IntPtr)read2A4 + 0x10);
                    read4A4 = vam.ReadByte((IntPtr)read3A4 + 0x4);
                    return (read4A4 == 1) ? true : false;


                case 4:
                    read = 0x0C903360;
                    read1A5 = vam.ReadInt32((IntPtr)read + 0x4);
                    read2A5 = vam.ReadInt32((IntPtr)read1A5 + 0x2110);
                    read3A5 = vam.ReadInt32((IntPtr)read2A5 + 0x10);
                    read4A5 = vam.ReadByte((IntPtr)read3A5 + 0x4);
                    return (read4A5 == 1) ? true : false;


                case 5:
                    read = 0x0A68E020;
                    read1A6 = vam.ReadInt32((IntPtr)read + 0x4);
                    read2A6 = vam.ReadInt32((IntPtr)read1A6 + 0x2358);
                    read3A6 = vam.ReadInt32((IntPtr)read2A6 + 0x10);
                    read4A6 = vam.ReadByte((IntPtr)read3A6 + 0x4);
                    return (read4A6 == 1) ? true : false;

                default:
                    return false;
            }
        }

        /*public bool laVerdadera() //nuestro turno
        {
            int anterior = 0;
            int posterior = conCuantoIr();

            if (anterior != posterior)
            {
                return true;
            }
            else
            {
                anterior = conCuantoIr();
                if (anterior != posterior)
                {
                    return true;
                }
                else
                {

                }
            }
        }*/

        public int conCuantoIr()
        {

            VAMemory vam = new VAMemory();
            vam.processName = process;
            Process[] p = Process.GetProcessesByName(process);
            foreach (ProcessModule m in p[0].Modules)
            {
                if (m.ModuleName == "PokerStars.exe")
                {
                    direccionBase = (int)m.BaseAddress;//Leer adress base

                }
                if (m.ModuleName == "THREADSTACK0")
                {
                    direccionBase2 = (int)m.BaseAddress;
                }
            }
            int read = vam.ReadInt32((IntPtr)direccionBase + offsetBase2);
            int read2 = vam.ReadInt32((IntPtr)read + 0x8);
            int read3 = vam.ReadInt32((IntPtr)read2 + 0x10);
            return vam.ReadInt32((IntPtr)read3 + 0x3A0);
        }

        private void eliminardePosibleYo(List<EnemigosGedientos> eliminar)
        {
            foreach (EnemigosGedientos enemigos in eliminar)
            {
                posibleYo.Remove(enemigos);
            }
        }

        public void cargarPosibleYolista()
        {
            Process[] p = Process.GetProcessesByName(process);
            foreach (ProcessModule m in p[0].Modules)
            {
                if (m.ModuleName == "PokerStars.exe")
                {
                    direccionBase = (int)m.BaseAddress;//Leer adress base

                }
                if (m.ModuleName == "THREADSTACK0")
                {
                    direccionBase2 = (int)m.BaseAddress;
                }
            }
            VAMemory vam = new VAMemory();
            vam.processName = process;
            int read = vam.ReadInt32((IntPtr)direccionBase + offsetBase);
            int read2 = vam.ReadInt32((IntPtr)read + 0x10);
            int read3 = vam.ReadInt32((IntPtr)read2 + 0x0);
            for (int i = 0; i < 6; i++)
            {
                EnemigosGedientos enemigo = new EnemigosGedientos();
                int multiplicador = i * 0x138;
                int nuevaApuesta = vam.ReadInt32((IntPtr)read3 + 0xF20 + multiplicador);

                #region buscarCartas
                string carta1 = vam.ReadByte((IntPtr)read3 + 0xF54 + multiplicador).ToString();
                string palo1 = vam.ReadByte((IntPtr)read3 + 0xF58 + multiplicador).ToString();
                string carta2 = vam.ReadByte((IntPtr)read3 + 0xF5C + multiplicador).ToString();
                string palo2 = vam.ReadByte((IntPtr)read3 + 0xF60 + multiplicador).ToString();
                carta1 = convertirCarta(carta1, palo1);
                carta2 = convertirCarta(carta2, palo2);
                string cartas = carta1 + " " + carta2;
                #endregion

                enemigo.ID = i;
                enemigo.carta1 = carta1;
                enemigo.carta2 = carta2;
                enemigo.pozo = vam.ReadInt32((IntPtr)read3 + 0x0F10 + multiplicador);
                enemigo.cartas = cartas;
                enemigo.cuantoGano = vam.ReadInt32((IntPtr)read3 + 0x0F28 + multiplicador);
                posibleYo.Add(enemigo);
            }
        }

        public bool tengoAsiento()
        {
            int caca = buscarPozo();
            List<EnemigosGedientos> auxlist = new List<EnemigosGedientos>();
            foreach (EnemigosGedientos enemigo in posibleYo)
            {
                if (enemigo.pozo!=buscarPozo())
                {
                    auxlist.Add(enemigo);
                }
            }
            eliminardePosibleYo(auxlist);
            miAsiento = (posibleYo.Count == 1) ? posibleYo.First().ID : -1;
            return (posibleYo.Count == 1) ? true : false;

        }

        public bool tengoBuenasCartasInicio()
        {
            int c1 = buscarMiCarta1Cruda();
            int p1 = buscarMiPalo1Crudo();
            int c2 = buscarMiCarta2Cruda();
            int p2 = buscarMiPalo2Crudo();

            return ((c1 > 10 && c2 > 10) || (c1 == c2) || ((p1 == p2) && (c1 > 8 && c2 > 8)) && (buscarTablero() == "")) ? true : false;

        }

        public bool tengoAceptablesCartasInicio()
        {
            int c1 = buscarMiCarta1Cruda();
            int p1 = buscarMiPalo1Crudo();
            int c2 = buscarMiCarta2Cruda();
            int p2 = buscarMiPalo2Crudo();

            return ((c1 >= 10 || c2 >= 10) || (c1 == c2) || (p1 == p2)) && (buscarTablero() == "") ? true : false;

        }
        public bool tengoExcelentesCartasInicio()
        {
            int c1 = buscarMiCarta1Cruda();
            int c2 = buscarMiCarta2Cruda();

            return ((c1 > 11 && c2 > 11) && (c1 == c2) && (buscarTablero() == "")) ? true : false;

        }

        public int buscarMiCarta1Cruda()
        {
            VAMemory vam = new VAMemory();
            vam.processName = process;
            int read = vam.ReadInt32((IntPtr)direccionBase + offsetBase2);
            int read2 = vam.ReadInt32((IntPtr)read + 0x8);
            int read3 = vam.ReadInt32((IntPtr)read2 + 0x10);
            int carta1 = vam.ReadByte((IntPtr)read3 + 0x1C60);
            return carta1;
        }

        public int buscarMiPalo1Crudo()
        {
            VAMemory vam = new VAMemory();
            vam.processName = process;
            int read = vam.ReadInt32((IntPtr)direccionBase + offsetBase);
            int read2 = vam.ReadInt32((IntPtr)read + 0x8);
            int read3 = vam.ReadInt32((IntPtr)read2 + 0x10);
            int palo1 = vam.ReadByte((IntPtr)read3 + 0x1C64);
            return palo1;
        }

        public int buscarMiPalo2Crudo()
        {
            VAMemory vam = new VAMemory();
            vam.processName = process;
            int read = vam.ReadInt32((IntPtr)direccionBase + offsetBase);
            int read2 = vam.ReadInt32((IntPtr)read + 0x8);
            int read3 = vam.ReadInt32((IntPtr)read2 + 0x10);
            int palo2 = vam.ReadByte((IntPtr)read3 + 0x1C70);
            return palo2;
        }

        public int buscarMiCarta2Cruda()
        {
            VAMemory vam = new VAMemory();
            vam.processName = process;
            int read = vam.ReadInt32((IntPtr)direccionBase + offsetBase);
            int read2 = vam.ReadInt32((IntPtr)read + 0x8);
            int read3 = vam.ReadInt32((IntPtr)read2 + 0x10);
            int carta2 = vam.ReadByte((IntPtr)read3 + 0x1C6C);
            return carta2;
        }

        public int buscarPozo()
        {
            VAMemory vam = new VAMemory();
            vam.processName = process;
            Process[] p = Process.GetProcessesByName(process);
            foreach (ProcessModule m in p[0].Modules)
            {
                if (m.ModuleName == "PokerStars.exe")
                {
                    direccionBase = (int)m.BaseAddress;//Leer adress base

                }
                if (m.ModuleName == "THREADSTACK0")
                {
                    direccionBase2 = (int)m.BaseAddress;
                }
            }
            int read = vam.ReadInt32((IntPtr)direccionBase + offsetBase2);
            int read2 = vam.ReadInt32((IntPtr)read + 0x8);
            int read3 = vam.ReadInt32((IntPtr)read2 + 0x10);
            return vam.ReadInt32((IntPtr)read3 + 0x0D00);

        }

        public int buscarCiegaPokerstars()
        {
            Process[] p = Process.GetProcessesByName(process);
            foreach (ProcessModule m in p[0].Modules)
            {
                if (m.ModuleName == "PokerStars.exe")
                {
                    direccionBase = (int)m.BaseAddress;//Leer adress base

                }
                if (m.ModuleName == "THREADSTACK0")
                {
                    direccionBase2 = (int)m.BaseAddress;
                }
            }
            VAMemory vam = new VAMemory();
            vam.processName = process;
            try
            {
            string[] splitMitad=p[0].MainWindowTitle.Split(new char[] { '/'});
            string splitCiega = splitMitad[1].Split(' ')[0].Replace("$", "");
            return Convert.ToInt32(splitCiega)*100;

            }
            catch
            {
                return 0;
            }
        }

        private string buscarmiCarta1()
        {
            VAMemory vam = new VAMemory();
            vam.processName = process;
            int read = vam.ReadInt32((IntPtr)direccionBase + offsetBase);
            int read2 = vam.ReadInt32((IntPtr)read + 0x8);
            int read3 = vam.ReadInt32((IntPtr)read2 + 0x10);
            string carta1 = vam.ReadByte((IntPtr)read3 + 0x1C60).ToString();
            string palo1 = vam.ReadByte((IntPtr)read3 + 0x1C64).ToString();
            carta1 = convertirCarta(carta1, palo1);
            return carta1;
        }

        private string buscarmiCarta2()
        {
            VAMemory vam = new VAMemory();
            vam.processName = process;
            int read = vam.ReadInt32((IntPtr)direccionBase + offsetBase);
            int read2 = vam.ReadInt32((IntPtr)read + 0x8);
            int read3 = vam.ReadInt32((IntPtr)read2 + 0x10);
            string carta2 = vam.ReadByte((IntPtr)read3 + 0x1C6C).ToString();
            string palo2 = vam.ReadByte((IntPtr)read3 + 0x1C70).ToString();
            carta2 = convertirCarta(carta2, palo2);
            return carta2;
        }

        private string buscarMiscartas()
        {
            VAMemory vam = new VAMemory();
            vam.processName = process;
            int read = vam.ReadInt32((IntPtr)direccionBase + offsetBase2);
            int read2 = vam.ReadInt32((IntPtr)read + 0x8);
            int read3 = vam.ReadInt32((IntPtr)read2 + 0x10);
            string carta1 = vam.ReadByte((IntPtr)read3 + 0x1C60).ToString();
            string palo1 = vam.ReadByte((IntPtr)read3 + 0x1C64).ToString();
            string carta2 = vam.ReadByte((IntPtr)read3 + 0x1C6C).ToString();
            string palo2 = vam.ReadByte((IntPtr)read3 + 0x1C70).ToString();
            carta1 = convertirCarta(carta1, palo1);
            carta2 = convertirCarta(carta2, palo2);
            string misCartas = carta1 + " " + carta2;
            return misCartas;
        }

        public string buscarTablero()
        {
            string todoTablero = "";
            Process[] p = Process.GetProcessesByName(process);
            foreach (ProcessModule m in p[0].Modules)
            {
                if (m.ModuleName == "PokerStars.exe")
                {
                    direccionBase = (int)m.BaseAddress;//Leer adress base

                }
                if (m.ModuleName == "THREADSTACK0")
                {
                    direccionBase2 = (int)m.BaseAddress;
                }
            }

            VAMemory vam = new VAMemory();
            vam.processName = process;
            int read = vam.ReadInt32((IntPtr)direccionBase + offsetBase);
            int read2 = vam.ReadInt32((IntPtr)read + 0x10);
            int read3 = vam.ReadInt32((IntPtr)read2 + 0x0);
            int pote = vam.ReadInt32((IntPtr)read3 + 0x0D00);

            tablero.cantidad = vam.ReadInt32((IntPtr)read3 + 0x0D38);
            tablero.cartas[0] = convertirCarta(vam.ReadInt32((IntPtr)read3 + 0x0D44).ToString(), vam.ReadInt32((IntPtr)read3 + 0x0D48).ToString());
            tablero.cartas[1] = convertirCarta(vam.ReadInt32((IntPtr)read3 + 0x0D4C).ToString(), vam.ReadInt32((IntPtr)read3 + 0x0D50).ToString());
            tablero.cartas[2] = convertirCarta(vam.ReadInt32((IntPtr)read3 + 0x0D54).ToString(), vam.ReadInt32((IntPtr)read3 + 0x0D58).ToString());
            tablero.cartas[3] = convertirCarta(vam.ReadInt32((IntPtr)read3 + 0x0D5C).ToString(), vam.ReadInt32((IntPtr)read3 + 0x0D60).ToString());
            tablero.cartas[4] = convertirCarta(vam.ReadInt32((IntPtr)read3 + 0x0D64).ToString(), vam.ReadInt32((IntPtr)read3 + 0x0D68).ToString());

            for (int i = 0; i < tablero.cantidad; i++)
            {
                todoTablero = todoTablero + tablero.cartas[i] + " ";
            }
            todoTablero = todoTablero.Trim();
            return todoTablero;
        }

        public List<EnemigosGedientos> buscarEnemigos()
        {
            Process[] p = Process.GetProcessesByName(process);
            foreach (ProcessModule m in p[0].Modules)
            {
                if (m.ModuleName == "PokerStars.exe")
                {
                    direccionBase = (int)m.BaseAddress;//Leer adress base

                }
                if (m.ModuleName == "THREADSTACK0")
                {
                    direccionBase2 = (int)m.BaseAddress;
                }
            }
            VAMemory vam = new VAMemory();
            vam.processName = process;
            int read = vam.ReadInt32((IntPtr)direccionBase + offsetBase);
            int read2 = vam.ReadInt32((IntPtr)read + 0x10);
            int read3 = vam.ReadInt32((IntPtr)read2 + 0x0);
            List<EnemigosGedientos> lista = new List<EnemigosGedientos>();
            //            0x30 para bit de vida o no
            for (int i = 0; i < 6; i++)
            {
                    EnemigosGedientos enemigo = new EnemigosGedientos();
                int multiplicador = i * 0x138;
                #region buscarCartas
                string carta1 = vam.ReadByte((IntPtr)read3 + 0xF54 + multiplicador).ToString();
                string palo1 = vam.ReadByte((IntPtr)read3 + 0xF58 + multiplicador).ToString();
                string carta2 = vam.ReadByte((IntPtr)read3 + 0xF5C + multiplicador).ToString();
                string palo2 = vam.ReadByte((IntPtr)read3 + 0xF60 + multiplicador).ToString();
                carta1 = convertirCarta(carta1, palo1);
                carta2 = convertirCarta(carta2, palo2);
                string cartas = carta1 + " " + carta2;
                #endregion
                enemigo.ID = i;
                enemigo.carta1 = carta1;
                enemigo.carta2 = carta2;
                enemigo.pozo = vam.ReadInt32((IntPtr)read3 + 0x0F10 + multiplicador);
                enemigo.apuesta = vam.ReadInt32((IntPtr)read3 + 0xF20 + multiplicador);
                enemigo.cuantoGano = vam.ReadInt32((IntPtr)read3 + 0x0F28 + multiplicador);
                enemigo.cartas = cartas;
                if (vam.ReadByte((IntPtr)read3 + 0x0F40 + multiplicador) == 1)
                {
                    enemigo.vivo = true;
                }
                else 
                {
                    enemigo.vivo = false;
                }
                lista.Add(enemigo);
            }
            return lista;
        }

        public void actualizarEnemigos(ref List<EnemigosGedientos> enemigos)
        {
            Process[] p = Process.GetProcessesByName(process);
            foreach (ProcessModule m in p[0].Modules)
            {
                if (m.ModuleName == "PokerStars.exe")
                {
                    direccionBase = (int)m.BaseAddress;//Leer adress base

                }
                if (m.ModuleName == "THREADSTACK0")
                {
                    direccionBase2 = (int)m.BaseAddress;
                }
            }
            VAMemory vam = new VAMemory();
            vam.processName = process;
            int read = vam.ReadInt32((IntPtr)direccionBase + offsetBase);
            int read2 = vam.ReadInt32((IntPtr)read + 0x10);
            int read3 = vam.ReadInt32((IntPtr)read2 + 0x0);

            for (int i = 0; i < 6; i++)
            {
                if (!(i==miAsiento))
                {
                int multiplicador = i * 0x138;
                int nuevaApuesta = vam.ReadInt32((IntPtr)read3 + 0xF20 + multiplicador);

                #region buscarCartas
                string carta1 = vam.ReadByte((IntPtr)read3 + 0xF54 + multiplicador).ToString();
                string palo1 = vam.ReadByte((IntPtr)read3 + 0xF58 + multiplicador).ToString();
                string carta2 = vam.ReadByte((IntPtr)read3 + 0xF5C + multiplicador).ToString();
                string palo2 = vam.ReadByte((IntPtr)read3 + 0xF60 + multiplicador).ToString();
                carta1 = convertirCarta(carta1, palo1);
                carta2 = convertirCarta(carta2, palo2);
                string cartas = carta1 + " " + carta2;
                #endregion

                enemigos[i].ID = i;
                enemigos[i].carta1 = carta1;
                enemigos[i].carta2 = carta2;
                enemigos[i].pozo = vam.ReadInt32((IntPtr)read3 + 0x0F10 + multiplicador);
                enemigos[i].cartas = cartas;
                enemigos[i].cuantoGano = vam.ReadInt32((IntPtr)read3 + 0x0F28 + multiplicador);

               if (nuevaApuesta!=0)
                {
                    enemigos[i].apuesta = nuevaApuesta;
                }
                }
            }
        }

        public string convertirCarta(string carta, string palo)
        {
            switch (carta)
            {
                case "11":
                    carta = "j";
                    break;
                case "12":
                    carta = "q";
                    break;
                case "13":
                    carta = "k";
                    break;
                case "14":
                    carta = "a";
                    break;
                default:
                    break;
            }
            switch (palo)
            {
                case "99":
                    palo = "s";
                    break;
                case "115":
                    palo = "d";
                    break;
                case "104":
                    palo = "h";
                    break;
                case "100":
                    palo = "c";
                    break;
                default:
                    break;
            }
            string retornado = carta + palo;
            return retornado;
        }

        /*private string probabilidadesSinTablero(string misCartas)
        {
            
        } */

        public double buscarMisProbabilidades()
        {
            string todoTablero = "";
            string misCartas = "";
            Process[] p = Process.GetProcessesByName(process);
            foreach (ProcessModule m in p[0].Modules)
            {
                if (m.ModuleName == "PokerStars.exe")
                {
                    direccionBase = (int)m.BaseAddress;//Leer adress base

                }
                if (m.ModuleName == "THREADSTACK0")
                {
                    direccionBase2 = (int)m.BaseAddress;
                }
            }

            todoTablero = buscarTablero();

            List<EnemigosGedientos> lista = buscarEnemigos();

            int nroplayers = 0;
            foreach (EnemigosGedientos enemigo in lista)
            {
                if (enemigo.vivo == true)
                {
                    nroplayers++;
                }
            }
            nroplayers--;

            misCartas = buscarMiscartas();

            if (misCartas.Split(' ')[1] == "00")
            {
                return 0;
            }

            else if (tablero.cantidad == 0)
            {
                return 0;
                /* misprobabilidades = probabilidadesSinTablero(); */
            }
            else
            {
                return val.misProbabilidades(misCartas, todoTablero, nroplayers);
            }
        }//Actualizador del controlador


    }
}
