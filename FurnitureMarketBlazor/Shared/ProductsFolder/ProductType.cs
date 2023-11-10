﻿using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureMarketBlazor.Shared.ProductsFolder
{
    public class ProductType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [NotMapped]
        public bool Editing { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;
    }
}
