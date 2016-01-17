using System;
using System.Runtime.InteropServices;

namespace CSharpVAST
{
    class Vast
    {
        // make sure LoadLibrary is usable 
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr LoadLibrary(string lpFileName);

        /// <summary>
        /// basic init of VAST
        /// </summary>
        [DllImport("VASTWrapperC.dll")]
        public static extern void checkVASTJoin();
        [DllImport("VASTWrapperC.dll")]
        public static extern bool InitVAST(bool is_gateway, string gateway);
        [DllImport("VASTWrapperC.dll")]
        public static extern bool ShutVAST();

        /// <summary>
        /// unique layer
        /// </summary>
        [DllImport("VASTWrapperC.dll")]
        public static extern void VASTReserveLayer(UInt32 layer);
        [DllImport("VASTWrapperC.dll")]
        public static extern UInt32 VASTGetLayer();
        [DllImport("VASTWrapperC.dll")]
        public static extern bool VASTReleaseLayer();

        /// <summary>
        /// main join / move / publish functions
        /// </summary>
        [DllImport("VASTWrapperC.dll")]
        public static extern bool VASTJoin(ushort world_id, float x, float y, ushort radius);
        [DllImport("VASTWrapperC.dll")]
        public static extern bool VASTLeave();
        [DllImport("VASTWrapperC.dll")]
        public static extern bool VASTMove(float x, float y);
        [DllImport("VASTWrapperC.dll")]
        public static extern int VASTTick(uint time_budget);
        [DllImport("VASTWrapperC.dll")]
        public static extern bool VASTPublish(String msg, uint size, ushort radius);
        [DllImport("VASTWrapperC.dll")]
        public static extern IntPtr VASTReceive(ref UInt64 from, ref uint size);

        /// <summary>
        /// socket messaging
        /// </summary>
        [DllImport("VASTWrapperC.dll")]
        public static extern ulong VASTOpenSocket(String ip_port, bool is_secure);
        [DllImport("VASTWrapperC.dll")]
        public static extern bool VASTCloseSocket(ulong socket);
        [DllImport("VASTWrapperC.dll")]
        public static extern bool VASTSendSocket(ulong socket, String msg, uint size);
        [DllImport("VASTWrapperC.dll")]
        public static extern IntPtr VASTReceiveSocket(ref UInt64 from, ref uint size);

        /// <summary>
        /// helpers
        /// </summary>
        [DllImport("VASTWrapperC.dll")]
        public static extern bool isVASTInit();
        [DllImport("VASTWrapperC.dll")]
        public static extern bool isVASTJoined();
        [DllImport("VASTWrapperC.dll")]
        public static extern ulong VASTGetSelfID();
        [DllImport("VASTWrapperC.dll")]
        public static extern ulong VASTGetSubscriptionID();
    }
}
