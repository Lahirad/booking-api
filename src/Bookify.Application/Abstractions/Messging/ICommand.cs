using Bookify.Domain.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Application.Abstractions.Messging
{
    interface ICommand : IRequest<Result> , IBaseCommand
    {
    }

    interface ICommand<TResponse> : IRequest<Result<TResponse>> ,IBaseCommand
    {
    }
    interface IBaseCommand
    {
    }
}
