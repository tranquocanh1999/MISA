using MISA.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.DataLayer.Interfaces
{
    /// <summary>
    /// interface cho base chung database
    /// </summary>
    /// <typeparam name="Entity"></typeparam>
    ///   CreatedBy: TQAnh (22/02/2021)
    public interface IDbContext<Entity>
    {
        /// <summary>
        /// lấy tất cả danh sách đối tượng
        /// </summary>
        /// <returns>trả về danh sách đối tượng</returns>
        ///   CreatedBy: TQAnh (22/02/2021)
        IEnumerable<Entity> GetAll();


        /// <summary>
        /// lấy đối tượng theo id
        /// </summary>
        /// <param name="value">giá trị id</param>
        /// <param name="name">giá trị tên trường</param>
        /// <returns>trả về đối tượng </returns>
        ///   CreatedBy: TQAnh (22/02/2021)
        Entity GetByID(int value);



        /// <summary>
        /// thêm mới đối tượng 
        /// </summary>
        /// <param name="entity">đối tượng cần thêm mới</param>
        /// <returns> trả về số dòng được thay đổi</returns>
        ///   CreatedBy: TQAnh (22/02/2021)
        int Insert(Entity entity);

        /// <summary>
        /// chỉnh sửa 1 đối tượng
        /// </summary>
        /// <param name="id">khóa chính đối tượng được thay đổi </param>
        /// <param name="entity"> đối tượng được thay đổi </param>
        /// <returns> trả về số dòng được thay đổi </returns>
        ///   CreatedBy: TQAnh (22/02/2021)
        int Update(int id, Entity entity);

        /// <summary>
        /// Xóa một đối tượng
        /// </summary>
        /// <param name="id">khóa chính đối tượng cần xóa</param>
        /// <returns>trả về số dòng được thay đổi </returns>
        ///   CreatedBy: TQAnh (22/02/2021)
        int Delete(int id);

        /// <summary>
        /// check trùng trong database
        /// </summary>
        /// <param name="propertyValue"> giá trị cần kiểm tra</param>
        /// <param name="propertyName">trường cần kiểm tra</param>
        /// <returns> true: tồn tại , false: không tồn tại  </returns>
        ///   CreatedBy: TQAnh (22/02/2021)
        bool CheckExits(string propertyValue, string propertyName);


        /// <summary>
        /// check trùng trong database
        /// </summary>
        /// <param name="propertyValue"> giá trị cần kiểm tra</param>
        /// <param name="propertyName">trường cần kiểm tra</param>
        /// <param name="className">bảng cần kiểm tra</param>
        /// <returns> true: tồn tại , false: không tồn tại  </returns>
        ///   CreatedBy: TQAnh (22/02/2021)
        bool CheckExits(int? propertyValue, string propertyName, string className);

        bool isSystem(int propertyValue, string propertyName);
    }
}
