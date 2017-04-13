using AccountBooks.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Webdiyer.WebControls.Mvc;

namespace AccountBooks.Repository
{
    public class Repository<T>:IRepository<T> where T:class
    {
        readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IUnitOfWork UnitOfWork { get; set; }
        private readonly DataContext _db;
        private readonly DbSet<T> _dbSet;
        #region 构造函数
        public Repository(IUnitOfWork unitOfWork)
        {
            this._db = unitOfWork.dbContext;
            this._dbSet = this._db.Set<T>();
            UnitOfWork = unitOfWork;
        }
        #endregion

        #region IRepository 成员
        public void Add(T item)
        {
            this._dbSet.Add(item);
        }

        public void Remove(T item)
        {
            this._dbSet.Remove(item);
        }

        public void Modify(T item)
        {
            this._db.Entry(item).State = EntityState.Modified;
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return this._dbSet.Where(filter).SingleOrDefault();
        }

        public IEnumerable<T> FindAll(Func<T,bool> exp)
        {
            return this._dbSet.ToList();
        }

        public IEnumerable<T> GetPaged<KProperty>(int pageIndex, int pageSize, out int total, Expression<Func<T, bool>> filter, Expression<Func<T, KProperty>> orderBy, bool ascending = true, string[] includes = null)
        {
             pageIndex = pageIndex > 0 ? pageIndex : 1;
 
             var result = this.GetFiltered(filter, orderBy, ascending, includes);
 
             total = result.Count();
 
             return result.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
 
        public IEnumerable<T> GetFiltered<KProperty>(Expression<Func<T, bool>> filter, Expression<Func<T, KProperty>> orderBy, bool ascending = true, string[] includes = null)
        {
             var result = filter == null ? this._dbSet : this._dbSet.Where(filter);
 
             if (ascending)
                 result = result.OrderBy(orderBy);
             else
                 result = result.OrderByDescending(orderBy);
 
             if (includes != null && includes.Length > 0)
             {
                 foreach (var include in includes)
                 {
                     result = result.Include(include);
                 }
             }
 
             return result.ToList();
        }
 
        #endregion


        PagedList<ChargeModels> IRepository<T>.ShowAllRecordsByPagination(int pageNumber, int pageSize)
        {  //c# Linq及Lamda表达式应用经验之 GroupBy 分组:http://www.cnblogs.com/han1982/p/4138163.html
           // var query = _db.Charge.GroupBy(q => q.Date.Year).Select(q => (new { Date = q.Key, count = q.Count(), Amount = q.Sum(item => item.Amount) }));
            PagedList<ChargeModels> lst= _db.Charge.OrderBy(p => p.Date).ToPagedList(pageNumber, pageSize);
            return lst;
        }

        //LINQ基本语法及其示例 http://blog.csdn.net/ycwol/article/details/42102939
        PagedList<ChargeModels> IRepository<T>.ajaxSearchGetResult(string year, string month, string Category, DateTime selectDate, int pageSize, int pageNumber)
        {
           // logger.Error("This is log4net test");
            var query = _db.Charge.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(Category))
                  query = query.Where(q => q.Category.Contains(Category));
            if (selectDate.Year>=1900)//既日期不为空时
                  query = query.Where(q => q.Date.Year == selectDate.Year && q.Date.Month == selectDate.Month);
            if (!string.IsNullOrWhiteSpace(year) || !string.IsNullOrWhiteSpace(month))
                query = query.Where(q => q.Date.Year.ToString().Contains(year) && q.Date.Month.ToString().Contains(month));

            var model = query.OrderByDescending(p => p.Date).ToPagedList(pageNumber, pageSize);          
            return model;
        }

    }
}