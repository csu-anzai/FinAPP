using DAL.IRepositories;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class Service<TResult> : IService<TResult> where TResult : class
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IBaseRepository<TResult> _repository;

        public Service(IUnitOfWork unitOfWork, IBaseRepository<TResult> baseRepository)
        {
            _unitOfWork = unitOfWork;
            _repository = baseRepository;
        }
        public async Task<TResult> CreateAsync(TResult entity)
        {
            await _repository.AddAsync(entity);
            var smth = await _unitOfWork.Complete();

            return entity;
        }

        public async Task<IEnumerable<TResult>> ReadAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TResult> ReadAsync(int id)
        {
            return await _repository.GetAsync(id);
        }

        // TODO: Update realization
        public Task<TResult> UpdateAsync(TResult user)
        {
            throw new NotImplementedException();
        }

        public async Task<TResult> DeleteAsync(int id)
        {
            var entity = await _repository.GetAsync(id);

            if (entity == null)
                return null;

            _repository.Remove(entity);
            await _unitOfWork.Complete();

            return entity;
        }
    }
}
