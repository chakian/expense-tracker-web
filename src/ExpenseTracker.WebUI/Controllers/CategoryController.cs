using ExpenseTracker.Business;
using ExpenseTracker.Persistence;
using ExpenseTracker.WebUI.Models.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExpenseTracker.WebUI.Controllers
{
    public class CategoryController : BaseAuthenticatedController<CategoryController>
    {
        public CategoryController(ILogger<CategoryController> logger, DbContextOptions<ExpenseTrackerDbContext> options, UserManager<IdentityUser> userManager)
            : base(logger, options, userManager)
        {
        }

        // GET: CategoryController
        public ActionResult Index()
        {
            _logger.LogInformation("Started controller action: Category/Index");

            CategoryBusiness categoryBusiness = new CategoryBusiness(_dbContextOptions);
            var list = categoryBusiness.GetCategoriesOfBudget(BudgetId);

            ListModel listModel = new ListModel();
            listModel.CategoryList = new System.Collections.Generic.List<ListModel.Category>();
            list.ForEach(l =>
            {
                listModel.CategoryList.Add(new ListModel.Category()
                {
                    Id = l.Id,
                    //Balance = 0,
                    //Type = 0,
                    Name = l.Name
                });
            });

            _logger.LogInformation("Finished controller action: Category/Index");

            return View(listModel);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            CategoryBusiness categoryBusiness = new CategoryBusiness(_dbContextOptions);
            var acc = categoryBusiness.GetCategoryDetails(id);
            DetailModel detailModel = new DetailModel()
            {
                Id = acc.Id,
                Name = acc.Name
                //Type = "a",
                //Balance = 502
            };
            return View(detailModel);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            CreateModel createModel = new CreateModel();
            return View(createModel);
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateModel createModel)
        {
            try
            {
                // TODO: Validations
                CategoryBusiness categoryBusiness = new CategoryBusiness(_dbContextOptions);
                categoryBusiness.CreateNewCategory(BudgetId, createModel.Name, UserId);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            CategoryBusiness categoryBusiness = new CategoryBusiness(_dbContextOptions);
            var acc = categoryBusiness.GetCategoryDetails(id);
            UpdateModel updateModel = new UpdateModel()
            {
                Id = acc.Id,
                Name = acc.Name
                //Type = "a",
                //Balance = 502
            };
            return View(updateModel);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UpdateModel updateModel)
        {
            try
            {
                CategoryBusiness categoryBusiness = new CategoryBusiness(_dbContextOptions);
                categoryBusiness.UpdateCategory(id, updateModel.Name, UserId);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            CategoryBusiness categoryBusiness = new CategoryBusiness(_dbContextOptions);
            var acc = categoryBusiness.GetCategoryDetails(id);
            DeleteModel deleteModel = new DeleteModel()
            {
                Id = acc.Id,
                Name = acc.Name
                //Type = "a",
                //Balance = 502
            };
            return View(deleteModel);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                CategoryBusiness categoryBusiness = new CategoryBusiness(_dbContextOptions);
                categoryBusiness.UpdateCategoryAsInactive(id, UserId);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
