using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElKhattabiNaima_Prova6.Core.Models
{
    public class Policy
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PolicyNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal MontlyPayment { get; set; }
        public EnumPolicyType Type { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

    }
    public enum EnumPolicyType
    {
        RCAuto,
        Furto,
        Vita
    }
}
