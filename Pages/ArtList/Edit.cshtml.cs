using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtGallery.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArtGallery.Pages.ArtList
{
    public class EditModel : PageModel
    {
        private ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        } 

        [BindProperty]
        public Arts Arts { get; set; }
        
        public async Task OnGet(int id)
        {
            Arts = await _db.Arts.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var ArtsFromDb = await _db.Arts.FindAsync(Arts.Id);
                ArtsFromDb.Name = Arts.Name;
                ArtsFromDb.Author = Arts.Author;
                ArtsFromDb.ISBN = Arts.ISBN;

                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }

            return RedirectToPage();
        }
    }
}
