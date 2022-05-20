using System.Collections.Generic;
using MediatR;

namespace Itmo.Dormitory.Core
{
    public interface IListRequest
    {
    }

    public interface IListRequest<out T> : IListRequest, IRequest<T>
    {
    }

    public interface IListResponse
    {
        public int Size { get; init; }
    }

    public interface IListResponse<TListItem> : IListResponse
    {
        public IList<TListItem> Results { get; init; }
    }
}