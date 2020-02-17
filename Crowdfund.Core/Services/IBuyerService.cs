using System.Linq;
using Crowdfund.Core.Model;

namespace Crowdfund.Core.Services {
    public interface IBuyerService 
    {
        Buyer CreateBuyer(
            Model.Options.CreateBuyerOptions options);

        IQueryable<Model.Buyer> SearchBuyer(
            Model.Options.SearchBuyerOptions options);

        Buyer SearchBuyerById(int buyerId);

        bool UpdateBuyer(int id,
            Model.Options.UpdateBuyerOptions options);
    }
}
