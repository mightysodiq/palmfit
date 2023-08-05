using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Core.Dtos
{
    public class FoodDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public string Origin { get; set; }
        public string Image { get; set; }
        public decimal Calorie { get; set; }
        public string Unit { get; set; }
        public string FoodClassId { get; set; }

    }
}
