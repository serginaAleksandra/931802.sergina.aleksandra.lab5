﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab5.Models;
namespace Lab5.Controllers
{
    public class LabsController : Controller
    {
        private readonly ApplicationContext _context;

        public LabsController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Labs.ToListAsync());
        }

        public async Task<IActionResult> Details(Int32? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lab = await _context.Labs
                .SingleOrDefaultAsync(m => m.Id == id);
            if (lab == null)
            {
                return NotFound();
            }

            return View(lab);
        }

        public IActionResult Create()
        {
            return View(new Lab());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Lab model)
        {
            if (ModelState.IsValid)
            {
                _context.Labs.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(model);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lab = await _context.Labs
                .SingleOrDefaultAsync(m => m.Id == id);
            if (lab == null)
            {
                return NotFound();
            }

            return View(lab);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Int32? id, Lab model)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lab = await _context.Labs
                .SingleOrDefaultAsync(m => m.Id == id);
            if (lab == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                lab.Name = model.Name;
                lab.Address = model.Address;

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(Int32? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lab = await _context.Labs
                .SingleOrDefaultAsync(m => m.Id == id);
            if (lab == null)
            {
                return NotFound();
            }

            return View(lab);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Int32 id)
        {
            var lab = await _context.Labs.SingleOrDefaultAsync(m => m.Id == id);
            _context.Labs.Remove(lab);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool LabExists(int id)
        {
            return _context.Labs.Any(e => e.Id == id);
        }
    }
}