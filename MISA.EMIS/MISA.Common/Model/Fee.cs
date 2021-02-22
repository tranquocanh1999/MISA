using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Common.Model
{
    /// <summary>
    /// khoản thu
    /// createdBy: TQAnh (22/02/2021)
    /// </summary>
    public class Fee
    {
        #region Constructor
        #endregion

        #region Method

        #endregion

        #region Property 
        /// <summary>
        /// khóa chính
        /// </summary>
        public int FeeID { get; set; }

        /// <summary>
        /// Tên khoản thu 
        /// </summary>
        public string FeeName { get; set; }

        /// <summary>
        /// Mã nhóm khoản thu
        /// </summary>
        public int? FeeGroupID { get; set; }

        /// <summary>
        /// chi phí 
        /// </summary>
        public Decimal Price { get; set; }

        /// <summary>
        /// Đơn vị tính 1: Ngày , 2: Tháng ,3:Quý , 4:Học kì, 5:Năm học 
        /// </summary>
        public int? Unit { get; set; }


        /// <summary>
        /// phạm vi áp dụng 0: toàn trường  
        /// </summary>
        public String ApplyObject { get; set; }

        /// <summary>
        /// tính chất 1: bắt buộc  - 2: không bắt buộc 
        /// </summary>
        public int? Property { get; set; }

        /// <summary>
        ///kỳ thu 1: tháng , 2:Quý , 3:Học kì  , 4:Năm
        /// </summary>
        public int? Period { get; set; }

        /// <summary>
        /// được miễn giảm true: được miến , false:không
        /// </summary>
        public Boolean IsApplyRemisson { get; set; }

        /// <summary>
        /// bắt buộc đóng true: bắt buộc đóng , false:không
        /// </summary>
        public Boolean IsRequire { get; set; }

        /// <summary>
        /// cho phép tạo hóa đơn true: có , false: không
        /// </summary>
        public Boolean AllowCreateInvoice { get; set; }

        /// <summary>
        /// cho phép in chứng từ true: có , false: không
        /// </summary>
        public Boolean AllowCreateReceipt { get; set; }


        /// <summary>
        ///đang theo dõi true: có , false: không
        /// </summary>
        public Boolean IsActive { get; set; }

        /// <summary>
        ///thu nội bộ true: có , false: không
        /// </summary>
        public Boolean IsInternal { get; set; }

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
