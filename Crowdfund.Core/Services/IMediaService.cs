using Crowdfund.Core.Models;
using Crowdfund.Core.Services.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crowdfund.Core.Services
{
    public interface IMediaService
    {
        Result<Media> CreateMedia(CreateMediaOptions options);
        Result<bool> UpdateMedia(int mediaId, UpdateMediaOptions options);
        Result<bool> DeleteMedia(int mediaId);
        IQueryable<Media> SearchMedia(SearchMediaOptions options);
        Result<Media> GetMediaById(int MediaId);

        //IQueryable<Media> SearchMediaByProjectId(int? projectId);





    }
}
