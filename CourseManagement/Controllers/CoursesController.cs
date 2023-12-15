using Microsoft.AspNetCore.Mvc;
using CourseManagement.ViewModels;
using CourseManagement.Services;

namespace CourseManagement.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICoursesService _coursesService;
        int PAGESIZE = 10;
        public CoursesController(ICoursesService coursesService)
        {
            _coursesService = coursesService;
        }

        // GET: Courses
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) || sortOrder.Equals("title") ? "title_desc" : "";
            ViewData["TopicSortParm"] = String.IsNullOrEmpty(sortOrder) || sortOrder.Equals("topic") ? "topic_desc" : "topic";
            ViewData["ReleaseDateSortParm"] = String.IsNullOrEmpty(sortOrder) || sortOrder.Equals("release_date") ? "release_date_desc" : "release_date";

            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSort"] = sortOrder;

            return View(await _coursesService.GetAllFilter(sortOrder, currentFilter, searchString, pageNumber, PAGESIZE));
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var course = await _coursesService.GetById(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseRequest request)
        {
            if (ModelState.IsValid)
            {
                await _coursesService.Create(request);
                return RedirectToAction(nameof(Index));
            }
            return View(request);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _coursesService.GetById(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseViewModel course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _coursesService.Update(id, course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _coursesService.GetById(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _coursesService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
