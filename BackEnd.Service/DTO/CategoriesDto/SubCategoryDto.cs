using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Service.DTO.CategoriesDto
{
  public  class SubCategoryDto
    {
        public int Id { get; set; }
        public string SubCategoryName { get; set; }
        public int? ServiceCondition { get; set; }
        public int? CategoryId { get; set; }
    }
    public class ShowSubCategoryDto
    {
        public int? CategoryId { get; set; }
        public int Id { get; set; }
        public string SubCategoryName { get; set; }
        public string ServiceCondition { get; set; }
        public string CategoryName { get; set; } 
    }
}
