using Xunit;
using FluentAssertions;
using OriolOr.KanjiDome.Services;

namespace OriolOr.KanjiDome.Test
{
    public class KanjiServiceTest
    {
        KanjiService KanjiService = new KanjiService();

        [Fact]
        public void UserSelectionTest()
        {
            var selection = KanjiService.UserSelection();
            
            selection.Should().NotBeNull();
        }
    }
}