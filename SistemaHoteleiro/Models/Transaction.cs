using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaHoteleiro.Models
{
    public class Transaction : BaseEntity
    {
        public Transaction(string type, string source, decimal value)
        {
            Type = type;
            Source = source;
            Value = value;
        }

        public Transaction()
        {

        }

        [Display(Name = "Tipo")]
        public string Type { get; set; }


        [Display(Name = "Origem")]
        public string Source { get; set; }


        [Display(Name = "Valor")]
        public decimal Value { get; set; }
    }
}
