using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Apartements
{
   public interface IApartementRepository
    {
        Task<Apartement?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    }
}
