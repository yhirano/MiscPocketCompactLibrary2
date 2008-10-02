using System;

namespace MiscPocketCompactLibrary2.Net
{
    /// <summary>
    /// ウェブプロキシクラス
    /// </summary>
    [Serializable()]
    public class WebProxySetting
    {
        /// <summary>
        /// プロキシの接続方法列挙
        /// </summary>
        [Serializable()]
        public enum ProxyConnects
        {
            /// <summary>
            /// プロキシを使用しない
            /// </summary>
            NoUse,
            /// <summary>
            /// 自動
            /// </summary>
            AutoDetect,
            /// <summary>
            /// 手動
            /// </summary>
            Manual
        }

        /// <summary>
        /// プロキシの接続方法
        /// </summary>
        private ProxyConnects proxyUse = ProxyConnects.AutoDetect;

        /// <summary>
        /// プロキシの接続方法を取得・設定する
        /// </summary>
        public ProxyConnects ProxyUse
        {
            get { return proxyUse; }
            set { proxyUse = value; }
        }

        /// <summary>
        /// プロキシのサーバ名
        /// </summary>
        private string proxyServer = string.Empty;

        /// <summary>
        /// プロキシのサーバ名を取得・設定する
        /// </summary>
        public string ProxyServer
        {
            get { return proxyServer; }
            set { proxyServer = value; }
        }

        /// <summary>
        /// プロキシのポート番号
        /// </summary>
        private int proxyPort = 0;

        /// <summary>
        /// プロキシのポート番号を取得・設定する
        /// </summary>
        public int ProxyPort
        {
            get
            {
                if (0x00 <= proxyPort && proxyPort <= 0xFFFF)
                {
                    return proxyPort;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (0x00 <= value && value <= 0xFFFF)
                {
                    proxyPort = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Proxy port number out of range");
                }
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public WebProxySetting()
        { }
    }
}
