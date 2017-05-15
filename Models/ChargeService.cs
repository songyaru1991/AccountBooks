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
        private IUnitOfWork _unitWork;
        private IRepository<ChargeModels> _accountBookRep;

        public ChargeService(IUnitOfWork UnitOfWork)
        {
            _unitWork = UnitOfWork;
            _accountBookRep = new Repository<ChargeModels>(UnitOfWork);
        }

        public IEnumerable<ChargeModels> ShowAllRecordsByPagination(int pageNumber, int pageSize)
        {                     
            return _accountBookRep.ShowAllRecordsByPagination(pageNumber, pageSize);
        }

        public PagedList<ChargeModels> ajaxSearchGetResult(string year, string month, string Category, DateTime selectDate, int pageSize, int pageNumber = 1)
        {
            return _accountBookRep.ajaxSearchGetResult(year, month,Category,selectDate, pageSize, pageNumber);         
        }

        public bool Add(ChargeModels charge)
        {           
            var chargeRecord = new ChargeModels()
            {
                Id = Guid.NewGuid(),
                Amount = charge.Amount,
                Category=charge.Category,
                Date = charge.Date,
                Remarks = charge.Remarks
            };

            _accountBookRep.Add(chargeRecord);

            var isAdd = this._unitWork.Commit();
            return isAdd;
        }

        public bool DelectCharge(Guid id)
        {
            ChargeModels charge = _unitWork.dbContext.Charge.Find(id);
            _accountBookRep.Remove(charge);

            var isDelect = this._unitWork.Commit();
            return isDelect;
        }

        public ChargeModels EditItem(Guid id)
        {
            ChargeModels charge = _unitWork.dbContext.Charge.Find(id);
            return charge;
        }

        public bool EditCharge(ChargeModels charge)
        {
            _accountBookRep.Modify(charge);

            bool isEdit = this._unitWork.Commit();
            return isEdit;
        }

        

    }
}