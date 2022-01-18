using OriolOr.KanjiDome.Entities;
using Newtonsoft.Json;

namespace OriolOr.KanjiDome.Services
{
    public class KanjiService
    {
        private static readonly Random random = new Random();
        private readonly Match Match;
        private LogService LogService;

        public  KanjiService(){
            
            this.Match = new Match();
            this.LogService = new LogService(this.Match);
        }

        public bool CheckType(KanjiCard userCard, KanjiCard botCard)
        {
            var userWins = false;
            foreach(var typeStrength in userCard.Strength)
            {
                if (botCard.Type == typeStrength)
                {
                    userWins = true;
                }
            }
            return userWins;
        }

        public void FlushKanji(List<KanjiCard> CardDeck) {

            Match.UserDeck = new List<KanjiCard>();
            Match.BotDeck = new List<KanjiCard>();

            var cardOrderList = this.GetRandomSequenceWithoutRepeat(0,5,5);
            var userDeckint = cardOrderList.GetRange(0, 2);
            Match.UserDeck.Add(CardDeck[userDeckint[0]]);
            Match.UserDeck.Add(CardDeck[userDeckint[1]]);

            var botDeckint = cardOrderList.GetRange(2, 2);
            Match.BotDeck.Add(CardDeck[botDeckint[0]]);
            Match.BotDeck.Add(CardDeck[botDeckint[1]]);

            var notUsedCard = cardOrderList.GetRange(4, 1)[0];
            Match.UnnusedCard = CardDeck[notUsedCard];

            this.LogService.PrintUserDeck();
        }

        public List<KanjiCard> LoadDeck() { 
            
           var cardList = new List<KanjiCard>();

            this.FillKanjiInfo(cardList,KanjiType.Water, "水",ConsoleColor.Blue,KanjiType.Ground,KanjiType.Fire);
            this.FillKanjiInfo(cardList, KanjiType.Fire, "火", ConsoleColor.DarkRed, KanjiType.Wind, KanjiType.Electricity);
            this.FillKanjiInfo(cardList, KanjiType.Wind, "風", ConsoleColor.Cyan, KanjiType.Ground, KanjiType.Water);
            this.FillKanjiInfo(cardList, KanjiType.Ground, "土", ConsoleColor.Red, KanjiType.Fire, KanjiType.Electricity);
            this.FillKanjiInfo(cardList, KanjiType.Electricity, "電", ConsoleColor.Yellow, KanjiType.Wind, KanjiType.Water);

            return cardList;

        }

        public void FillKanjiInfo(List<KanjiCard> kanjiCardList ,KanjiType type, string kanjiSymbol, ConsoleColor color, KanjiType type1, KanjiType type2)
        {
            KanjiCard card = new KanjiCard
            {
                Type = type,
                KanjiSymbol = kanjiSymbol,
                Color = color,
                
            };
            card.Strength = new List<KanjiType>();

            card.Strength.Add(type1);
            card.Strength.Add(type2);

            kanjiCardList.Add(card);
        }

        public KanjiCard UserSelection()
        {
            var valid = false;
            var inputCard = -1;

            while (valid == false)
            {
                inputCard = short.Parse(Console.ReadLine());

                if (inputCard == 0 || inputCard == 1)
                {
                    this.LogService.PrintUserInfo(inputCard);
                    valid = true;
                }
                else if(inputCard == 2)
                {
                    this.LogService.PrintTypeTable();
                }
            }

            return Match.UserDeck[inputCard];
        }

        public KanjiCard BotSelection()
        {
            var num = random.Next(0, 1);
            var selectedCard = Match.BotDeck[num];

            this.LogService.PrintBotInfo(num);

            return selectedCard;
        }
        public void PlayGame() {

            this.Match.CardsDeck = LoadDeck();
            this.FlushKanji(this.Match.CardsDeck);
 
            var userSelection = this.UserSelection();
            var botSelection = this.BotSelection();

            var userWins = CheckType(userSelection, botSelection);
            Console.WriteLine(" ");

            if (userWins == true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(userSelection.Type.ToString() + " wins to " + botSelection.Type.ToString() + ".You win !!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(botSelection.Type.ToString() + " wins to " + userSelection.Type.ToString() + ". You lose...");
            }

            Console.ResetColor();
            Console.ReadKey();
        }


        private List<int> GetRandomSequenceWithoutRepeat(int min, int max, int arrayLength)
        {
            var numList = new List<int>();

            while (numList.Count < arrayLength)
            {
                var num = random.Next(min, max);
                if (!numList.Contains(num)) numList.Add(num);
            }

            return numList;
        }
    }
}
