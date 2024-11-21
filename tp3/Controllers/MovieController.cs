using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using tp3.Models;

public class MovieController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public MovieController(ApplicationDbContext dbContext, IWebHostEnvironment hostingEnvironment)
    {
        _db = dbContext;
        _hostingEnvironment = hostingEnvironment;
    }


    public IActionResult Index()
    {
        var movies = _db.movies.ToList(); // Ensure DbSet is correctly named as Movies
        return View(movies);
    }

    public IActionResult Create()
    {
        ViewBag.GenreId = new SelectList(_db.genres, "Id", "Name"); // Populate genres for dropdown
        return View();
    }

    [HttpPost]
    public IActionResult Create(Movie movie, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            string wwwRootPath = _hostingEnvironment.WebRootPath;

            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string imagesFolder = Path.Combine(wwwRootPath, "images");

                // Ensure the images directory exists
                if (!Directory.Exists(imagesFolder))
                {
                    Directory.CreateDirectory(imagesFolder);
                }

                string path = Path.Combine(imagesFolder, fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                movie.PhotoPath = "/images/" + fileName;
            }
            else
            {
                ModelState.AddModelError("PhotoPath", "Please upload an image");
                ViewBag.GenreId = new SelectList(_db.genres, "Id", "Name");
                return View(movie);
            }

            _db.movies.Add(movie);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        ViewBag.GenreId = new SelectList(_db.genres, "Id", "Name");
        return View(movie);
    }




}
