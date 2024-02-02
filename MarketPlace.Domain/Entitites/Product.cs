﻿using MarketPlace.Domain.Common;

namespace MarketPlace.Domain.Entitites;

public class Product : AuditableEntity
{
    public long Id { get; set; }
    public string? ProductName { get; set; }
    public string? Description { get; set; }
    public DateTime? Release { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public long LogoImageId { get; set; }
    public Image Image { get; set; } = default!;
    public long CategoryId { get; set; }
    public Category Category { get; set; } = default!;
    public long SellerId { get; set; }
    public Seller Seller { get; set; } = default!;
    public virtual ICollection<Review>? Reviews { get; set; }
    public virtual ICollection<OrderDetailProduct>? OrderDetails { get; set; }
    public virtual ICollection<Specification>? Specifications { get; set; }
    public virtual ICollection<Image>? Images { get; set; }
}
