using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tp3.Models;

public class GenreController : Controller
{
    private readonly ApplicationDbContext _db;

    public GenreController(ApplicationDbContext dbContext)
    {
        _db = dbContext;
    }

    public IActionResult Index()
    {
        var genres = _db.genres.ToList();
        return View(genres);
    }


}
