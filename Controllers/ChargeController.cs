using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccountBooks.Models;
using Webdiyer.WebControls.Mvc;
using AccountBooks.Repository;

namespace AccountBooks.Controllers
{
    public class ChargeController : Controller
    {
        private ChargeService chargeService;
        public ChargeController()
        {
            var unitOfWork = new UnitOfWork();
            chargeService = new ChargeService(unitOfWork);
        }
        // GET: /Charge/
        int pageSize = 2;
        public ActionResult Index(int pageNumber = 1)
        {
            //PagedList<ChargeModels> lst = context.Charge.OrderBy(p => p.Id).ToPagedList(pageNumber, pageSize);
            IEnumerable<ChargeModels> lst = chargeService.ShowAllRecordsByPagination(pageNumber, pageSize);
            return View(lst);
        }

        [HttpPost]
        public ActionResult ChargeAmount(ChargeModels chargeItem)
        {
            return View(chargeItem);
        }

        public ActionResult AddCharge(ChargeModels charge)
        {
            return View();
        }

        public ActionResult Add(ChargeModels charge)
        {
            if (ModelState.IsValid)
            {
                if (chargeService.Add(charge))
                {
                    return Content("<script>alert('新增成功!');location.href='../Charge/Index';</script>");
                }
                else
                {
                    return Content("<script>alert('新增失败!');location.href='../Charge/Index';</script>");
                }
            }
            else
                return RedirectToAction("AddCharge");
        }

	}
}