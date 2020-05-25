using Crowdfund.Models;
using Crowdfund.Services.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crowdfund.Services
{
    public interface IStatusUpdateService
    {
        Result<StatusUpdate> CreateStatusUpdate(CreateStatusUpdateOptions options);

        Result<bool> UpdateStatusUpdate(UpdateStatusUpdateOptions options);

        Result<bool> DeleteStatusUpdate(int? statusUpdateId);

        IQueryable<StatusUpdate> SearchStatusUpdateByProjectId(int? projectId);

        StatusUpdate GetStatusUpdateById(int? statusUpdateId); 
    }
}
