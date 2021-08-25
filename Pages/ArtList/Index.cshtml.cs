using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtGallery.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Pages.ArtList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Arts> Arts { get; set; }

        public async Task OnGet()
        {
            Arts = await _db.Arts.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var arts = await _db.Arts.FindAsync(id);
            if(arts== null)
            {
                return NotFound();
            }
            _db.Arts.Remove(arts);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
