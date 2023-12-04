using crud1.Data;
using crud1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace crud1.Controllers
{
    public class UserController1 : Controller
    {
        private readonly AppDbContext appDbContext;
        public UserController1(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await appDbContext.Users.ToListAsync();
            return View(users);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public  async Task<IActionResult> Add(AddUserViewModel addUserViewModel)
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Name = addUserViewModel.Name,
                Email = addUserViewModel.Email,
                Salary = addUserViewModel.Salary,
                Department = addUserViewModel.Department,
                DateOfBirth = addUserViewModel.DateOfBirth


            };

            await appDbContext.Users.AddAsync(user);
            await appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }



        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var user = await appDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if(user != null)
            {
                var viewModel = new UpdateUser()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Salary = user.Salary,
                    Department = user.Department,
                    DateOfBirth = user.DateOfBirth
                };
                return await Task.Run(()=> View("view",viewModel));
            }


            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateUser model)
        {
            var user = await appDbContext.Users.FindAsync(model.Id);
            if(user != null)
            {
                user.Name = model.Name;
                user.Email = model.Email;
                user.Salary = model.Salary;
                user.DateOfBirth = model.DateOfBirth;
                user.Department = model.Department;

                await appDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateUser model)
        {
            var user = await appDbContext.Users.FindAsync(model.Id);
            if (user != null)
            {
                appDbContext.Users.Remove(user);
                await appDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
