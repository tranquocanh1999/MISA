using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Common.Model;
using MISA.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
    /// <summary>
    /// Controller cho nhóm khách hàng
    /// </summary>
    ///   CreatedBy: TQAnh (22/02/2021)
    [Route("api/v1/fee-groups")]
    [ApiController]

   
    public class FeeGroupsController : BaseController<FeeGroup>
    {


        public FeeGroupsController(IFeeGroupService feeGroupService) : base(feeGroupService)
        {
        }
    }
}
