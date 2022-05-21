using System.Collections.Generic;

namespace Itmo.Dormitory.Core
{
    public class PagedResponse<T>
    {
        public List<T> Data { get; set; } = new List<T>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        
        public bool HasPreviousPage => CurrentPage > 1;

        public bool HasNextPage => CurrentPage < Pages;
    }
}