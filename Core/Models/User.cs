using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElKhattabiNaima_Prova6.Core.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(16)]
        public string CF { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(20)]
        public string LastName { get; set; }

        public List<Policy> policies { get; set; } = new List<Policy>();

        public string Print()
        {
            return $"Id : {Id}, CF : {CF}, Nome : {Name}, Cognome : {LastName}";
        }

    }
}
