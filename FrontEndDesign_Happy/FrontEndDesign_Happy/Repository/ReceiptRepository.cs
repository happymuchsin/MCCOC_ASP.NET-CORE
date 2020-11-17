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
    public class ReceiptRepository : IReceiptRepository
    {
        IConfiguration _configuration;

        DynamicParameters parameters = new DynamicParameters();
        public ReceiptRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public int Create(ReceiptVM receiptVM)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SP_Insert_Receipt";
                parameters.Add("OrderDate", receiptVM.OrderDate);
                parameters.Add("itemId", receiptVM.itemId);
                parameters.Add("Quantity", receiptVM.Quantity);
                parameters.Add("TotalPrice", receiptVM.TotalPrice);
                parameters.Add("buyerId", receiptVM.buyerId);
                var Insert = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return Insert;
            }
        }

        public int Delete(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SP_Delete_Receipt";
                parameters.Add("Id", Id);
                var Delete = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return Delete;
            }
        }

        public int Edit(ReceiptVM receiptVM, int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SP_Edit_Receipt";
                parameters.Add("Id", Id);
                parameters.Add("OrderDate", receiptVM.OrderDate);
                parameters.Add("itemId", receiptVM.itemId);
                parameters.Add("Quantity", receiptVM.Quantity);
                parameters.Add("TotalPrice", receiptVM.TotalPrice);
                parameters.Add("buyerId", receiptVM.buyerId);
                var Edit = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return Edit;
            }
        }

        public ReceiptVM Get(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SP_GetId_Receipt";
                parameters.Add("Id", Id);
                var GetId = connection.Query<ReceiptVM>(procName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return GetId;
            }
        }

        public async Task<IEnumerable<ReceiptVM>> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SP_GetAll_Receipt";
                var GetAll = await connection.QueryAsync<ReceiptVM>(procName, commandType: CommandType.StoredProcedure);
                return GetAll;
            }
        }
    }
}
