using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuplementsStore1.Models.ViewModels
{
    public class ProductEditViewModel : ProductCreateViewModel
    {
        public int ProductID { get; set; }
        public string ExistingPhotoPath { get; set; }
    }
}
