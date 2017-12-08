using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.DataAccess.UnitOfWork.Abstraction;

namespace TaskManager.BL.Abstraction
{
    public abstract class BaseService
    {
        protected BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        ~BaseService()
        {
            Dispose(false);
        }

        protected bool IsDisposed { get; set; }

        protected IUnitOfWork UnitOfWork { get; }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                UnitOfWork?.Dispose();
                GC.SuppressFinalize(this);
            }

            IsDisposed = true;
        }
    }
}
