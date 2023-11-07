using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scriptingo.Admin.Models;
using Scriptingo.Common;
using Scriptingo.Common.Models;

namespace Scriptingo.Admin.Controllers
{
    public class ApiController : BaseLoginedController
    {
        // GET: ApiController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ApiController/Details/5
        public ActionResult Manage(int id)
        {
            var model = new ApiModel();
            var dbApi = new FastApiContext<_api>();
            var api = dbApi.Data.FirstOrDefault(x => x.api_code == id && x.user_id == GetUserId());
            if (api != null)
            {
                model = new ApiModel()
                {
                    Active = api.active,
                    Description = api.description,
                    Host = api.host,
                    IsPrivate = api.is_private,
                    ID = api.api_code,
                    Name = api.name,
                    ApiCode = api.api_code
                };
            }
            else
            {
                //AddWarningMessage("Api not exist or you are not owner.");
                return RedirectToAction("index");
            }
            return View(model);
        }

        // GET: ApiController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApiController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ApiController/Edit/5
        public ActionResult Edit(long? id)
        {
            var model = new ApiModel();
            var dbApi = new FastApiContext<_api>();
            if (id.HasValue)
            {
                var api = dbApi.Data.FirstOrDefault(x => x.api_code == id && x.user_id == GetUserId());
                if (api != null)
                {
                    model = new ApiModel()
                    {
                        Active = api.active,
                        Description = api.description,
                        Host = api.host,
                        ID = api.ID,
                        IsPrivate = api.is_private,
                        Name = api.name
                    };
                }
                else
                {
                    AddErrorMessage(T("Your requrested api is not exist or you are not owner."));
                    return RedirectToAction("index");
                }
            }
            else
            {

            }
            return View(model);
        }

        // POST: ApiController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApiModel model)
        {
            var dbApi = new FastApiContext<_api>();
            if (dbApi.Data.Count(x => x.name == model.Name && x.api_code != model.ID) > 0)
                ModelState.AddModelError("Name", T("Api name is already using."));
            else if (ModelState.IsValid)
            {
                if (model.ID <= 0)
                {
                    long apiCode;
                    while (true)
                    {
                        apiCode = (new Random()).Next(100000, 999999);
                        if (dbApi.Data.Count(x => x.api_code == apiCode) == 0)
                        {
                            break;
                        }
                    }
                    var api = new _api()
                    {
                        name = model.Name,
                        description = model.Description,
                        active = model.Active,
                        api_code = apiCode,
                        is_private = model.IsPrivate,
                        host = model.Host,
                        user_id = GetUserId().Value
                    };
                    dbApi.Add(api);
                    dbApi.SaveChanges();
                    AddSuccessMessage(T("Your api saved successfully."));
                }
                else
                {
                    var api = dbApi.Data.FirstOrDefault(x => x.ID == model.ID);
                    api.description = model.Description;
                    api.active = model.Active;
                    api.host = model.Host;
                    api.is_private = model.IsPrivate;
                    dbApi.Entry(api).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbApi.SaveChanges();
                    AddSuccessMessage(T("Your api updated successfully."));
                }
            }
            return View(model);
        }

        // GET: ApiController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ApiController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
