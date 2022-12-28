using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Graph;
using Microsoft.IdentityModel.Tokens;
using System.Drawing;
using System.Net;
using VIM_WEBAPI.Models;
using VIM_WEBAPI.Repositories;


namespace VIM_WEBAPI.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    public class vimbillsController : ControllerBase
    {
        IbillsRepository _ibillsRepository;

        public vimbillsController(IbillsRepository ibillsRepository)
        {
            _ibillsRepository = ibillsRepository;
        }
        //[HttpGet]
        //[Route("api/GetEmployeeDetails/{approver_no}")]

        //public ActionResult GetEmployeeList(string approver_no)
        //{
        //    var result = _ibillsRepository.GetEmployeeDetails(approver_no);

        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(result);
        //}

     
        [HttpGet]       
        [Route("api/VimData/{approver_no}")]
        public object DownloadFile(string approver_no)
        {
            Vimmdetails objlist = new Vimmdetails();
           
            IEnumerable<Vimmdetails> collection = _ibillsRepository.GetBillDetails(approver_no);
            if (collection == null)
            {
                return NotFound();
            }
            else
            {
                foreach (var item in collection)
                {
                    String fileserver1 = @"E:\APPLICATION_DATA\HSE_MIS_SYSTEM_ATTACH\CRITICAL_TOOLBOX\" + item.PDFNAME;
                    var provider = new FileExtensionContentTypeProvider();
                    if (!provider.TryGetContentType(fileserver1, out var contentType))
                    {
                        contentType = "application/Octet-stream";
                    }
                    byte[] bytes = System.IO.File.ReadAllBytes(fileserver1);
                 //   objlist.arr = System.Text.Encoding.UTF8.GetString(bytes);
                    objlist.arr = Convert.ToBase64String(bytes);

                    objlist.REQUESTOR = item.REQUESTOR;
                    objlist.MANAGER_EMPNO = item.MANAGER_EMPNO;
                    objlist.FC_OFFICER_EMPNO = item.FC_OFFICER_EMPNO;
                    objlist.FINAL_APPROVER_EMPNO = item.FINAL_APPROVER_EMPNO;
                    objlist.APPROVED_AMT = item.APPROVED_AMT;
                    objlist.DT_APPROVAL = item.DT_APPROVAL;
                    objlist.DOP = item.DOP;
                    objlist.PDFNAME= item.PDFNAME;
                }
                return Ok(objlist);
            }
            
          
        }
       
        [HttpGet]
        [Route("api/GetEmployeeDetails")]

        public object   GetEmployeeList()
        {
            var result = _ibillsRepository.GetBillDetails();
            String fileserver1 = @"E:\APPLICATION_DATA\HSE_MIS_SYSTEM_ATTACH\CRITICAL_TOOLBOX\90002022IS001.PDF";
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileserver1, out var contentType))
            {
                contentType = "application/Octet-stream";
            }


            //byte[] bytes = await System.IO.File.ReadAllBytesAsync(fileserver1);
            //objlist.arr = System.Text.Encoding.UTF8.GetString(bytes);
            //string CONT = Convert.ToBase64String(bytes);





            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
           
        }

    }
}
