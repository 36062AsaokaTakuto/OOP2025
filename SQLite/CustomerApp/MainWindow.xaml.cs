using CustomerApp.Data;
using Microsoft.Win32;
using SQLite;
using System.IO;
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

namespace CustomerApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    private List<Customer> _customers = new List<Customer>();
    public MainWindow() {
        InitializeComponent();
        ReadDatabase();
        CustomerListView.ItemsSource = _customers;
    }

    private string? selectedImagePath;

    private void ReadDatabase() {
        using (var connection = new SQLiteConnection(App.databasePath)) {
            connection.CreateTable<Customer>();
            _customers = connection.Table<Customer>().ToList();
        }
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e) {
        var customer = new Customer() {
            Name = NameTextBox.Text,
            Phone = PhoneTextBox.Text,
            Address = AddressTextBox.Text,
            Picture = selectedImagePath is not null ? File.ReadAllBytes(selectedImagePath) : Array.Empty<byte>(),
        };

        using (var connection = new SQLiteConnection(App.databasePath)) {
            connection.CreateTable<Customer>();
            connection.Insert(customer);
        }
    }

    private void UpdateButton_Click(object sender, RoutedEventArgs e) {
        var SelectedPerson = CustomerListView.SelectedItem as Customer;
        if (SelectedPerson is null) return;

        using (var connection = new SQLiteConnection(App.databasePath)) {
            connection.CreateTable<Customer>();

            var person = new Customer() {
                Id = SelectedPerson.Id,
                Name = NameTextBox.Text,
                Phone = PhoneTextBox.Text,
                Address = AddressTextBox.Text,
                Picture = selectedImagePath != null ? File.ReadAllBytes(selectedImagePath) : Array.Empty<byte>(),
            };

            connection.Update(person);

            ReadDatabase();
            CustomerListView.ItemsSource = _customers;
        }
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e) {
        var item = CustomerListView.SelectedItem as Customer;
        if (item == null) {
            MessageBox.Show("行を選択してください");
            return;
        }

        //データベース接続
        using (var connection = new SQLiteConnection(App.databasePath)) {
            connection.CreateTable<Customer>();
            connection.Delete(item);    //データベースから選択されているレコードの削除
            ReadDatabase();
            CustomerListView.ItemsSource = _customers;
        }
    }

    private void CustomerListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
        var SelectedCustomer = CustomerListView.SelectedItem as Customer;
        //if (SelectedPerson is null) return;これか「?.」をつける
        NameTextBox.Text = SelectedCustomer?.Name;
        PhoneTextBox.Text = SelectedCustomer?.Phone;
        AddressTextBox.Text = SelectedCustomer?.Address;
        if (SelectedCustomer?.Picture?.Length > 0) {
            using (var ms = new MemoryStream(SelectedCustomer.Picture)) {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = ms;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                PictureImage.Source = bitmap;
            }
        } else {
            PictureImage.Source = null;
        }

    }

    private void PictureButton_Click(object sender, RoutedEventArgs e) {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        if (openFileDialog.ShowDialog() ?? false) {
            selectedImagePath = openFileDialog.FileName;
            PictureImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
        }
    }
}