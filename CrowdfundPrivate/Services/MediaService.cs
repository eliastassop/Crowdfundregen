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

        public bool CreateMedia(CreateMediaOptions options)
        {
            if (options == null)
            {
                return false;
            }
            var project = projectService_.GetProjectById(options.ProjectId);

            var media = new Media()
            {
                MediaLink = options.MediaLink,
                Category = options.Category,
            };
            
            if(string.IsNullOrWhiteSpace(options.MediaLink))
            {
                return false;
            }

            project.Media.Add(media);

            if (context_.SaveChanges() > 0)
            {
                return true;
            }
            return false;

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

            var rewardQuery = SearchMedia(new SearchMediaOptions()
            {
                ProjectId = projectId,
            });

            return rewardQuery;
        }
        */
        public Media GetMediaById(int? mediaId)
        {
            if (mediaId == null)
            {
                return null;
            }

            var media = SearchMedia(new SearchMediaOptions()
            {
                MediaId = mediaId,
            }).SingleOrDefault();
            return media;
        }

        public bool DeleteMedia(int? mediaId)
        {
            if (mediaId == null)
            {
                return false;
            }
            var media = GetMediaById(mediaId);

            context_.Remove(media);
            if (context_.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public bool UpdateMedia(UpdateMediaOptions options)
        {
            if (options == null)
            {
                return false;
            }

            var media = GetMediaById(options.MediaId);

            if (media == null)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(options.MediaLink))
            {
                media.MediaLink = options.MediaLink;
            }

            if (media.IsValidCategory(options.Category))
            {
                media.Category = options.Category;
            }
            if (context_.SaveChanges() > 0)
            {
                return true;
            }
            return false;
            
            






        }


    }
}
