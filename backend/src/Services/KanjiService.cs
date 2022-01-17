﻿using OriolOr.KanjiDome.Entities;

namespace OriolOr.KanjiDome.Services
{
    public class KanjiService
    {
        private static readonly Random random = new Random();
        private readonly Match Match;

        public  KanjiService(){
            this.Match = new Match();
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

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" ---------------------------------------- ");

            Console.ForegroundColor = Match.UserDeck[0].Color;
            Console.WriteLine("UserDeck 1:\t" + Match.UserDeck[0].Type);

            Console.ForegroundColor = Match.UserDeck[1].Color;
            Console.WriteLine("UserDeck 2:\t" + Match.UserDeck[1].Type);

            Console.ForegroundColor = ConsoleColor.White;

        }

        public  List<int> GetRandomSequenceWithoutRepeat(int min, int max, int arrayLength)
        {
            var numList = new List<int>();

            while (numList.Count < arrayLength)
            {
                var num = random.Next(min, max);
                if (!numList.Contains(num)) numList.Add(num);
            }

            return numList;
        }

        public List<KanjiCard> GenerateDeck() { 
            
           var crdList = new List<KanjiCard>();

            KanjiCard crd = new KanjiCard();
            crd.Strength = new List<KanjiType>();

            crd.Type = KanjiType.Water;
            crd.KanjiSymbol = "水";
            crd.Color = ConsoleColor.Blue;
            
            crd.Strength.Add(KanjiType.Ground);
            crd.Strength.Add(KanjiType.Fire);


            KanjiCard crd2 = new KanjiCard();
            crd2.Strength = new List<KanjiType>();
            crd2.Type = KanjiType.Fire;
            crd2.KanjiSymbol = "火";
            crd2.Color = ConsoleColor.Red;

            crd2.Strength.Add(KanjiType.Wind);
            crd2.Strength.Add(KanjiType.Electricity);


            KanjiCard crd3 = new KanjiCard();
            crd3.Strength = new List<KanjiType>();
            crd3.Type = KanjiType.Wind;
            crd3.KanjiSymbol = "風";
            crd3.Color = ConsoleColor.White;

            crd3.Strength.Add(KanjiType.Ground);
            crd3.Strength.Add(KanjiType.Water);


            KanjiCard crd4 = new KanjiCard();
            crd4.Strength = new List<KanjiType>();
            crd4.Type = KanjiType.Ground;
            crd4.KanjiSymbol = "土";
            crd4.Color = ConsoleColor.Green;

            crd4.Strength.Add(KanjiType.Electricity);
            crd4.Strength.Add(KanjiType.Fire);

            KanjiCard crd5 = new KanjiCard();
            crd5.Strength = new List<KanjiType>();
            crd5.Type = KanjiType.Electricity;
            crd5.KanjiSymbol = "電";
            crd5.Color = ConsoleColor.Yellow;

            crd5.Strength.Add(KanjiType.Wind);
            crd5.Strength.Add(KanjiType.Water);


            crdList.Add(crd);
            crdList.Add(crd2);
            crdList.Add(crd3);
            crdList.Add(crd4);
            crdList.Add(crd5);

            return crdList;

        }

        public KanjiCard UserSelection()
        {
            var valid = false;
            var inputCard = -1;

            while (valid == false)
            {
                inputCard = Int16.Parse(Console.ReadLine());

                if (inputCard == 0 || inputCard == 1)
                {
                    Console.WriteLine("Carta seleccionada: \t[" + Match.UserDeck[inputCard].Type.ToString() + "]");
                    valid = true;
                }
            }

            return Match.UserDeck[inputCard];
        }

        public KanjiCard BotSelection()
        {
            var num = random.Next(0, 1);
            var selectedCard = Match.BotDeck[num];
            Console.WriteLine("Bot has selected: \t[" + selectedCard.Type + "]");

            return selectedCard;
        }
        public void PlayGame() {

            this.Match.CardsDeck = GenerateDeck();
            this.FlushKanji(this.Match.CardsDeck);
 
            var userSelection = this.UserSelection();
            var botSelection = this.BotSelection();

            var userWins = CheckType(userSelection, botSelection);

            if (userWins == true)
            {
                Console.WriteLine(userSelection.Type.ToString() + " gana a " + botSelection.Type.ToString() + " has ganado !");
            }
            else
            {
                Console.WriteLine(botSelection.Type.ToString() + " gana a " + userSelection.Type.ToString() + " has perdido...");
            }

            Console.ReadKey();
        }
        
    }
}
