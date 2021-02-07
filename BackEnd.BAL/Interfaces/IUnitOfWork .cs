using BackEnd.BAL.Repository;
using BackEnd.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.BAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        string Save();

        GenericRepository<T> Repository<T>() where T : class, new();

        Task<int> SaveAsync();
        IGenericRepository<ApplicationRole> ApplicationRole { get; }
       IGenericRepository<ApplicationUserRole> ApplicationUserRole{ get; }
        IGenericRepository<ApplicationUser> ApplicationUser { get; }
        IGenericRepository<City> City { get; }
        IGenericRepository<Country> Country { get; } 
      
        IGenericRepository<Category> Category { get; } 
        IGenericRepository<SubCategory> SubCategory{ get; }
       
       
      
    }
}
