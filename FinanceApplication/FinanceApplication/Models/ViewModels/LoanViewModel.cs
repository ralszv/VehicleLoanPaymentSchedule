using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FinanceApplication.ViewModels
{
    /// <summary>
    /// ViewModel which is used for data interaction
    /// </summary>
    public class LoanViewModel
    {
        [Required(ErrorMessage = "Please enter the loan amount.")]
        [Range(1, 100000000, ErrorMessage = "Loan amount must be greater than 0")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Please enter the deposit amount.")]
        [MinimumDeposit(PropName = "Amount", ErrorMessage = "Minimum 15% deposit required")]
        public decimal Deposit { get; set; }

        [Required(ErrorMessage = "Please select the term of the loan.")]
        public string Term { get; set; }

        [Required(ErrorMessage = "Please select the delivery date.")]
        [DataType(DataType.Date)]
        [Display(Name = "Delivery Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Deliverydate { get; set; }

        public decimal ArrangementFee { get; set; }

        public decimal CompletionFee { get; set; }

        public decimal LoanAmount
        {
            get { return (Amount - Deposit); }
        }

        [Display(Name = "Payment Schedule")]
        public List<SheduledPayments> PaymentSchedule { get; set; } = new List<SheduledPayments>();


    }

    /// <summary>
    /// Minumum deposit validation method
    /// </summary>
    public class MinimumDepositAttribute : ValidationAttribute
    {
        public string PropName { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo amount = validationContext.ObjectType.GetProperty(PropName);

            var amountValue = amount.GetValue(validationContext.ObjectInstance, null).ToString();

            decimal mimDeposit = (Convert.ToDecimal(amountValue) / 100) * 15;
            if (Convert.ToDecimal(value) < mimDeposit)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return null;
        }
    }
}