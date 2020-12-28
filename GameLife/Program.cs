using SFML.Graphics;
using SFML.Window;

namespace GameLife
{
    class Program
    {
        static void Main(string[] args)
        {
            var window = new RenderWindow(new VideoMode(840, 920), "Game of Life", Styles.Default);
            window.SetVerticalSyncEnabled(true);

            Core core = new Core(window);
            core.Run();
        }

    }
}
