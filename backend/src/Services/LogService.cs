using OriolOr.KanjiDome.Entities;

namespace OriolOr.KanjiDome.Services
{
    public class LogService
    {
        public Match Match;
        public LogService(Match match)
        {
            this.Match = match;
        }

        public void PrintUserDeck()
        {
            Console.ResetColor();
            Console.WriteLine(" ---------------------------------------- ");

            Console.Write("UserDeck 1:\t");
            Console.ForegroundColor = this.Match.UserDeck[0].Color;
            Console.WriteLine(this.Match.UserDeck[0].Type);

            Console.ResetColor();

            Console.Write("UserDeck 2:\t");
            Console.ForegroundColor = Match.UserDeck[1].Color;
            Console.WriteLine(Match.UserDeck[1].Type);

            Console.ForegroundColor = ConsoleColor.White;
        }

        public void PrintTypeTable()
        {
            foreach (var card in this.Match.CardsDeck)
            {
                Console.WriteLine(" ");
                Console.ForegroundColor = card.Color;
                Console.Write(card.Type);
                Console.ResetColor();
                Console.Write(" -> \t");
                Console.ForegroundColor = this.Match.CardsDeck.FirstOrDefault(c => c.Type == card.Strength[0]).Color;
                Console.Write(card.Strength[0]);
                Console.ResetColor();
                Console.Write(" ");
                Console.ForegroundColor = this.Match.CardsDeck.FirstOrDefault(c => c.Type == card.Strength[1]).Color;
                Console.WriteLine(card.Strength[1]);
                Console.ResetColor();
            }
        }

        public void PrintUserInfo(int inputCard)
        {
            Console.WriteLine(" ");
            Console.Write("User Selection: \t[");
            Console.ForegroundColor = Match.UserDeck[inputCard].Color;
            Console.Write(Match.UserDeck[inputCard].Type.ToString());
            Console.ResetColor();
            Console.WriteLine("]");
        }


        public void PrintBotInfo(int num)
        {
            Console.Write("Bot has selected: \t[");
            Console.ForegroundColor = Match.BotDeck[num].Color;
            Console.Write(Match.BotDeck[num].Type.ToString());
            Console.ResetColor();
            Console.WriteLine("]");
        }
    }
}
