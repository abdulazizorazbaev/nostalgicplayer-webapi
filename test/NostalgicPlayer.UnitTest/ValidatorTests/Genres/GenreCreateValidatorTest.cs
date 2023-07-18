using Microsoft.AspNetCore.Http;
using NostalgicPlayer.Service.DTOs.Genres;
using NostalgicPlayer.Service.Validators.DTOs.Genres;
using System.Text;

namespace NostalgicPlayer.UnitTest.ValidatorTests.Genres;

public class GenreCreateValidatorTest
{
    [Theory]
    [InlineData(3.1)]
    [InlineData(3.01)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    public void ShouldReturnIncorrectImageFileSize(double imageSizeInMB)
    {
        byte[] byteImage = Encoding.UTF8.GetBytes("Each departing client is serenaded with a close-harmony farewell.");
        long imageSizeInByte = (long) (imageSizeInMB * 1024 * 1024);
        IFormFile imageFile = new FormFile(new MemoryStream(byteImage), 0, imageSizeInByte, "data", "file.png");
        GenreCreateDto genreCreateDto = new GenreCreateDto()
        {
            GenreName = "Close Harmony",
            Description = "A chord is in close harmony (also called close position or close structure) if its notes are arranged within a narrow range, usually with no more than an octave between the top and bottom notes.",
            ImagePath = imageFile
        };
        var validator = new GenreCreateValidator();
        var result = validator.Validate(genreCreateDto);
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData(2.95)]
    [InlineData(3)]
    [InlineData(2.5)]
    [InlineData(1)]
    [InlineData(0.5)]
    [InlineData(0.75)]
    public void ShouldReturnCorrectImageFileSize(double imageSizeInMB)
    {
        byte[] byteImage = Encoding.UTF8.GetBytes("Each departing client is serenaded with a close-harmony farewell.");
        long imageSizeInByte = (long)(imageSizeInMB * 1024 * 1024);
        IFormFile imageFile = new FormFile(new MemoryStream(byteImage), 0, imageSizeInByte, "data", "file.png");
        GenreCreateDto genreCreateDto = new GenreCreateDto()
        {
            GenreName = "Close Harmony",
            Description = "A chord is in close harmony (also called close position or close structure) if its notes are arranged within a narrow range, usually with no more than an octave between the top and bottom notes.",
            ImagePath = imageFile
        };
        var validator = new GenreCreateValidator();
        var result = validator.Validate(genreCreateDto);
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("file.png")]
    [InlineData("file.jpg")]
    [InlineData("file.jpeg")]
    [InlineData("file.bmp")]
    [InlineData("file.svg")]
    public void ShouldReturnCorrectImageFileExtension(string imagename)
    {
        byte[] byteImage = Encoding.UTF8.GetBytes("A chord is in close harmony (also called close position or close structure) if its notes are arranged within a narrow range, usually with no more than an octave between the top and bottom notes.");
        IFormFile imageFile = new FormFile(new MemoryStream(byteImage), 0, byteImage.Length, "data", imagename);
        GenreCreateDto genreCreateDto = new GenreCreateDto()
        {
            GenreName = "Close Harmony",
            Description = "A chord is in close harmony (also called close position or close structure) if its notes are arranged within a narrow range, usually with no more than an octave between the top and bottom notes.",
            ImagePath = imageFile
        };
        var validator = new GenreCreateValidator();
        var result = validator.Validate(genreCreateDto);
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("file.zip")]
    [InlineData("file.rar")]
    [InlineData("file.mpg")]
    [InlineData("file.mp4")]
    [InlineData("file.avi")]
    [InlineData("file.mvk")]
    [InlineData("file.mp3")]
    [InlineData("file.wav")]
    [InlineData("file.html")]
    [InlineData("file.txt")]
    [InlineData("file.webp")]
    [InlineData("file.HEIC")]
    [InlineData("file.gif")]
    [InlineData("file.pdf")]
    [InlineData("file.xls")]
    [InlineData("file.doc")]
    [InlineData("file.docx")]
    [InlineData("file.exe")]
    [InlineData("file.dll")]
    public void ShouldReturnIncorrectImageFileExtension(string imagename)
    {
        byte[] byteImage = Encoding.UTF8.GetBytes("A chord is in close harmony (also called close position or close structure) if its notes are arranged within a narrow range, usually with no more than an octave between the top and bottom notes.");
        IFormFile imageFile = new FormFile(new MemoryStream(byteImage), 0, byteImage.Length, "data", imagename);
        GenreCreateDto genreCreateDto = new GenreCreateDto()
        {
            GenreName = "Close Harmony",
            Description = "A chord is in close harmony (also called close position or close structure) if its notes are arranged within a narrow range, usually with no more than an octave between the top and bottom notes.",
            ImagePath = imageFile
        };
        var validator = new GenreCreateValidator();
        var result = validator.Validate(genreCreateDto);
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData("AA")]
    [InlineData("A")]
    [InlineData("A chord is in close harmony (also called close position or close structure) if its notes are arranged within a narrow range")]
    public void ShouldReturnInCorrectValidation(string name)
    {
        byte[] byteImage = Encoding.UTF8.GetBytes("A chord is in close harmony (also called close position or close structure) if its notes are arranged within a narrow range, usually with no more than an octave between the top and bottom notes.");
        IFormFile imageFile = new FormFile(new MemoryStream(byteImage), 0, byteImage.Length, "data", "file.png");
        GenreCreateDto genreCreateDto = new GenreCreateDto()
        {
            GenreName = name,
            Description = "A chord is in close harmony (also called close position or close structure) if its notes are arranged within a narrow range, usually with no more than an octave between the top and bottom notes.",
            ImagePath = imageFile
        };
        var validator = new GenreCreateValidator();
        var result = validator.Validate(genreCreateDto);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void ShouldReturnCorrectValidation()
    {
        byte[] byteImage = Encoding.UTF8.GetBytes("A chord is in close harmony (also called close position or close structure) if its notes are arranged within a narrow range, usually with no more than an octave between the top and bottom notes.");
        IFormFile imageFile = new FormFile(new MemoryStream(byteImage), 0, byteImage.Length, "data", "file.png");
        GenreCreateDto genreCreateDto = new GenreCreateDto()
        {
            GenreName = "Close Harmony",
            Description = "A chord is in close harmony",
            ImagePath = imageFile
        };
        var validator = new GenreCreateValidator();
        var result = validator.Validate(genreCreateDto);
        Assert.True(result.IsValid);
    }
}