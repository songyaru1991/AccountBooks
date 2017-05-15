using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccountBooks.Models;
using AccountBooks.Repository;
using AccountBooks.Filter;
using Webdiyer.WebControls.Mvc;
using System.Globalization;

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
        int pageSize = 5;

       // [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();         
        }

        public ActionResult ChargeAmount(string year,string month, string Category,string selectTime, int pageNumber=1)
        {
            if (selectTime != null)
            {
                year = null;
                month = null;
            }
          //  DateTime selectDate = Convert.ToDateTime(selectTime); //此方式string格式有要求，必须是yyyy-MM-dd hh:mm:ss
            DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
            dtFormat.ShortDatePattern = "yyyy/MM";
            DateTime selectDate = Convert.ToDateTime(selectTime, dtFormat);
            var model = chargeService.ajaxSearchGetResult(year,month,Category,selectDate,pageSize, pageNumber);        
            return View(model);
        }

       // [ChildActionOnly]

        public ActionResult AddCharge()
        {
            return View();
        }

        /* ValidationMessage的误触发 
         * 错误的验证是在ModelState去取的，所以对于Action的参数的Model要注意
         * Action有参数时页面上class="input-validation-error"
         * 无参数时则为正常class="field-validation-valid"
         public ActionResult AddCharge(ChargeModels charge)
        {
            return View();
        }  
        */
        //[ValidateAntiForgeryToken]
        public ActionResult Add(ChargeModels charge)
        {
            if (ModelState.IsValid)
            {
                if (chargeService.Add(charge))
                {
                    return Content("<script>alert('新增成功!');location.href='../Charge/Index';</script>");
                  //  return RedirectToAction("Index");
                }
                else
                {
                    return Content("<script>alert('新增失败!');location.href='../Charge/Index';</script>");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(Guid id)
        {
            if (chargeService.DelectCharge(id))
            {
               return Content("<script>alert('删除成功!');location.href='../Index';</script>");
            }
            else{
                return Content("<script>alert('删除失败!');location.href='../Index';</script>");
              }
        }

        public ActionResult Edit(Guid id)
        {
            ChargeModels charge = chargeService.EditItem(id);
            return View(charge);

        }

        public ActionResult EditCharge(ChargeModels charge)
        {
          //  ChargeModels charge = chargeService.EditItem(id);
            if (chargeService.EditCharge(charge))
            {
                return Content("<script>alert('修改成功!');location.href='../Charge/Index';</script>");
            }
            else
            {
                return Content("<script>alert('修改失败!');location.href='../Charge/Index';</script>");
            }
        }

	}
}