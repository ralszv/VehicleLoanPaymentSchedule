using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinanceApplication.Models;
using FinanceApplication.ViewModels;
using FinanceApplication.Repository;
using FinanceApplication.Interfaces;

namespace FinanceApplication.Controllers
{
    public class HomeController : Controller
    {
        private IPaymentScheduleRepository _paymentScheduleRepository;

        public HomeController(IPaymentScheduleRepository paymentScheduleRepository)
        {
            _paymentScheduleRepository = paymentScheduleRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new LoanViewModel());
        }


        /// <summary>
        /// Calls the method that calculates the payment schedule 
        /// </summary>
        /// <param name="requestParameters"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Index(LoanViewModel requestParameters)
        {
            if (ModelState.IsValid)
            {
                LoanViewModel output = await _paymentScheduleRepository.GetPaymentSchedule(requestParameters);
                return View("Index", output);
            }
            else
            { return View(requestParameters); }
        }

        
    }
}
