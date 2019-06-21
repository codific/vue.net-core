using GSK.DAL.Repositories;

namespace Core.DataAccess
{
    public class StandardRepository : StandardRepository<EntityContext>
    {
        public StandardRepository(EntityContext context) : base(context)
        {

        }
    }
}
