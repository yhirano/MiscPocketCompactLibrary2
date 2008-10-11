using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace MiscPocketCompactLibrary2.Net
{
    /// <summary>
    /// HTTP接続クラス
    /// </summary>
    public class HttpConnection
    {
        /// <summary>
        /// プロキシの設定
        /// </summary>
        WebProxySetting proxySetting;

        /// <summary>
        /// プロキシの設定を取得・設定する
        /// </summary>
        public WebProxySetting ProxySetting
        {
            get { return proxySetting; }
            set { proxySetting = value; }
        }

        /// <summary>
        /// Web接続時のタイムアウト時間（ミリ秒）
        /// </summary>
        private int timeout = 60000;

        /// <summary>
        /// Web接続時のタイムアウト時間（ミリ秒）を設定する
        /// </summary>
        public int Timeout
        {
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Time out value out of range.");
                }
                timeout = value;
            }
        }

        /// <summary>
        /// UserAgent
        /// </summary>
        private string userAgent;

        /// <summary>
        /// UserAgentを設定・取得する
        /// </summary>
        public string UserAgent
        {
            get { return userAgent; }
            set { userAgent = value; }
        }

        /// <summary>
        /// リクエストメソッド
        /// </summary>
        private string requestMethod;

        /// <summary>
        /// リクエストメソッドを取得・設定する
        /// </summary>
        public string RequestMethod
        {
            get { return requestMethod; }
            set { requestMethod = value; }
        }

        /// <summary>
        /// 証明書（ユーザ名とパスワード）
        /// </summary>
        private NetworkCredential credential;

        /// <summary>
        /// 証明書（ユーザ名とパスワード）を取得・設定する
        /// </summary>
        public NetworkCredential Credential
        {
            get { return credential; }
            set { credential = value; }
        }

        /// <summary>
        /// 追加するヘッダ
        /// </summary>
        System.Collections.Specialized.NameValueCollection extraHeaders = new System.Collections.Specialized.NameValueCollection();

        /// <summary>
        /// 追加するヘッダを取得する
        /// </summary>
        public System.Collections.Specialized.NameValueCollection ExtraHeaders
        {
            get { return extraHeaders; }
        }

        /// <summary>
        /// HTTPでアクセスしたストリームを生成する
        /// </summary>
        public Stream CreateStream(Uri url)
        {
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(url);
            webReq.AllowWriteStreamBuffering = true;
            if (credential != null)
            {
                webReq.Credentials = credential;
            }

            if (requestMethod != null && requestMethod != string.Empty)
            {
                webReq.Method = requestMethod;
            }

            switch (proxySetting.ProxyUse)
            {
                case WebProxySetting.ProxyConnects.NoUse:
                    WebProxy webProxy = new WebProxy();
                    webProxy.Address = null;
                    webReq.Proxy = webProxy;
                    break;
                case WebProxySetting.ProxyConnects.Manual:
                    webReq.Proxy = new WebProxy(proxySetting.ProxyServer, proxySetting.ProxyPort);
                    break;
                case WebProxySetting.ProxyConnects.AutoDetect:
                default:
                    break;
            }

            if (timeout >= 0)
            {
                webReq.Timeout = timeout;
            }
            if (userAgent != null && userAgent != string.Empty)
            {
                webReq.UserAgent = userAgent;
            }

            if (extraHeaders.Count > 0)
            {
                webReq.Headers.Add(extraHeaders);
            }

            return webReq.GetResponse().GetResponseStream();
        }
    }
}
