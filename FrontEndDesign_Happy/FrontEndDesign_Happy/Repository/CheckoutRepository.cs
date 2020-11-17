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
    public class CheckoutRepository : ICheckoutRepository
    {
        IConfiguration _configuration;

        DynamicParameters parameters = new DynamicParameters();
        public CheckoutRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public int Create(CheckoutVM checkoutVM)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SP_Insert_Checkout";
                parameters.Add("receiptId", checkoutVM.receiptId);
                var Insert = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return Insert;
            }
        }

        public int Delete(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SP_Delete_Checkout";
                parameters.Add("Id", Id);
                var Delete = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return Delete;
            }
        }

        public int Edit(CheckoutVM checkoutVM, int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SP_Edit_Checkout";
                parameters.Add("Id", Id);
                parameters.Add("receiptId", checkoutVM.receiptId);
                var Edit = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return Edit;
            }
        }

        public CheckoutVM Get(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SP_GetId_Checkout";
                parameters.Add("Id", Id);
                var GetId = connection.Query<CheckoutVM>(procName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return GetId;
            }
        }

        public async Task<IEnumerable<CheckoutVM>> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SP_GetAll_Checkout";
                var GetAll = await connection.QueryAsync<CheckoutVM>(procName, commandType: CommandType.StoredProcedure);
                return GetAll;
            }
        }
    }
}
