using FluentValidator;
using System.Collections.Generic;

namespace CoreStore.Shared.Command
{
    public class Result
    {
        public Result()
        {
            _notifications = new List<Notification>();
        }

        protected List<Notification> _notifications;
        public bool IsSuccess { get; protected set; }
        public IReadOnlyCollection<Notification> Notifications => _notifications;

        public void AddNotification(Notification notification)
        {
            IsSuccess = false;
            _notifications.Add(notification);
        }

        public void AddNotifications(IReadOnlyCollection<Notification> notifications)
        {
            IsSuccess = false;
            _notifications.AddRange(notifications);
        }
    }
}
