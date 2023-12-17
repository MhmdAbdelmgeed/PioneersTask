﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepository
{
    public interface IUnitOfWork<T> : IDisposable where T : class
    {
        IGenericRepository<T> Repository { get; }

        Task SaveAsync();

        void Save();

    }
}