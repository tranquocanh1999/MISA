using MISA.Common.Model;
using MISA.Common;
using MISA.DataLayer;
using System;
using System.Collections.Generic;
using System.Text;
using MISA.Service.Interfaces;
using MISA.DataLayer.Interfaces;

namespace MISA.Service
{
    /// <summary>
    /// service cho khoản thu
    /// </summary>
    /// CreatedBy: TQAnh (22/02/2021)
    public class FeeService : BaseService<Fee>, IFeeService
    {
        public FeeService(IFeeRepository customerRepository) : base(customerRepository)
        {

        }
        protected override bool ValidateData(Fee fee, ErroMsg erroMsg,int type)
        {
            var isValid = true;
            // 1. Trường bắt buộc nhập

            // check tên khoản thu có trống hay không 
            if ((fee.FeeName == null || fee.FeeName == string.Empty))
            {
                if (erroMsg != null)
                    erroMsg.UserMsg.Add(MISA.Common.Properties.Resources.UserMsg_EmptyFeeName);

                isValid = false;
            }

            // check mức thu có trống hay không 
            if (fee.Price <= 0)
            {
                if (erroMsg != null)
                    erroMsg.UserMsg.Add(MISA.Common.Properties.Resources.UserMsg_EmptyPrice);

                isValid = false;
            }

            // check đơn vị tính (mức thu)  có trống hay không 
            if (fee.Unit <= 0)
            {
                if (erroMsg != null)
                    erroMsg.UserMsg.Add(MISA.Common.Properties.Resources.UserMsg_EmptyUnit);

                isValid = false;
            }


            // check phakm vi áp dụng có trống hay không 
            if (fee.ApplyObject == null || fee.ApplyObject == string.Empty)
            {
                if (erroMsg != null)
                    erroMsg.UserMsg.Add(MISA.Common.Properties.Resources.UserMsg_EmptyApplyObject);

                isValid = false;
            }


            // check kì thu  có trống hay không 
            if (fee.Period <= 0)
            {
                if (erroMsg != null)
                    erroMsg.UserMsg.Add(MISA.Common.Properties.Resources.UserMsg_EmptyPeriod);

                isValid = false;
            }






            var isExits=false;
            // 2. Kiểm tra dữ liệu không được phép trùng : Trùng tên khoản phí 
            // - kiểm tra trong database đã có mã khách hàng hay chưa 
            
                isExits = _dbContext.CheckExits(fee.FeeName.Trim(), "FeeName");
                if (isExits == true)
                {
                    if (erroMsg != null)
                        erroMsg.UserMsg.Add(MISA.Common.Properties.Resources.UserMsg_Duplicated_FeeName);

                    isValid = false;
                }


            if (type == 2)
            {
                var name = _dbContext.GetByID(fee.FeeID).FeeName;
                if (fee.FeeName.Trim().ToLower() == name.ToLower())
                erroMsg.UserMsg.Remove(MISA.Common.Properties.Resources.UserMsg_Duplicated_FeeName);

                if (erroMsg.UserMsg.Count == 0) isValid = true;
            }


            // 2. Kiểm tra nhóm khoản thu đã có hay chwa
            // - kiểm tra trong database đã có nhóm khoản thu hay chưa

            isExits = _dbContext.CheckExits(fee.FeeGroupID, "FeeGroupID", "FeeGroup");
            if (isExits == false&& fee.FeeGroupID>=1)
            {
                if (erroMsg != null)
                    erroMsg.UserMsg.Add(MISA.Common.Properties.Resources.UserMsg_NoGroup);

                isValid = false;
            }
            if (fee.FeeGroupID < 1) fee.FeeGroupID = null;

            return isValid;

        }

        protected override bool isSystem(int id, ErroMsg erroMsg)
        {
            var isValid = true;

            var isSystem = _dbContext.isSystem(id, "FeeID");
            if (isSystem == false)
            {
                if (erroMsg != null)
                    erroMsg.UserMsg.Add(MISA.Common.Properties.Resources.UserMsg_isSystem);

                isValid = false;
            }

            return isValid;
        }

   
    }
}
