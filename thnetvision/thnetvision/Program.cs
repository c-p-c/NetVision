using System;
using System.Windows.Forms;

namespace thnetvision
{
    enum SERVERSTAT
    {
        WAITP1,
        WAITP2,
        PLAYING
    }

    enum TOPBYTE : byte
    {
        STRING,
        NETVISIONCORE,
        DECK
    }

    class VisionServer : TCP_async.Server
    {
        public NetvisionCore core = new NetvisionCore();
        public int player1;
        public int player2;

        public VisionServer(int port, TCP_async.ConnectFunc accptdFunc, TCP_async.AsyncRecvBytesFunc recvFunc, TCP_async.DisconnectFunc discFunc)
            : this(port, accptdFunc, recvFunc, discFunc, 1024) { }
        public VisionServer(int port, TCP_async.ConnectFunc accptdFunc, TCP_async.AsyncRecvBytesFunc recvFunc, TCP_async.DisconnectFunc discFunc, int dataLength)
            : base(port, accptdFunc, recvFunc, discFunc, dataLength)
        {
            player1 = -1;
            player2 = -1;
        }

        public void Send(int remoteID, object sendObj, TOPBYTE topbyte)
        {
            byte[] sendBytes = VisionFunctions.Serialize(sendObj);
            sendBytes = VisionFunctions.SetTopByte(sendBytes, (byte)topbyte);
            try
            {
                Send(remoteID, sendBytes);
            }
            catch (Exception ex)
            {
                Program.fm1.Chat_Add(ex.Message);
            }
        }

        public void Broadcast(object sendObj,TOPBYTE topbyte)
        {
            int i;
            for (i = 0; i < ConnectedList().Length; i++)
            {
                try
                {
                    Send(i, sendObj, topbyte);
                }
                catch
                {
                    Program.fm1.Chat_Add(string.Format("server:remote{0}へ送信できませんでした", i));
                }
            }
        }
        public void BroadcastCore()
        {
            Send(player1, core, TOPBYTE.NETVISIONCORE);
            Send(player2, core.ReverseSide(), TOPBYTE.NETVISIONCORE);
        }

        //============================== ゲーム進行 ==============================
        public void StartGame()
        {
            // カード・コアを初期化
            core.Initialize();

            // coreにデッキの情報を書き込み
            for (int i = 0; i < 50; i++)
            {
                core.home.libraryOrder[i] = i;
                core.cards[i].section = SECTION.HOMELIBRARY;
                core.cards[i].owner = true;

                core.away.libraryOrder[i] = 50 + i;
                core.cards[50 + i].section = SECTION.AWAYLIBRARY;
                core.cards[50 + i].owner = false;
            }
            core.home.libraryNum = 50;
            core.away.libraryNum = 50;

            VisionFunctions.Shuffle(core.home.libraryOrder, core.home.libraryNum);
            VisionFunctions.Shuffle(core.away.libraryOrder, core.away.libraryNum);
        }
    }

    class VisionClient : TCP_async.Client
    {
        public VisionClient(string host, int port, TCP_async.ConnectFunc accptdFunc, TCP_async.AsyncRecvBytesFunc recvFunc, TCP_async.DisconnectFunc discFunc)
            : this(host, port, accptdFunc, recvFunc, discFunc, 1024)
        { }
        public VisionClient(string host, int port, TCP_async.ConnectFunc accptdFunc, TCP_async.AsyncRecvBytesFunc recvFunc, TCP_async.DisconnectFunc discFunc, int dataLength)
            : base(host, port, accptdFunc, recvFunc, discFunc, dataLength)
        {
        }

        public void Send(object sendObj, TOPBYTE topbyte)
        {
            byte[] sendBytes = VisionFunctions.Serialize(sendObj);
            sendBytes = VisionFunctions.SetTopByte(sendBytes, (byte)topbyte);
            try
            {
                Send(sendBytes);
            }
            catch (Exception ex)
            {
                Program.fm1.Chat_Add(ex.Message);
            }
        }
    }

    static class Program
    {
        static public Form1 fm1;
        static public VisionServer sv;
        static public VisionClient cl;
        static SERVERSTAT svStat = SERVERSTAT.WAITP1;
        
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            fm1 = new Form1();
            Application.Run(fm1);
        }

        static public bool OpenServer(int port)
        {
            try
            {
                sv = new VisionServer(port, ServerOnConnect, ServerOnRecv, ServerOnDisconnect, 12288);
            }
            catch (Exception)
            {
                return false;
            }
            svStat = SERVERSTAT.WAITP1;
            return true;
        }
        static public bool OpenClient(string host, int port)
        {
            try
            {
                cl = new VisionClient(host, port, ClientOnConnect, ClientOnRecv, ClientOnDisconnect, 12288);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        // ServerEvents
        static void ServerOnConnect(int remoteID, string remoteIP)
        {
            fm1.Chat_Add(string.Format("server:client{0}({1})からの接続を確立しました", remoteID, remoteIP));
            if (svStat == SERVERSTAT.WAITP1) {
                sv.player1 = remoteID;
                if (sv.player2 == -1)
                {
                    svStat = SERVERSTAT.WAITP2;
                }
                else
                {
                    svStat = SERVERSTAT.PLAYING;
                }
            }
            else if (svStat == SERVERSTAT.WAITP2) {
                sv.player2 = remoteID;
                if (sv.player1 == -1)
                {
                    svStat = SERVERSTAT.WAITP1;
                }
                else
                {
                    svStat = SERVERSTAT.PLAYING;
                }
            }
        }

        static void ServerOnRecv(int remoteID, byte[] accptBytes)
        {
            TOPBYTE topbyte = (TOPBYTE)accptBytes[0];
            accptBytes = VisionFunctions.DeleteTopByte(accptBytes);
            if (topbyte == TOPBYTE.STRING)
            {
                string sendStr;
                sendStr = VisionFunctions.DeserializeToString(accptBytes);
                //fm1.Chat_Add(string.Format("server:client{0}からの通信>{1}", remoteID, accptStr));
                sv.Broadcast(sendStr, TOPBYTE.STRING);
            }
            else if (topbyte == TOPBYTE.NETVISIONCORE)
            {
                NetvisionCore accptCore = new NetvisionCore();
                accptCore.setAll(VisionFunctions.DeserializeToNetvisionCore(accptBytes));
                if (remoteID == sv.player1) { sv.core.setAll(accptCore); }
                else if (remoteID == sv.player2) { sv.core.setAll(accptCore.ReverseSide()); }

                // 返信
                sv.BroadcastCore();
            }
            else if (topbyte == TOPBYTE.DECK)
            {
                Card[] accptDeck = new Card[50];
                accptDeck = VisionFunctions.DeserializeToDeck(accptBytes);
                if (remoteID == sv.player1)
                {
                    for (int i = 0; i < 50; i++) sv.core.cards[i].setAll(accptDeck[i]);
                }
                else if (remoteID == sv.player2)
                {
                    for (int i = 0; i < 50; i++) sv.core.cards[50 + i].setAll(accptDeck[i]);
                }

                if (svStat == SERVERSTAT.PLAYING)
                {
                    sv.StartGame();
                    sv.BroadcastCore();
                }
            }
            else
            {
                string sendStr;
                sendStr = accptBytes.Length.ToString();
                sv.Broadcast(sendStr, TOPBYTE.STRING);
            }
        }

        static void ServerOnDisconnect(int remoteID)
        {
            //Console.WriteLine("server:client{0}との接続が切れました", remoteID);
        }

        // ClientEvents
        static void ClientOnConnect(int remoteID, string remoteIP)
        {
            //Console.WriteLine("client:server{0}({1})からの接続を確立しました", remoteID, remoteIP);
        }

        static void ClientOnRecv(int remoteID, byte[] accptBytes)
        {
            byte[] accptBytes2 = VisionFunctions.DeleteTopByte(accptBytes);
            string accptStr;
            NetvisionCore accptCore;

            switch ((TOPBYTE)accptBytes[0])
            {
                case TOPBYTE.STRING:
                    accptStr = VisionFunctions.DeserializeToString(accptBytes2);
                    fm1.Chat_Add(string.Format("client:server{0}からの通信>{1}", remoteID, accptStr));
                    break;
                case TOPBYTE.NETVISIONCORE:
                    accptCore = VisionFunctions.DeserializeToNetvisionCore(accptBytes2);
                    fm1.setNetvisionCore(accptCore);
                    break;
            }
        }

        static void ClientOnDisconnect(int remoteID)
        {
            //Console.WriteLine("server{0}との接続が切れました", remoteID);
        }
    }
}
