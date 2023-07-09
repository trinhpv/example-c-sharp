using Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using TasksMVC.Data;
using TasksMVC.Models;
using TaskStatus = TasksMVC.Models.TaskStatus;

namespace TasksMVC.Controllers
{
    public class WorkController : Controller
    {
        private readonly WorkContext _context;

        public WorkController(WorkContext context)
        {
            _context = context;
        }
        
        
        public async Task<IActionResult> Index(WorkRequest request)
        {
            
            var works = from m in _context.Works
                        select m ;
            switch (request.orderBy)
            {
                case OrderBy.Name:
                    switch (request.orderType)
                    {
                        case OrderType.Descending:
                            works = works.OrderByDescending(x => x.Name); break;
                        default:
                            works = works.OrderByDescending(x => x.Name).Reverse();
                            break;
                    }
                    break;
                case OrderBy.CreatedDate:
                    switch (request.orderType)
                    {
                        case OrderType.Descending:
                            works = works.OrderByDescending(x => x.CreatedDate); break;
                        default:
                            works = works.OrderByDescending(x => x.CreatedDate).Reverse();
                            break;
                    }
                    break;
                case OrderBy.HandleDate:
                    switch (request.orderType)
                    {
                        case OrderType.Descending:
                            works = works.OrderByDescending(x => x.HandleDate); break;
                        default:
                            works = works.OrderByDescending(x => x.HandleDate).Reverse();
                            break;
                    }
                    break;
                default : 
                    break;
            }


            
            if (!String.IsNullOrEmpty(request.searchString))
            {
                works = works.Where(s => s.Name!.Contains(request.searchString));
            }
            if (request.searchDate != null)
            {
                works = works.Where(s => s.CreatedDate! <= request.searchDate && s.HandleDate >= request.searchDate);
            }

            if (request.status != null)
            {
                works = works.Where(s => s.Status == request.status);
            }

            var totalRecord = works.Count();

            var  totalPage = (works !=null)? Math.Ceiling((double) totalRecord / request.perPage) : 0;
            
            ViewBag.TotalRecord = totalRecord;
       
            works = works.Skip((request.page - 1)* request.perPage).Take(request.perPage);
            ViewBag.SearchString = request.searchString;
            ViewBag.SearchDate = request.searchDate;
            ViewBag.TotalPage = totalPage;
            ViewBag.OrderBy = request.orderBy;
            ViewBag.OrderType = request.orderType;
            ViewBag.Page = request.page;
            ViewBag.PerPage = request.perPage;
            ViewBag.Status = request.status;
            

            return View(await works.ToListAsync());
        }

        public string Welcome()
        {
            return "this is welcome page";
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null || _context.Works == null) {
                return NotFound();
            }
            var task = await _context.Works.FindAsync(id);

            if(task == null) { return NotFound(); }
            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if ( _context.Works == null)
            {
                return Problem("Entity not found");
            }

            var task = await _context.Works.FindAsync(id);
            if(task != null)
            {
                 _context.Works.Remove(task);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Work task)
        {
            if(ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return View(task);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Works == null)
            {
                return NotFound();
            }
            var work = await _context.Works
                .FirstOrDefaultAsync(m => m.Id == id);
            if (work == null)
            {
                return NotFound();
            }

            return View(work);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if(_context.Works == null)
            {
                return NotFound();
            }
            var work = await _context.Works.FindAsync(id);
            if (work == null)
            {
                return NotFound();
            }

            return View(work);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] Work work)
        {
            if (_context.Works == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(work);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(work.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(work);
        }

        private bool MovieExists(int id)
        {
            return (_context.Works?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
