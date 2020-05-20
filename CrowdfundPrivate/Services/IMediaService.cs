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
        bool CreateMedia(CreateMediaOptions options);

        IQueryable<Media> SearchMedia(SearchMediaOptions options);

        //IQueryable<Media> SearchMediaByProjectId(int? projectId);

        Media GetMediaById(int? MediaId);

        bool UpdateMedia(UpdateMediaOptions options);

        bool DeleteMedia(int? mediaId);
    }
}
