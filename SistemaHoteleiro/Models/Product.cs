using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaHoteleiro.Models
{ 
    public class Product : BaseEntity
    {
        public Product(string name, double price, int stock)
        {
            Name = name;
            Price = price;
            Stock = stock;
        }

        public Product()
        {
            
        }

        [Display(Name = "Nome do Produto")]
        [Required(ErrorMessage = "Informe o nome do produto.")]
        public string Name { get; set; }


        [Display(Name = "Preço")]
        [Required(ErrorMessage = "Informe o preço do produto.")]
        public double Price { get; set; }

        [Display(Name = "Estoque")]
        public int Stock { get; set; }

        public IEnumerable<ReserveProduct> ReserveProducts { get; set; }

        public void Update(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }
}