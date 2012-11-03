namespace Pirates
{
#if WINDOWS || XBOX

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
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