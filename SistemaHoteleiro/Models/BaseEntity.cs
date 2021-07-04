using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaHoteleiro.Models
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            CreatedAt = DateTime.Now;
            Active = true;
        }

        public int Id { get; set; }

        [Display(Name = "Criado em")]
        public DateTime CreatedAt { get; private set; }

        [Display(Name = "Ativo")]
        public bool Active { get; private set; }

        public void Deactivate()
        {
            Active = false;
        }
    }
}
