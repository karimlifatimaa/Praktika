using DoorangApp.Business.Exceptions;
using DoorangApp.Business.Services.Abstracts;
using DoorangApp.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Composition;

namespace DoorangApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class ExplorerController : Controller
    {
        private readonly IExplorerServices _services;

        public ExplorerController(IExplorerServices services)
        {
            _services = services;
        }

        public IActionResult Index()
        {
            List<Explorer> explorers= _services.GetAllExplorer();
            return View(explorers);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Explorer explore)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _services.AddExplorer(explore);
            }
            catch (FileContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var item = _services.GetExplorer(x => x.Id == id);
            if (item == null)
            {
                ModelState.AddModelError("", "Explore is null referance");
                return RedirectToAction("index");
            }
            return View(item);

        }
        [HttpPost]
        public IActionResult Update(Explorer explore)
        {
            if(!ModelState.IsValid)
            {

                return View();
            }
            try
            {
                _services.UpdateExplore(explore.Id, explore);
            }
            catch (FileContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var item=_services.GetExplorer(x=>x.Id == id);
            if(item == null) throw new NullReferenceException();
            try
            {
                _services.DeleteExplorer(item.Id);
            }
            catch (FileNameNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return RedirectToAction("Index");
        }
    }
}
