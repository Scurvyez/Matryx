using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matryx
{
    public class Matryx
    {
        // Lets piece together that cool matrix effect!
        // Smoothness of the falling effect is linked directly to PC hardware & window size.

        public static void Main()
        {
            Console.Title = "Matryx Shit";

            // Makes console window max available screen size.
            //Console.WindowHeight = Console.BufferHeight = Console.LargestWindowHeight;
            //Console.WindowWidth = Console.BufferWidth = Console.LargestWindowWidth;
            Console.CursorVisible = false;

            Initialize(out int w, out int h, out int[] y, out int[] l);

            while (true)
            {
                // Keep creating new falling columns.
                MatrixStep(w, h, y, l);
                
                // Determine speed of falling columns.
                // Different depending on window size and PC.
                System.Threading.Thread.Sleep(40);

                // Refreshes the console, if needed. (Window re-sizing)
                if (Console.KeyAvailable)
                    if (Console.ReadKey().Key == ConsoleKey.F5)
                        Initialize(out w, out h, out y, out l);
            }
        }

        // Get new random.
        public static Random r = new Random();

        // int y and int l are arrays.
        public static void Initialize(out int width, out int height, out int[] y, out int[] l)
        {
            int heightOne;

            // Sets length of falling columns, relative to window size.
            int heightTwo = heightOne = (height = Console.WindowHeight) / 7;
            width = Console.WindowWidth - 1;
            y = new int[width];
            l = new int[width];
            int x;
            // W/o clearing the console the old characters will remain and the console will fill up.
            Console.Clear();

            for (x = 0; x < width; ++x)
            {
                y[x] = r.Next(height - 1);
                l[x] = r.Next(heightTwo * ((x % 11 != 4) ? 2 : 1), heightOne * ((x % 11 != 4) ? 2 : 1));
            }
        }

        private static void MatrixStep(int width, int height, int[] y, int[] l)
        {
            int x;

            for (x = 0; x < width; ++x)
            {
                if (x % 11 == 10)
                {
                    // Any column without a leading character.
                    // To spice things up. lol
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                else
                {
                    // The typical color of most characters.
                    Console.ForegroundColor = ConsoleColor.Red;
                    // Make first character in each column white.
                    Console.SetCursorPosition(x, Columns(y[x] - 1 - (l[x] / 25), height));
                    Console.Write(RandChar());
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.SetCursorPosition(x, y[x]);
                Console.Write(RandChar());
                // Keeps columns shifting down by 1 every frame.
                // Anything more than "+ 1" and it breaks.
                y[x] = Columns(y[x] + 1, height);
                Console.SetCursorPosition(x, Columns(y[x] - l[x], height));
                Console.Write(value: " ");
            }
        }

        static char RandChar()
        {
            // Generates a random ASCII character.
            int i = r.Next(1, 3) == 1 ? r.Next(48, 57) : r.Next(192, 255);
            char randomLetter = Convert.ToChar(i);

            return randomLetter;
        }

        public static int Columns(int n, int height)
        {
            n %= height;
            if (n < 0) return n + height;
            else return n;
        }
    }
}
