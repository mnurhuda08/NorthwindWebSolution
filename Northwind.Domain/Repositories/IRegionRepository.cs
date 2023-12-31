using Northwind.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Domain.Repositories
{
    public interface IRegionRepository
    {
        IEnumerable<Region> FindAllRegion();
        Task<IEnumerable<Region>> FindAllRegionsAsync();
        Region FindAllRegionByID(int id);
        void Insert(Region region);
        void Edit(Region region);
        void Remove(Region region);
    }
}
