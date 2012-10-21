using System;
namespace PiratesArr
{
#if WINDOWS || XBOX

    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Main game = new Main();
            game.Run();
        }
    }

#endif
}