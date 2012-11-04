namespace Pirates
{
#if WINDOWS || XBOX

    internal static class Program
    {
        
        private static void Main(string[] args)
        {
            using (BaseClass game = BaseClass.GetInstance())
            {
                game.Run();
            }
        }
    }

#endif
}