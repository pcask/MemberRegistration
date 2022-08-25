using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DevFramework.Core.Aspects.PostSharp.TransactionAspects
{
    [Serializable]
    public class TransactionScopeAspect : OnMethodBoundaryAspect
    {
        private readonly TransactionScopeOption _option;

        public TransactionScopeAspect(TransactionScopeOption option)
        {
            _option = option;
        }

        public TransactionScopeAspect()
        {

        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            // Try-Catch bloğunu sarmallayacak nesneyi belirliyoruz. Using (var scope = new TransactionScope()) { try - catch }
            args.MethodExecutionTag = new TransactionScope(_option);
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            // OnSuccess try bloğunun en son satırına yerleşecek ve herhangi bir hata çıkmadıysa Transaction Commit edilecektir. 
            ((TransactionScope)args.MethodExecutionTag).Complete();
        }

        public override void OnException(MethodExecutionArgs args)
        {
            // OnException catch bloğuna yerleşecek ve hata yakalandığı için Transaction RollBack edilecektir.
            // ((TransactionScope)args.MethodExecutionTag).Dispose();
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            // OnExit Aspect'in uygulandığı method'ın en son satırına yerleşecek ve son olarak yapılması gereken işlemler varsa buraya yazılacaktır.
            // İstersek Dispose işlemini burada da yapabiliriz tüm işlemler bittikten sonra nihayet OnExit method'ı devreye gireceği için 
            // Transaction RollBack edilecektir, tabi öncesinde geçerli Transaction Commit edildiyse buradaki RollBack bir anlam ifade etmeyecektir.
            ((TransactionScope)args.MethodExecutionTag).Dispose();
        }
    }
}
