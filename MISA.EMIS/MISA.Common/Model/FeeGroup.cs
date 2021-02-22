using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Common.Model
{
    /// <summary>
    /// nhóm khách hàng
    /// CreatedBy: TQAnh (22/02/2021) 
    /// </summary>

    public class FeeGroup
    {
        #region Constructor
        #endregion

        #region Method

        #endregion

        #region Property 
        /// <summary>
        /// khóa chính
        /// </summary>
        public int FeeGroupID { get; set; }

        /// <summary>
        /// Tên nhóm khoản thu
        /// </summary>
        public string FeeGroupName { get; set; }

        /// <summary>
        ///bản ghi hệ thống(nếu bản ghi hệ thống không được xóa)  true: có , false: không
        /// </summary>
        public Boolean IsSystem { get; set; }
        #endregion

        #region Other

        /// <summary>
        /// Ngày tạo mới 
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo mới
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày chỉnh sửa gần nhất
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người chỉnh sửa gần nhất
        /// </summary>
        public string ModifiedBy { get; set; }
        #endregion

    }
}
