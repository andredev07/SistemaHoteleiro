using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaHoteleiro.Models
{
    public class ReserveProduct : BaseEntity
    {
        public ReserveProduct(int reserveId, int productId)
        {
            ReserveId = reserveId;
            ProductId = productId;
        }

        public ReserveProduct()
        {
            
        }

        [Display(Name = "Reserva")]
        [Required(ErrorMessage = "Informe o nome da Reserva.")]
        public int ReserveId { get; set; }

        [Display(Name = "Produto")]
        [Required(ErrorMessage = "Informe o nome do Produto.")]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public Reserve Reserve { get; set; }


        public void Update(int reserveId, int productId) 
        {
            ReserveId = reserveId;
            ProductId = productId;
        }
    }
}