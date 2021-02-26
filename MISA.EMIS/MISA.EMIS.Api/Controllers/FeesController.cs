using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Common.Model;
using MISA.Service;
using MISA.Service.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
    /// <summary>
    /// controller cho khách hàng
    /// </summary>
    ///   CreatedBy: TQAnh (22/02/2021)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FeesController : BaseController<Fee>
    {


        public FeesController(IFeeService _iFeeService) : base(_iFeeService)
        {
        
        }


        #region Method
        [HttpGet("active")]
        public IActionResult GetActiveFee()
        {

            try
            {
                var serviceResult = _baseService.GetData("select * from Fee where isActive=true");
                var entity = serviceResult.Data as List<Fee>;
                if (entity.Count == 0)
                    return StatusCode(204, serviceResult.Data);
                else return StatusCode(200, serviceResult.Data);
            }
            catch (Exception ex)
            {
                var serviceResult = new ServiceResult();
                var erroMsg = new ErroMsg();
                erroMsg.UserMsg.Add(MISA.Common.Properties.Resources.UserMsg_Exception);
                erroMsg.DevMsg = ex.ToString();
                serviceResult.Data = erroMsg;
                return StatusCode(500, serviceResult.Data);
            }
        }
        #endregion
    }

}
