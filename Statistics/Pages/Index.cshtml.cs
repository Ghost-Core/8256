using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Statistics.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        [BindProperty]
        public string Num1 { get; set; }
        [BindProperty]
        public string Num2 { get; set; }
        [BindProperty]
        public string Num3 { get; set; }

        public double Min { get; set; }
        public double Max { get; set; }
        public double Avg { get; set; }
        public double Total { get; set; }
        public string TxtError101 { get; set; }
        public string TxtError102 { get; set; }
        public string Info { get; set; }




        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;

        }

        

        public void OnGet()
        {

        }


        public void OnPost()
        {
            double min=0;
            double max=0;
            bool error101 = false;
            string txtError101 = "";
            
            bool error102 = false;
            string txtError102 = "";
            

            string[] StrInputNums = { Num1, Num2, Num3};
            List<double> NumInputNums = new List<double>();
            double num;
            foreach (string StrNum in StrInputNums)
            {
                if (double.TryParse(StrNum, out num))
                {
                    NumInputNums.Add(num);
                }

                else if (String.IsNullOrEmpty(StrNum))
                {
                    continue;
                }
                else
                {
                    error101 = true;
                }

            }
            if (NumInputNums.Count > 0)
            {
                foreach (double DouNum in NumInputNums)
                {
                    if (DouNum == NumInputNums.Max())
                    {
                        max = DouNum;
                    }
                    else if (DouNum == NumInputNums.Min())
                    {
                        min = DouNum;
                    }

                }

                double avg = NumInputNums.Average();
                double total = NumInputNums.Sum();
                string info = "You entered "+NumInputNums.Count+ " number(s). The following are their statistics.";
                Info = info;
                Avg = avg;
                Min = min;
                Max = max;
                Total = total;
            }
            if (error101 == true)
            {
                txtError101 = "Please enter numbers only! If you see the results, it is based on only two numbers.";

            }
            if (NumInputNums.Count <= 0)
            {
                error102 = true;
            }

            if (error102 == true)
            {
                txtError102 = "Please enter at least 1 numbers!";
                
            }
            TxtError101 = txtError101;
            TxtError102 = txtError102;

        }

    }
}
