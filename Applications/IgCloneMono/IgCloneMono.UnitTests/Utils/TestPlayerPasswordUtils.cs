using FluentAssertions;
using IgCloneMono.Api.Utils;
using Xunit;

namespace IgCloneMono.UnitTests.Utils
{
    public class TestPlayerPasswordUtils
    {
        public TestPlayerPasswordUtils()
        {
            _passwordUtils = new PlayerPasswordUtils();
        }
        
        private readonly PlayerPasswordUtils _passwordUtils;
        
        [Fact]
        public void ItShouldReturnDifferentString()
        {
            var playerPassword = "somePassword";
            var hashedPassword = _passwordUtils.DoHash(playerPassword);
            hashedPassword.Should().NotBeSameAs(playerPassword);
        }

        [Fact]
        public void ItShouldReturnTrueForCorrectPasswords()
        {
            var playerPasswords = "somePass";
            var hashedPass = _passwordUtils.DoHash(playerPasswords);
            var isCorrect = _passwordUtils.IsCompareValid(playerPasswords, hashedPass);
            isCorrect.Should().BeTrue();
        }

        [Fact]
        public void ItShouldReturnFalseForIncorrectPasswords()
        {
            var correctPassword = "somePassword";
            var incorrectPassword = "wrongPass";
            var hashedPass = _passwordUtils.DoHash(correctPassword);
            var validation = _passwordUtils.IsCompareValid(incorrectPassword, hashedPass);
            validation.Should().BeFalse();
        }
    }
}