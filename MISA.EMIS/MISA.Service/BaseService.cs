using MISA.Common.Model;
using MISA.DataLayer;
using MISA.DataLayer.Interfaces;
using MISA.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Service
{
    /// <summary>
    /// base chung cho service
    /// </summary>
    /// <typeparam name="Entity"> loại đối tượng thêm vào  </typeparam>
    /// CreatedBy: TQAnh ( 22/02/2021)
    public class BaseService<Entity> : IBaseService<Entity>
    {

        #region DECLARE
        // khai báo các thuộc tính
        protected ServiceResult serviceResult;
        protected IDbContext<Entity> _dbContext;
        protected String sqlQuerry;
        #endregion

        #region Constructor

        /// <summary>
        /// hàm khởi tạo 
        /// </summary>
        /// <param name="dbContext"> một interface của IDbContext<Entity> </param>
        /// CreatedBy: TQAnh ( 08/02/2021)
        public BaseService(IDbContext<Entity> dbContext)
        {
            serviceResult = new ServiceResult();
            _dbContext = dbContext;
        }



        #endregion

        #region Method

        /// <summary>
        /// lấy danh sách 
        /// </summary>
        /// <returns> trả về một ServiceResult  </returns>
        ///  CreatedBy: TQAnh ( 22/02/2021)
        public ServiceResult GetData()
        {

            serviceResult.Data = _dbContext.GetAll();
            return serviceResult;
        }
        /// <summary>
        /// lấy danh sách 
        /// </summary>
        /// <returns> trả về một ServiceResult  </returns>
        ///  CreatedBy: TQAnh ( 22/02/2021)
        public ServiceResult GetData(String sqlQuerry)
        {

            serviceResult.Data = _dbContext.GetAll(sqlQuerry);
            return serviceResult;
        }


        /// <summary>
        /// lấy đối tượng theo id
        /// </summary>
        /// <param name="value">giá trị id</param>
        /// <param name="name">giá trị tên trường</param>
        /// <returns>trả về đối tượng </returns>
        ///   CreatedBy: TQAnh (22/02/2021)
        public ServiceResult GetByID(int value) {

            serviceResult.Data = _dbContext.GetByID(value);
            return serviceResult;
        }

        /// <summary>
        /// thêm mới đối tượng
        /// </summary>
        /// <param name="entity">đối tượng cần thêm mới </param>
        /// <returns> trả về một ServiceResult  </returns>
        ///  CreatedBy: TQAnh ( 22/02/2021)
        public ServiceResult Insert(Entity entity)
        {

            var erroMsg = new ErroMsg();
            var isValid = ValidateData(entity, erroMsg,1);
            if (isValid == true)
            {
                serviceResult.Data = _dbContext.Insert(entity);
            }
            else
            {
                serviceResult.Success = false;
                serviceResult.Data = erroMsg;

            }
            return serviceResult;
        }

        /// <summary>
        /// chỉnh sửa đối tượng 
        /// </summary>
        /// <param name="id"> khóa chính đối tượng cần thay đổi</param>
        /// <param name="entity">đối tượng cần thay đổi </param>
        /// <returns> trả về một ServiceResult  </returns>
        ///  CreatedBy: TQAnh ( 22/02/2021)
        public ServiceResult Delete(int id)
        {

            var erroMsg = new ErroMsg();
          
            var isValid = isSystem(id, erroMsg);
            if (isValid == true)
            {
                serviceResult.Data = _dbContext.Delete(id);
            }
            else
            {
                serviceResult.Success = false;
                serviceResult.Data = erroMsg;

            }
            return serviceResult;
          
        }

        /// <summary>
        /// xóa đối tượng
        /// </summary>
        /// <param name="id">khóa chính đối tượng cần xóa</param>
        /// <returns> trả về một ServiceResult  </returns>
        ///  CreatedBy: TQAnh ( 22/02/2021)
        public ServiceResult Update(int id, Entity entity)
        {

            var erroMsg = new ErroMsg();
            var isValid = ValidateData(entity, erroMsg,2);
            if (isValid == true)
            {
                serviceResult.Data = _dbContext.Update(id, entity);

             
            }
            else
            {
                serviceResult.Success = false;
                serviceResult.Data = erroMsg;

            }
            return serviceResult;
          
        }

        /// <summary>
        /// validate dữ liệu nhập vào
        /// </summary>
        /// <param name="entity">đối tượng cần validate </param>
        /// <param name="erroMsg">chứa lỗi trả về nếu có</param>
        /// <param name="type">loại validate 0: thêm , 1: chỉnh sửa</param>
        /// <returns></returns>
        protected virtual bool ValidateData(Entity entity, ErroMsg erroMsg ,int type)
        { return true; }


        /// <summary>
        /// kiểm tra có phải dữ liệu hệ thống hay không 
        /// </summary>
        /// <param name="id">là dữ liệu hệ thống </param>
        /// <param name="erroMsg">chứa lỗi trả về </param>
        /// <returns></returns>
        protected virtual bool isSystem(int id, ErroMsg erroMsg)
        { return true; }


        #endregion


    }
}
