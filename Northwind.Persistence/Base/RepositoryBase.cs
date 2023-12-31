using Northwind.Persistence.Interface;
using Northwind.Persistence.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Persistence.Base
{
    internal abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected AdoDbContext _adoContext;

        protected RepositoryBase(AdoDbContext adoContext) {
            _adoContext = adoContext;
        }

        public void Create(SqlCommandModel model) {
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();

        }

        public void Delete(SqlCommandModel model)
        {
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public IEnumerator<T> FindAll<T>(string sql)
        {
            var listOfData = _adoContext.ExecuteReader<T>(sql);
            _adoContext.Dispose();
            return listOfData;
        }


        public IAsyncEnumerator<T> FindAllAsync<T>(SqlCommandModel model)
        {
            var dataT = _adoContext.ExecuteReaderAsync<T>(model);
            _adoContext.DisposeAsync();
            return dataT;
        }


        public IEnumerator<T> FindByCondition<T>(SqlCommandModel model)
        {
            var listOfData = _adoContext.ExecuteReader<T>(model);
            _adoContext.Dispose();
            return listOfData;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(SqlCommandModel model)
        {
            var dataT = _adoContext.ExecuteReaderAsync<T>(model);

            var listData = new List<T>();

            while (await dataT.MoveNextAsync())
            {
                listData.Add(dataT.Current);
            }

            _adoContext.DisposeAsync();

            return listData;
        }

        public void Update(SqlCommandModel model)
        {
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }



    }
}
