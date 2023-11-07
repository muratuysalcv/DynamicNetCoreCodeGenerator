using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scriptingo.Common;
using Scriptingo.FastApi;

namespace Scriptingo.Common
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class FastApiController<T> : Controller where T : BaseModel
    {
        FastApiContext<T> db = null;

        public FastApiController()
        {
            db = new FastApiContext<T>();
        }

        /// <summary>
        /// List Of Data
        /// </summary>
        /// <returns>Returns a object list.</returns>
        [HttpGet]
        public IQueryable<T> List()
        {
            return db.Data.ToList<T>().AsQueryable<T>();
        }
        /// <summary>
        /// Detail Of Data
        /// </summary>
        /// <returns>Returns single object by id.</returns>
        [HttpGet("{id}")]
        public T Detail(int id)
        {
            return db.Data.FirstOrDefault(x => x.ID == id);
        }
        /// <summary>
        /// List Of Data
        /// </summary>
        /// <returns>Returns created object.</returns>
        [HttpPost("{id}")]
        public T Create([FromBody] T value)
        {
            db.Data.Add(value);
            db.SaveChanges();
            return value;
        }
        /// <summary>
        /// List Of Data
        /// </summary>
        /// <returns>Returns created object.</returns>
        [HttpPost("{id}")]
        public T Update([FromBody] T value)
        {
            db.Entry(value).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return value;
        }
    }
}
