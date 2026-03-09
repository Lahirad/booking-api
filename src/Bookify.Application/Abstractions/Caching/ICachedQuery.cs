using Bookify.Application.Abstractions.Messging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Application.Abstractions.Caching
{
    // Ensure that IQuery<TResponse> is public to match the accessibility of ICachedQuery<TResponse>
    public interface ICachedQuery<TResponse> : IQuery<TResponse>, ICachedQuery;

    public interface ICachedQuery
    {
        string CacheKey { get; }

        TimeSpan? Expiration { get; }
    }
}
