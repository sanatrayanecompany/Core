using System;
using System.Data;

namespace Core.Base
{
    public abstract class _Service : IDisposable
    {
        private IToken _token;

        private IContext _context;

        protected IDbConnection Data
        {
            get
            {
                if (this._context == null)
                {
                    _context = new Context();
                }
                return _context.Data;
            }
        }


        protected T GetCurrentUser<T>() where T : IPayLoad
        {
            return GetCurrentUser<T>("");
        }
        protected T GetCurrentUser<T>(string token) where T : IPayLoad
        {
            return _token.Decode<T>(token);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }
    }
}