using Crowdfund.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crowdfund.Services
{
    public interface IStatusUpdateService
    {
        bool CreateStatusUpdate();

        bool UpdateStatusUpdate();

        bool DeleteStatusUpDate(int? statusUpdateId);

        IQueryable<StatusUpdate> SearchStatusUpdateByProjectId(int? projectId);

        StatusUpdate GetStatusUpdateById(int? statusUpdateId); 
    }
}
