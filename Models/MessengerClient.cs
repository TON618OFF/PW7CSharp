using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MessengerClient
{
    public class MessengerClientClass
    {
        private Socket _socket;
        private string _ipAddress;
        private int _port;
        private CancellationTokenSource _cancellationTokenSource;

        public MessengerClientClass(string ipAddress, int port)
        {
            _ipAddress = ipAddress;
            _port = port;
        }

        public async Task ConnectAsync(CancellationToken cancellationToken)
        {
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            await _socket.ConnectAsync(IPAddress.Parse(_ipAddress), _port, _cancellationTokenSource.Token);
        }

        public async Task SendMessageAsync(string message)
        {
            var noliki = Encoding.UTF8.GetBytes(message);
            await _socket.SendAsync(noliki, _cancellationTokenSource.Token);
        }

        public async Task<string> ReceiveMessageAsync()
        {
            byte[] noliki = new byte[1024];
            await _socket.ReceiveAsync(noliki, _cancellationTokenSource.Token);
            return Encoding.UTF8.GetString(noliki);
        }

        public void Close()
        {
            _cancellationTokenSource.Cancel();
            _socket.Close();
        }
    }
}
