using AccountBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webdiyer.WebControls.Mvc;

namespace AccountBooks.Repository
{
    public interface IRepository<T> where T:class
    {
  //      IUnitOfWork UnitOfWork { get; set; }
        PagedList<ChargeModels> ShowAllRecordsByPagination(int pageNumber, int pageSize);
        PagedList<ChargeModels> ajaxSearchGetResult(string Category,int pageNumber, int pageSize);        
        IEnumerable<T> FindAll(Func<T, bool> exp);
        void Add(T entity);
        void Modify(T entity);
        void Remove(T entity);
        //bool AddCharge(ChargeModels charge);


    }
}
