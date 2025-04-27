﻿using Ordering.Domain.Entities;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Extensions;
internal class InitialData
{
    public static IEnumerable<Customer> Customers =>
    new List<Customer>
    {
        Customer.CreateCustomer(new Guid("58c49479-ec65-4de2-86e7-033c546291aa"), "mehmet","koko", "mehmet@gmail.com"),
        Customer.CreateCustomer(new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d"), "john","douglas", "john@gmail.com")
    };

    public static IEnumerable<Product> Products =>
        new List<Product>
        {
            Product.CreateProduct(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"), "IPhone X", 500),
            Product.CreateProduct(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"), "Samsung 10", 400),
            Product.CreateProduct(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8"), "Huawei Plus", 650),
            Product.CreateProduct(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27"), "Xiaomi Mi", 450)
        };

  
}
