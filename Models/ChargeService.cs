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
            _accountBookRep = new Repository<ChargeModels>(UnitOfWork);
        }

        public IEnumerable<ChargeModels> ShowAllRecordsByPagination(int pageNumber, int pageSize)
        {           
            
            return _accountBookRep.ShowAllRecordsByPagination(pageNumber, pageSize);
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
            this._unitWork.Commit();

            var isAdd = new bool();
            isAdd = true;
            return isAdd;
        }
    }
}