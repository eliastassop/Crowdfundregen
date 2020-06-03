using Crowdfund.Core.Models;
using Crowdfund.Core.Services.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crowdfund.Core.Services
{
    public interface IStatusUpdateService
    {
        Result<StatusUpdate> CreateStatusUpdate(CreateStatusUpdateOptions options);

        Result<bool> UpdateStatusUpdate(UpdateStatusUpdateOptions options);

        Result<bool> DeleteStatusUpdate(int statusUpdateId);

        IQueryable<StatusUpdate> SearchStatusUpdateByProjectId(int projectId);

        Result<StatusUpdate> GetStatusUpdateById(int statusUpdateId); 
    }
}
