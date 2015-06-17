using PayrollSystemDemo.Data.Models;
using PayrollSystemDemo.Repo.Repository;
using PayrollSystemDemo.Repo.UnitOfWork;
using PayrollSystemDemo.Service.Base;

namespace PayrollSystemDemo.Service
{
    public class DiscountService : EntityService<Discount>, IDiscountService
    {
        private readonly IRepository<Discount> _discountRepository;

        public DiscountService(IUnitOfWork unitOfWork, IRepository<Discount> discountRepository)
            : base(unitOfWork, discountRepository)
        {
            _discountRepository = unitOfWork.GetRepository<Discount>();
        }
    }
}
