using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SistemaHoteleiro.Models
{
    [Authorize]
    public class CategoryRoom : BaseEntity
    {
        public CategoryRoom(string description, double price)
        {
            Description = description;
            Price = price;
        }
        
        public CategoryRoom()
        {        
            
        }

        [Display(Name = "Categoria de Quarto")]
        [Required(ErrorMessage = "Informe a descrição")]
        public string Description { get; set; }

        [Display(Name = "Preço")]
        [Required(ErrorMessage = "Informe o preço")]
        public double Price { get; set; }

        public Room Room { get; set; }

        public IEnumerable<Room> Rooms { get; set; }
        
        public void Update(string description, double price)
        {
            Description = description;
            Price = price;
        }
    }
}
