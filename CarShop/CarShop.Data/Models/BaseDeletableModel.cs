﻿namespace CarShop.Data.Models
{
    public class BaseDeletableModel
    {
        public BaseDeletableModel()
        {
            IsDeleted = false;
        }

        public bool IsDeleted { get; set; }
    }
}
