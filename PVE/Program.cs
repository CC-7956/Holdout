

namespace PVE;

class Program
{
    static void Main(string[] args)
    {
        GamePlay.CreatePlayer();
        GamePlay.StartGame();

        GamePlay.SaveAndExit();
    }
}