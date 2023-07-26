using NostalgicPlayer.DataAccess.Common.Interfaces;
using NostalgicPlayer.DataAccess.ViewModels;
using NostalgicPlayer.Domain.Entities.Musics.Downloads;

namespace NostalgicPlayer.DataAccess.Interfaces.Musics;

public interface IDownloadRepository : IRepository<Download, Download>,
    IGetAll<DownloadViewModel>, ISearchable<DownloadViewModel>
{
}