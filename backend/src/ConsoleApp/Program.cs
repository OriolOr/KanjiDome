
using OriolOr.KanjiDome.Services;

public class Program
{
    public static void Main(string[] args)
    {
        while (true)
        {
            KanjiService kanjiService = new KanjiService();
            
            Console.Clear();
            
            Console.WriteLine("Kanji Dome 0.1");
            
            kanjiService.PlayGame();
        }

    }
}

