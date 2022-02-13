using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Components
{
    [ViewComponent]
    public class Calc
    {
        public int Invoke(int x, int y)
        {
            return x + y;
        }
    }
}
