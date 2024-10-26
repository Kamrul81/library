using library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace library.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentDBContext dbContext;

        public StudentController(StudentDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            var student = new Student
            {
                Name = viewModel.Name,
                StudentRoll = viewModel.StudentRoll,
                Gender = viewModel.Gender,
                Std = viewModel.Std,
            };

            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await dbContext.Students.ToListAsync();

            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await dbContext.Students.FindAsync(id);

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            var student = await dbContext.Students.FindAsync(viewModel.ID);
            if (student is not null)
            {
                student.Name = viewModel.Name;
                student.StudentRoll = viewModel.StudentRoll;
                student.Gender = viewModel.Gender;
                student.Std = viewModel.Std;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Student");
        }


        /*[HttpPost]
        public async Task<IActionResult> Delete(Student viewModel)
        {
            var student = await dbContext.Students.AsNoTracking().FirstOrDefaultAsync( x=> x.ID == viewModel.ID);

            if (student is not null)
            {
                dbContext.Students.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Student");
        }*/

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var student = await dbContext.Students.FindAsync(id);

            if (student is not null)
            {
                dbContext.Students.Remove(student); // Use the found student entity
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List"); // Redirect after deletion
        }
    }
}