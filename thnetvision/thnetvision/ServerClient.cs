using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCP_async
{
    /// <summary>
    /// TCP_asyncの例外
    /// 
    /// </summary>
    public class TcpException : System.Exception
    {
        static private string[] messages = 
		{
			"",
			"最大長を超過する長さの送信パケットです",
			"指定したremoteIDは無効です",
			"接続に失敗しました",
			"接続が切断されました",
			"このsocketは既に閉じています",
		};
        /// <summary>
        /// TcpExceptionの例外をエラーIDを指定して生成します
        /// </summary>
        /// <param name="errID">エラーID</param>
        public TcpException(int errID)
            : base(messages[errID])
        {

        }
    }

    /// <summary>
    /// 通信先からのデータを受信した時に呼び出されるコールバック関数用のデリゲートです．
    /// </summary>
    /// <param name="remoteID">
    /// 接続先のremoteIDが渡されます．
    /// 相手がサーバーの場合（Clientプログラム）では，（接続するサーバーは1つなので）特に意味はありません．
    /// </param>
    /// <param name="recvBytes">
    /// 受信したデータが渡されます．
    /// </param>
    public delegate void AsyncRecvBytesFunc(int remoteID, byte[] recvBytes);
    /// <summary>
    /// 接続が成功し，以後通信が可能になった時に呼び出されるコールバック関数用のデリゲートです．
    /// </summary>
    /// <param name="remoteID">
    /// Serverプログラムの場合:新たに通信が可能になったクライアントのremoteID（新しいもの）が渡されます．
    /// Clientプログラムの場合:（接続するサーバーは1つなので）特に意味はありません．
    /// </param>
    /// <param name="remoteIP">
    /// 接続先のIPアドレスを渡します．リモートからの接続なのかローカルからの接続なのかを判別できると思われます．
    /// </param>
    public delegate void ConnectFunc(int remoteID, string remoteIP);
    /// <summary>
    /// 接続先との通信が切断した場合に呼び出されるコールバック関数用のデリゲートです．
    /// </summary>
    /// <param name="remoteID">
    /// 切断された接続先のremoteIDが渡されます．以後このremoteIDを使った送受信を行うことは出来ません．
    /// </param>
    public delegate void DisconnectFunc(int remoteID);

    class Channel
    {
        public Channel(int rID, AsyncRecvBytesFunc recvFunc, System.Net.Sockets.Socket workingSoc, DisconnectFunc cFunc, int dataLength )
        {
            this.remoteID = rID;
            this.FuncWhenRecvd = recvFunc;
            this.FuncWhenClosed = cFunc;
            this.socket = workingSoc;
            this.bufferSize = dataLength;
            AsyncStateObject.bufferSizeStatic = dataLength;
            StartReceive(this.socket);
        }

        public int remoteID;
        private System.Net.Sockets.Socket socket;
        private AsyncRecvBytesFunc FuncWhenRecvd;
        private DisconnectFunc FuncWhenClosed;
        private int bufferSize;

        //非同期データ受信のための状態オブジェクト
        private class AsyncStateObject
        {
            public System.Net.Sockets.Socket Socket;
            public byte[] ReceiveBuffer;
            public System.IO.MemoryStream ReceivedData;
            static public int bufferSizeStatic;

            public AsyncStateObject(System.Net.Sockets.Socket soc)
            {
                this.Socket = soc;
                this.ReceiveBuffer = new byte[bufferSizeStatic];
                this.ReceivedData = new System.IO.MemoryStream();
            }
        }

        //データ受信スタート
        private void StartReceive(System.Net.Sockets.Socket soc)
        {
            AsyncStateObject so = new AsyncStateObject(soc);
            Console.WriteLine("buffer size is {0}", so.ReceiveBuffer.Length);

            //非同期受信を開始
            soc.BeginReceive(so.ReceiveBuffer,
                0,
                so.ReceiveBuffer.Length,
                System.Net.Sockets.SocketFlags.None,
                new System.AsyncCallback(this.ReceiveDataCallback),
                so);
        }

        //BeginReceiveのコールバック
        private void ReceiveDataCallback(System.IAsyncResult ar)
        {
            //状態オブジェクトの取得
            AsyncStateObject so = (AsyncStateObject)ar.AsyncState;

            /* so.Socketとthis.socketの違いはあるか？*/

            //読み込んだ長さを取得
            int len = 0;
            try
            {
                len = so.Socket.EndReceive(ar);
            }
            catch (System.ObjectDisposedException)
            {
                FuncWhenClosed(remoteID);
                return;
            }
            catch (System.Net.Sockets.SocketException)
            {
                FuncWhenClosed(remoteID);
                return;
            }
            if (len == 0)
            {
                FuncWhenClosed(remoteID);
                return;
            }
            //受信したデータを蓄積する
            so.ReceivedData.Write(so.ReceiveBuffer, 0, len);
            if (so.Socket.Available == 0)
            {
                //最後まで受信した時
                //受信したデータを文字列に変換
                /*string str = System.Text.Encoding.UTF8.GetString(
                    so.ReceivedData.ToArray());
                */
                this.FuncWhenRecvd(remoteID, so.ReceivedData.ToArray());
                so.ReceivedData.Close();
                so.ReceivedData = new System.IO.MemoryStream();
            }

            //再び受信開始
            so.Socket.BeginReceive(so.ReceiveBuffer,
                0,
                so.ReceiveBuffer.Length,
                System.Net.Sockets.SocketFlags.None,
                new System.AsyncCallback(ReceiveDataCallback),
                so);
        }
        public void Send(byte[] sendBytes)
        {
            // 送信文字列が長すぎる
            if (sendBytes.Length > this.bufferSize)
            {
                TcpException e = new TcpException(1);
                throw e;
            }
            //socket.Send(System.Text.Encoding.UTF8.GetBytes(sendStr));
            socket.Send(sendBytes);
        }

        ~Channel()
        {
            socket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
            socket.Close(); //BeginReceiveで開始している非同期受け付けも終了する
        }
    }

    /// <summary>
    /// Serverクラスの概要・・・▽
    /// <newpara>
    /// TCPサーバを立ち上げ，接続したクライアントと通信するクラスです．
    /// </newpara>
    /// </summary>
    /// <remarks>
    /// Serverクラスの解説・・・▽
    /// <newpara>
    /// クライアントの接続およびクライアントからの通信を非同期に待つことができます．
    /// 接続してきたクライアントには順に一意なremoteIDが割り振られ，この値を指定することにより送受信が行われます．
    /// 同じIPアドレス，ポート番号を持つクライアントが接続，切断を繰り返した場合でも同じremoteIDがふられるとは限りません．
    /// クライアントの同時接続数およびクライアントからの通信の数に制限はありませんが，
    /// クライアントプログラムはTCP_async.Clientを用いることを推奨しますが，送信する信号は純粋にバイト型の配列で特に制御信号を載せません．
    /// このためTCP_async.Client以外のクライアントプログラムを利用することができます．
    /// </newpara>
    /// </remarks>
    public class Server
    {

        /// <summary>
        /// コンストラクタ，クライアントからの接続を開始します．
        /// </summary>
        /// <param name="port">
        /// クライアントからの接続を待つポート番号（0 から 65535）を指定します．
        /// Well known port numbersは使うべきではありません．
        /// </param>
        /// <param name="accptdFunc">
        /// クライアントからの接続を受け入れた時に呼び出すコールバック関数のデリゲートを指定します．
        /// コールバックが不要な場合はnullを指定します．
        /// </param>
        /// <param name="recvFunc">
        /// 接続したクライントからの受信があった場合に呼び出すコールバック関数のデリゲートを指定します．
        /// コールバックが不要な場合はnullを指定します．
        /// </param>
        /// <param name="discFunc">
        /// 接続したクライントが切断された場合に呼び出すコールバック関数のデリゲートを指定します．
        /// コールバックが不要な場合はnullを指定します．
        /// </param>
        /// <remarks>
        /// 最大データ長は1024が用いられます．</remarks>
        public Server(int port, ConnectFunc accptdFunc, AsyncRecvBytesFunc recvFunc, DisconnectFunc discFunc)
            : this(port, accptdFunc, recvFunc, discFunc, 1024)
        {}

        /// <summary>
        /// コンストラクタ，クライアントからの接続を開始します．
        /// </summary>
        /// <param name="port">
        /// クライアントからの接続を待つポート番号（0 から 65535）を指定します．
        /// Well known port numbersは使うべきではありません．
        /// </param>
        /// <param name="accptdFunc">
        /// クライアントからの接続を受け入れた時に呼び出すコールバック関数のデリゲートを指定します．
        /// コールバックが不要な場合はnullを指定します．
        /// </param>
        /// <param name="recvFunc">
        /// 接続したクライントからの受信があった場合に呼び出すコールバック関数のデリゲートを指定します．
        /// コールバックが不要な場合はnullを指定します．
        /// </param>
        /// <param name="discFunc">
        /// 接続したクライントが切断された場合に呼び出すコールバック関数のデリゲートを指定します．
        /// コールバックが不要な場合はnullを指定します．
        /// </param>
        /// <param name="dataLength">
        /// 送受信する際に用いるデータ長（byte型配列の要素数）を指定します．
        /// Sendで送信できる最大データ長になりますが，Sendに与えた配列の大きさによらず，
        /// このデータ長のデータをクライアントに送ります．このためdataLengthが大きすぎると1回1回の通信に時間がかかります．
        /// TCP_async.Clientに指定する値と同じにする必要があります．
        /// </param>
        public Server(int port, ConnectFunc accptdFunc, AsyncRecvBytesFunc recvFunc, DisconnectFunc discFunc, int dataLength)
        {
            nextRemoteID = 0;
            this.FuncWhenRecv = recvFunc;
            this.FuncWhenAccptd = accptdFunc;
            this.FuncWhenDisconnect = discFunc;
            this.chList = new List<Channel>();
            this.bufferSize = dataLength;

            System.Net.IPEndPoint endPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Any, port);
            Console.WriteLine("server:Local address and port: {0}", endPoint.ToString());
            this.listenerSoc = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork,
                System.Net.Sockets.SocketType.Stream,
                System.Net.Sockets.ProtocolType.Tcp);
            this.listenerSoc.Bind(endPoint);
            this.listenerSoc.Listen(1000);

            StartAccept();
        }

        /// <summary>
        /// クライアントにデータを送る関数です．（ブロッキング）
        /// </summary>
        /// <remarks>
        /// <newpara>
        /// 指定したremoteIDを持つクライアントにbyte型の配列（sendBytes）を送信します．配列の大きさは
        /// Serverクラスのコンストラクタで指定した最大データ長を越えてはいけません．
        /// 越えた場合はTcpException型の例外をthrowします．
        /// 送信が終わるまで待ち，送信が終わってから戻る同期的な動作（ブロッキング）を行います．
        /// </newpara>
        /// </remarks>
        /// <param name="remoteID">送信先のクライアントに対応するremoteIDを指定します</param>
        /// <param name="sendBytes">送信するbyte型の配列を指定します．配列の大きさはClientクラスのコンストラクタで指定した最大データ長を越えてはいけません．</param>
        public void Send(int remoteID, byte[] sendBytes)
        {
            int idxSockList;
            //Sendに指定したremoteIDが無効な値である
            if (!rID2idx(remoteID, out idxSockList))
            {
                TcpException e = new TcpException(2);
                throw e;
            }
            chList[idxSockList].Send(sendBytes);
        }
        /// <summary>
        /// クライアントが現在接続中であるかを答える関数です．
        /// </summary>
        /// <param name="remoteID">接続中かどうかを問い合わせたいクライアントに対応するremoteIDを指定します．</param>
        /// <returns>true:クライアントは接続中です．false:クライアントは接続していません．</returns>
        public bool IsExistRemoteID(int remoteID)
        {
            int idxSockList;
            return rID2idx(remoteID, out idxSockList);
        }

        /// <summary>
        /// 現在接続しているクライアントのリストを返す関数です．
        /// </summary>
        /// <returns>接続しているクライアントのremoteIDの配列を返します．</returns>
        public int[] ConnectedList()
        {
            int[] ret = new int[chList.Count];
            for (int idx = 0; idx < chList.Count; idx++) ret[idx] = chList[idx].remoteID;
            return ret;
        }

        /// <summary>
        /// クライアントの接続待ちをリスタートする関数です．
        /// </summary>
        /// <remarks>
        /// StopAccept()で接続待ちをストップした場合に，再びクライアントからの接続待ちの状態に復帰するための関数です．
        /// StopAccept()を呼び出していないのであれば，特に呼ぶ必要はありません．
        /// Serverのインスタンスを作ると同時に呼ばれる関数です．
        /// </remarks>
        public void StartAccept()
        {
            //接続要求待機を開始する
            if (!flgIsAccepting)
                listenerSoc.BeginAccept(new System.AsyncCallback(AcceptCallback), this.listenerSoc);
            flgIsAccepting = true;
            flgRefuseNextConnect = false;
        }

        /// <summary>
        /// クライアントの接続待ちをストップする関数です．
        /// </summary>
        /// <remarks>
        /// Serverクラスはインスタンスを作ると同時にコンストラクタで接続待ちを開始しますが，クライアントからの接続を停止したい時に呼び出す関数です．
        /// 再びクライアントからの接続待ちをしたい場合はStartAccept()を呼び出します．
        /// <newpara>
        /// 厳密にはこの関数を呼び出すことで，クライアントからの接続待ちが完全にストップされるわけではありません．
        /// 接続してきたクライアントを一旦受け入れ，すぐにサーバー側から切断します．
        /// これはクライアント側がすぐに接続できないことを知るための配慮であり，かつ，技術的な問題に起因します．
        /// </newpara>
        /// </remarks>
        public void StopAccept()
        {
            flgRefuseNextConnect = true;
        }

        private int nextRemoteID;
        private List<Channel> chList;
        private AsyncRecvBytesFunc FuncWhenRecv;
        private ConnectFunc FuncWhenAccptd;
        private DisconnectFunc FuncWhenDisconnect;
        private System.Net.Sockets.Socket listenerSoc;
        private bool flgRefuseNextConnect = false;
        private bool flgIsAccepting = false;
        private int bufferSize;


        //BeginAcceptのコールバック
        private void AcceptCallback(System.IAsyncResult ar)
        {
            //サーバーSocketの取得
            System.Net.Sockets.Socket server =
                (System.Net.Sockets.Socket)ar.AsyncState;

            //接続要求を受け入れる
            System.Net.Sockets.Socket client = null;
            bool flgConnected = false;
            try
            {
                //クライアントSocketの取得
                client = server.EndAccept(ar);
                flgIsAccepting = false;
                flgConnected = true;
            }
            catch
            {
                System.Console.WriteLine("接続失敗");
            }
            if (flgRefuseNextConnect)
            {
                client.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                client.Close();
                flgConnected = false;
            }
            if (flgConnected)
            {
                //クライアントが接続した
                if (FuncWhenAccptd != null)
                    FuncWhenAccptd(nextRemoteID, ((System.Net.IPEndPoint)client.RemoteEndPoint).Address.ToString());

                //接続先の同期/非同期受信を設定する
                Channel arecv = new Channel(nextRemoteID, FuncWhenRecv, client, ChClosed, bufferSize);
                this.nextRemoteID++;
                chList.Add(arecv);
            }
            //接続要求待機を再開する
            server.BeginAccept(
                new System.AsyncCallback(AcceptCallback), server);
            flgIsAccepting = true;
        }

        //クライアントが終了した時のコールバック関数
        private void ChClosed(int remoteID)
        {
            int chIdx;
            if (rID2idx(remoteID, out chIdx))
            {
                chList.Remove(chList[chIdx]);
            }
            if (FuncWhenDisconnect != null)
                FuncWhenDisconnect(remoteID);
        }

        private bool rID2idx(int remoteID, out int chListIdx)
        {
            for (int idxSockList = 0; idxSockList < chList.Count(); idxSockList++)
            {
                if (chList[idxSockList].remoteID == remoteID)
                {
                    chListIdx = idxSockList;
                    return true;
                }
            }
            chListIdx = -1;
            return false;
        }

        /// <summary>
        /// デストラクタ
        /// </summary>
        ~Server()
        {
            listenerSoc.Close();
        }
    }

    /// <summary>
    /// Clientクラスの概要・・・▽
    /// <newpara>
    /// TCPサーバに接続し，サーバーと通信するクラスです．
    /// </newpara>
    /// </summary>
    /// <remarks>
    /// Clientクラスの解説・・・▽
    /// <newpara>
    /// サーバーからの受信を非同期に待つことができます．
    /// サーバーへの接続は同期的な関数を用いているため，サーバーが応答しない場合（アドレスが間違っている場合を含む），タイムアウトまで処理待ちします．
    /// このため，フォームアプリケーションなどでこのクラスを使う場合には注意が必要です．
    /// </newpara>
    /// <newpara>
    /// サーバープログラムはTCP_async.Serverを用いることを推奨しますが，送信する信号は純粋にバイト型の配列で特に制御信号を載せません．
    /// このためTCP_async.Server以外のサーバープログラムを利用することができます．
    /// </newpara>
    /// </remarks>
    public class Client
    {
        /// <summary>
        /// コンストラクタ，サーバーへ接続します．
        /// </summary>
        /// <param name="host">
        /// 接続を待っているサーバーのIPアドレスおよびホスト名を指定します．
        /// IPアドレスにはIPv4およびIPv6を用いることができます．
        /// </param>
        /// <param name="port">
        /// 接続を待っているサーバーのポート番号（0 から 65535）を指定します．
        /// </param>
        /// <param name="connectFunc">
        /// サーバーへの接続が成功し，送受信の準備が整った場合に呼び出すコールバック関数のデリゲートを指定します．
        /// これ以降，Sendを呼び出すことができます．
        /// コールバックが不要な場合はnullを指定します．
        /// </param>
        /// <param name="recvFunc">
        /// 接続したサーバーからの受信があった場合に呼び出すコールバック関数のデリゲートを指定します．
        /// コールバックが不要な場合はnullを指定します．
        /// </param>
        /// <param name="discFunc">
        /// サーバーとの通信が切断された場合に呼び出すコールバック関数のデリゲートを指定します．
        /// コールバックが不要な場合はnullを指定します．
        /// </param>
        /// <remarks>最大データ長は1024が使用されます．</remarks>
        public Client(string host, int port, ConnectFunc connectFunc, AsyncRecvBytesFunc recvFunc, DisconnectFunc discFunc)
            : this(host, port, connectFunc, recvFunc, discFunc, 1024)
        { }

        /// <summary>
        /// コンストラクタ，サーバーへ接続します．
        /// </summary>
        /// <param name="host">
        /// 接続を待っているサーバーのIPアドレスおよびホスト名を指定します．
        /// IPアドレスにはIPv4およびIPv6を用いることができます．
        /// </param>
        /// <param name="port">
        /// 接続を待っているサーバーのポート番号（0 から 65535）を指定します．
        /// </param>
        /// <param name="connectFunc">
        /// サーバーへの接続が成功し，送受信の準備が整った場合に呼び出すコールバック関数のデリゲートを指定します．
        /// これ以降，Sendを呼び出すことができます．
        /// コールバックが不要な場合はnullを指定します．
        /// </param>
        /// <param name="recvFunc">
        /// 接続したサーバーからの受信があった場合に呼び出すコールバック関数のデリゲートを指定します．
        /// コールバックが不要な場合はnullを指定します．
        /// </param>
        /// <param name="discFunc">
        /// サーバーとの通信が切断された場合に呼び出すコールバック関数のデリゲートを指定します．
        /// コールバックが不要な場合はnullを指定します．
        /// </param>
        /// <param name="dataLength">
        /// 送受信する際に用いるデータ長（byte型配列の要素数）を指定します．
        /// Sendで送信できる最大データ長になりますが，Sendに与えた配列の大きさによらず，
        /// このデータ長のデータをクライアントに送ります．このためdataLengthが大きすぎると1回1回の通信に時間がかかります．
        /// TCP_async.Serverに指定する値と同じにする必要があります．
        /// </param>    
        public Client(string host, int port, ConnectFunc connectFunc, AsyncRecvBytesFunc recvFunc, DisconnectFunc discFunc, int dataLength)
        {
            System.Net.Sockets.Socket soc = ConnectSocket(host, port);
            ch = new Channel(0, recvFunc, soc, discFunc, dataLength);
            if (connectFunc != null)
                connectFunc(0, ((System.Net.IPEndPoint)soc.RemoteEndPoint).Address.ToString());
        }

        /// <summary>
        /// サーバーにデータを送る関数です．（ブロッキング）
        /// </summary>
        /// <remarks>
        /// <newpara>
        /// サーバーにbyte型の配列（sendBytes）を送信します．配列の大きさは
        /// Clientクラスのコンストラクタで指定した最大データ長を越えてはいけません．
        /// 越えた場合はTcpException型の例外をthrowします．
        /// 送信が終わるまで待ち，サーバーへの送信が終わってから戻る同期的な動作（ブロッキング）を行います．
        /// </newpara>
        /// </remarks>
        /// <param name="sendBytes">送信するbyte型の配列を指定します．配列の大きさはClientクラスのコンストラクタで指定した最大データ長を越えてはいけません．</param>
        public void Send(byte[] sendBytes)
        {
            ch.Send(sendBytes);
        }

        private Channel ch;
        
        private static System.Net.Sockets.Socket ConnectSocket(string server, int port)
        {
            System.Net.Sockets.Socket s = null;
            System.Net.IPHostEntry hostEntry = null;

            // Get host related information.
            hostEntry = System.Net.Dns.GetHostEntry(server);

            // Loop through the AddressList to obtain the supported AddressFamily. This is to avoid
            // an exception that occurs when the host IP Address is not compatible with the address family
            // (typical in the IPv6 case).
            bool isConnected = false;
            foreach (System.Net.IPAddress address in hostEntry.AddressList)
            {
                System.Net.IPEndPoint ipe = new System.Net.IPEndPoint(address, port);
                System.Net.Sockets.Socket tempSocket =
                    new System.Net.Sockets.Socket(ipe.AddressFamily, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
                try
                {
                    tempSocket.Connect(ipe);
                }
                catch (System.Net.Sockets.SocketException)
                {
                    continue;
                }
                if (tempSocket.Connected)
                {
                    s = tempSocket;
                    isConnected = true;
                    break;
                }
                else
                {
                    continue;
                }
            }
            if (isConnected == false)
            {
                TcpException e = new TcpException(3);
                throw e;
            }
            return s;
        }
    }
}
