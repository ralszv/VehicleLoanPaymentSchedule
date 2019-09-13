using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceApplication.ViewModels
{
    public class SheduledPayments : LoanViewModel
    {
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal RemainingBalance { get; set; }
        public decimal TotalPaid { get; set; }
    }
}
