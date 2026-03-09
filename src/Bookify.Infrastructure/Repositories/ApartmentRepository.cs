using Bookify.Domain.Apartements;
using Bookify.Domain.Apartments;

namespace Bookify.Infrastructure.Repositories;

internal sealed class ApartmentRepository : Repository<Apartement>, IApartementRepository
{
    public ApartmentRepository(ApplicationDBContext dbContext)
        : base(dbContext)
    {
    }
}
