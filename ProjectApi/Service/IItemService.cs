using ProjectApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectApi.Service
{
    public interface IItemService
    {
        Task<List<Item>> GetAll();
        Task<Item> Get(long id);
        Task<Item> Create(Item item);
        Task<bool> Update(Item item);
        Task<bool> Delete(long id);
    }
}
