namespace Pirates
{
#if WINDOWS || XBOX

    internal static class Program
    {
        private static void Main()
        {
            using (BaseClass.GetInstance())
            {
                BaseClass.GetInstance().Run();
            }
        }
    }

#endif
}