
namespace Crowdfund.Core.Services {
    public enum StatusCode {

        /// <summary>
        /// Ok
        /// </summary>
        Ok = 200,

        /// <summary>
        /// Request not found
        /// </summary>
        NotFound = 404,

        /// <summary>
        /// Bad parameters of the request 
        /// </summary>
        BadRequest = 400,

        /// <summary>
        /// Conflict
        /// </summary>
        Conflict = 409,

        /// <summary>
        /// Internal Server Error
        /// </summary>
        InternalServerError = 500
    }
}

