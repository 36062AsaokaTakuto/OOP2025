using Microsoft.Web.WebView2.WinForms;
using System.Security.Policy;
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

namespace WebBrowser;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void BackButton_Click(object sender, RoutedEventArgs e) {
        if (WebView.CanGoBack) {
            WebView.GoBack();
        }
    }

    private void ForwardButton_Click(object sender, RoutedEventArgs e) {
        if (WebView.CanGoForward) {
            WebView.GoForward();
        }
    }

    private void GoButton_Click(object sender, RoutedEventArgs e) {
        //WebView.Source = new Uri(AddressBar.Text);
        var url = AddressBar.Text.Trim();

        if (string.IsNullOrWhiteSpace(url)) return;

        WebView.Source = new Uri(url);
    }

    
}