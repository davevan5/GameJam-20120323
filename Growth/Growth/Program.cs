using System;

namespace Growth
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (GrowthGame game = new GrowthGame())
            {
                game.Run();
            }
        }
    }
#endif
}

