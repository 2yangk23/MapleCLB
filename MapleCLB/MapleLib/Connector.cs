using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace MapleCLB.MapleLib {
    public sealed class Connector {
        readonly IPAddress Ip;
        readonly int Port;

        public event EventHandler<Session> OnConnected;
        public event EventHandler<SocketError> OnError;


        public Connector(IPAddress ip, int port) {
            this.Ip = ip;
            this.Port = port;
        }

        public void Connect(int timeout = 13000) {
            Debug.WriteLine("Connecting to: " + Ip + ":" + Port);
            var client = new TcpClient(AddressFamily.InterNetwork);
            var iar = client.BeginConnect(Ip, Port, EndConnect, client);
            iar.AsyncWaitHandle.WaitOne(timeout, true);
            if (!client.Connected) {
                Debug.WriteLine("Bug Testing");
                client.Close(); //Do I want to close the client?
                throw new SocketException(10060); //Connection timeout
            }
        }


        public void OnReconnect(object sender, Session s)
        {
            Debug.WriteLine("Test Reconnecting to: " + Ip + ":" + Port);
            var client = new TcpClient(AddressFamily.InterNetwork);
            var iar = client.BeginConnect(Ip, Port, EndConnect, client);
            iar.AsyncWaitHandle.WaitOne(13000, true);
            if (!client.Connected)
            {
                Debug.WriteLine("Bug Testing");
                client.Close(); //Do I want to close the client?
                throw new SocketException(10060); //Connection timeout
            }
        }



        private void EndConnect(IAsyncResult iar) {
            var client = iar.AsyncState as TcpClient;

            try {
                // ReSharper disable once PossibleNullReferenceException
                client.EndConnect(iar);

                if (client.Connected) {
                    var session = new Session(client.Client, SessionType.CLIENT);
                    session.OnReconnect += new EventHandler<Session>(OnReconnect);
                    if (OnConnected != null) {
                        OnConnected(this, session);
                    }

                    session.Start(null);
                } else {
                    Debug.WriteLine("Failed to connect, let's try again?");
                    Connect();
                }
            } catch (SocketException ex) {
                if (OnError != null) {
                    OnError(this, ex.SocketErrorCode);
                }
            }
        }
    }
}
