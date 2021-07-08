using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SistemaHoteleiro.Models
{
    [Authorize]
    public class Reserve : BaseEntity  
    {
        public Reserve(int guestId, int roomId, long fone, long cpf, string email, string cidade,int numberPeople, double totalPrice, DateTime dataInicio, DateTime dataFim)
        {
            GuestId = guestId;
            RoomId = roomId;
            Fone = fone;
            Cpf = cpf;
            Email = email;
            Cidade = cidade;
            NumberPeople = numberPeople;
            TotalPrice = totalPrice;
            DataInicio = dataInicio;
            DataFim = dataFim;
        }

        public Reserve()
        {
            
        }


        [Display(Name = "Hóspede")]
        public int GuestId { get; set; }


        [Display(Name = "Quarto")]
        public int RoomId { get; set; }


        [DataType(DataType.PhoneNumber)]
        public long Fone { get; set; }
        
        
        [Display(Name = "CPF")]
        public long Cpf { get; set; }


        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        public string Cidade { get; set; }


        [Display(Name = "Quantidade de Pessoas")]
        public int NumberPeople { get; set; }


        [Display(Name = "Preço Total")]
        public double TotalPrice { get; set; }


        [Display(Name = "Check-in")]
        public DateTime DataInicio { get; set; }


        [Display(Name = "Check-out")]
        public DateTime DataFim { get; set; }

        

        public Guest Guest { get; set; }


        public Room Room { get; set; }

        public IEnumerable<ReserveProduct> ReserveProducts { get; set; }

        public void Update(int guestId, DateTime dataInicio, DateTime dataFim)
        {
            GuestId = guestId;
            DataInicio = dataInicio;
            DataFim = dataFim;
        }
    }
}
