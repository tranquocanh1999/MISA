using Dapper;
using MISA.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MISA.DataLayer.Interfaces;

namespace MISA.DataLayer
{
    /// <summary>
    /// kho lưu trữ cho khoản thu
    /// </summary>
    /// CreatedBy: TQAnh ( 22/02/2021)
    public class FeeRepository : MariaDbContext<Fee>,IFeeRepository
    {


   


    }
}
