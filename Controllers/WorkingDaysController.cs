using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class WorkingDaysController : Controller
    {
        private readonly IWorkingDaysService _workingDaysService;

        public WorkingDaysController(IWorkingDaysService workingDaysService)
        {
            _workingDaysService = workingDaysService;
        }

        // GET: WorkingDays
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Calculate(DateTime startDate, DateTime endDate)
        {
            try
            {
                var result = _workingDaysService.CalculateWorkingDays(startDate, endDate);
                ViewBag.Result = $"Total working days between {startDate:dd/MM/yyyy} and {endDate:dd/MM/yyyy} are: {result}";
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View("Index");
        }
    }

}
