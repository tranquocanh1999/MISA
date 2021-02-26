using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using MISA.DataLayer.Interfaces;

namespace MISA.DataLayer
{
    public class MariaDbContext<Entity> : IDbContext<Entity>
    {
        #region DECLARE

        protected string className = typeof(Entity).Name;
        // khai báo thông tin kết nối 
        String _connectionString = "Host=47.241.69.179;" +
        "Database =MISA.FeeManagement_MF730_TQAnh ;" +
        "Port=3306;User Id=dev;" +
        " Password =12345678 ;" +
        "Character Set=utf8";

        // khởi tạo kết nối 
        protected IDbConnection _dbConnection;
        #endregion

        #region Constructor
        /// <summary>
        /// hàm khởi tạo 
        /// </summary>
        ///   CreatedBy: TQAnh (08/02/2021)
        public MariaDbContext()
        {
            _dbConnection = new MySqlConnector.MySqlConnection(_connectionString);
        }

        #endregion

        #region Method

        /// <summary>
        /// lấy dữ liệu 
        /// </summary>
        /// <returns>trả về thông tin cần lấy </returns>
        ///   CreatedBy: TQAnh (22/02/2021)
        public IEnumerable<Entity> GetAll()
        {



            // thực thi truy vấn
            var entities = _dbConnection.Query<Entity>($"Select * from {className}", commandType: CommandType.Text);

            // trả về cho client
            return entities;
        }

        /// <summary>
        /// lấy dữ liệu với câu lệnh nhập vào
        /// </summary>
        /// <returns>trả về thông tin cần lấy </returns>
        ///   CreatedBy: TQAnh (22/02/2021)
        public IEnumerable<Entity> GetAll(string sqlQuery)
        {



            // thực thi truy vấn
            var entities = _dbConnection.Query<Entity>(sqlQuery, commandType: CommandType.Text);

            // trả về cho client
            return entities;
        }



        /// <summary>
        /// lấy đối tượng theo id
        /// </summary>
        /// <param name="value">giá trị id</param>
        /// <param name="name">giá trị tên trường</param>
        /// <returns>trả về đối tượng </returns>
        ///   CreatedBy: TQAnh (22/02/2021)
        public  Entity GetByID(int value)
        {

            // thực thi truy vấn
            var entities = _dbConnection.Query<Entity>($"Select * from {className} where {className}ID={value.ToString()} ", commandType: CommandType.Text).FirstOrDefault();

            // trả về cho client
            return entities;
        }



        /// <summary>
        /// Thêm mới object vào database
        /// </summary>
        /// <param name="entity">Đối tượng được thêm mới</param>
        /// <returns>số dòng thêm thành công</returns>
        /// CreatedBy: TQAnh (22/02/2021)
        public int Insert(Entity entity)
        {
            // lấy ra các property của object
            var properties = typeof(Entity).GetProperties();

            // khởi tạo các biến string để lưu các giá trị property

            var sqlPropName = string.Empty;
            var sqlPropParam = string.Empty;
            var dynamiParameters = new DynamicParameters();
            // Duyệt tùng properties để lấy giá trị
            foreach (var propety in properties)
            {

                var propName = propety.Name;

                // sinh id mới
                if ((propety.PropertyType == typeof(Guid) || propety.PropertyType == typeof(Guid?)) && propName.ToLower() == $"{className}Id".ToLower())
                    propety.SetValue(entity, Guid.NewGuid());
                var propValue = propety.GetValue(entity);


                sqlPropName = sqlPropName + $",{propName}";
                sqlPropParam = sqlPropParam + $",@{propName}";
                dynamiParameters.Add($"@{propName}", propValue);



            }

            // Xóa đi kí tự "," đầu câu 
            sqlPropParam = sqlPropParam.Remove(0, 1);
            sqlPropName = sqlPropName.Remove(0, 1);


            var sqlComand = $"INSERT INTO {className} ({sqlPropName}) Value ({sqlPropParam})";
            var res = _dbConnection.Execute(sqlComand, param: dynamiParameters, commandType: CommandType.Text);
            return res;


        }


        /// <summary>
        /// chỉnh sửa 1 đối tượng
        /// </summary>
        /// <param name="id"> khóa chính của đối tượng cần chỉnh sửa </param>
        /// <param name="entity">giá trị cần chỉnh sửa </param>
        /// <returns> số dòng thành công </returns>
        /// CreatedBy: TQAnh (22/02/2021)
        public int Update(int id, Entity entity)
        {

            // lấy ra các property của object
            var properties = typeof(Entity).GetProperties();

            // khởi tạo các biến string để lưu các giá trị property
            var parameters = new DynamicParameters();
            var sqlParamBuider = string.Empty;
            // duyệt từng property
            foreach (var propety in properties)
            {
                var propetyName = propety.Name;
                var propetyValue = propety.GetValue(entity);

                parameters.Add($"@{propetyName}", propetyValue);

                sqlParamBuider += $",{propetyName}=@{propetyName}";

            }

            var sql = $"UPDATE  {className} SET {sqlParamBuider.Substring(1)} Where {className}id='{id.ToString()}'";
            var efectRows = _dbConnection.Execute(sql, parameters, commandType: CommandType.Text);
            return efectRows;


        }

        /// <summary>
        /// xóa 1 đối tượng
        /// </summary>
        /// <param name="id">khóa chính đối tượng cần xóa</param>
        /// <returns> số dòng thành công </returns>
        /// CreatedBy: TQAnh (22/02/2021)
        public int Delete(int id)
        {

            var sql = $"Delete From  {className}  Where {className}id='{id.ToString()}'";
            var efectRows = _dbConnection.Execute(sql);
            return efectRows;

        }




        /// <summary>
        /// check trùng trong database
        /// </summary>
        /// <param name="propertyValue"> giá trị cần kiểm tra</param>
        /// <param name="propertyName">trường cần kiểm tra</param>
        /// <returns> true: tồn tại , false: không tồn tại  </returns>
        ///   CreatedBy: TQAnh (22/02/2021)
        public bool CheckExits(string propertyValue, string propertyName)
        {
            var sql = $"SELECT {propertyName} From {className} as C Where C.{propertyName}='{propertyValue}'";
            var customerCodeExits = _dbConnection.Query<string>(sql).FirstOrDefault();
            if (customerCodeExits == null)
                return false;

            else return true;
        }




        /// <summary>
        /// check trùng trong database
        /// </summary>
        /// <param name="propertyValue"> giá trị cần kiểm tra</param>
        /// <param name="propertyName">trường cần kiểm tra</param>
        /// <param name="className">bảng cần kiểm tra</param>
        /// <returns> true: tồn tại , false: không tồn tại  </returns>
        ///   CreatedBy: TQAnh (22/02/2021)
        public bool CheckExits(int? propertyValue, string propertyName, string className)
        {
            var sql = $"SELECT {propertyName} From {className} as C Where C.{propertyName}='{propertyValue.ToString()}'";
            var isExits = _dbConnection.Query<string>(sql).FirstOrDefault();
            if (isExits == null)
                return false;

            else return true;
        }

        /// <summary>
        /// kiểm tra có phải bản lưu hệ thống
        /// </summary>
        /// <param name="propertyValue"> giá trị cần kiểm tra</param>
        /// <param name="propertyName">trường cần kiểm tra</param>
        /// <returns> true: đúng , false: sai  </returns>
        ///   CreatedBy: TQAnh (22/02/2021)
        public bool isSystem(int propertyValue, string propertyName)
        {
            var sql = $"    SELECT * FROM {className} WHERE {className}.{propertyName}={propertyValue} AND IsSystem";
            var isSystem = _dbConnection.Query<string>(sql).FirstOrDefault();
            if (isSystem == null)
                return true;

            else return false;
        }

     


        #endregion
    }
}
