﻿using MISA.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Service.Interfaces
{
    /// <summary>
    /// interface cho base chung của service
    /// </summary>
    /// <typeparam name="Entity">loại đối tượng truyền vào  </typeparam>
    ///  CreatedBy: TQAnh ( 22/02/2021)
    public interface IBaseService<Entity>
    {
        /// <summary>
        /// lấy danh sách 
        /// </summary>
        /// <returns> trả về một ServiceResult  </returns>
        ///  CreatedBy: TQAnh ( 22/02/2021)
        ServiceResult GetData();

        /// <summary>
        /// lấy đối tượng theo id
        /// </summary>
        /// <param name="value">giá trị id</param>
        /// <param name="name">giá trị tên trường</param>
        /// <returns>trả về đối tượng </returns>
        ///   CreatedBy: TQAnh (22/02/2021)
        ServiceResult GetByID(int value);

        /// <summary>
        /// thêm mới đối tượng
        /// </summary>
        /// <param name="entity">đối tượng cần thêm mới </param>
        /// <returns> trả về một ServiceResult  </returns>
        ///  CreatedBy: TQAnh ( 22/02/2021)
        ServiceResult Insert(Entity entity);

        /// <summary>
        /// chỉnh sửa đối tượng 
        /// </summary>
        /// <param name="id"> khóa chính đối tượng cần thay đổi</param>
        /// <param name="entity">đối tượng cần thay đổi </param>
        /// <returns> trả về một ServiceResult  </returns>
        ///  CreatedBy: TQAnh ( 22/02/2021)
        ServiceResult Update(int id, Entity entity);

        /// <summary>
        /// xóa đối tượng
        /// </summary>
        /// <param name="id">khóa chính đối tượng cần xóa</param>
        /// <returns> trả về một ServiceResult  </returns>
        ///  CreatedBy: TQAnh ( 22/02/2021)
        ServiceResult Delete(int id);
    }
}
