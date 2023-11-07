using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Scriptingo.Admin.Pages
{
    public class ProcessIndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public ProcessIndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
       
    }
}