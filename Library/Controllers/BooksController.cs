using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Library.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
      private readonly LibraryContext _db;
      private readonly UserManager<ApplicationUser> _userManager;
      public BooksController(UserManager<ApplicationUser> userManager, LibraryContext db)
      {
        _userManager = userManager;
        _db = db;
      }
      // public async Task<ActionResult> Index()
      // {
      //   var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      //   var currentUser = await _userManager.FindByIdAsync(userId);
      //   var userBooks = _db.Books.Where(entry => entry.BookId == currentUser.Id).ToList();
      //   return View(userBooks);
      // }

      public ActionResult Create()
      {
      ViewBag.PatronId = new SelectList(_db.Books, "BookId", "BookTitle");
      return View();
      }

      // [HttpPost]
      // public async Task<ActionResult> Create(Book book, int )
      // {

      // }
    }
}