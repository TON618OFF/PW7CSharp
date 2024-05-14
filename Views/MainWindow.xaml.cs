using MessengerClient;
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
        private MessengerClientClass _client;
        private CancellationTokenSource _cancellationTokenSource;

        public MainWindow()
        {
            InitializeComponent();
            _cancellationTokenSource = new CancellationTokenSource();
            _client = new MessengerClientClass("127.0.0.1", 5000);
            //_client.MessageReceived += OnMessageReceived;
        }

        private async void OnMessageReceived(object sender, string message)
        {
            pole_message.Items.Add(message);
        }

        private async void btn_send_Click(object sender, RoutedEventArgs e)
        {
            string message = your_message.Text;
            await _client.SendMessageAsync(message);
            your_message.Clear();
        }

        private void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _cancellationTokenSource.Cancel();
            _client.Close();
        }
    }
}