using System;
using System.ComponentModel.DataAnnotations;

namespace SmartLMS.ViewModels
{
    public class CategoryViewModel
    {
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
    }
}