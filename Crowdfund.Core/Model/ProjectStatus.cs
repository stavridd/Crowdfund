namespace Crowdfund.Core.Model {
    public enum ProjectStatus 
   {

        /// <summary>
        /// The project status is invalid
        /// </summary>
        Invalid = 0,

        /// <summary>
        /// The project status is active 
        /// </summary>
        Active = 1,

        /// <summary>
        /// The project status is closed
        /// </summary>
        Closed = 2,

        /// <summary>
        /// The project status is paused
        /// </summary>
        Paused = 3,

        /// <summary>
        /// The project status is completed
        /// </summary>
        Completed = 4
   }
}
