using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_AspCore.Models;

namespace Task_AspCore.Controllers
{

    public class DeveloperController : Controller
    {
        public static List<Developer> dList = new List<Developer>(); 
        private DeveloperContext _DB;
        public DeveloperController(DeveloperContext Db)
        {
            _DB = Db;

        }
        [HttpGet]
        public IActionResult DeveloperList()
        {
            try
            {
                var lst = _DB.Developer.ToList();
                DeveloperList devList = new DeveloperList();
                devList.Developers=lst;
                return View(devList);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.ToString();
                return View();
            }
        }
        public IActionResult Create(Developer dev) 
        {
            return View(dev);
        }
        [HttpPost]
        public async Task<IActionResult> SaveCreate(DeveloperList dev)
        {
            try
            {
                if (dev!=null)
                {
                    foreach (var item in dev.Developers)
                    {
                        _DB.Developer.Add(item);
                        await _DB.SaveChangesAsync();

                    }
                    return RedirectToAction("DeveloperList", "Developer");

                }
            }
            catch (Exception)
            {

                return RedirectToAction("DeveloperList", "Developer");
            }
            return RedirectToAction("DeveloperList", "Developer");
        }
        [HttpPost]
        public async Task<IActionResult> EditDeveloper(Developer dev)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (dev.ID == 0)
                    {
                        _DB.Developer.Add(dev);
                        await _DB.SaveChangesAsync();
                        return RedirectToAction("DeveloperList", "Developer");
                    }
                    else
                    {
                        _DB.Entry(dev).State = EntityState.Modified;
                        await _DB.SaveChangesAsync();

                    }
                }
            }
            catch (Exception)
            {

                return RedirectToAction("DeveloperList", "Developer");
            }
            return RedirectToAction("DeveloperList", "Developer");
        }
        public async Task<IActionResult> DeleteDeveloper(int id)
        {
            try
            {
                var emp = await _DB.Developer.FindAsync(id);
                if (emp != null)
                {
                    _DB.Developer.Remove(emp);
                    await _DB.SaveChangesAsync();
                    return RedirectToAction("DeveloperList", "Developer");
                }
                return RedirectToAction("DeveloperList", "Developer");
            }
            catch (Exception)
            {

                return RedirectToAction("DeveloperList", "Developer");
            }
        }   
    }
}
