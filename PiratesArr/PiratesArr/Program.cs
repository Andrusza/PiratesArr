namespace PiratesArr
{
#if WINDOWS || XBOX

    internal static class Program
    {
        private static void Main(string[] args)
        {
            Main game = new Main();
            game.Run();
        }
    }

#endif
}