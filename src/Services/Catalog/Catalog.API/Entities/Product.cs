﻿namespace Catalog.API.Entities
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Category { get; set; }
        public decimal Price { get; set; }
    }
}
