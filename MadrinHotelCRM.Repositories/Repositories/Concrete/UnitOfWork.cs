using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadrinHotelCRM.DataAccess.Context;
using MadrinHotelCRM.Entities.Models;
using MadrinHotelCRM.Repositories.Repositories.Interfaces;

namespace MadrinHotelCRM.Repositories.Repositories.Concrete
{
    /// <summary>
    /// UnitOfWork sınıfı, AppDbContext üzerinden tüm repository erişimlerini sağlar.
    /// Tek bir SaveChangesAsync ile tüm işlemler commit edilir.
    /// IDisposable uygulanarak DbContext ömrü yönetilir ve bağlantı sızıntıları önlenir.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IReadRepository<T> Read<T>() where T : class
            => new GenericRepository<T>(_context);

        public ICreateRepository<T> Create<T>() where T : class
            => new GenericRepository<T>(_context);

        public IUpdateRepository<T> Update<T>() where T : class
            => new GenericRepository<T>(_context);

        public IDeleteRepository<T> Delete<T>() where T : class
            => new GenericRepository<T>(_context);

        public async Task<int> CommitAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}

