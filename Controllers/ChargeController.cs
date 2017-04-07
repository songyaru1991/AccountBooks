using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccountBooks.Models;
using AccountBooks.Repository;
using AccountBooks.Filter;
using Webdiyer.WebControls.Mvc;

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

        public ActionResult ChargeAmount( string Category, int pageNumber=1)
        {

            var model = chargeService.ajaxSearchGetResult(Category, pageSize, pageNumber);
            if (Request.IsAjaxRequest())
                return View(model);
            return View(model);

        }
       
       // [ChildActionOnly]
        //public ActionResult ChargeAmount(int pageNumber = 1)
        //{
        //    IEnumerable<ChargeModels> lst = chargeService.ShowAllRecordsByPagination(pageNumber, pageSize);
        //    return View(lst);
        //}

        //public ActionResult AjaxSearchGet(string Category, int pageNumber = 1)
        //{
        //    var model = chargeService.ajaxSearchGetResult(Category, pageSize, pageNumber);
        //    if (Request.IsAjaxRequest())
        //        return View("ChargeAmount", model);
        //    return View("Index", model);

        //}

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