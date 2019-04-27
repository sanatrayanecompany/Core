using System;

namespace Core.Base
{
    internal interface IToken
    {
        string Encode<T>(T payload) where T : IPayLoad;

        T Decode<T>(string token = "") where T : IPayLoad;
    }
}
