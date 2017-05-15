﻿using AccountBooks.Models;
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
        IRepository<T> Repository<T>() where T : class;
        DataContext dbContext{ get; set; }
        bool Commit();
//        void Save();
    }
}