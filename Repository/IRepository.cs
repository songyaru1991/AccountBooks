using AccountBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webdiyer.WebControls.Mvc;

namespace AccountBooks.Repository
{
    /// <summary>
    ///     定义仓储模型中的数据标准操作
    /// </summary>
    /// <typeparam name="T">动态实体类型</typeparam>
    public interface IRepository<T> where T:class
    {
  //      IUnitOfWork UnitOfWork { get; set; }
        PagedList<ChargeModels> ShowAllRecordsByPagination(int pageNumber, int pageSize);
        PagedList<ChargeModels> ajaxSearchGetResult(string year, string month, string Category, DateTime selectDate, int pageNumber, int pageSize);        
        IEnumerable<T> FindAll(Func<T, bool> exp);
        void Add(T entity);
        void Modify(T entity);
        void Remove(T entity);
        //bool AddCharge(ChargeModels charge);


    }
}
