using NostalgicPlayer.Service.Validators;

namespace NostalgicPlayer.UnitTest.ValidatorTests;

public class PasswordValidatorTest
{
    [Theory]
    [InlineData("AAaa55##")]
    [InlineData("Aa55#!@$")]
    [InlineData("kbHeb2%.")]
    [InlineData("$AAAhe2%&")]
    [InlineData("_abcdEFG8")]
    [InlineData("aBCd884^")]
    [InlineData("!23()Ads")]
    [InlineData("123AAa#*")]
    [InlineData("#vbA=32*")]
    public void IsStrongPassword(string password)
    {
        var result = PasswordValidator.IsStrongPassword(password);
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("asAA12@")]
    [InlineData("asAAdfg!")]
    [InlineData("KJAAA12#")]
    [InlineData("KJAAA12a")]
    [InlineData("ababABAB")]
    [InlineData("w4t4g12@")]
    [InlineData("w4t4g1@ ")]
    [InlineData("DSGSEGER")]
    [InlineData("ergerrrr")]
    [InlineData("@%#@%$@#")]
    [InlineData("34534643")]
    public void IsWeakPassword(string password)
    {
        var result = PasswordValidator.IsStrongPassword(password);
        Assert.False(result.IsValid);
    }
}