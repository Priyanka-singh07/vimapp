using Microsoft.AspNetCore.Mvc;
using static Dapper.SqlMapper;

namespace VIM_WEBAPI.Repositories
{
    public interface IbillsRepository
    {
        IEnumerable<Vimmdetails> GetBillDetails(string approver_no);
        object GetBillDetails();
    
        //Byte[] Readbinaryfile(string path,string name);
         // Task<ActionResult> DownloadFile();

        //   public object GetBillsByQuery();
    }
}
