using Crowdfund.Models;
using Crowdfund.Services.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crowdfund.Services
{
    public interface IMediaService
    {
        Result<Media> CreateMedia(CreateMediaOptions options);
        Result<bool> UpdateMedia(int mediaId, UpdateMediaOptions options);
        Result<bool> DeleteMedia(int mediaId);
        IQueryable<Media> SearchMedia(SearchMediaOptions options);
        Media GetMediaById(int MediaId);

        //IQueryable<Media> SearchMediaByProjectId(int? projectId);





    }
}
