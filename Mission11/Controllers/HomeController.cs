using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission11.Models;
using Mission11.Models.ViewModels;

namespace Mission11.Controllers;

public class HomeController : Controller
{
    private IBookRepository _repo;
    
    public HomeController(IBookRepository temp)
    {
        _repo = temp;
    }
    public IActionResult Index(int pageNum)
    {
        int pageSize = 10;
        
        var book = new ProjectListViewModel
        {
            Books = _repo.Books
                .OrderBy(x => x.Title)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),
                
                PaginationInfo = new PaginationInfo
                {
                    currentPage = pageNum,
                    itemsPerPage = pageSize,
                    totalItems = _repo.Books.Count()
                }
        };
            
        return View(book);
    }
}