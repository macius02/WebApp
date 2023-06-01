using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Projekt;

namespace Projekt.CRUD
{
    [BindProperties(SupportsGet = true)]
    public class IndexModel : PageModel
    {
        private readonly Projekt.Itemdb _context;

        public String Category { get; set; } = "Any";
        public String? Search { get; set; }

        public IndexModel(Projekt.Itemdb context)
        {
            _context = context;
        }

        public IList<Item> Item { get;set; } = default!;
		public IList<Item> Temp { get; set; } = default!;

        

		public async Task OnGetAsync()
        {
         
            if (_context.Itemos != null)
            {
                Temp = await _context.Itemos.ToListAsync();
                if (Category.Equals("Any") && Search == null) Item = Temp;
                else if(!Category.Equals("Any") && Search == null)
                {
                    for (int i = 0; i < Temp.Count; i++)
                    {
                        if(Temp[i].Category == Category)
                        {
                            Item.Add(Temp[i]);
                        }
                    }
                }
                else if(Category.Equals("Any") && Search != null)
                {
                    for(int i = 0; i < Temp.Count;i++)
                    {
                        if (Temp[i].Name.Contains(Search) )
                        {
                            Item.Add(Temp[i]);
                        }
                    }
                }
                else
                {
					for (int i = 0; i < Temp.Count; i++)
					{
						if (Temp[i].Name.Contains(Search) && Temp[i].Category == Category)
						{
							Item.Add(Temp[i]);
						}
					}
				}

            }
        }
    }
}
