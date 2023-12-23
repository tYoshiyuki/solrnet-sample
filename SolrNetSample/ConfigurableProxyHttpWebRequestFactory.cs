using HttpWebAdapters.Adapters;
using HttpWebAdapters;
using System.Net;

namespace SolrNetSample
{
    /// <summary>
    /// <see cref="HttpWebRequestFactory"/> を引用し、プロキシ設定を可能にしたクラス。
    /// </summary>
    public class ConfigurableProxyHttpWebRequestFactory : IHttpWebRequestFactory
    {
        public IWebProxy WebProxy { get; set; } = new WebProxy();

        public IHttpWebRequest Create(string url)
        {
            return new HttpWebRequestAdapter((HttpWebRequest)WebRequest.Create(url))
            {
                Proxy = WebProxy
            };
        }

        /// <inheritdoc />
        public IHttpWebRequest Create(Uri url)
        {
            return new HttpWebRequestAdapter((HttpWebRequest)WebRequest.Create(url))
            {
                Proxy = WebProxy
            };
        }
    }
}
