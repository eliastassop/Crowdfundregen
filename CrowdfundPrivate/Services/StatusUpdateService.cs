using Crowdfund.Data;
using Crowdfund.Models;
using Crowdfund.Services.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crowdfund.Services
{
    public class StatusUpdateService:IStatusUpdateService
    {
        private CrowdfundDB context_;
        private IProjectService projectService_;

        public StatusUpdateService(CrowdfundDB context, IProjectService projectService)
        {
            context_ = context;
            projectService_ = projectService;
        }

        public bool CreateStatusUpdate(CreateStatusUpdateOptions options)
        {
            if (options == null)
            {
                return false;
            }
            var project = projectService_.GetProjectById(options.ProjectId);

            var statusupdate = new StatusUpdate()
            {
                 Title= options.Title,
                 Description = options.Description,
            };
            if(!statusupdate.IsValidTitle(options.Title) || !statusupdate.IsValidDescription(options.Description))
            {
                return false;
            }

            project.StatusUpdate.Add(statusupdate);

            if (context_.SaveChanges() > 0)
            {
                return true;
            }
            return false;

        }

        public IQueryable<StatusUpdate> SearchStatusUpdateByProjectId(int? projectId)
        {
            var query = context_
                 .Set<StatusUpdate>()
                 .AsQueryable();

            if (projectId == null)
            {
                return null;
            }

            var project = projectService_.GetProjectById(projectId);

            query = query.Where(c => project.StatusUpdate.Contains(c));


            query = query.Take(500);
            return query;
        }

        public StatusUpdate GetStatusUpdateById(int? statusUpdateId)
        {
            var query = context_
                 .Set<StatusUpdate>()
                 .AsQueryable();

            if (statusUpdateId == null)
            {
                return null;
            }

           query = query.Where(c => c.StatusUpdateId == statusUpdateId);


            query = query.Take(500);
            return query.SingleOrDefault();
        }

        public bool DeleteStatusUpdate(int? statusUpdateId)
        {
            if (statusUpdateId == null)
            {
                return false;
            }
            var statusUpdate = GetStatusUpdateById(statusUpdateId);

            context_.Remove(statusUpdate);
            if (context_.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public bool UpdateStatusUpdate(UpdateStatusUpdateOptions options)
        {
            if (options == null)
            {
                return false;
            }

            var statusUpdate = GetStatusUpdateById(options.StatusUpdateId);

            if (statusUpdate == null)
            {
                return false;
            }

            if (!statusUpdate.IsValidTitle(options.Title))
            {
                statusUpdate.Title = options.Title;
            }

            if (!statusUpdate.IsValidDescription(options.Description))
            {
                statusUpdate.Description = options.Description;
            }

            if (context_.SaveChanges() > 0)
            {
                return true;
            }
            return false;



        }

    }
}
