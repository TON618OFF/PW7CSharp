using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace practice_7.Models
{
    public class MessengerClient
    {
        private Socket _socket;
        private string _ipAddress;
        private int _port;

        public MessengerClient(string ipAddress, int port)
        {
            _ipAddress = ipAddress;
            _port = port;
        }

        public async Task ConnectAsync()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            await _socket.ConnectAsync(IPAddress.Parse(_ipAddress), _port);
        }

        public async Task SendMessageAsync(string message)
        {
            var noliki = Encoding.UTF8.GetBytes(message);
            await _socket.SendAsync(noliki);
        }

        public async Task<string> ReceiveMessageAsync()
        {
            byte[] noliki = new byte[1024];
            await _socket.ReceiveAsync(noliki);
            return Encoding.UTF8.GetString(noliki);
        }

        public void Close()
        {
            _socket.Close();
        }
    }
}
