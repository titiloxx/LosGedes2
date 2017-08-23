using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Windows.Input;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace ConsoleApp7
{

    public class Win32
    {
        [DllImport("User32.Dll")]
        public static extern long SetCursorPos(int x, int y);

        [DllImport("User32.Dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref POINT point);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }
    }

    public class Clicking
    {

        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);
        private const UInt32 MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const UInt32 MOUSEEVENTF_LEFTUP = 0x0004;

        // public static void SendClick(Point location)
        public static void SendClick()
        {
            // Cursor.Position = location;
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, new System.UIntPtr());
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, new System.UIntPtr());
        }
    }
    class Bot
    {
        // MOUSE
        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        // move relative from where the cursor is
        public static void Move(int xDelta, int yDelta)
        {

            mouse_event(0x0001, xDelta, yDelta, 0, 0);
        }

        public void Move(int xDelta, int yDelta, int timeInMilliseconds, bool async = false)
        {
            // No need to move the mouse at all.
            if (xDelta == 0 && yDelta == 0) return;

            // No need to move smoothly.
            if (timeInMilliseconds <= 0)
            {
                Move(xDelta, yDelta);
                return;
            }

            // Set direction factors and then make the delta's positive
            var xFactor = 1;
            var yFactor = 1;

            if (xDelta < 0)
            {
                xDelta *= (xFactor = -1);
            }

            if (yDelta < 0)
            {
                yDelta *= (yFactor = -1);
            }

            // Calculate the rates of a single x or y movement, in milliseconds
            // And avoid dividing by zero
            var xRate = xDelta == 0 ? -1 : (double)timeInMilliseconds / xDelta;
            var yRate = yDelta == 0 ? -1 : (double)timeInMilliseconds / yDelta;

            // Make a thread that will move the mouse in the x direction
            var xThread = new Thread(() =>
            {
                // No need to move in the x direction
                if (xDelta == 0) return;

                var sw = Stopwatch.StartNew();
                var c = 1;

                for (var i = 0; i < xDelta; i++)
                {
                    // Wait for another "rate" amount of time to pass
                    while (sw.ElapsedMilliseconds / xRate < c)
                    {
                    }

                    c++;

                    // Move by a single pixel (x)
                    Move(xFactor, 0);
                }
            });

            // Make a thread that will move the mouse in the y direction
            var yThread = new Thread(() =>
            {
                // No need to move in the y direction
                if (yDelta == 0) return;

                var sw = Stopwatch.StartNew();
                var c = 1;

                for (var i = 0; i < yDelta; i++)
                {
                    // Wait for another "rate" amount of time to pass
                    while (sw.ElapsedMilliseconds / yRate < c)
                    {
                    }

                    c++;

                    // Move by a single pixel (y)
                    Move(0, yFactor);
                }
            });

            // Activate the movers threads
            xThread.Start();
            yThread.Start();

            if (async)
            {
                return;
            }

            // Wait for both to end (remove this if you want it async)
            xThread.Join();
            yThread.Join();
        }
        // FIN MOUSE 


        public bool irTabladeOdds(int outs, int apuesta, int pozo)
        {
            double multiplicador, resultado;
            switch (outs)
            {
                case 2:
                    multiplicador = 22.5;
                    break;
                case 3:
                    multiplicador = 14.6;
                    break;
                case 4:
                    multiplicador = 10.7;
                    break;
                case 5:
                    multiplicador = 8.4;
                    break;
                case 6:
                    multiplicador = 6.8;
                    break;
                case 7:
                    multiplicador = 5.7;
                    break;
                case 8:
                    multiplicador = 4.9;
                    break;
                case 9:
                    multiplicador = 4.2;
                    break;
                case 10:
                    multiplicador = 3.7;
                    break;
                case 11:
                    multiplicador = 3.3;
                    break;
                case 12:
                    multiplicador = 2.9;
                    break;
                case 13:
                    multiplicador = 2.6;
                    break;
                case 14:
                    multiplicador = 1.9;
                    break;
                case 15:
                    multiplicador = 2.1;
                    break;
                case 16:
                    multiplicador = 1.9;
                    break;
                default:
                    multiplicador = 100000;
                    break;
            }
            return (multiplicador * apuesta < pozo) ? true : false;

        }
        public void pasar()
        {
            Random rnd = new Random();
            int velocidad = rnd.Next(1000, 3000);
            int sleepvar = rnd.Next(1000, 4000);
            int x = rnd.Next(1145, 1165);
            int y = rnd.Next(684, 695);
            Move(x - Cursor.Position.X, y - Cursor.Position.Y, velocidad, false);
            Thread.Sleep(sleepvar);
            Clicking.SendClick();
            //int rx = rnd.Next(0, 1365);
            //int ry = rnd.Next(0, 767);
            // Move(rx - Cursor.Position.X, ry - Cursor.Position.Y, 1000, false);
        }
        public void irse() //cuando no queda otra
        {
            Random rnd = new Random();
            int velocidad = rnd.Next(2000, 3000);
            int sleepvar = rnd.Next(1000, 4000);
            int x = rnd.Next(1011, 1021);
            int y = rnd.Next(678, 688);
            Move(x - Cursor.Position.X, y - Cursor.Position.Y, velocidad, false);
            Thread.Sleep(sleepvar);
            Clicking.SendClick();
            //int rx = rnd.Next(0, 1365);
            //int ry = rnd.Next(0, 767);
            // Move(rx - Cursor.Position.X, ry - Cursor.Position.Y, 1000, false);
        }
        public void subir() //subir
        {
            Random rnd = new Random();
            int velocidad = rnd.Next(1000, 3000);
            int sleepvar = rnd.Next(1000, 4000);
            int x = rnd.Next(1276, 1286);
            int y = rnd.Next(684, 695);
            Move(x - Cursor.Position.X, y - Cursor.Position.Y, velocidad, false);
            Thread.Sleep(sleepvar);
            Clicking.SendClick();
            //int rx = rnd.Next(0, 1365);
            //int ry = rnd.Next(0, 767);
           // Move(rx - Cursor.Position.X, ry - Cursor.Position.Y, 1000, false);
        }
        public void allIn()
        {
            Random rnd = new Random();
            int velocidad = rnd.Next(1000, 3000);
            int sleepvar = rnd.Next(1000, 4000);
            int x1 = 1315;
            int y1 = 618;
            int x2 = rnd.Next(1276, 1286);
            int y2 = rnd.Next(684, 694);
            Move(x1 - Cursor.Position.X, y1 - Cursor.Position.Y, velocidad, false);
            Thread.Sleep(50);
            Clicking.SendClick();
            Thread.Sleep(sleepvar);
            Move(x2 - Cursor.Position.X, y2 - Cursor.Position.Y, velocidad, false);
            Thread.Sleep(50);
            Clicking.SendClick();
            //int rx = rnd.Next(0, 1365);
            //int ry = rnd.Next(0, 767);
            // Move(rx - Cursor.Position.X, ry - Cursor.Position.Y, 1000, false);
        }
        public void igualar() // cuando no queda otra
        {
            Random rnd = new Random();
            int velocidad = rnd.Next(1000, 3000);
            int sleepvar = rnd.Next(1000, 4000);
            int x = rnd.Next(1142, 1152);
            int y = rnd.Next(683, 693);
            Move(x - Cursor.Position.X, y - Cursor.Position.Y, velocidad, false);
            Thread.Sleep(sleepvar);
            Clicking.SendClick();
            //int rx = rnd.Next(0, 1365);
            //int ry = rnd.Next(0, 767);
            // Move(rx - Cursor.Position.X, ry - Cursor.Position.Y, 1000, false);
        }
        
        public void apostar(int cantidad) //cantidad pasada en forma pozo*numero
        {
            Random rnd = new Random();
            int cantidadClicks = cantidad / 100;
            int velocidad = rnd.Next(2000, 3000);
            Move(1239 - Cursor.Position.X, 646 - Cursor.Position.Y, 1000, false); //posicion barra
            Thread.Sleep(2000);
            for (int i = 0; i < cantidadClicks; i++)
            {
                Clicking.SendClick();
                Thread.Sleep(500);
            }

            Move(1285 - Cursor.Position.X, 688 - Cursor.Position.Y, velocidad, false); //posicion apostar
            Thread.Sleep(50);
            Clicking.SendClick();

        }
    }
}
