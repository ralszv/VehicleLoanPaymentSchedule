using FinanceApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceApplication.Interfaces
{
    public interface IPaymentScheduleRepository
    {
        Task<LoanViewModel> GetPaymentSchedule(LoanViewModel requestParameters);
    }
}
