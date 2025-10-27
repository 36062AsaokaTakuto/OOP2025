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
        SaveButton.IsEnabled = false;
        UpdateButton.IsEnabled = false;
        DeleteButton.IsEnabled = false;
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
        Customer customers = new Customer {
            Name = NameTextBox.Text,
            Phone = PhoneTextBox.Text,
            Address = AddressTextBox.Text,
            Picture = ImageSourceToByteArray(PictureImage.Source)
        };

        Customer customer = new Customer();

        customer.Picture = ImageSourceToByteArray(PictureImage.Source);

        using (var connection = new SQLiteConnection(App.databasePath)) {
            connection.CreateTable<Customer>();
            connection.Insert(customers);
        }
        ReadDatabase();
        CustomerListView.ItemsSource = _customers;
        ButtonStates();
        SaveButton.IsEnabled = false;
    }
    
    public static byte[] ImageSourceToByteArray (ImageSource imageSource) {
        if (imageSource == null) {
            return Array.Empty<byte>();
        }

        byte[] byteArray = Array.Empty<byte>();
        using (var stream = new MemoryStream()) {
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)imageSource));
            encoder.Save(stream);
            byteArray = stream.ToArray();            
        }
        return byteArray;
    }

    private void UpdateButton_Click(object sender, RoutedEventArgs e) {
        var SelectedPerson = CustomerListView.SelectedItem as Customer;
        if (SelectedPerson is null) return;

        using (var connection = new SQLiteConnection(App.databasePath)) {
            connection.CreateTable<Customer>();

            var customer = new Customer() {
                Id = SelectedPerson.Id,
                Name = NameTextBox.Text,
                Phone = PhoneTextBox.Text,
                Address = AddressTextBox.Text,
                Picture = selectedImagePath != null ? File.ReadAllBytes(selectedImagePath) : ImageSourceToByteArray(PictureImage.Source),
            };

            connection.Update(customer);

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
        selectedImagePath = null;
        ButtonStates();
    }

    private void PictureButton_Click(object sender, RoutedEventArgs e) {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        if (openFileDialog.ShowDialog() ?? false) {
            selectedImagePath = openFileDialog.FileName;
            PictureImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            selectedImagePath = null;
            ButtonStates();
        }
    }

    private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e) {
        var filterList = _customers.Where(p => p.Name.Contains(SearchTextBox.Text));
        CustomerListView.ItemsSource = filterList;
    }

    private void ButtonStates() {
        var selected = CustomerListView.SelectedItem as Customer;

        // 入力欄がすべて埋まっているか
        bool hasInput = !string.IsNullOrWhiteSpace(NameTextBox.Text)
                     && !string.IsNullOrWhiteSpace(PhoneTextBox.Text)
                     && !string.IsNullOrWhiteSpace(AddressTextBox.Text);

        // 現在の画像を取得
        //byte[] currentImageBytes;
        //if (selectedImagePath != null && File.Exists(selectedImagePath)) {
        //    currentImageBytes = File.ReadAllBytes(selectedImagePath);
        //} else {
        //    currentImageBytes = ImageSourceToByteArray(PictureImage.Source);
        //}
        byte[] currentImageBytes = ImageSourceToByteArray(PictureImage.Source);



        // 選択された顧客と入力内容がすべて同じかどうか
        bool isSameAsSelected = false;
        if (selected != null) {
            bool isSameText = selected.Name == NameTextBox.Text
                           && selected.Phone == PhoneTextBox.Text
                           && selected.Address == AddressTextBox.Text;

            bool isSameImage = selected.Picture != null
                            && currentImageBytes.SequenceEqual(selected.Picture);

            isSameAsSelected = isSameText && isSameImage;
        }

        // 顧客リストに完全一致するものがあるか（重複チェック）
        bool isDuplicate = _customers.Any(c =>
            c.Name == NameTextBox.Text &&
            c.Phone == PhoneTextBox.Text &&
            c.Address == AddressTextBox.Text &&
            c.Picture != null &&
            currentImageBytes.SequenceEqual(c.Picture)
        );

        // SaveButton：選択されていない状態で入力がある場合のみ有効（新規登録用）
        SaveButton.IsEnabled = hasInput && selected == null && !isDuplicate;

        // UpdateButton：選択された顧客がいて、内容が変更されている場合のみ有効
        bool isModified = selected != null && !isSameAsSelected;
        UpdateButton.IsEnabled = isModified;

        // DeleteButton：選択されていれば有効
        DeleteButton.IsEnabled = selected != null;

    }

    private void InputFieldsChanged(object sender, TextChangedEventArgs e) {
        ButtonStates();
    }
}