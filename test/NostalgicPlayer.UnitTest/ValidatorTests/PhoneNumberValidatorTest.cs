using NostalgicPlayer.Service.Validators;

namespace NostalgicPlayer.UnitTest.ValidatorTests;

public class PhoneNumberValidatorTest
{
    [Theory]
    [InlineData("+998933644016")]
    [InlineData("+998943644016")]
    [InlineData("+998953644016")]
    [InlineData("+998333644016")]
    [InlineData("+998913644016")]
    [InlineData("+998903644016")]
    [InlineData("+998973644016")]
    [InlineData("+998993644016")]
    public void ShouldReturnCorrect(string phoneNumber)
    {
        var result = PhoneNumberValidator.IsValid(phoneNumber);
        Assert.True(result);
    }

    [Theory]
    [InlineData("998933644016")]
    [InlineData("+99893364401623")]
    [InlineData("AAABBBEF")]
    [InlineData("wegerger")]
    [InlineData("+99893364401")]
    [InlineData("+998933644O16")]
    [InlineData("+998933644o16")]
    [InlineData("+99893 3644O1")]
    [InlineData("#998933644O16")]
    [InlineData("#998933644o16")]
    [InlineData("+99893 364 40 16")]
    public void ShouldReturnIncorrect(string phoneNumber)
    {
        var result = PhoneNumberValidator.IsValid(phoneNumber);
        Assert.False(result);
    }
}