using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stanford_University.BusinessEntities;
using Stanford_University.Models;
using Stanford_University.Services;

namespace Stanford_University
{
    [Authorize(Roles = Roles.Admin + "," + Roles.Student)]
    public class StudentController : Controller
    {
        private readonly StudentService _studentService;
        private readonly UserService _userService;
        public StudentController() // Constructor
        {
            _studentService  = new StudentService();
            _userService = new UserService();

        }

        
        public IActionResult StudentList()
        { 
            if(User.IsInRole(Roles.Admin))
            {
                var Students = _studentService.FetchStudents();
                return View(Students);
            }
            else
            {
                return RedirectToAction("AccessDeniedPage", "Account");
            }
              
            
        }

        [HttpGet]
        public IActionResult AddStudent() 
        {
            // new Code with service Class
            StudentEditorModel model = _studentService.PreapareStudentEditorModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult StudentForm(StudentEditorModel UIinputs) 
        {
            ModelState.Remove("Categories");
            ModelState.Remove("Countries");
            ModelState.Remove("Courses");
            if (ModelState.IsValid)
            {
                // new Code with service Class
                var userobj = _userService.CreateUser(UIinputs.SName, "123456", UIinputs.email,Roles.Student);
                _studentService.CreateStudent(UIinputs, userobj.UserId);
                return RedirectToAction("StudentList");
            }
            else
            {
                ModelState.AddModelError("", "Student record not Save, please fix errors and save again!");
                return RedirectToAction("AddStudent", UIinputs);
            }
        }

        [HttpGet]
        public IActionResult StudentEditor(int Studentid)
        {
            // New Code with service Class
            var Updatedform = _studentService.Studenteditorform(Studentid);
            return View(Updatedform);
        }

        [HttpPost]
        public IActionResult updateStudent(StudentEditorModel updatedinputs)
        {
            // new Code with service Class
            _studentService.UpdateStudent(updatedinputs);
            return RedirectToAction("StudentList");
        }
        public IActionResult StudentRo(int Studentid)
        {
            var dbcontex = new Collegedbcontex();
            var studentobj = dbcontex.Students.Include(p => p.Country)
                                              .Include(p => p.Category)
                                              .Include(p => p.Course)
                                              .Include(p => p.User)
                                              .FirstOrDefault(p => p.StudentId == Studentid);

            return View(studentobj);
        }

        public JsonResult DeleteStudent (int Studentid)
        {
            try
            {
                // new Code with service Class
                _studentService.DeleteOperation(Studentid);
                return Json(true);
            }
            catch
            {
                return Json(false);
            }
            
            
        }







    }

}
