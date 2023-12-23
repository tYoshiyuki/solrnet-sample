using SolrNet.Impl;
using SolrNet.Microsoft.DependencyInjection;
using System.Net;

namespace SolrNetSample
{
    /// <summary>
    /// SolrNetの設定ヘルパークラス
    /// </summary>
    public static class SolrNetSettingHelper
    {
        /// <summary>
        /// SolrNetのプロキシ設定を行います。
        /// </summary>
        /// <typeparam name="T">対象となるSolrNetドキュメントの型</typeparam>
        /// <param name="provider"><see cref="IServiceProvider"/></param>
        /// <param name="proxy"><see cref="IWebProxy"/></param>
        public static void SetSolrNetProxy<T>(this IServiceProvider provider, IWebProxy proxy)
        {
            // DI登録処理 (AddSolrNet) にプロキシ登録の口が無いため、DI登録後のSolrコネクションインスタンスに対して明示的にプロキシを処理する
            // SolrコネクションはシングルトンでDIコンテナに登録されているため、プロキシ設定はSolrNetアプリケーション全体に対して適用される
            // 内部処理的にHttpClientとWebRequestの両方が使われているため、どちらに対しても設定する
            var injectedConnection = provider.GetRequiredService<ISolrInjectedConnection<T>>();

            // ライブラリ実装上、必ずAutoSolrConnection型になる想定、ライブラリ更新時には注意が必要
            if (injectedConnection.Connection is AutoSolrConnection autoSolrConnection)
            {
                injectedConnection.Connection = new AutoSolrConnection(autoSolrConnection.ServerURL, new HttpClient(new HttpClientHandler
                {
                    Proxy = proxy
                }), new ConfigurableProxyHttpWebRequestFactory { WebProxy = proxy });
            }
        }
    }
}
