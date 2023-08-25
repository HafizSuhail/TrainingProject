using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stanford_University.BusinessEntities;
using Stanford_University.Models;
using Stanford_University.Services;

namespace Stanford_University
{

    [Authorize(Roles = Roles.Admin)]
    public class CourseController : Controller
    {

        private readonly CourseService _CourseService;

        public CourseController ()
        {
            _CourseService = new CourseService ();
        }
        public IActionResult CoursesList()
        {
            var Courselist = _CourseService.CourseList();
            return View(Courselist);
        }

        [HttpGet]
        public IActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CourseForm(CourseEditorModel CformInputs) 
        {
            _CourseService.CreateCourse(CformInputs);
            return RedirectToAction("CoursesList");
        }

        public IActionResult CourseRo() 
        {
            return View();
        }

        [HttpGet]
        public IActionResult CourseEditor(int CourseId)
        {
            var uiInputs = _CourseService.CourseEditorform(CourseId);
            return View(uiInputs);
        }
        [HttpPost]
        public IActionResult CEditorUpdate(CourseEditorModel cupdateinputs)
        {
            _CourseService.UpdateCourse(cupdateinputs);
            return RedirectToAction("CoursesList");
        }

        public IActionResult Deletecourse(int courseid)
        {
            try
            {
                _CourseService.DeleteCourse(courseid);
                return Json(true);
            }
            catch { return Json(false); }
            
        }

    }
}
