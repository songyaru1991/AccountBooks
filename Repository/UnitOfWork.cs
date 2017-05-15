using AccountBooks.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AccountBooks.Repository
{
    /// <summary>
    /// 此类将负责创建数据上下文实例,并移交到控制器的所有repository实例。
    /// 为了给各个实体维护一个共同的DbContext上下文对象，保证所有的操作都是在共同的上下文中进行
    /// </summary>
    public class UnitOfWork : IUnitOfWork, IDisposable   //IDisposable垃圾资源回收机制
    {
        public DataContext dbContext { get; set; }

        private bool _disposed;
        private Hashtable _repositories;

        public UnitOfWork()
        {
          dbContext = new DataContext();
          this._repositories = new Hashtable();
        }     
         #region  IUnitOfWork 成员
         public IRepository<T> Repository<T>() where T : class
        {
         //   throw new NotImplementedException();
            var typeName = typeof(T).Name;
            if (!this._repositories.ContainsKey(typeName))
            {
                var paramDict = new Dictionary<string, object>();
                paramDict.Add("context", this.dbContext);

                //Repository接口的实现统一在UnitOfWork中执行，通过Unity来实现IOC，同时把IDbContext的实现通过构造函数参数的方式传入
                var repositoryInstance = UnityConfig.Resolve<IRepository<T>>(paramDict);

                if (repositoryInstance != null)
                    this._repositories.Add(typeName, repositoryInstance);
            }
            return (IRepository<T>)this._repositories[typeName];
        }

        public bool Commit()
        {
          return this.dbContext.SaveChanges() > 0 ? true : false;
        }

        #endregion

        // Dispose()销毁了对象,是一种垃圾回收机制。
        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
                if (disposing)
                    this.dbContext.Dispose();

            this._disposed = true;
        }

        #region IDisposable 成员
        //Close后连接可以再次打开;而Dispose后连接字串被清空,连接不能再打开
        public void Dispose()
        {
            //必须为true
            Dispose(true);
            //通知垃圾回收机制不再调用终结器（析构器）
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}