﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Emprise.Admin.Data;
using Emprise.Admin.Entity;
using Emprise.Admin.Models.Script;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Emprise.Admin.Pages.NpcScript
{
    public class AddModel : PageModel
    {
        protected readonly EmpriseDbContext _db;
        private readonly IMapper _mapper;

        public AddModel(EmpriseDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [BindProperty]
        public ScriptInput Script { get; set; }

        public string Tips { get; set; }
        public string SueccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        [BindProperty]
        public string UrlReferer { get; set; }

        public async Task OnGetAsync()
        {
            UrlReferer = Request.Headers["Referer"].ToString();
            if (string.IsNullOrEmpty(UrlReferer))
            {
                UrlReferer = Url.Page("/Script/Index");
            }
        }

        public async Task<IActionResult> OnPostAsync(int id, string position)
        {
            SueccessMessage = "";
            ErrorMessage = "";
            if (!ModelState.IsValid)
            {
                ErrorMessage = ModelState.Where(e => e.Value.Errors.Count > 0).Select(e => e.Value.Errors.First().ErrorMessage).First();
                return Page();
            }

            var script = _mapper.Map<ScriptEntity>(Script);
    
            await _db.Scripts.AddAsync(script);

            await _db.SaveChangesAsync();



            SueccessMessage = $"添加成功！";

            //return RedirectToPage("Edit", new { id = script.Id });

            return Redirect(UrlReferer);
        }
    }
}