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
    public class UpsertModel : PageModel
    {
        private ApplicationDbContext _db;

        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Arts Arts { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            Arts = new Arts();
            if (id == null)
            {
                //create
                return Page();
            }

            //update
            Arts = await _db.Arts.FirstOrDefaultAsync(u => u.Id == id);
            if(Arts == null)
            {
                return NotFound();
            }
            return Page();
            
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if( Arts.Id == 0)
                {
                    _db.Arts.Add(Arts);
                }
                else
                {
                    _db.Arts.Update(Arts); 
                }

                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }

            return RedirectToPage();
        }
    }
}
