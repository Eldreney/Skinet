using Core.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenericRepository<T>(StoreContext storeContext) : IGenericRepository<T> where T : BaseEntity
    {
        public void Add(T entity)
        {
           storeContext.Set<T>().Add(entity);
        }

        public bool Exists(int id)
        {
            return storeContext.Set<T>().Any(e => e.Id == id);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await storeContext.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<T?> GetEntityWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<TResult?> GetEntityWithSpecAsync<TResult>(ISpecification<T, TResult> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await storeContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> spec)
        {   
         return await ApplySpecification(spec).ToListAsync();
        }

        public void Remove(T entity)
        {
            storeContext.Set<T>().Remove(entity);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await storeContext.SaveChangesAsync() > 0;
        }

        public void Update(T entity)
        {
            storeContext.Set<T>().Attach(entity);
            storeContext.Entry(entity).State = EntityState.Modified;
        }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
            => SpecificationEvaluator<T>.GetQuery(storeContext.Set<T>().AsQueryable(), spec);       
        

    private IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T,TResult> spec)
            => SpecificationEvaluator<T>.GetQuery<T, TResult>(storeContext.Set<T>().AsQueryable(), spec);


    }
        }  
