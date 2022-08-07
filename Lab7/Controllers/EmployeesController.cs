using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab7.Models.DataAccess;
using Lab7.Models;

namespace Lab7.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly StudentRecordContext _context;

        public EmployeesController(StudentRecordContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            EmployeeRoleSelections employeeRoleSelections = new EmployeeRoleSelections();
            return View(employeeRoleSelections);
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeRoleSelections employeeRoleSelections)
        {
            if (!employeeRoleSelections.roleSelections.Any(m => m.Selected))
            {
                ModelState.AddModelError("roleSelections", "You must select at least one role");
            }
            if (_context.Employees.Any(e => e.UserName == employeeRoleSelections.employee.UserName))
            {
                ModelState.AddModelError("employee.UserName", "This username already exists");
            }

            if (ModelState.IsValid)
            {
                _context.Add(employeeRoleSelections.employee);
                _context.SaveChanges();
                foreach (RoleSelection roleSelection in employeeRoleSelections.roleSelections)
                {
                    if (roleSelection.Selected)
                    {
                        EmployeeRole employeeRole = new EmployeeRole { RoleId = roleSelection.role.Id, EmployeeId = employeeRoleSelections.employee.Id };
                        _context.EmployeeRoles.Add(employeeRole);
                    }
                }
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employeeRoleSelections);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            EmployeeRoleSelections employeeRoleSelections = new EmployeeRoleSelections();
            employeeRoleSelections.employee = employee;
            foreach (RoleSelection selection in employeeRoleSelections.roleSelections)
            {
                foreach (Role role in employee.Roles)
                {
                    if (role.Id == selection.role.Id)
                    {
                        selection.Selected = true;
                    }
                }
            }
            return View(employeeRoleSelections);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeRoleSelections employeeRoleSelections)
        {
            if (!employeeRoleSelections.roleSelections.Any(m => m.Selected))
            {
                ModelState.AddModelError("roleSelections", "You must select at least one role");
            }
            if (_context.Employees.Any(e => e.UserName == employeeRoleSelections.employee.UserName))
            {
                ModelState.AddModelError("employee.UserName", "This username already exists");
            }

            if (ModelState.IsValid)
            {
                _context.Update(employeeRoleSelections.employee);
                _context.SaveChanges();
                foreach (RoleSelection roleSelection in employeeRoleSelections.roleSelections)
                {

                    if (roleSelection.Selected)
                    {
                        bool exists = false;
                        foreach (Role role in employeeRoleSelections.employee.Roles)
                        {
                            if (roleSelection.role.Id == role.Id)
                            {
                                exists = true;
                            }
                        }

                        if (!exists)
                        {
                            EmployeeRole employeeRole = new EmployeeRole { RoleId = roleSelection.role.Id, EmployeeId = employeeRoleSelections.employee.Id };
                            _context.EmployeeRoles.Add(employeeRole);
                        }
                    }

                    if (!roleSelection.Selected)
                    {
                        bool exists = false;
                        foreach (Role role in employeeRoleSelections.employee.Roles)
                        {
                            if (roleSelection.role.Id == role.Id)
                            {
                                exists = true;
                            }
                        }

                        if (exists)
                        {
                            EmployeeRole employeeRole = new EmployeeRole { RoleId = roleSelection.role.Id, EmployeeId = employeeRoleSelections.employee.Id };
                            _context.EmployeeRoles.Remove(employeeRole);
                        }
                    }

                }
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employeeRoleSelections);
        }
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UserName,Password")] Employee employee, [Bind("RoleName")] Role roles)
        //{
        //    if (id != employee.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(employee);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!EmployeeExists(employee.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(employee);
        //}

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
