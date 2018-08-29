using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectApi.Data.Core;
using ProjectApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectApi.Service
{
    public class ItemService : IItemService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public Task<List<Item>> GetAll()
        {
            var itemRepo = unitOfWork.GetRepository<Data.Models.Item>();
            var items = itemRepo.GetAll().ToList();
            return Task.FromResult(mapper.Map<List<Item>>(items));
        }

        public Task<Item> Get(long id)
        {
            var itemRepo = unitOfWork.GetRepository<Data.Models.Item>();
            var item = itemRepo.Get(id);
            return Task.FromResult(mapper.Map<Item>(item));
        }

        public Task<Item> Create(Item item)
        {
            var itemRepo = unitOfWork.GetRepository<Data.Models.Item>();
            var result = itemRepo.Add(mapper.Map<Data.Models.Item>(item));
            //unitOfWork.Commit();
            //groupRepo.
            return Task.FromResult(mapper.Map<Item>(result));

        }

        public Task<bool> Update(Item item)
        {
            var itemRepo = unitOfWork.GetRepository<Data.Models.Item>();
            var result = itemRepo.Update(mapper.Map<Data.Models.Item>(item));
            unitOfWork.Commit();

            return Task.FromResult(result == EntityState.Modified);
        }
        
        public Task<bool> Delete(long id)
        {
            var itemRepo = unitOfWork.GetRepository<Data.Models.Item>();
            var result = itemRepo.Delete<long>(id);
            unitOfWork.Commit();

            return Task.FromResult(result == EntityState.Deleted);
        }
    }
}
