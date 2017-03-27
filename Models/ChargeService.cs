using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webdiyer.WebControls.Mvc;
using AccountBooks.Repository;
using Microsoft.Practices.Unity;

namespace AccountBooks.Models
{
    public class ChargeService
    {
  //      private IUnitOfWork UnitofWork { get; set; }
        private IUnitOfWork _unitWork;
        private IRepository<ChargeModels> _accountBookRep;

        public ChargeService(IUnitOfWork UnitOfWork)
        {
            _unitWork = UnitOfWork;
            _accountBookRep = new Repository<ChargeModels>();
        }

        public IEnumerable<ChargeModels> ShowAllRecordsByPagination(int pageNumber, int pageSize)
        {           
            return _accountBookRep.ShowAllRecordsByPagination(pageNumber, pageSize);
        }

        public bool Add(ChargeModels charge)
        {
            //var isAdd = new bool();
            //using (this._unitWork)
            //{
            //    _accountBookRep = this.UnitofWork.Repository<ChargeModels>();

            //    //添加到内存中
            //    context.Products.Add(pro);
            //    //保存到数据库中
            //    bool isAdd = context.SaveChanges() > 0 ? true : false;

            //    _accountBookRep.Add(charge);
            //    this._unitWork.Commit();
            //}
            //isAdd = true;
            //return isAdd;

            var chargeRecord = new ChargeModels()
            {
                Id = Guid.NewGuid(),
                Amount = charge.Amount,
                Category = charge.Category,
                Date = charge.Date,
                Remarks = charge.Remarks
            };

            _accountBookRep.Add(chargeRecord);
            this._unitWork.Commit();

            var isAdd = new bool();
            isAdd = true;
            return isAdd;
        }
    }
}