using Data.Entities.Abstractions;
using Data.IRepositories;
using Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class Service<TResult> : IService<TResult> where TResult : class, IEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<TResult> _repository;

        public Service(IUnitOfWork unitOfWork, IBaseRepository<TResult> baseRepository)
        {
            _unitOfWork = unitOfWork;
            _repository = baseRepository;
        }
        public async Task<TResult> CreateAsync(TResult entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.SaveAsync();

            return entity;
        }

        public async Task<TResult> DeleteAsync(int id)
        {
            var entity = await _repository.GetAsync(id);

            if (entity == null)
                return null;

            _repository.Remove(entity);

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

        public Task<TResult> UpdateAsync(TResult user)
        {
            throw new NotImplementedException();
        }
    }
}
