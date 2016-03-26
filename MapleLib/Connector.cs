using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using MapleLib.Crypto;

namespace MapleLib {
    public sealed class Connector {
        private readonly IPAddress ip;
        private readonly int port;
        private readonly AesCipher aesCipher;


        public event EventHandler<Session> OnConnected;
        public event EventHandler<SocketError> OnError;

        public Connector(IPAddress ip, int port, AesCipher aesCipher) {
            this.ip = ip;
            this.port = port;
            this.aesCipher = aesCipher;
        }

        public void Connect(int timeout = 13000) {
            Debug.WriteLine("Connecting to: " + ip + ":" + port);
            var client = new TcpClient(AddressFamily.InterNetwork);
            var iar = client.BeginConnect(ip, port, EndConnect, client);
            iar.AsyncWaitHandle.WaitOne(timeout, true);
            if (!client.Connected) {
                Debug.WriteLine("Bug Testing");
                client.Close(); //Do I want to close the client?
                throw new SocketException(10060); //Connection timeout
            }
        }

        // TODO: Try to reuse same session
        public void OnReconnect(object sender, Session s) {
            Debug.WriteLine("Test Reconnecting to: " + ip + ":" + port);
            var client = new TcpClient(AddressFamily.InterNetwork);
            var iar = client.BeginConnect(ip, port, EndConnect, client);
            iar.AsyncWaitHandle.WaitOne(13000, true);
            if (!client.Connected) {
                Debug.WriteLine("Bug Testing");
                client.Close(); //Do I want to close the client?
                throw new SocketException(10060); //Connection timeout
            }
        }

        private void EndConnect(IAsyncResult iar) {
            var client = iar.AsyncState as TcpClient;
            try {
                client.EndConnect(iar);
                if (client.Connected) {
                    var session = new Session(client.Client, SessionType.CLIENT, aesCipher);
                    session.OnReconnect += OnReconnect;
                    OnConnected?.Invoke(this, session);
                    session.Start(null);
                } else {
                    Debug.WriteLine("Failed to connect, let's try again?");
                    Connect();
                }
            } catch (SocketException ex) {
                OnError?.Invoke(this, ex.SocketErrorCode);
            }
        }
    }
}
