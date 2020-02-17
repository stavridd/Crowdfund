using System.Linq;
using Crowdfund.Core.Model;

namespace Crowdfund.Core.Services {
    public interface IOwnerService 
    {
        Owner CreateOwner(
            Model.Options.CreateOwnerOptions options);

        IQueryable<Model.Owner> SearchOwner(
            Model.Options.SearchOwnerOptions options);

        Owner SearchOwnerById(int ownerId);

        bool UpdateOwner(int id,
            Model.Options.UpdateOwnerOptions options);

        bool AddReward(int ownerId, Reward reward);
    }
}
