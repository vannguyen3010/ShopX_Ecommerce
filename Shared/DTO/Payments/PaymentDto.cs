﻿using Shared.DTO.Cart;

namespace Shared.DTO.Payments
{
    public class PaymentDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; }
        public IEnumerable<CartItemDto> CartItems { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public bool OrderStatus { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}