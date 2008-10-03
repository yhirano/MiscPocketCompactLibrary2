using System;
using System.Reflection;
using System.Drawing;
using System.Runtime.InteropServices;

namespace MiscPocketCompactLibrary2.Reflection
{
    /// <summary>
    /// アセンブリのユーティリティ
    /// </summary>
    public static class AssemblyUtility
    {
        /// <summary>
        /// AssemblyTitleを取得する
        /// </summary>
        public static string GetTitle(Assembly assembly)
        {
            return ((AssemblyTitleAttribute)assembly.GetCustomAttributes(
                typeof(AssemblyTitleAttribute), false)[0]).Title;
        }

        /// <summary>
        /// AssemblyDescriptionを取得する
        /// </summary>
        public static string GetDescription(Assembly assembly)
        {
            return ((AssemblyDescriptionAttribute)assembly.GetCustomAttributes(
                typeof(AssemblyDescriptionAttribute), false)[0]).Description;
        }

        /// <summary>
        /// AssemblCompanyを取得する
        /// </summary>
        public static string GetCompany(Assembly assembly)
        {
            return ((AssemblyCompanyAttribute)assembly.GetCustomAttributes(
                typeof(AssemblyCompanyAttribute), false)[0]).Company;
        }

        /// <summary>
        /// AssemblyProductを取得する
        /// </summary>
        public static string GetProduct(Assembly assembly)
        {
            return ((AssemblyProductAttribute)assembly.GetCustomAttributes(
                typeof(AssemblyProductAttribute), false)[0]).Product;
        }

        /// <summary>
        /// AssemblyCopyrightを取得する
        /// </summary>
        public static string GetCopyright(Assembly assembly)
        {
            return ((AssemblyCopyrightAttribute)assembly.GetCustomAttributes(
                typeof(AssemblyCopyrightAttribute), false)[0]).Copyright;
        }

        /// <summary>
        /// AssemblyTrademarkを取得する
        /// </summary>
        public static string GEtTrademark(Assembly assembly)
        {
            return ((AssemblyTrademarkAttribute)assembly.GetCustomAttributes(
                typeof(AssemblyTrademarkAttribute), false)[0]).Trademark;
        }

        /// <summary>
        /// Locationを取得する
        /// </summary>
        public static string GetLocation(Assembly assembly)
        {
            return assembly.GetModules()[0].FullyQualifiedName;
        }

        /// <summary>
        /// バージョンを取得する
        /// </summary>
        public static Version GetVersion(Assembly assembly)
        {
            return assembly.GetName().Version;
        }

        /// <summary>
        /// このアプリケーションのアイコンを取得する
        /// </summary>
        public static Icon GetApplicationIcon(Assembly assembly)
        {
            // アプリケーション・アイコンを取得
            SHFILEINFO shinfo = new SHFILEINFO();
            IntPtr hSuccess = SHGetFileInfo(
                assembly.GetModules()[0].FullyQualifiedName,
                0, ref shinfo, (uint)Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_LARGEICON);
            if (hSuccess != IntPtr.Zero)
            {
                return Icon.FromHandle(shinfo.hIcon);
            }
            else
            {
                return null;
            }
        }


        #region アイコン取得のための関数

        // SHGetFileInfo関数
        [DllImport("shell32.dll")]
        private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        // SHGetFileInfo関数で使用するフラグ
        private const uint SHGFI_ICON = 0x100; // アイコン・リソースの取得
        private const uint SHGFI_LARGEICON = 0x0; // 大きいアイコン
        private const uint SHGFI_SMALLICON = 0x1; // 小さいアイコン

        /// <summary>
        /// SHGetFileInfo関数で使用する構造体
        /// </summary>
        private struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        #endregion
    }
}
