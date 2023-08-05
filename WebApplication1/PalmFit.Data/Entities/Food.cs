using Palmfit.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Data.Entities
{
    public class Food : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public string Origin { get; set; }
        public string Image { get; set; }
        public decimal Calorie { get; set; }
        public string Unit { get; set; }
        public string FoodClassId { get; set; }
        public FoodClass FoodClass { get; set; }
    }
}