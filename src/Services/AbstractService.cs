using AutoMapper;
using Entities.Identity;
using GSK.DAL;

namespace Services
{
    public abstract class AbstractService
    {
        protected readonly IUnitOfWork uow;
        protected readonly IMapper mapper;


        public AbstractService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        protected User User
        {
            get
            {
                return this.uow.GetStandardRepository().Get<User>(this.uow.CurrentUserId);
            }
        }
    }
}
