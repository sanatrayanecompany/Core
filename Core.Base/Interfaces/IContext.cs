using System;
using System.Data;

namespace Core.Base
{
    internal interface IContext: IDisposable
    {
        IDbConnection Data { get; }
    }
}
