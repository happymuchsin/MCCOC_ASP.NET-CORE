using Dapper;
using FrontEndDesign_Happy.Repository.Interface;
using FrontEndDesign_Happy.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndDesign_Happy.Repository
{
    public class ItemRepository : IItemRepository
    {
        IConfiguration _configuration;

        DynamicParameters parameters = new DynamicParameters();
        public ItemRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public int Create(ItemVM itemVM)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SP_Insert_Item";
                parameters.Add("Name", itemVM.Name);
                parameters.Add("Stock", itemVM.Stock);
                parameters.Add("Price", itemVM.Price);
                parameters.Add("categoryId", itemVM.categoryId);
                var Insert = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return Insert;
            }
        }

        public int Delete(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SP_Delete_Item";
                parameters.Add("Id", Id);
                var Delete = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return Delete;
            }
        }

        public int Edit(ItemVM itemVM, int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SP_Edit_Item";
                parameters.Add("Id", Id);
                parameters.Add("Name", itemVM.Name);
                parameters.Add("Stock", itemVM.Stock);
                parameters.Add("Price", itemVM.Price);
                parameters.Add("categoryId", itemVM.categoryId);
                var Edit = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return Edit;
            }
        }

        public ItemVM Get(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SP_GetId_Item";
                parameters.Add("Id", Id);
                var GetId = connection.Query<ItemVM>(procName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return GetId;
            }
        }

        public async Task<IEnumerable<ItemVM>> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SP_GetAll_Item";
                var GetAll = await connection.QueryAsync<ItemVM>(procName, commandType: CommandType.StoredProcedure);
                return GetAll;
            }
        }
    }
}
