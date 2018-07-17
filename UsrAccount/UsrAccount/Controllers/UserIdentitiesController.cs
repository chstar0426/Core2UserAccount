using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UsrAccount.Data;
using UsrAccount.Models;

namespace UsrAccount.Controllers
{
    [Authorize(Roles = "Manager")]
    public class UserIdentitiesController : Controller
    {
        private readonly AccountContext _context;

        public UserIdentitiesController(AccountContext context)
        {
            _context = context;
        }

        // GET: UserIdentities
        [Authorize(Policy = "rpGroup")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserIdentities.ToListAsync());
        }

        // GET: UserIdentities/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userIdentity = await _context.UserIdentities
                .SingleOrDefaultAsync(m => m.UserId == id);
            if (userIdentity == null)
            {
                return NotFound();
            }

            return View(userIdentity);
        }

        // GET: UserIdentities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserIdentities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Password,NicName,UserGroup,InActivity")] UserIdentity userIdentity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userIdentity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userIdentity);
        }

        // GET: UserIdentities/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userIdentity = await _context.UserIdentities.SingleOrDefaultAsync(m => m.UserId == id);
            if (userIdentity == null)
            {
                return NotFound();
            }
            return View(userIdentity);
        }

        // POST: UserIdentities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserId,Password,NicName,UserGroup,InActivity")] UserIdentity userIdentity)
        {
            if (id != userIdentity.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userIdentity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserIdentityExists(userIdentity.UserId))
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
            return View(userIdentity);
        }

        // GET: UserIdentities/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userIdentity = await _context.UserIdentities
                .SingleOrDefaultAsync(m => m.UserId == id);
            if (userIdentity == null)
            {
                return NotFound();
            }

            return View(userIdentity);
        }

        // POST: UserIdentities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userIdentity = await _context.UserIdentities.SingleOrDefaultAsync(m => m.UserId == id);
            _context.UserIdentities.Remove(userIdentity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserIdentityExists(string id)
        {
            return _context.UserIdentities.Any(e => e.UserId == id);
        }
    }
}
