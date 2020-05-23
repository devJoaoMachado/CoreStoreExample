using CoreStore.Shared.Command;
using FluentValidator;
using System.Collections.Generic;

namespace CoreStore.Shared.Shared.Command
{
    public class ResultQuery<T> : Result
    {
        public T Data { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }

        public ResultQuery()
        {
            IsSuccess = true;
            _notifications = new List<Notification>();
        }
        public ResultQuery(T entity)
        {
            this.Data = entity;
            IsSuccess = true;
            _notifications = new List<Notification>();
        }

        public ResultQuery(T entity, int page, int pageSize, int totalItems, int totalPages)
        {
            this.Data = entity;
            IsSuccess = true;
            _notifications = new List<Notification>();
            Page = page;
            PageSize = pageSize;
            TotalItems = totalItems;
            TotalPages = totalPages;
        }
    }
}
