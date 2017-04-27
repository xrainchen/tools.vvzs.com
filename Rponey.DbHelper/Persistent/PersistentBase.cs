using RPoney.DbHelper.Transaction;
using System.Data;
using RPoney.Data.Contract;

namespace RPoney.DbHelper.Persistent
{
    public abstract class PersistentBase
    {
        private ITransaction _transaction;

        public PersistentBase()
        {
            this._transaction = null;
        }

        public PersistentBase(ITransaction Transaction)
        {
            this._transaction = null;
            this.Transaction = Transaction;
        }

        public string GetDBCFileName { get; private set; }

        public ITransaction Transaction
        {
            get { return this._transaction; }

            private set
            {
                this._transaction = value;
            }
        }

        public virtual int ExecuteNonQuery(string commandText) =>
                            this._transaction.GetTransactionContext<IDbHelper>().ExecuteNonQuery(commandText, new IDataParameter[0]);

        public virtual int ExecuteNonQuery(string commandText, CommandType cmdType, params IDataParameter[] dataParameters) =>
            this._transaction.GetTransactionContext<IDbHelper>().ExecuteNonQuery(commandText, cmdType, dataParameters);

        public virtual object ExecuteScalar(string commandText, CommandType cmdType, params IDataParameter[] dataParameters) =>
            this._transaction?.GetTransactionContext<IDbHelper>().ExecuteScalar(commandText, cmdType, dataParameters);
    }
}
