using System;
using System.Configuration;
using RPoney.Data;
using RPoney.Data.Contract;

namespace RPoney.DbHelper.Transaction.Imp
{
    public class SysDefaultTransaction : ITransaction
    {
        private IDbHelper _idbhelper = null;
        private string _DBCFileName = "";
        public SysDefaultTransaction()
        {
            _DBCFileName = ConfigurationManager.AppSettings["DBCFileName"].ToLower().Replace(".dbc", "");
        }
        public SysDefaultTransaction(string dBCFileName)
        {
            _DBCFileName = dBCFileName;
        }
        public void BeginTransaction()
        {
            if (this._idbhelper != null)
            {
                throw new Exception("您已经打开了一个事务，在一个事务管理实例中只能打开一次");
            }
            this._idbhelper = DataBaseManager.MainDb(_DBCFileName);
            this._idbhelper.SetHandClose(true);
        }

        public void CommitTransaction()
        {
            if (this._idbhelper == null)
            {
                throw new NullReferenceException("事务没有被打开,无法提交一个事务");
            }
            this._idbhelper.EndConnection(ECloseTransactionType.Commit);
            this._idbhelper = null;
        }

        public void Dispose()
        {
            if (this._idbhelper != null)
            {
                this._idbhelper.EndConnection(ECloseTransactionType.RollBack);
                this._idbhelper = null;
            }
        }

        public T GetTransactionContext<T>()
        {
            if (this._idbhelper == null)
            {
                throw new NullReferenceException("事务没有被打开，无法获取事务的上下文");
            }
            return (T)this._idbhelper;
        }

        public void RollbackTransaction()
        {
            if (this._idbhelper == null)
            {
                throw new NullReferenceException("事务没有被打开,无法回滚一个事务");
            }
            this._idbhelper.EndConnection(ECloseTransactionType.RollBack);
            this._idbhelper = null;
        }
    }
}
