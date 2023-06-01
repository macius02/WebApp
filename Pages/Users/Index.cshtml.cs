using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projekt;
using Projekt.Authentication;

namespace Projekt.Pages.Users
{
    [RequireAuth(RequiredRole ="admin")]
    public class IndexModel : PageModel
    {
        private readonly Projekt.Userdb _context;

        public IndexModel(Projekt.Userdb context)
        {
            _context = context;
        }

        public IList<User> User { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Users != null)
            {
                User = await _context.Users.ToListAsync();
            }
        }
    }
}
