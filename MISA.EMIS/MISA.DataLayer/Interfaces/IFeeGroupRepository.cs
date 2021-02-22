using MISA.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;


namespace MISA.DataLayer.Interfaces
{
    /// <summary>
    /// interface cho kho lưu trữ nhóm khoản thu
    /// </summary>
    ///   CreatedBy: TQAnh (22/02/2021)
    public interface IFeeGroupRepository : IDbContext<FeeGroup>
    {
    }
}
