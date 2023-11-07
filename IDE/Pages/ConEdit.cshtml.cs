using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Scriptingo.Common;
using Scriptingo.Common.Models;

namespace Scriptingo.Admin.Pages
{
    public class ConEditModel : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPost(_con model)
        {
            var dbCon=new FastApiContext<_con>();
            var con = dbCon.Data.FirstOrDefault(x => x.ID == model.ID);
            if (con == null)
            {
                dbCon.Data.Add(model);
                dbCon.SaveChanges();
                ViewData["inserted"] = true;
            }
            else
            {
                con.db_port = model.db_port;
                con.db_password = model.db_password;
                con.active = model.active;
                con.connection = model.connection;
                con.con_type_id = model.con_type_id;
                con.db_name=model.db_name;
                con.db_user=model.db_user;
                con.db_source=model.db_source;
                con.name = model.name;
                con.db_schema=model.db_schema;
                dbCon.Entry(con).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbCon.SaveChanges();
                ViewData["updated"] = true;
            }
        }
    }
}
