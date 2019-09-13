using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceApplication.Models
{
    public class Option
    {
        public int OptionId { get; set; }
        public Options OptionValue { get; set; }
    }

    public enum Options
    {
        One, Two, Three
    }
}
