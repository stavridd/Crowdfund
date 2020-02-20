using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Core.Services {
    public enum StatusCode {
        Ok = 200,
        NotFound = 404,
        BadRequest = 400,
        Conflict = 409,
        InternalServerError = 500
    }
}

