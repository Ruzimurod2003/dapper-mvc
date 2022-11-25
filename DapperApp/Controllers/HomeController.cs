using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DapperApp.Models;
using DapperApp.Repository;

namespace DapperApp.Controllers;

public class HomeController : Controller
{
    IUserRepository _repository;
    public HomeController(IUserRepository repo)
    {
        _repository = repo;
    }
    public IActionResult Index()
    {
        return View(_repository.GetAll());
    }

    public IActionResult Details(int id)
    {
        User user = _repository.Get(id);
        if (user != null)
            return View(user);
        return NotFound();
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(User user)
    {
        user.Id = _repository.MaxId;
        _repository.Create(user);
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        User user = _repository.Get(id);
        if (user != null)
            return View(user);
        return NotFound();
    }

    [HttpPost]
    public IActionResult Edit(User user)
    {
        _repository.Update(user);
        return RedirectToAction("Index");
    }

    [HttpGet]
    [ActionName("Delete")]
    public IActionResult ConfirmDelete(int id)
    {
        User user = _repository.Get(id);
        if (user != null)
            return View(user);
        return NotFound();
    }
    [HttpPost]
    public IActionResult Delete(int id)
    {
        _repository.Delete(id);
        return RedirectToAction("Index");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
