using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
//3
namespace Core.Aspect.Autofac.Transaction
{
    public class TransactionScopeAspect : MethodInterceptions
    {
        public override void Intercept(IInvocation invocation)//ıntercept için override yaptık
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    invocation.Proceed();//metodu çalıştırmaya çalış
                    transactionScope.Complete();
                }
                catch (Exception e)
                {

                    transactionScope.Dispose();
                    throw;
                }
            }
        }
    }
}
