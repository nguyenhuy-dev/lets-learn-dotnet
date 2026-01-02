using DBExporter.Helpers;

namespace OptionsBuilderTests
{
    public class StringFilterUnitTest
    {
        [Theory]
        [InlineData("\"Server=.;Database=Test\"", "\"SELECT * FROM table\"", "-f:exporter")]
        public void FilterStringAgainTest(params string[] args)
        {
            var str1 = StringHelper.FilterStringAgain(args[0]);
            var str2 = StringHelper.FilterStringAgain(args[1]);
            var str3 = StringHelper.FilterStringAgain(args[2]);

            Assert.Equal("Server=.;Database=Test", str1);
            Assert.Equal("SELECT * FROM table", str2);
            Assert.Equal("-f:exporter", str3);
        }
    }
}
