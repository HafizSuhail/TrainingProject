using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stanford_University.BusinessEntities;
using Stanford_University.Models;

namespace Stanford_University.Services
{
    public class StudentService
    {
        //In this Class i Will keep the methods which perform the Business logic

        private readonly Collegedbcontex dbContext;

        public StudentService ()
        {
            dbContext = new Collegedbcontex ();
        }

        public IList<Student> FetchStudents()
        {
            var students = dbContext.Students.Include(p => p.Category)
                                             .Include(p => p.Country)
                                             .Include(p => p.Course)
                                             .ToList();
            return students;
        }

        public StudentEditorModel PreapareStudentEditorModel()
        {
            //Create an object of model class for keep list in it

            var dropdownlists = new StudentEditorModel();
            using (var dbcontexobj = new Collegedbcontex()) // here created the dbclas object
            {
                // 1ST DROPDOWN LIST
                // we are getting list of category objects from DB
                var country = dbcontexobj.Countries.ToList();
                // Create the select listitem for Categories
                dropdownlists.Countries = new List<SelectListItem>();
                // create Categories loop using for each
                dropdownlists.Countries.Add(new SelectListItem { Value = null, Text = "--Select Country--" });
                foreach (var countryitem in country)
                {
                    // Create the categoryname variable
                    var countryname = $"{countryitem.CountryName}";

                    var countryItem = new SelectListItem
                    {
                        Value = countryitem.CountryId.ToString(),
                        Text = countryname
                    };
                    dropdownlists.Countries.Add(countryItem);
                }
                // 2ND DROPDOWN LIST
                // we are getting list of category objects from DB
                var category = dbcontexobj.Categories.ToList();
                // Create the select listitem for Categories
                dropdownlists.Categories = new List<SelectListItem>();
                // create Categories loop using for each
                dropdownlists.Categories.Add(new SelectListItem { Value = null, Text = "--Select Category--" });
                foreach (var categitem in category)
                {
                    // Create the categoryname variable
                    var categoryname = $"{categitem.CategoryName}";

                    var categoryItem = new SelectListItem
                    {
                        Value = categitem.CategoryId.ToString(),
                        Text = categoryname
                    };
                    dropdownlists.Categories.Add(categoryItem);
                }

                //3RD DROPDOWN LIST
                // we are getting list of category objects from DB
                var course = dbcontexobj.Courses.ToList();
                // Create the select listitem for Categories
                dropdownlists.Courses = new List<SelectListItem>();
                // create Categories loop using for each
                dropdownlists.Courses.Add(new SelectListItem { Value = null, Text = "--Select Course--" });
                foreach (var courseitem in course)
                {
                    // Create the categoryname variable
                    var coursename = $"{courseitem.Title}|{courseitem.Duration}Days|{courseitem.Price}/INR";

                    var courseItem = new SelectListItem
                    {
                        Value = courseitem.CourseId.ToString(),
                        Text = coursename
                    };
                    dropdownlists.Courses.Add(courseItem);
                }
            }

            return dropdownlists;
        }


        public Student CreateStudent(StudentEditorModel userinputs, int userid)
        {
            // create an object of Student Entity Class 
            Student newstudent = new Student();

            //3,Assign form input to Entity Class object

            newstudent.RollNo = userinputs.RollNo;
            newstudent.StudentName = userinputs.SName;
            newstudent.Gender = userinputs.gender;
            newstudent.Dob = userinputs.dob;
            newstudent.MobileNumber = userinputs.mobilenumber;
            newstudent.Email = userinputs.email;
            newstudent.CountryId = userinputs.countryid;
            newstudent.CategoryId = userinputs.categoryid;
            newstudent.CourseId = userinputs.courseid;
            newstudent.UserId = userid;

            dbContext.Students.Add(newstudent);
            dbContext.SaveChanges();

            return newstudent;
        }

        public StudentEditorModel Studenteditorform(int Studentid)
        {
           
            // Create a varaible & Fetch the studentID from DB_Class
            var fetchstudent = dbContext.Students.Where(p => p.StudentId == Studentid).FirstOrDefault();

            // create an object of model class
            var modeldataandlists = new StudentEditorModel();
       
            using (var dbcontexobj = new Collegedbcontex()) // here created the dbclas object
            {
                // 2ND DROPDOWN LIST
                // we are getting list of category objects from DB
                var country = dbcontexobj.Countries.ToList();
                // Create the select listitem for Categories
                modeldataandlists.Countries = new List<SelectListItem>();
                // create Categories loop using for each
                modeldataandlists.Countries.Add(new SelectListItem { Value = null, Text = "--Select Country--" });
                foreach (var countryitem in country)
                {
                    // Create the categoryname variable
                    var countryname = $"{countryitem.CountryName}";

                    var countryItem = new SelectListItem
                    {
                        Value = countryitem.CountryId.ToString(),
                        Text = countryname
                    };
                    modeldataandlists.Countries.Add(countryItem);
                }

                // 1ST DROPDOWN LIST
                // we are getting list of category objects from DB
                var category = dbcontexobj.Categories.ToList();
                // Create the select listitem for Categories
                modeldataandlists.Categories = new List<SelectListItem>();
                // create Categories loop using for each
                modeldataandlists.Categories.Add(new SelectListItem { Value = null, Text = "--Select Category--" });
                foreach (var categitem in category)
                {
                    // Create the categoryname variable
                    var categoryname = $"{categitem.CategoryName}";

                    var categoryItem = new SelectListItem
                    {
                        Value = categitem.CategoryId.ToString(),
                        Text = categoryname
                    };
                    modeldataandlists.Categories.Add(categoryItem);
                }

                //3RD DROPDOWN LIST
                // we are getting list of category objects from DB
                var course = dbcontexobj.Courses.ToList();
                // Create the select listitem for Categories
                modeldataandlists.Courses = new List<SelectListItem>();
                // create Categories loop using for each
                modeldataandlists.Courses.Add(new SelectListItem { Value = null, Text = "--Select Course--" });
                foreach (var courseitem in course)
                {
                    // Create the categoryname variable
                    var coursename = $"{courseitem.Title}|{courseitem.Duration}Days|{courseitem.Price}/INR";

                    var courseItem = new SelectListItem
                    {
                        Value = courseitem.CourseId.ToString(),
                        Text = coursename
                    };
                    modeldataandlists.Courses.Add(courseItem);
                }
            }

            // and bind the data from student obj
            modeldataandlists.Studentid = fetchstudent.StudentId;
            modeldataandlists.RollNo = fetchstudent.RollNo;
            modeldataandlists.SName = fetchstudent.StudentName;
            modeldataandlists.gender = fetchstudent.Gender;
            modeldataandlists.dob = fetchstudent.Dob;
            modeldataandlists.mobilenumber = fetchstudent.MobileNumber;
            modeldataandlists.email = fetchstudent.Email;
            modeldataandlists.countryid = fetchstudent.CountryId;
            modeldataandlists.categoryid = fetchstudent.CategoryId;
            modeldataandlists.courseid = fetchstudent.CourseId;

            return modeldataandlists;
        }

        public Student UpdateStudent( StudentEditorModel upinputs)
        {
           
            var Studentdata = dbContext.Students.Where(p => p.StudentId == upinputs.Studentid).FirstOrDefault();

            Studentdata.RollNo = upinputs.RollNo;
            Studentdata.StudentName = upinputs.SName;
            Studentdata.Gender = upinputs.gender;
            Studentdata.Dob = upinputs.dob;
            Studentdata.MobileNumber = upinputs.mobilenumber;
            Studentdata.Email = upinputs.email;
            Studentdata.CountryId = upinputs.countryid;
            Studentdata.CategoryId = upinputs.categoryid;
            Studentdata.CourseId = upinputs.courseid;

            dbContext.Students.Update(Studentdata);
            dbContext.SaveChanges();

            return Studentdata;
        }


        //public IList<Student> Studentdata(int Studentid)
        //{
           
        //    var studentobj = dbContext.Students.Include(p => p.Country)
        //                                      .Include(p => p.Category)
        //                                      .Include(p => p.Course)
        //                                      .Include(p => p.User)
        //                                      .FirstOrDefault(p => p.StudentId == Studentid);
        //    return studentobj;
        //}


        public void DeleteOperation(int Studentid) 
        {
            var studentobj = dbContext.Students.Where(p => p.StudentId == Studentid).FirstOrDefault();
            dbContext.Students.Remove(studentobj);
            dbContext.SaveChanges();
        }







    }
}
