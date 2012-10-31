using System;
using Pirates;

namespace Pirates
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (BaseClass game = BaseClass.GetInstance())
            {
                game.Run();
            }
        }
    }
#endif
}

