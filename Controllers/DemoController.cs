using System.Linq;
using System.Threading.Tasks;
using AngularWebApp.Auth.DB;
using AngularWebApp.DB;
using LinqToDB;
using LinqToDB.Data;
using Microsoft.AspNetCore.Mvc;

namespace AngularWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : Controller
    {
        private LinqDB _db;

        public DemoController(DataConnection db)
        {
            _db = (LinqDB)db;
        }

        public async Task<IActionResult> Index()
        {
            // return the current user count.
            var result = await _db.GetTable<AspNetUsers>().Select(u => u.Email).ToListAsync();
            return Ok(result);
        }

        protected override void Dispose(bool disposing)
        {
            _db?.Dispose();
            if (_db != null)
            {
                _db = null;
            }
        }
    }
}