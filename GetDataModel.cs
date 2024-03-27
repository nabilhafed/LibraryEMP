// GetData.cshtml.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace MyHtmxApp.Pages
{
    public class GetDataModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Simulate data retrieval
            string data = "This is the data from the server at " + DateTime.Now.ToString("HH:mm:ss");
            return new JsonResult(data);
        }
    }
}
