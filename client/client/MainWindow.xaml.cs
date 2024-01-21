using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.AspNetCore.SignalR.Client;

namespace client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void sendMessage(object sender, RoutedEventArgs e)
        {
            var hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5145/chatHub")
                .Build();

            hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                Debug.Print(user + ":" + message);
            });

            await hubConnection.StartAsync();

            // Gửi tin nhắn
            await hubConnection.InvokeAsync("SendMessage", userId.Text, message.Text);
        }
    }
}
