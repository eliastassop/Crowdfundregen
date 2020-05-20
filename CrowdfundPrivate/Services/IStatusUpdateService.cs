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
        bool CreateStatusUpdate(CreateStatusUpdateOptions options);

        bool UpdateStatusUpdate(UpdateStatusUpdateOptions options);

        bool DeleteStatusUpdate(int? statusUpdateId);

        IQueryable<StatusUpdate> SearchStatusUpdateByProjectId(int? projectId);

        StatusUpdate GetStatusUpdateById(int? statusUpdateId); 
    }
}
