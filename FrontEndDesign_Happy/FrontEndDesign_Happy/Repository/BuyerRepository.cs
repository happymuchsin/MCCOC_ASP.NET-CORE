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
    public class BuyerRepository : IBuyerRepository
    {
        IConfiguration _configuration;

        DynamicParameters parameters = new DynamicParameters();
        public BuyerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public int Create(Buyer buyer)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SP_Insert_Buyer";
                parameters.Add("Name", buyer.Name);
                var Insert = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return Insert;
            }
        }

        public int Delete(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SP_Delete_Buyer";
                parameters.Add("Id", Id);
                var Delete = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return Delete;
            }
        }

        public int Edit(Buyer buyer, int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SP_Edit_Buyer";
                parameters.Add("Id", Id);
                parameters.Add("Name", buyer.Name);
                var Edit = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return Edit;
            }
        }

        public Buyer Get(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SP_GetId_Buyer";
                parameters.Add("Id", Id);
                var GetId = connection.Query<Buyer>(procName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return GetId;
            }
        }

        public async Task<IEnumerable<Buyer>> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SP_GetAll_Buyer";
                var GetAll = await connection.QueryAsync<Buyer>(procName, commandType: CommandType.StoredProcedure);
                return GetAll;
            }
        }
    }
}
