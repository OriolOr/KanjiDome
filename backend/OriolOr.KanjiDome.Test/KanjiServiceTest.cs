using Xunit;
using FluentAssertions;
using OriolOr.KanjiDome.Services;
using OriolOr.KanjiDome.Entities;

namespace OriolOr.KanjiDome.Test
{
    public class KanjiServiceTest
    {
        KanjiService KanjiService = new KanjiService();
        Match Match;

        public KanjiServiceTest()
        {
            this.Match = new Match();
        }

        [Fact]
        public void UserSelectionTest()
        {
            this.Match.CardsDeck = KanjiService.LoadDeck();
            this.KanjiService.FlushKanji(this.Match.CardsDeck);
            var selection = KanjiService.BotSelection();
            
            selection.Should().NotBeNull();
            selection.Strength.Count.Should().Be(2);
        }
    }
}