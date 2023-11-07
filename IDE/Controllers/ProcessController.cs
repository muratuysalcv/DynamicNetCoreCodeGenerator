using Microsoft.AspNetCore.Mvc;
using Scriptingo.Admin.Helpers;
using Scriptingo.Admin.Models;
using Scriptingo.Common;
using Scriptingo.Common.Models;
using System.Drawing;

namespace Scriptingo.Admin.Controllers
{
    public class ProcessController : BaseLoginedController
    {
        [HttpGet]
        public IActionResult Edit(long? id)
        {
            ViewData["save_button"] = true;
            var model = new ProcessModel();

            int apiCodeNumber = 0;
            string apiName = "";
            try
            {
                string apiCode = Request.Query["addNewProcessToConnection"].ToString();
                apiCodeNumber = Convert.ToInt32(apiCode);
                var newConnectionApi = (new FastApiContext<_api>()).Data.FirstOrDefault(x => x.api_code == apiCodeNumber && x.user_id == GetUserId());
                if (newConnectionApi == null)
                {
                    AddErrorMessage(T("Api not exist or you are not owner."));
                    return RedirectToAction("manage", "api", new { id = apiCodeNumber });
                }
                else
                {
                    ViewData["back_url"] = Url.Action("manage", "api", new { id = apiCodeNumber });
                }
            }
            catch (Exception)
            {

            }

            if (id.HasValue)
            {
                var dbCon = new FastApiContext<_con>();
                var dbProcess = new FastApiContext<_process>();
                var process = dbProcess.Data.FirstOrDefault(x => x.ID == id.Value);
                var con = dbCon.Data.FirstOrDefault(x => x.ID == process.con_id);
                var api = (new FastApiContext<_api>()).Data.FirstOrDefault(x => x.ID == con.api_id && x.user_id == GetUserId());
                if (api != null)
                {
                    ViewData["back_url"] = Url.Action("manage", "api", new { id = api.api_code });
                    model = new ProcessModel()
                    {
                        ID = process.ID,
                        active = process.active,
                        con_id = process.con_id,
                        description = process.description,
                        sql = process.sql,
                        request = process.request,
                        response = process.response,
                        name = process.name,
                    };

                }
                else
                {
                    AddErrorMessage(T("Api not exist or you are not owner."));
                }
            }
            return View(model);
        }

        [HttpPost()]
        public IActionResult Edit(ProcessModel model)
        {

            if (ModelState.IsValid)
            {
                var dbCon = new FastApiContext<_con>();
                var dbProcess = new FastApiContext<_process>();
                if (model.ID > 0)
                {
                    var process = dbProcess.Data.FirstOrDefault(x => x.ID == model.ID);
                    var con = dbCon.Data.FirstOrDefault(x => x.ID == model.con_id);
                    var api = (new FastApiContext<_api>()).Data.FirstOrDefault(x => x.ID == con.api_id);
                    if (api.user_id == GetUserId() && con.api_id == api.ID)
                    {
                        process.name = model.name;
                        process.description= model.description;
                        process.sql = model.sql;
                        process.name= model.name;
                        process.active = model.active;
                        process.con_id = model.con_id;
                        process.request = model.request;
                        process.response = model.response;
                        
                        dbProcess.Entry(process).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        dbProcess.SaveChanges();
                        AddSuccessMessage(T("Process update completed successfully"));
                        return RedirectToAction("manage", "api", new { id = api.api_code });

                    }
                    else
                    {
                        AddErrorMessage(T("Api not exist or you are not owner."));
                    }
                }
                else
                {
                    var con = dbCon.Data.FirstOrDefault(x => x.ID == model.con_id);
                    var api = (new FastApiContext<_api>()).Data.FirstOrDefault(x => x.ID == con.api_id);
                    if (api.user_id == GetUserId())
                    {
                        var process = new _process();
                        process.name = model.name;
                        process.description = model.description;
                        process.sql = model.sql;
                        process.name = model.name;
                        process.active = model.active;
                        process.con_id = model.con_id;
                        process.request = model.request;
                        process.response = model.response;
                        dbProcess.Data.Add(process);
                        dbProcess.SaveChanges();

                        AddSuccessMessage(T("Process created successfully"));
                        return RedirectToAction("manage", "api", new { id = api.api_code });
                    }
                    else
                    {
                        AddErrorMessage(T("Api not exist or you are not owner."));
                    }
                }
            }
            return View(model);
        }
    }
}
