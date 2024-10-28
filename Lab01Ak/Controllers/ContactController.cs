using Lab01Ak.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaboratoriumASPNET.Controllers;

public class ContactController : Controller
{
    //rozwiazanie tymczasowe
    static private Dictionary<int, ContactModel> _contacts = new Dictionary<int, ContactModel>(){};

    private static int currentId = 0;
    // Lista kontaktów
    public IActionResult Index()
    {
        return View(_contacts);
    }
    
    //formularz dodania kontaktu
    public IActionResult Add()
    {
        return View();
    }

    // odebranie danych z formularza i zapisanie w kontaktach
    [HttpPost]
    public IActionResult Add(ContactModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        model.Id = currentId++;
        _contacts.Add(model.Id, model);
        return View("Index", _contacts);
    }

    public IActionResult Delete(int id)
    {
        _contacts.Remove(id);
        return View("Index", _contacts);
    }

    public IActionResult Details(int id)
    {
        return View(_contacts[id]);
    }
    
    public IActionResult Edit(int id)
    {
        if (_contacts.TryGetValue(id, out var contact))
        {
            return View(contact);
        }
        return NotFound();
    }

    // POST: Zapisz zmiany kontaktu
    [HttpPost]
    public IActionResult Edit(ContactModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        if (_contacts.ContainsKey(model.Id))
        {
            _contacts[model.Id] = model;
            return RedirectToAction("Index");
        }
        
        return NotFound();
    }
}


  