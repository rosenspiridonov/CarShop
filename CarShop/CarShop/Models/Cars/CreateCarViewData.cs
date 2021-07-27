﻿namespace CarShop.Models.Cars
{
    using System.Collections.Generic;

    using CarShop.Data.Models;

    public class CreateCarViewData
    {
        public ICollection<Brand> Brands { get; set; }

        public ICollection<Model> Models { get; set; }

        public ICollection<Engine> EngineTypes { get; set; }

        public ICollection<EuroStandard> EuroStandards { get; set; }

        public ICollection<Transmision> TransmisionTypes { get; set; }

        public ICollection<Coupe> CoupeTypes { get; set; }

        public ICollection<Safety> SafetyProperties { get; set; }

        public ICollection<Comfort> ComfortProperties { get; set; }

        public ICollection<Other> OtherProperties { get; set; }

        public ICollection<Exterior> ExteriorProperties { get; set; }

        public ICollection<Interior> InteriorProperties { get; set; }

        public ICollection<Protection> ProtectionProperties { get; set; }

        public ICollection<Special> SpecialProperties { get; set; }
    }
}
