﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund
{
    public enum StatusCode
    {
        OK = 200,
        NotFound = 404,
        BadRequest = 400,
        InternalServerError = 500
    }
}