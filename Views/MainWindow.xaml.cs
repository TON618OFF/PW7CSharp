using practice_7.Models;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading.Tasks;

namespace practice_7
{

    public partial class MainWindow : Window
    {
        private MessengerClient _client;

        public MainWindow()
        {
            InitializeComponent();
            _client = new MessengerClient("26.17.59.203", 5000);
            _client.MessageReceived += OnMessageReceived;
        }

        private async void OnMessageReceived(object sender, string message)
        {
            pole_message.Items.Add(message);
        }

        private async void OnSendButtonClick(object sender, RoutedEventArgs e)
        {
            string message = your_message.Text;
            await _client.SendMessageAsync(message);
            your_message.Clear();
        }

        private void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _client.Close();
        }
    }
}