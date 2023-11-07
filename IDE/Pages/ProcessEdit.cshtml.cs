using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Scriptingo.Common;
using Scriptingo.Common.Models;

namespace Scriptingo.Admin.Pages
{
    public class ProcessEditModel : PageModel
    {
        public void OnGet()
        {
        }
        public void OnPost(_process model) {
            var dbProcess = new FastApiContext<_process>();
            var process = dbProcess.Data.FirstOrDefault(x => x.ID == model.ID);
            if (process == null)
            {
                dbProcess.Data.Add(model);
                dbProcess.SaveChanges();
                ViewData["inserted"] = true;
                Response.Redirect("processEdit?id=" + process.ID);
            }
            else
            {
                process.description = model.description;
                process.name= model.name;
                process.sql = model.sql;
                process.con_id = model.con_id;
                process.description = model.description;
                dbProcess.Entry(process).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbProcess.SaveChanges();
                ViewData["updated"] = true;
                Response.Redirect("processEdit?id=" + process.ID);
            }

        }
    }
}
