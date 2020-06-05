using Crowdfund.Core.Data;
using Crowdfund.Core.Models;
using Crowdfund.Core.Services.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crowdfund.Core.Services
{
    public class MediaService: IMediaService
    {
        private CrowdfundDB context_;
        private IProjectService projectService_;

        public MediaService(CrowdfundDB context, IProjectService projectService)
        {
            context_ = context;
            projectService_ = projectService;
        }

        public Result<Media> CreateMedia(CreateMediaOptions options)
        {
            if (options == null)
            {
                return Result<Media>.CreateFailed(StatusCode.BadRequest, "Null options");
            }
            var project = projectService_.GetProjectById(options.ProjectId).Data;
            if (project == null)
            {
                return Result<Media>.CreateFailed(StatusCode.BadRequest, $"Project with {options.ProjectId} was not found");
            }
            MediaCategory cat;
            if(!Enum.TryParse(options.Category, true, out cat))
            {
                return Result<Media>.CreateFailed(StatusCode.BadRequest, "Please check the available categories");
            }        

            var media = new Media()
            {
                MediaLink = options.MediaLink,
                Category = cat,
            };
            
            if(string.IsNullOrWhiteSpace(options.MediaLink))
            {
                return Result<Media>.CreateFailed(StatusCode.BadRequest, "The link  was incorrect");
            }

            if (!media.IsValidCategory(cat))
            {
                return Result<Media>.CreateFailed(StatusCode.BadRequest, "Please check the available categories");
            }

            project.Media.Add(media);

            if (context_.SaveChanges() <= 0)
            {
                return Result<Media>.CreateFailed(
                    StatusCode.InternalServerError,
                    "Media could not be created");
            }

            return Result<Media>.CreateSuccessful(media);

        }

        public IQueryable<Media> SearchMedia(SearchMediaOptions options) 
        {
            var query = context_
                 .Set<Media>()
                 .AsQueryable();

            if (options == null)
            {
                return null;
            }

            if (options.ProjectId != null)
            {

                var project = projectService_.GetProjectById(options.ProjectId.Value).Data;

                query = query.Where(c => project.Media.Contains(c)); 
            }


            if (options.MediaId != null)
            {
                query = query.Where(r => r.MediaId == options.MediaId);  
            }

            query = query.Take(500);
            return query;
        }

        // NULLABLE giati etsi to exoume sta searchmediaoptions den eixa kouragio na dw an odws xreiazete.??
        public Result<Media> GetMediaByProjectId(int projectId)
        {
            
            var media = SearchMedia(new SearchMediaOptions()
            {
                ProjectId = projectId,
            }).FirstOrDefault();
            if (media == null)
            {
                return Result<Media>.CreateFailed(StatusCode.NotFound, "No such Media exists");
            }
            return Result<Media>.CreateSuccessful(media);
        }
        
        public Result<Media> GetMediaById(int mediaId)
        {
            var media = SearchMedia(new SearchMediaOptions()
            {
                MediaId = mediaId,
            }).SingleOrDefault();
            if (media == null)
            {
                return Result<Media>.CreateFailed(StatusCode.NotFound, "No such Media exists");
            }
            return Result<Media>.CreateSuccessful(media);
        }

        public Result<bool> DeleteMedia(int mediaId)
        {            
            var media = GetMediaById(mediaId);
            if (media == null)
            {
                return Result<bool>.CreateFailed(StatusCode.BadRequest, $"Media with {mediaId} was not found");
            }

            context_.Remove(media);
            if (context_.SaveChanges() == 0)
            {
                return Result<bool>.CreateFailed(StatusCode.InternalServerError, "Something went wrong");
            }

            return Result<bool>.CreateSuccessful(true);
        }

        public Result<bool> UpdateMedia(int mediaId,UpdateMediaOptions options)
        {
            if (options == null)
            {
                return Result<bool>.CreateFailed(StatusCode.BadRequest, "Null options");
            }

            var media = GetMediaById(mediaId).Data;

            if (media == null)
            {
                return Result<bool>.CreateFailed(StatusCode.BadRequest, $"Media with {mediaId} was not found");
            }

            if (!string.IsNullOrWhiteSpace(options.MediaLink))
            {
                media.MediaLink = options.MediaLink;
            }

            if (media.IsValidCategory(options.Category))
            {
                media.Category = options.Category;
            }
            if (context_.SaveChanges() == 0)
            {
                return Result<bool>.CreateFailed(StatusCode.InternalServerError, "Something went wrong");
            }

            return Result<bool>.CreateSuccessful(true);
        }


    }
}
