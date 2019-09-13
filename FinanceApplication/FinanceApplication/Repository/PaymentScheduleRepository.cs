using FinanceApplication.Interfaces;
using FinanceApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceApplication.Repository
{
    public class PaymentScheduleRepository : IPaymentScheduleRepository
    {
        /// <summary>
        /// Based on the the finance option set the months for calculations
        /// </summary>
        /// <param name="requestParameters"></param>
        /// <returns></returns>
      public async Task<LoanViewModel> GetPaymentSchedule(LoanViewModel requestParameters)
        {
            LoanViewModel output = new LoanViewModel();
           
            output = requestParameters;
            int loanMonths = 0;

            switch(output.Term)
            {
                case "One":
                    {
                        loanMonths = 12;
                        break;
                    }
                case "Two":
                    {
                        loanMonths = 24;
                        break;
                    }
                case "Three":
                    {
                        loanMonths = 36;
                        break;
                    }
            }

            output.PaymentSchedule = GetLoanScheduleDetails(requestParameters, loanMonths);


            return output;
        }

        /// <summary>
        /// Calculates the loan schedule
        /// </summary>
        /// <param name="requestParameters"></param>
        /// <param name="loanMonths"></param>
        /// <returns></returns>
        private List<SheduledPayments> GetLoanScheduleDetails(LoanViewModel requestParameters, int loanMonths)
        {
            decimal paymentAmount = requestParameters.LoanAmount / loanMonths;
            DateTime lastPaymentDate = requestParameters.Deliverydate;
            List<SheduledPayments> payments = new List<SheduledPayments>();

            for (int i=1;i<=loanMonths;i++)
            {
                SheduledPayments item = new SheduledPayments();
                item.PaymentDate = GetFirstMondayOfTheMonth(lastPaymentDate);
                item.PaymentAmount = paymentAmount;
                item.RemainingBalance = requestParameters.LoanAmount - (paymentAmount * i);
                item.TotalPaid = paymentAmount * i;
                payments.Add(item);
                lastPaymentDate = item.PaymentDate;
            }

            //add the arrangement and the completion fees on the first and the last entry
            var firstPayment = payments.First();
            firstPayment.PaymentAmount = paymentAmount + requestParameters.ArrangementFee;

            var lastPayment = payments.Last();
            lastPayment.PaymentAmount = paymentAmount + requestParameters.CompletionFee;
            return payments;
        }


        /// <summary>
        /// Gets the 1st Monday of the month
        /// </summary>
        /// <param name="lastPaymentDate"></param>
        /// <returns></returns>
        private DateTime GetFirstMondayOfTheMonth(DateTime lastPaymentDate)
        {
            DateTime date = new DateTime(lastPaymentDate.Year, lastPaymentDate.Month, 1).AddMonths(1);
            while (date.DayOfWeek != DayOfWeek.Monday)
            {
                date = date.AddDays(1);
            }
            return date;
        }
    }
}
