using System.Linq;
using Crowdfund.Core.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using Crowdfund.Core.Model.Options;

namespace Crowdfund.Core.Services {
    public interface IBuyerService 
    {
        Task<ApiResult<Buyer>> CreateBuyerAsync(
            Model.Options.CreateBuyerOptions options);

        IQueryable<Model.Buyer> SearchBuyer(
            Model.Options.SearchBuyerOptions options);

        Task<ApiResult<Buyer>> SearchBuyerByIdAsync(int buyerId);

        Task<ApiResult<Buyer>> UpdateBuyerAsync(int id,
            Model.Options.UpdateBuyerOptions options);

        Task<ApiResult<ICollection<Project>>> GetMyProjectsAsync(
                    int buyerId);

        Task<ApiResult<ICollection<Project>>> GetMyCompletedProjectsAsync(int buyerId);

        Task<bool> IsBuyerAllowedToSee(int buyerId, int projectId);
    }
}
