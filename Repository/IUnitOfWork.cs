using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountBooks.Repository
{
    /// <summary>
    /// 工作单元
    /// 提供一个保存方法，它可以对调用层公开，为了减少连库次数
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        #region
        IRepository<T> Repository<T>() where T : class;    
        void Commit();
//        void Save();
        #endregion
    }
}