using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;

namespace ServiceLayer.Common
{
    public class HTMLFormatter : BufferedMediaTypeFormatter
    {
        public HTMLFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            return true;
        }

        public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
        {
            //base.WriteToStream(type, value, writeStream, content);

            content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            using (StreamWriter streamWriter = new StreamWriter(writeStream))
            {
                streamWriter.WriteLine(value.ToString());
            }

            writeStream.Close();
        }
    }
}