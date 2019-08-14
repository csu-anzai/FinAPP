using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DTOs
{
    public class IncomeCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ImageDTO Image { get; set; }
    }
}
