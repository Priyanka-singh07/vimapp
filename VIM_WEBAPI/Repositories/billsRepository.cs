using Dapper;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Oracle.ManagedDataAccess.Client;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Net;
using System.Net.Http.Headers;
using System.Web.Http;
using VIM_WEBAPI.Data;

namespace VIM_WEBAPI.Repositories
{
    public class billsRepository : IbillsRepository
    {
        IConfiguration _configuration;

        string rootpath;

        public billsRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

    
        public IDbConnection GetConnection()
        {
           var connectionString = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
            //var connectionString = _configuration["Data:DefaultConnection:ConnectionStrings"];
            var conn = new OracleConnection(connectionString);
            return conn;
        }

        public object GetBillDetails()
        {
            IEnumerable<Vimmdetails> objlist = null;
            string objslist = null;
            try
            {
                var dyParam = new OracleDynamicParameters();
                // dyParam.Add(name:":APPROVERNO", OracleDbType.Varchar2, ParameterDirection.Input, approver_no);
                dyParam.Add("BILL_DETAIL_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

                var conn = this.GetConnection();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                if (conn.State == ConnectionState.Open)
                {
                    // var query = "S_ESUVIDHA.GET_VIMBILL_BY_APPPROVERNO1";
                    objlist = conn.Query<Vimmdetails>("S_ESUVIDHA.GET_VIMBILL_BY_APPPROVERNO1", dyParam, commandType: CommandType.StoredProcedure);

                   objlist = objlist.ToList();



                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objlist;

        }

        public IEnumerable<Vimmdetails> GetBillDetails(string approver_no)
        {
           
            IEnumerable<Vimmdetails> objlist  =null   ;
            try
            {
                var dyParam = new OracleDynamicParameters();
                dyParam.Add("APPROVERNO", OracleDbType.Varchar2, ParameterDirection.Input, approver_no);
                dyParam.Add("BILL_DETAIL_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

                var conn = this.GetConnection();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                if (conn.State == ConnectionState.Open)
                {
                    // var query = "S_ESUVIDHA.GET_VIMBILL_BY_APPPROVERNO1";
                    objlist = conn.Query<Vimmdetails>("S_ESUVIDHA.GET_VIMBILL_BY_APPPROVERNO", dyParam, commandType: CommandType.StoredProcedure);

                    //   result = results.ToList();
                    objlist.ToList();


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objlist;
        }

   
    }
}
