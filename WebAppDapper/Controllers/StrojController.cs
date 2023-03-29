using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using NuGet.Protocol.Core.Types;
using Services;
using System.Data;
using WebAppDapper.Models;

public class StrojController : Controller
{
    private readonly IStrojServices _strojservices;

    public StrojController(IStrojServices? strojServices)
    {
        _strojservices = strojServices;
    }

    public IActionResult Index()
    {
        
            return View(_strojservices.GetStrojs());
        
    }

    [HttpGet]
    [Route("Stroj/GetById/{id}")]
    public IActionResult GetById(int id)
    {

        return View(_strojservices.GetStrojById(id));

    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Stroj strojevi)
    {
        if (ModelState.IsValid)
        {

                _strojservices.CreateStrojs(strojevi);
                return RedirectToAction(nameof(Index));

        }

        return View(strojevi);
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
 
            
            return View(_strojservices.GetByIdUpdate(id));

    }

    [HttpPost]
    public IActionResult Update(int id, Stroj strojevi)
    {
        if (id != strojevi.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            
                _strojservices.UpdateKvar(id,strojevi);
                return RedirectToAction(nameof(Index));
            
            
        }

        return View(strojevi);
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {       
            _strojservices.DeleteKvar(id);
            return RedirectToAction(nameof(Index));

    }
}

