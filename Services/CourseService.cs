using Stanford_University.BusinessEntities;
using Stanford_University.Models;

namespace Stanford_University.Services
{
    public class CourseService
    {
        private readonly Collegedbcontex dbContext;
        public CourseService() 
        {
            dbContext = new Collegedbcontex();
        }

        public IList<Course> CourseList()
        {
             // Create an varaible of DB Tablelist
            var Courselist = dbContext.Courses.ToList();

            return Courselist;
        }

        public Course CreateCourse(CourseEditorModel CformInputs) 
        {
            // Follow these steps
            // 1,Model Binding (Internally its done by binding the model Class)
            // 2,Create an abject of Course Entity Class
            // 3,Taking the userinputs into entity class object
            // 4,Create the object of dbcontex class 
            // 5,Add form inputs to dbcontex class
            // 6,save changes of dbcontex class


            // 2,object of Course Entity Class
            //3,Assign form input to Entity Class object
            Course newcourse = new Course
            {

                Title = CformInputs.CourseName,
                Duration = CformInputs.Duration,
                Price = CformInputs.Price
            };

            // 5,Add form inputs to dbcontex clas
            dbContext.Add(newcourse);

            // 6,save changes of dbcontex class
            dbContext.SaveChanges();

            return newcourse;

        }

        public CourseEditorModel CourseEditorform(int Courseid)
        {

            // Create a varaible & Fetch the studentID from DB_Class
            var fetchcourse = dbContext.Courses.Where(p => p.CourseId == Courseid).FirstOrDefault();
            // Create an object of Model Class
            var Cmodelobj = new CourseEditorModel();
            // Bind the data from ({Smodelobj}_Model-Class Object with {FetchStuID}_DB-Object) 

            Cmodelobj.CourseName = fetchcourse.Title;
            Cmodelobj.Duration = fetchcourse.Duration;
            Cmodelobj.Price = fetchcourse.Price;
            Cmodelobj.Courseid = fetchcourse.CourseId;

            return Cmodelobj;

        }

        public Course UpdateCourse (CourseEditorModel cupdateinputs)
        {
            //fetching the course  from database
            var fetchcourse = dbContext.Courses.Where(p => p.CourseId == cupdateinputs.Courseid).FirstOrDefault();

            // updating the details of existing course

            fetchcourse.CourseId = cupdateinputs.Courseid;
            fetchcourse.Title = cupdateinputs.CourseName;
            fetchcourse.Duration = cupdateinputs.Duration;
            fetchcourse.Price = cupdateinputs.Price;

            dbContext.Courses.Update(fetchcourse);
            dbContext.SaveChanges();

            return fetchcourse;
        }

        public void DeleteCourse (int Courseid)
        {
            // get student obj
            var courseobj = dbContext.Courses.Where(p => p.CourseId == Courseid).FirstOrDefault();
            dbContext.Courses.Remove(courseobj);
            dbContext.SaveChanges();
        }


    }
}
