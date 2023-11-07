using Microsoft.AspNetCore.Mvc;
using Scriptingo.Admin.Helpers;
using Scriptingo.Admin.Models;
using Scriptingo.Common;
using Scriptingo.Common.Models;
using System.Drawing;

namespace Scriptingo.Admin.Controllers
{
    public class ConController : BaseLoginedController
    {
        [HttpGet]
        public IActionResult Edit(long? id)
        {
            ViewData["save_button"] = true;
            var model = new ConModel();
            int apiCodeNumber = 0;
            string apiName = "";
            try
            {
                string apiCode = Request.Query["addNewConnectionToApi"].ToString();
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
                    model.api_id = newConnectionApi.ID;
                }
            }
            catch (Exception)
            {

            }
           
            if (id.HasValue)
            {
                var dbCon = new FastApiContext<_con>();
                var con = dbCon.Data.FirstOrDefault(x => x.ID == id.Value);
                var api = (new FastApiContext<_api>()).Data.FirstOrDefault(x => x.ID == con.api_id);
                if (api.user_id == GetUserId())
                {
                    ViewData["back_url"] = Url.Action("manage", "api", new { id = api.api_code });
                    model = new ConModel()
                    {
                        ID = con.ID,
                        active = con.active,
                        api_id = con.api_id,
                        user_id = con.user_id,
                        comment = con.comment,
                        con_type_id = con.con_type_id,
                        db_name = con.db_name,
                        db_password = con.db_password,
                        db_port = con.db_port,
                        db_schema = con.db_schema,
                        db_source = con.db_source,
                        db_user = con.db_user,
                        name = con.name,
                        connection = con.connection
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
        public IActionResult Edit(ConModel model)
        {

            if (ModelState.IsValid)
            {
                var dbCon = new FastApiContext<_con>();
                if (model.ID > 0)
                {
                    var con = dbCon.Data.FirstOrDefault(x => x.ID == model.ID);
                    var api = (new FastApiContext<_api>()).Data.FirstOrDefault(x => x.ID == con.api_id);
                    if (api.user_id == GetUserId())
                    {
                        api = (new FastApiContext<_api>()).Data.FirstOrDefault(x => x.ID == model.api_id);
                        if (api.user_id == GetUserId())
                        {
                            con.name = model.name;
                            con.db_password = model.db_password;
                            con.db_port = model.db_port;
                            con.db_schema = model.db_schema;
                            con.db_user = model.db_user;
                            con.comment = model.comment;
                            con.api_id = model.api_id;
                            con.is_main = model.is_main;
                            con.con_type_id = model.con_type_id;
                            con.active = model.active;
                            dbCon.Entry(con).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            dbCon.SaveChanges();
                            if (con.is_main)
                            {
                                var allOtherCons = dbCon.Data.Where(x => x.api_id == api.ID && x.ID!=con.ID);
                                foreach (var item in allOtherCons)
                                {
                                    item.is_main = false;
                                    dbCon.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                    dbCon.SaveChanges();
                                }
                            }
                            AddSuccessMessage(T("Your update completed successfully"));
                            try
                            {
                                ConnectTryHelper.Connect(con);
                                AddSuccessMessage(T("Connected successfully."));
                            }
                            catch (Exception)
                            {
                                AddErrorMessage(T("Could not connected to DB with your credentials."));
                            }
                            return RedirectToAction("manage", "api", new { id = api.api_code });
                        }
                        else
                        {
                            AddErrorMessage(T("Api not exist or you are not owner."));
                        }
                    }
                    else
                    {
                        AddErrorMessage(T("Api not exist or you are not owner."));
                    }
                }
                else
                {
                    var api = (new FastApiContext<_api>()).Data.FirstOrDefault(x => x.ID == model.api_id);
                    if (api.user_id == GetUserId())
                    {
                        var con = new _con();
                        con.name = model.name;
                        con.db_password = model.db_password;
                        con.db_port = model.db_port;
                        con.db_schema = model.db_schema;
                        con.db_user = model.db_user;
                        con.comment = model.comment;
                        con.api_id = model.api_id;
                        con.is_main = model.is_main;
                        con.con_type_id = model.con_type_id;
                        con.active = model.active;
                        dbCon.Data.Add(con);
                        dbCon.SaveChanges();
                        AddSuccessMessage(T("Your connection created successfully."));
                        if (con.is_main)
                        {
                            var allOtherCons = dbCon.Data.Where(x => x.api_id == api.ID && x.ID != con.ID);
                            foreach (var item in allOtherCons)
                            {
                                item.is_main = false;
                                dbCon.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                dbCon.SaveChanges();
                            }
                        }
                        try
                        {
                            ConnectTryHelper.Connect(con);
                            AddSuccessMessage(T("Connected successfully."));
                        }
                        catch (Exception)
                        {
                            AddErrorMessage(T("Could not connected to DB with your credentials."));
                        }

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
