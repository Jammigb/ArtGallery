using ArtGallery.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtGallery.Controllers
{
    [Route("api/Art")]
    [ApiController]

    public class ArtsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ArtsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Arts.ToListAsync()});
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var artsFromDb = await _db.Arts.FirstOrDefaultAsync(u => u.Id == id);

            if(artsFromDb == null)
            {
                return Json(new { success = false, message = "Error on deleteing" });
            }
            _db.Arts.Remove(artsFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successfil" });
        }
    }
}
