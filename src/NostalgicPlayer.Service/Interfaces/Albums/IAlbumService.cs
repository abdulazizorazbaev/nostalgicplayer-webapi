﻿using NostalgicPlayer.DataAccess.Utilities;
using NostalgicPlayer.DataAccess.ViewModels;
using NostalgicPlayer.Domain.Entities.Albums;
using NostalgicPlayer.Service.DTOs.Albums;

namespace NostalgicPlayer.Service.Interfaces.Albums;

public interface IAlbumService
{
    public Task<bool> CreateAsync(AlbumCreateDto dto);

    public Task<IList<AlbumViewModel>> GetAllAsync(PaginationParams @params);

    public Task<long> CountAsync();

    public Task<bool> DeleteAsync(long albumId);

    public Task<Album> GetByIdAsync(long albumId);

    public Task<bool> UpdateAsync(long albumId, AlbumUpdateDto dto);

    public Task<IList<AlbumViewModel>> SearchAsync(string search, PaginationParams @params);
}
