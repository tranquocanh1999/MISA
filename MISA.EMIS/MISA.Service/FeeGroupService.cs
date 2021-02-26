using MISA.Common.Model;
using MISA.DataLayer.Interfaces;
using MISA.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Service
{  
   /// <summary>
   /// service cho nhóm khoản thu
   /// </summary>
   /// CreatedBy: TQAnh (22/02/2021)
    public class FeeGroupService : BaseService<FeeGroup>, IFeeGroupService
    {

      
        public FeeGroupService(IFeeGroupRepository customerGroupRepository) : base(customerGroupRepository)
        {

        }
    }
}
