using CoreStore.Domain.StoreContext.Enums;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreStore.Domain.StoreContext.Entities
{
    public class Order : Notifiable
    {
        private IList<OrderItem> _orderItems;
        private IList<Delivery> _deliveries;

        public Order(Customer customer)
        {
            Customer = customer;
            CreateDate = DateTime.Now.ToUniversalTime();
            Status = EOrderStatus.Created;
            _orderItems = new List<OrderItem>();
            _deliveries = new List<Delivery>();
        }

        public Customer Customer { get; private set; }
        public string Number { get; private set; }
        public DateTime CreateDate { get; private set; }
        public EOrderStatus Status { get; private set; }
        public IReadOnlyCollection<OrderItem> Items => _orderItems.ToArray();
        public IReadOnlyCollection<Delivery> Deliveries => _deliveries.ToArray();

        public void AddItem(Product product, decimal quantity)
        {
            if (quantity > product.QuantityOnHand)
            {
                AddNotification("Order", $"O Produto {product.ToString()} não tem quantidade {quantity} em estoque.");
                return;
            }

            var orderItem = new OrderItem(product, quantity);
            _orderItems.Add(orderItem);
        }

        public override string ToString()
        {
            return Number;
        }

        public void Place()
        {
            if (_orderItems.Count == 0)
                AddNotification("Order", "Este pedido não possui itens no carrinho");

            Number = Guid.NewGuid().ToString("N").ToUpper();
        }

        public void Pay()
        {
            Status = EOrderStatus.Paid;
        }

        public void Ship()
        {
            var delivery = new Delivery(DateTime.Now.AddDays(5));
            _deliveries.Add(delivery);
            Status = EOrderStatus.Shipped;
        }

        public void Cancel()
        {
            if (Status != EOrderStatus.Shipped)
            {
                Status = EOrderStatus.Canceled;
                _deliveries.ToList().ForEach(d => d.Cancel());
            }
        }

    }
}
