using Xunit;

namespace GoedBezigWebApp.Tests
{
    public class Class
    {
        [Fact]
        public void PassingTest()//test ter referentie dat slagende testen effectief slagen
        {
            Assert.Equal(4,4);
        }

        [Fact]
        public void FailingTest()//test ter referentie dat falende testen effectief als falend worden gemarkeerd
        {
            Assert.Equal(5, 4);
        }
    }
}
