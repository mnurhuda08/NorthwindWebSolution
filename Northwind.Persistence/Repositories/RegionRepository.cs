using Northwind.Domain.Entities;
using Northwind.Domain.Repositories;
using Northwind.Persistence.Base;
using Northwind.Persistence.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Persistence.Repositories
{
    internal class RegionRepository : RepositoryBase<Region>, IRegionRepository
    {
        public RegionRepository(AdoDbContext adoContext) : base(adoContext)
        {

        }

        public void Edit(Region region)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "UPDATE region SET regionDescription=@regionDescription WHERE regionId= @regionId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@regionId",
                        DataType = DbType.Int32,
                        Value = region.RegionId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@regionDescription",
                        DataType = DbType.String,
                        Value = region.RegionDescription
                    }
                }
            };
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
         }

        public IEnumerable<Region> FindAllRegion() {
            IEnumerator<Region> dataSet = FindAll<Region>("SELECT RegionID,RegionDescription FROM dbo.region");
            while (dataSet.MoveNext())
            {
                var data = dataSet.Current;
                yield return data;

            }
        }

        public Task<IEnumerable<Region>> FindAllRegionAsync()
        {
            throw new NotImplementedException();
        }

        public Region FindRegionByI(int Id)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT RegionId,RegionDescription FROM DBO.region where regionId=@regionId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@regionId",
                        DataType = DbType.Int32,
                        Value = id
                    }
                }
            };

            var dataSet = FindByCondition<Region>(model);

            Region? item = dataSet.Current;

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }


            return item;
        }
        public void Insert(Region region)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "INSERT INTO region (regionId,RegionDescription) values (@regionId,@regionDescription);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@regionId",
                        DataType = DbType.Int32,
                        Value = region.RegionId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@regionDescription",
                        DataType = DbType.String,
                        Value = region.RegionDescription
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public void Remove(Region region)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM region WHERE regionId=@regionId;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@regionId",
                        DataType = DbType.Int32,
                        Value = region.RegionId
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
    }
}