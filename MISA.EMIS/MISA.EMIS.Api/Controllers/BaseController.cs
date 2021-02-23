
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Common.Model;
using MISA.Service;
using MISA.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
    /// <summary>
    /// base chung cho controlelr
    /// </summary>
    /// <typeparam name="Entity"></typeparam>
    /// CreatedBy: TQAnh (08/02/2021)

    [Route("api/[controller]")]
    [ApiController]

    public class BaseController<Entity> : ControllerBase
    {

        #region DECLARE
        IBaseService<Entity> _baseService;
        #endregion

        #region Constructor
        public BaseController(IBaseService<Entity> baseService)
        {
            _baseService = baseService;
        }
        #endregion

        #region Method
        /// <summary>
        /// Lấy danh sách các  đối tượng
        /// </summary>
        /// <returns> trả về danh sách các  đối tượng </returns>
        /// CreatedBy: TQAnh (22/02/2021)
        [HttpGet]
        public IActionResult Get()
        {

            try
            {
                var serviceResult = _baseService.GetData();
                var entity = serviceResult.Data as List<Entity>;
                if (entity.Count == 0)
                    return StatusCode(204, serviceResult.Data);
                else return StatusCode(200, serviceResult.Data);
            }
            catch (Exception ex)
            {
                var serviceResult = new ServiceResult();
                var erroMsg = new ErroMsg();
                erroMsg.UserMsg.Add(MISA.Common.Properties.Resources.UserMsg_Exception);
                erroMsg.DevMsg = ex.ToString();
                serviceResult.Data = erroMsg;
                return StatusCode(500, serviceResult.Data);
            }
        }

        /// <summary>
        /// Lấy danh   đối tượng theo id
        /// </summary>
        /// <returns>   đối tượng </returns>
        /// CreatedBy: TQAnh (22/02/2021)
        [HttpGet("{id}")]
        public IActionResult GetbyID(int id)
        {

            try
            {
                var serviceResult = _baseService.GetByID(id);

                return StatusCode(200, serviceResult.Data);
            }
            catch (Exception ex)
            {
                var serviceResult = new ServiceResult();
                var erroMsg = new ErroMsg();
                erroMsg.UserMsg.Add(MISA.Common.Properties.Resources.UserMsg_Exception);
                erroMsg.DevMsg = ex.ToString();
                serviceResult.Data = erroMsg;
                return StatusCode(500, serviceResult.Data);
            }
        }


        /// <summary>
        /// Thêm mới một đối tượng
        /// </summary>
        /// <param name="entity"> thực thể đối tượng cần thêm mới </param>
        /// <returns> trả về số dòng được thay đổi </returns>
        ///  CreatedBy: TQAnh (22/02/2021)
        [HttpPost]
        public IActionResult Post(Entity entity)
        {

            try
            {
                var res = _baseService.Insert(entity);
                if (res.Success == false)
                    return StatusCode(400, res.Data);
                else if (res.Success == true && (int)res.Data > 0)
                    return StatusCode(201, res.Data);
                else return StatusCode(200, res.Data);
            }
            catch (Exception ex)
            {
                var serviceResult = new ServiceResult();
                var erroMsg = new ErroMsg();
                erroMsg.UserMsg.Add(MISA.Common.Properties.Resources.UserMsg_Exception);
                erroMsg.DevMsg = ex.ToString();
                serviceResult.Data = erroMsg;
                return StatusCode(500, serviceResult.Data);
            }
        }

        /// <summary>
        /// Chỉnh sửa một đối tượng
        /// </summary>
        /// <param name="id">khóa chính của đối tượng</param>
        /// <param name="entity"> đối tượng cần thay đổi </param>
        /// <returns>Số dòng được thay đổi </returns>
        ///  CreatedBy: TQAnh (22/02/2021)
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Entity entity)
        {

            try
            {

                var res = _baseService.Update(id, entity);
                if (res.Success == false)
                    return StatusCode(400, res.Data);
                
                else return StatusCode(200, res.Data);

               
            }
            catch (Exception ex)
            {
                var serviceResult = new ServiceResult();
                var erroMsg = new ErroMsg();
                erroMsg.UserMsg.Add(MISA.Common.Properties.Resources.UserMsg_Exception);
                erroMsg.DevMsg = ex.ToString();
                serviceResult.Data = erroMsg;
                return StatusCode(500, serviceResult.Data);
            }
        }

        /// <summary>
        /// Xóa đối tượng
        /// </summary>
        /// <param name="id">khóa chính đối tượng cần xóa </param>
        /// <returns>trả về số dòng được thay đổi </returns>
        ///  CreatedBy: TQAnh (22/02/2021)
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {

                var res = _baseService.Delete(id);

              
                if (res.Success == false)
                    return StatusCode(400, res.Data);

                else return StatusCode(200, res.Data);
            }
            catch (Exception ex)
            {
                var serviceResult = new ServiceResult();
                var erroMsg = new ErroMsg();
                erroMsg.UserMsg.Add(MISA.Common.Properties.Resources.UserMsg_Exception);
                serviceResult.Data = erroMsg;
                return StatusCode(500, serviceResult.Data);
            }
        }

        #endregion

    }
}
