using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Services;
using System.Data;
using WebAppDapper.Models;

public class KvarController : Controller
{
    private readonly IKvarServices _kvarServices;

    public KvarController(IKvarServices? kvarServices)
    {
        _kvarServices = kvarServices;
    }

    public IActionResult Index()
    {
        
        return View(_kvarServices.GetKvars());
        
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Kvar kvarovi)
    {
        if (ModelState.IsValid)
        {


            _kvarServices.CreateKvar(kvarovi);


            return RedirectToAction(nameof(Index));


            
        }
        return View(kvarovi);
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        
            Kvar kvarovi = _kvarServices.GetKvarById(id);
            
            return View(kvarovi);


    }

    [HttpPost]
    public IActionResult Update(int id, Kvar kvarovi)
    {
        if (id != kvarovi.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {

            _kvarServices.UpdateKvar(id, kvarovi);
            return RedirectToAction(nameof(Index));

            
        }
        return View(kvarovi);
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {

        _kvarServices.DeleteKvar(id);
        return RedirectToAction(nameof(Index));
        

    }
}

