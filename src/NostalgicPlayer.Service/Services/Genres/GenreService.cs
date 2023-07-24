using NostalgicPlayer.DataAccess.Interfaces.Genres;
using NostalgicPlayer.DataAccess.Utilities;
using NostalgicPlayer.Domain.Entities.Genres;
using NostalgicPlayer.Domain.Exceptions.Files;
using NostalgicPlayer.Domain.Exceptions.Genres;
using NostalgicPlayer.Service.Common.Helpers;
using NostalgicPlayer.Service.DTOs.Genres;
using NostalgicPlayer.Service.Interfaces.Common;
using NostalgicPlayer.Service.Interfaces.Genres;

namespace NostalgicPlayer.Service.Services.Genres;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _genreRepository;
    private readonly IFileService _fileService;

    public GenreService(IGenreRepository genreRepository,
        IFileService fileService)
    {
        this._genreRepository = genreRepository;
        this._fileService = fileService;
    }

    public async Task<long> CountAsync() => await _genreRepository.CountAsync();

    public async Task<bool> CreateAsync(GenreCreateDto dto)
    {
        string imagepath = await _fileService.UploadImageAsync(dto.ImagePath);
        Genre genre = new Genre()
        {
            ImagePath = imagepath,
            GenreName = dto.GenreName,
            Description = dto.Description,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };
        var result = await _genreRepository.CreateAsync(genre);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(long genreId)
    {
        var genre = await _genreRepository.GetByIdAsync(genreId);
        if (genre is null) throw new GenreNotFoundException();

        var result = await _fileService.DeleteImageAsync(genre.ImagePath);
        if (result == false) throw new ImageNotFoundException();

        var dbResult = await _genreRepository.DeleteAsync(genreId);
        return dbResult > 0;
    }

    public async Task<IList<Genre>> GetAllAsync(PaginationParams @params)
    {
        var genres = await _genreRepository.GetAllAsync(@params);
        return genres;
    }

    public async Task<Genre> GetByIdAsync(long genreId)
    {
        var genre = await _genreRepository.GetByIdAsync(genreId);
        if (genre is null) throw new GenreNotFoundException();
        else return genre;
    }

    public async Task<bool> UpdateAsync(long genreId, GenreUpdateDto dto)
    {
        var genre = await _genreRepository.GetByIdAsync(genreId);
        if (genre is null) throw new GenreNotFoundException();

        genre.GenreName = dto.GenreName;
        genre.Description = dto.Description;

        if (dto.ImagePath is not null)
        {
            // deletes an old image
            var deleteResult = await _fileService.DeleteImageAsync(genre.ImagePath);
            if (deleteResult is false) throw new ImageNotFoundException();

            // uploads a new image
            string newImagePath = await _fileService.UploadImageAsync(dto.ImagePath);

            // parses a new path to a genre
            genre.ImagePath = newImagePath;
        }
        // else genre's old image have to be saved

        genre.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _genreRepository.UpdateAsync(genreId, genre);
        return dbResult > 0;
    }
}