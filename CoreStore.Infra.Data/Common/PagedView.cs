using System.Collections.Generic;

namespace CoreStore.Infra.Data.Common
{
    public class PagedView<T>
    {
        public List<T> Data { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
    }
}
