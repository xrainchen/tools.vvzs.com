using RPoney.DbHelper.Transaction.Imp;

namespace RPoney.DbHelper.Transaction
{
    public class TranstactionFactory
    {
        public static ITransaction InstanceTransaction(TransactionType TranType = 0) =>
            new SysDefaultTransaction();

        public static ITransaction InstanceTransaction(string dBCFileName, TransactionType TranType = 0) =>
            new SysDefaultTransaction(dBCFileName);
    }
}
