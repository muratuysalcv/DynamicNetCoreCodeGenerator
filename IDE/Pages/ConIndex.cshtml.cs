using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Scriptingo.Admin.Pages
{
    public class ConIndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public ConIndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
       
    }
}