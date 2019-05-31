using Xunit;

namespace Core.UnitTest.XUnit
{
    /// <summary>
    /// Api unit test.
    /// </summary>
    public class XUnit
    {
        [Fact]
        public void Add()
        {
            Assert.Equal(3, 3);
        }
    }
}
