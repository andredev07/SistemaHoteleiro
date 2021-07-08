using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SistemaHoteleiro.Models
{
    [Authorize]
    public class Room : BaseEntity
    {
        public Room(int categoryRoomId, string name)
        {
            CategoryRoomId = categoryRoomId;
            Name = name;
        }

        public Room()
        {
            
        }

        [Display(Name = "Número do Quarto")]
        [Required(ErrorMessage = "Informe o Número do Quarto.")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Informe a Categoria do quarto")]
        public int CategoryRoomId { get; set; }


        public IEnumerable<Reserve> Reserves { get; set; }

        public CategoryRoom CategoryRoom { get; set; }

        public void Update(int categoryRoomId)
        {
            CategoryRoomId = categoryRoomId;
        }
    }
}
