using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Core.Base
{
    public abstract partial class _Controller : ApiController
    {
        [NonAction]
        public string NewToken<T>(IPayLoad payload)
        {
            return _token.Encode(payload);
        }

        public HttpResponseMessage WriteResponse(string fileName, byte[] data)
        {
            HttpResponseMessage httpResponce = base.Request.CreateResponse();

            httpResponce.Content = new ByteArrayContent(data);

            httpResponce.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            httpResponce.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = fileName };

            return httpResponce;
        }

        public HttpResponseMessage WriteStreamResponse(string fileName, byte[] data, int bufferSize = 10000)
        {
            HttpResponseMessage httpResponce = base.Request.CreateResponse();

            httpResponce.Content = new PushStreamContent(async (Stream outputStream, HttpContent content, TransportContext transportContext) =>
            {
                byte[] buffer = new byte[bufferSize];

                using (Stream source = new MemoryStream(data))
                {
                    int totalSize = (int)source.Length;

                    while (totalSize > 0)
                    {
                        int count = totalSize > bufferSize ? bufferSize : totalSize;

                        int sizeOfReadedBuffer = source.Read(buffer, 0, count);

                        await outputStream.WriteAsync(buffer, 0, sizeOfReadedBuffer);

                        totalSize -= sizeOfReadedBuffer;
                    }
                }
                outputStream.Flush();
                outputStream.Close();
            });
            httpResponce.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            httpResponce.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = fileName };

            return httpResponce;
        }
    }
}