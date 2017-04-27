using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using tools.vvzs.com.Model.Entity;
using RPoney;
using RPoney.DbHelper;
using RPoney.Log;
namespace tools.vvzs.com.DAL
{
    /// <summary>
    ///外部地址映射表
    /// </summary>
    public class OutSideMapDal : IdentityDal<OutSideMapEntity>
    {
        public override OutSideMapEntity Get(long id)
        {
            var description = "根据id查询外部地址映射表";
            try
            {
                var sql = @"SELECT * FROM  OutSideMap WITH(NOLOCK) WHERE Id=@Id";
                IDataParameter[] parameters ={
                new SqlParameter("@Id", SqlDbType.BigInt,8) {Value = id},
                };
                LoggerManager.Debug(GetType().Name, $"{description}sql:{sql}{Environment.NewLine}参数:id={id}");
                return RPoney.Data.ModelConvertHelper<OutSideMapEntity>.ToModel(DataBaseManager.MainDb().ExecuteFillDataTable(sql, parameters));
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, $"{description}异常", ex);
                return null;
            }
        }
        public override bool Delete(long id)
        {
            var description = "根据id删除外部地址映射表";
            try
            {
                var sql = @"DELETE OutSideMap WHERE Id=@Id";
                IDataParameter[] parameters ={
                new SqlParameter("@Id", SqlDbType.BigInt,8) {Value = id},
                };
                LoggerManager.Debug(GetType().Name, $"{description}sql:{sql}{Environment.NewLine}参数:id={id}");
                return DataBaseManager.MainDb().ExecuteNonQuery(sql, parameters).CInt(0, false) > 0;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, $"{description}异常", ex);
                return false;
            }
        }
        public override bool Exist(long id)
        {
            var description = "检查外部地址映射表是否存在";
            try
            {
                var sql = @"IF EXISTS(SELECT Id FROM OutSideMap with(nolock) WHERE ID=@Id) BEGIN SELECT 1 END ELSE BEGIN SELECT 0 END";
                IDataParameter[] parameters ={
                new SqlParameter("@Id", SqlDbType.BigInt,8) {Value = id},
                };
                LoggerManager.Debug(GetType().Name, $"{description},sql:{sql}{Environment.NewLine}参数:id={id}");
                return DataBaseManager.MainDb().ExecuteScalar(sql, parameters).CInt(0, false) > 0;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, $"{description}异常", ex);
                return false;
            }
        }

        public override bool Add(OutSideMapEntity entity)
        {
            var description = "添加外部地址映射表";
            try
            {
                var sql = @"INSERT INTO OutSideMap(OutSideUrl,OutSideUrlMd5,UrlType,CreatedTime) VALUES(@OutSideUrl,@OutSideUrlMd5,@UrlType,getdate());";
                IDataParameter[] parameters = {
                                    new SqlParameter("@OutSideUrl", SqlDbType.NVarChar,1024) {Value = entity.OutSideUrl},
                                    new SqlParameter("@OutSideUrlMd5", SqlDbType.VarChar,32) {Value = entity.OutSideUrlMd5},
                                    new SqlParameter("@UrlType", SqlDbType.Int,4) {Value = entity.UrlType}

            };
                LoggerManager.Debug(GetType().Name, $"{description},sql:{sql}{Environment.NewLine}参数:{entity.SerializeToJSON()}");
                return DataBaseManager.MainDb().ExecuteNonQuery(sql, parameters).CInt(0, false) > 0;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, $"{description}异常", ex);
                return false;
            }
        }
        public override bool Update(OutSideMapEntity entity)
        {
            var description = "更新外部地址映射表";
            try
            {
                var sql = @"UPDATE OutSideMap  SET                                
            OutSideUrl=@OutSideUrl,                                     
            OutSideUrlMd5=@OutSideUrlMd5,                                     
            UrlType=@UrlType,              			
			WHERE Id=@Id ;
                ";
                IDataParameter[] parameters = {
                                    new SqlParameter("@OutSideUrl", SqlDbType.NVarChar,1024) {Value = entity.OutSideUrl},
                                    new SqlParameter("@OutSideUrlMd5", SqlDbType.VarChar,32) {Value = entity.OutSideUrlMd5},
                                    new SqlParameter("@UrlType", SqlDbType.Int,4) {Value = entity.UrlType}

            };
                LoggerManager.Debug(GetType().Name, $"{description},sql:{sql}{Environment.NewLine}参数:{entity.SerializeToJSON()}");
                return DataBaseManager.MainDb().ExecuteNonQuery(sql, parameters).CInt(0, false) > 0;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, $"{description}异常", ex);
                return false;
            }
        }
        public override IList<OutSideMapEntity> Get(OutSideMapEntity entity)
        {
            throw new System.NotImplementedException();
        }


        public OutSideMapEntity GetByOutSideUrlMd5(string outSideUrlMd5)
        {
            var description = "根据外部地址查询映射表";
            try
            {
                var sql = @"SELECT * FROM  OutSideMap WITH(NOLOCK) WHERE OutSideUrlMd5=@OutSideUrlMd5";
                IDataParameter[] parameters ={
                new SqlParameter("@OutSideUrlMd5", SqlDbType.VarChar) {Value = outSideUrlMd5},
                };
                LoggerManager.Debug(GetType().Name, $"{description}sql:{sql}{Environment.NewLine}参数:outSideUrlMd5={outSideUrlMd5}");
                return RPoney.Data.ModelConvertHelper<OutSideMapEntity>.ToModel(DataBaseManager.MainDb().ExecuteFillDataTable(sql, parameters));
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, $"{description}异常", ex);
                return null;
            }
        }
    }
}

