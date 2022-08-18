using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TheTrader.Controles
{
    class PrintingController
    {

        static public void ShowStartupAnimation()
        {


            Console.CursorVisible = false;

            var arr = new[]
             {
              " .d8888b.                    888            888 888                   ",
              "d88P  Y88b                   888            888 888                   ",
              "888    888                   888            888 888                   ",
              "888         8888b.  .d8888b  888888 .d88b.  888 888  8888b.  88888b.  ",
              "888            \"88b 88K      888   d8P  Y8b 888 888     \"88b 888 \"88b ",
              "888    888 .d888888 \"Y8888b. 888   88888888 888 888 .d888888 888  888 ",
              "Y88b  d88P 888  888      X88 Y88b. Y8b.     888 888 888  888 888  888 ",
              " \"Y8888P\"  \"Y888888  88888P'  \"Y888 \"Y8888  888 888 \"Y888888 888  888",

              "8888888                                     888                                    888    ",
              "  888                                       888                                    888    ",
              "  888                                       888                                    888    ",
              "  888   88888b.  888  888  .d88b.  .d8888b  888888 88888b.d88b.   .d88b.  88888b.  888888 ",
              "  888   888 \"88b 888  888 d8P  Y8b 88K      888    888 \"888 \"88b d8P  Y8b 888 \"88b 888    ",
              "  888   888  888 Y88  88P 88888888 \"Y8888b. 888    888  888  888 88888888 888  888 888    ",
              "  888   888  888  Y8bd8P  Y8b.          X88 Y88b.  888  888  888 Y8b.     888  888 Y88b.  ",
              "8888888 888  888   Y88P    \"Y8888   88888P'  \"Y888 888  888  888  \"Y8888  888  888  \"Y888",

              " .d8888b.  888          888      ",
              "d88P  Y88b 888          888      ",
              "888    888 888          888      ",
              "888        888 888  888 88888b.  ",
              "888        888 888  888 888 \"88b ",
              "888    888 888 888  888 888  888 ",
              "Y88b  d88P 888 Y88b 888 888 d88P ",
              " \"Y8888P\"  888  \"Y88888 88888P\"",
            };

            var maxLength = arr.Aggregate(0, (max, line) => Math.Max(max, line.Length));
            var x = Console.BufferWidth / 2 - maxLength / 2;
            for (int y = -arr.Length; y < Console.WindowHeight + arr.Length; y++)
            {
                ConsoleDraw(arr, x, y);
                Thread.Sleep(100);
            }
        }


        static void ConsoleDraw(IEnumerable<string> lines, int x, int y)
        {
            if (x > Console.WindowWidth) return;
            if (y > Console.WindowHeight) return;

            var trimLeft = x < 0 ? -x : 0;
            int index = y;

            x = x < 0 ? 0 : x;
            y = y < 0 ? 0 : y;

            var linesToPrint =
                from line in lines
                let currentIndex = index++
                where currentIndex > 0 && currentIndex < Console.WindowHeight
                select new
                {
                    Text = new String(line.Skip(trimLeft).Take(Math.Min(Console.WindowWidth - x, line.Length - trimLeft)).ToArray()),
                    X = x,
                    Y = y++
                };

            Console.Clear();
            foreach (var line in linesToPrint)
            {
                Console.SetCursorPosition(line.X, line.Y);
                Console.Write(line.Text);
            }
        }



       

    }
}
