﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static CarShop.Web.DataConstants;

namespace CarShop.Web.Data.Models
{
    public class Brand
    {
        public Brand()
        {
            Models = new HashSet<Model>();
            Cars = new HashSet<Car>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(BrandNameMaxLength)]
        public string Name { get; set; }

        public virtual ICollection<Model> Models { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
    }
}