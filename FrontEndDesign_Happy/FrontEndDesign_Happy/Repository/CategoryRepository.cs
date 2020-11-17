using Dapper;
using FrontEndDesign_Happy.Models;
using FrontEndDesign_Happy.Repository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndDesign_Happy.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        IConfiguration _configuration;

        DynamicParameters parameters = new DynamicParameters();
        public CategoryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public int Create(Category category)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SP_Insert_Category";
                parameters.Add("Name", category.Name);
                var Insert = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return Insert;
            }
        }

        public int Delete(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SP_Delete_Category";
                parameters.Add("Id", Id);
                var Delete = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return Delete;
            }
        }

        public int Edit(Category category, int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SP_Edit_Category";
                parameters.Add("Id", Id);
                parameters.Add("Name", category.Name);
                var Edit = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return Edit;
            }
        }

        public Category Get(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SP_GetId_Category";
                parameters.Add("Id", Id);
                var GetId = connection.Query<Category>(procName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return GetId;
            }
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SP_GetAll_Category";
                var GetAll = await connection.QueryAsync<Category>(procName, commandType: CommandType.StoredProcedure);
                return GetAll;
            }
        }
    }
}
