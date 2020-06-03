using Crowdfund.Core.Data;
using Crowdfund.Core.Models;
using Crowdfund.Core.Services.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crowdfund.Core.Services
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

        public Result<StatusUpdate> CreateStatusUpdate(CreateStatusUpdateOptions options)
        {
            if (options == null)
            {
                return Result<StatusUpdate>.CreateFailed(StatusCode.BadRequest, "Null options");
            }
            var project = projectService_.GetProjectById(options.ProjectId).Data;
            if (project == null)
            {
                return Result<StatusUpdate>.CreateFailed(StatusCode.BadRequest, $"Project with {options.ProjectId} was not found");
            }

            var statusupdate = new StatusUpdate()
            {
                ProjectId = options.ProjectId,
                Title = options.Title,
                Description = options.Description,
            };
            if(!statusupdate.IsValidTitle(options.Title) || !statusupdate.IsValidDescription(options.Description))
            {
                return Result<StatusUpdate>.CreateFailed(StatusCode.BadRequest, "Please check the validations");
            }

            project.StatusUpdates.Add(statusupdate);

            if (context_.SaveChanges() <= 0)
            {
                return Result<StatusUpdate>.CreateFailed(
                    StatusCode.InternalServerError,
                    "Status update could not be created");
            }

            return Result<StatusUpdate>.CreateSuccessful(statusupdate);

        }

        public IQueryable<StatusUpdate> SearchStatusUpdateByProjectId(int projectId)
        {
            var query = context_
                 .Set<StatusUpdate>()
                 .AsQueryable();
                       
            var project = projectService_.GetProjectById(projectId).Data;

            query = query.Where(c => project.StatusUpdates.Contains(c));

            query = query.Take(500);
            return query;
        }

        public Result<StatusUpdate> GetStatusUpdateById(int statusUpdateId)
        {
            var query = context_
                 .Set<StatusUpdate>()
                 .AsQueryable();                        

           query = query.Where(c => c.StatusUpdateId == statusUpdateId);

            query = query.Take(500);
            var statusUpdate= query.SingleOrDefault();
            if (statusUpdate == null)
            {
                return Result<StatusUpdate>.CreateFailed(StatusCode.NotFound, "No such status update exists");
            }
            return Result<StatusUpdate>.CreateSuccessful(statusUpdate);
        }

        public Result<bool> DeleteStatusUpdate(int statusUpdateId)
        {            
            var statusUpdate = GetStatusUpdateById(statusUpdateId);
            if (statusUpdate == null)
            {
                return Result<bool>.CreateFailed(StatusCode.BadRequest, $"Status update with {statusUpdateId} was not found");
            }

            context_.Remove(statusUpdate);
            if (context_.SaveChanges() == 0)
            {
                return Result<bool>.CreateFailed(StatusCode.InternalServerError, "Something went wrong");
            }

            return Result<bool>.CreateSuccessful(true);
        }

        public Result<bool> UpdateStatusUpdate(UpdateStatusUpdateOptions options)
        {
            if (options == null)
            {
                return Result<bool>.CreateFailed(StatusCode.BadRequest, "Null options");
            }

            var statusUpdate = GetStatusUpdateById(options.StatusUpdateId).Data;

            if (statusUpdate == null)
            {
                return Result<bool>.CreateFailed(StatusCode.BadRequest, $"Status update with ID {options.StatusUpdateId} was not found");
            }

            if (!statusUpdate.IsValidTitle(options.Title))
            {
                statusUpdate.Title = options.Title;
            }

            if (!statusUpdate.IsValidDescription(options.Description))
            {
                statusUpdate.Description = options.Description;
            }

            if (context_.SaveChanges() == 0)
            {
                return Result<bool>.CreateFailed(StatusCode.InternalServerError, "Something went wrong");
            }

            return Result<bool>.CreateSuccessful(true);

        }

    }
}
