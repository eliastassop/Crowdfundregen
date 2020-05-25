using Crowdfund.Data;
using Crowdfund.Models;
using Crowdfund.Services.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crowdfund.Services
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
            var project = projectService_.GetProjectById(options.ProjectId);
            if (project == null)
            {
                return Result<Media>.CreateFailed(StatusCode.BadRequest, $"Project with {options.ProjectId} was not found");
            }

            var media = new Media()
            {
                MediaLink = options.MediaLink,
                Category = options.Category,
            };
            
            if(string.IsNullOrWhiteSpace(options.MediaLink))
            {
                return Result<Media>.CreateFailed(StatusCode.BadRequest, "The link  was incorrect");
            }

            if (media.IsValidCategory(options.Category))
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

                var project = projectService_.GetProjectById(options.ProjectId);

                query = query.Where(c => project.Media.Contains(c)); 
            }


            if (options.MediaId != null)
            {
                query = query.Where(r => r.MediaId == options.MediaId);  
            }

            query = query.Take(500);
            return query;
        }


        /*public IQueryable<Media> SearchMediaByProjectId(int? projectId)
        {
            if (projectId == null)
            {
                return null;
            }

            var Query = SearchMedia(new SearchMediaOptions()
            {
                ProjectId = projectId,
            });

            return Query;
        }
        */
        public Media GetMediaById(int mediaId)
        {
            var media = SearchMedia(new SearchMediaOptions()
            {
                MediaId = mediaId,
            }).SingleOrDefault();
            return media;
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

            var media = GetMediaById(mediaId);

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
