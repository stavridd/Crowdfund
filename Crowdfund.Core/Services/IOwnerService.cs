using System.Linq;
using System.Threading.Tasks;
using Crowdfund.Core.Model;

namespace Crowdfund.Core.Services {
    public interface IOwnerService 
    {
        Task<ApiResult<Owner>> CreateOwnerAsync(
            Model.Options.CreateOwnerOptions options);

        IQueryable<Model.Owner> SearchOwner(
            Model.Options.SearchOwnerOptions options);

        Task<ApiResult<Owner>> SearchOwnerByIdAsync(int ownerId);

        Task<ApiResult<Owner>> UpdateOwnerAsync(int id,
            Model.Options.UpdateOwnerOptions options);

        Task<bool> AddRewardAsync(int ownerId, Reward reward);
    }
}
