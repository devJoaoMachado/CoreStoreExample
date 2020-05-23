using FluentValidator;
using System;
using System.Collections.Generic;

namespace CoreStore.Shared.Command
{
  
    public class ResultCommand<T> : Result
    {
        public T Data { get; private set; }
        public ResultCommand()
        {
            _notifications = new List<Notification>();
        }
        public ResultCommand(T entity)
        {
            this.Data = entity;
            IsSuccess = true;
            _notifications = new List<Notification>();
        }
    }
}
