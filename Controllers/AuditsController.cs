﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Audit.Data;
using Audit.Models;

namespace Audit.Controllers
{
    public class AuditsController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly DbSet<AuditEntity> _context;
        public AuditsController(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _context = uow.Set<AuditEntity>();
        }

        // GET: Audits
        public async Task<IActionResult> Index()
        {
            return View(await _context.ToListAsync());
        }

        // GET: Audits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auditEntity = await _context
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auditEntity == null)
            {
                return NotFound();
            }

            return View(auditEntity);
        }

        // GET: Audits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Audits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TableName,DateTime,KeyValues,OldValues,NewValues,Username,Action")] AuditEntity auditEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(auditEntity);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(auditEntity);
        }

        // GET: Audits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auditEntity = await _context.FindAsync(id);
            if (auditEntity == null)
            {
                return NotFound();
            }
            return View(auditEntity);
        }

        // POST: Audits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TableName,DateTime,KeyValues,OldValues,NewValues,Username,Action")] AuditEntity auditEntity)
        {
            if (id != auditEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(auditEntity);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuditEntityExists(auditEntity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(auditEntity);
        }

        // GET: Audits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auditEntity = await _context
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auditEntity == null)
            {
                return NotFound();
            }

            return View(auditEntity);
        }

        // POST: Audits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var auditEntity = await _context.FindAsync(id);
            _context.Remove(auditEntity);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuditEntityExists(int id)
        {
            return _context.Any(e => e.Id == id);
        }
    }
}
