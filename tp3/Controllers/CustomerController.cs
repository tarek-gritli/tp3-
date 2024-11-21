using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tp3.Models;

public class CustomerController : Controller
{
    private readonly ApplicationDbContext _db;

    public CustomerController(ApplicationDbContext dbContext)
    {
        _db = dbContext;
    }

    public IActionResult Index()
    {
        var customers = _db.customers.Select(c => new
        {
            c.Name,
            DiscountRate = c.MembershipType != null ? c.MembershipType.DiscountRate : 0
        }).ToList();
        return View(customers);
    }
    public IActionResult Create()
    {
        var members = _db.Membershiptypes.ToList();
        ViewBag.member = members.Select(members => new SelectListItem()
        {
            Text = "Membership " + members.Id.ToString(),
            Value = members.Id.ToString()
        });
        return View();
    }
    [HttpPost]
    public IActionResult Create(Customer c)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Errors = ModelState.Values
            .SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            var members = _db.Membershiptypes.ToList();
            ViewBag.member = members.Select(members => new SelectListItem()
            {
                Text = "Membership " + members.Id.ToString(),
                Value = members.Id.ToString()
            });

            return View();
        }
        c.Id = Guid.NewGuid();
        _db.customers.Add(c);
        _db.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

}
