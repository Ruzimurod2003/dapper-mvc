using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperApp.Models
{
    public class User
    {
        public int Id { get; set; }
        [DisplayName("Ф.И.О")]
        public string? FullName { get; set; }
        [DisplayName("Возраст")]
        public int Age { get; set; }
        [DisplayName("Электронная почта")]
        [Required]
        public string? Email { get; set; }
    }
}
