using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Group
{
    public class RegisterGroupModel
    {
        [Required]
        public string Name { get; set; }
    }
}
