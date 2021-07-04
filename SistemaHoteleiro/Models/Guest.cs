using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SistemaHoteleiro.Models
{
    public class Guest : BaseEntity
    {
        public Guest(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public Guest()
        {
            
        }

        [Display(Name = "Hóspede")]
        public string Name { get; set; }


        [DataType(DataType.PhoneNumber)]
        public long Fone { get; set; }


        [Display(Name = "CPF")]
        public string Cpf { get; set; }


        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        public string Cidade { get; set; }

        public IEnumerable<Reserve> Reserves { get; set; }

        public void Update(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
