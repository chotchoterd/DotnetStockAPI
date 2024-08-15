using DotnetStockAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StockAPI.Models;

namespace DotnetStockAPI.Controllers;

// [Authorize(Roles = UserRolesModel.Admin + "," + UserRolesModel.Manager)]
[Authorize]
[ApiController]
[Route("api/[controller]")]
[EnableCors("MultipleOrigins")]
public class CategoryController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CategoryController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("[action]")]
    public ActionResult<category> GetCategories()
    {
        var categories = _context.categories.ToList();
        return Ok(categories);
    }

    [HttpGet("[action]/{id}")]
    public ActionResult<category> GetCategory(int id)
    {
        var category = _context.categories.Find(id);
        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpPost("[action]")]
    public ActionResult<category> AddCategory([FromBody] category category)
    {
        _context.categories.Add(category);
        _context.SaveChanges();

        return Ok(category);
    }

    [HttpPut("[action]/{id}")]
    public ActionResult<category> UpdateCategory(int id, [FromBody] category category)
    {
        var cat = _context.categories.Find(id);
        if (cat == null)
        {
            return NotFound();
        }
        cat.categoryname = category.categoryname;
        cat.categorystatus = category.categorystatus;

        _context.SaveChanges();

        return Ok(cat);
    }

    [HttpDelete("[action]/{id}")]
    public ActionResult<category> DeleteCategory(int id)
    {
        var cat = _context.categories.Find(id);

        if (cat == null)
        {
            return NotFound();
        }

        _context.categories.Remove(cat);
        _context.SaveChanges();

        return Ok(cat);
    }
}