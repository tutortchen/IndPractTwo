using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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

namespace SonyaPPTwoVariant
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string lineConnection;

        public MainWindow()
        {
            InitializeComponent();
            Connect("DESKTOP-IGIV7GF\\SQLEXPRESS", "OnlineShop");
        }

        // Метод для установки строки подключения к базе данных
        public void Connect(string servername, string dbname)
            => lineConnection = "Data Source=" + servername + ";Initial Catalog=" + dbname + ";Integrated Security=True";

        // Метод для добавления нового пользователя
        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UserNameTextBox.Text;
            string password = UserPasswordBox.Password;
            string role = (UserRoleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            using (SqlConnection connection = new SqlConnection(lineConnection))
            {
                string query = "INSERT INTO UserAccounts (UserName, UserPassword, UserRole) VALUES (@UserName, @UserPassword, @UserRole)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserName", username);
                command.Parameters.AddWithValue("@UserPassword", password);
                command.Parameters.AddWithValue("@UserRole", role);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Метод для добавления нового товара
        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            string name = ProductNameTextBox.Text;
            string description = ProductDescriptionTextBox.Text;
            decimal price = decimal.Parse(ProductPriceTextBox.Text);
            int categoryID = (ProductCategoryComboBox.SelectedItem as ComboBoxItem).Tag as int? ?? 0;
            int stock = int.Parse(ProductStockTextBox.Text);

            using (SqlConnection connection = new SqlConnection(lineConnection))
            {
                string query = "INSERT INTO StoreProducts (ProductName, ProductDescription, ProductPrice, CategoryID, ProductStock) VALUES (@ProductName, @ProductDescription, @ProductPrice, @CategoryID, @ProductStock)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductName", name);
                command.Parameters.AddWithValue("@ProductDescription", description);
                command.Parameters.AddWithValue("@ProductPrice", price);
                command.Parameters.AddWithValue("@CategoryID", categoryID);
                command.Parameters.AddWithValue("@ProductStock", stock);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Метод для удаления товара
        private void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            int productID = int.Parse(ProductNameTextBox.Text); // Предполагается, что ID товара вводится в поле ProductNameTextBox

            using (SqlConnection connection = new SqlConnection(lineConnection))
            {
                string query = "DELETE FROM StoreProducts WHERE ProductID = @ProductID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductID", productID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Метод для обновления товара
        private void UpdateProductButton_Click(object sender, RoutedEventArgs e)
        {
            int productID = int.Parse(ProductNameTextBox.Text); // Предполагается, что ID товара вводится в поле ProductNameTextBox
            string name = ProductNameTextBox.Text;
            string description = ProductDescriptionTextBox.Text;
            decimal price = decimal.Parse(ProductPriceTextBox.Text);
            int categoryID = (ProductCategoryComboBox.SelectedItem as ComboBoxItem).Tag as int? ?? 0;
            int stock = int.Parse(ProductStockTextBox.Text);

            using (SqlConnection connection = new SqlConnection(lineConnection))
            {
                string query = "UPDATE StoreProducts SET ProductName = @ProductName, ProductDescription = @ProductDescription, ProductPrice = @ProductPrice, CategoryID = @CategoryID, ProductStock = @ProductStock WHERE ProductID = @ProductID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductName", name);
                command.Parameters.AddWithValue("@ProductDescription", description);
                command.Parameters.AddWithValue("@ProductPrice", price);
                command.Parameters.AddWithValue("@CategoryID", categoryID);
                command.Parameters.AddWithValue("@ProductStock", stock);
                command.Parameters.AddWithValue("@ProductID", productID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Метод для добавления новой категории
        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            string name = CategoryNameTextBox.Text;
            string description = CategoryDescriptionTextBox.Text;

            using (SqlConnection connection = new SqlConnection(lineConnection))
            {
                string query = "INSERT INTO ProductCategories (CategoryName, CategoryDescription) VALUES (@CategoryName, @CategoryDescription)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CategoryName", name);
                command.Parameters.AddWithValue("@CategoryDescription", description);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Метод для удаления категории
        private void DeleteCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            int categoryID = int.Parse(CategoryNameTextBox.Text); // Предполагается, что ID категории вводится в поле CategoryNameTextBox

            using (SqlConnection connection = new SqlConnection(lineConnection))
            {
                string query = "DELETE FROM ProductCategories WHERE CategoryID = @CategoryID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CategoryID", categoryID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Метод для обновления категории
        private void UpdateCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            int categoryID = int.Parse(CategoryNameTextBox.Text); // Предполагается, что ID категории вводится в поле CategoryNameTextBox
            string name = CategoryNameTextBox.Text;
            string description = CategoryDescriptionTextBox.Text;

            using (SqlConnection connection = new SqlConnection(lineConnection))
            {
                string query = "UPDATE ProductCategories SET CategoryName = @CategoryName, CategoryDescription = @CategoryDescription WHERE CategoryID = @CategoryID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CategoryName", name);
                command.Parameters.AddWithValue("@CategoryDescription", description);
                command.Parameters.AddWithValue("@CategoryID", categoryID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Метод для добавления нового клиента
        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            string fullName = CustomerNameTextBox.Text;
            string contactInfo = CustomerContactTextBox.Text;

            using (SqlConnection connection = new SqlConnection(lineConnection))
            {
                string query = "INSERT INTO StoreCustomers (CustomerFullName, CustomerContactInfo) VALUES (@CustomerFullName, @CustomerContactInfo)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CustomerFullName", fullName);
                command.Parameters.AddWithValue("@CustomerContactInfo", contactInfo);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Метод для удаления клиента
        private void DeleteCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            int customerID = int.Parse(CustomerNameTextBox.Text); // Предполагается, что ID клиента вводится в поле CustomerNameTextBox

            using (SqlConnection connection = new SqlConnection(lineConnection))
            {
                string query = "DELETE FROM StoreCustomers WHERE CustomerID = @CustomerID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CustomerID", customerID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Метод для обновления клиента
        private void UpdateCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            int customerID = int.Parse(CustomerNameTextBox.Text); // Предполагается, что ID клиента вводится в поле CustomerNameTextBox
            string fullName = CustomerNameTextBox.Text;
            string contactInfo = CustomerContactTextBox.Text;

            using (SqlConnection connection = new SqlConnection(lineConnection))
            {
                string query = "UPDATE StoreCustomers SET CustomerFullName = @CustomerFullName, CustomerContactInfo = @CustomerContactInfo WHERE CustomerID = @CustomerID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CustomerFullName", fullName);
                command.Parameters.AddWithValue("@CustomerContactInfo", contactInfo);
                command.Parameters.AddWithValue("@CustomerID", customerID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Метод для создания нового заказа
        private void CreateOrderButton_Click(object sender, RoutedEventArgs e)
        {
            int customerID = (OrderCustomerComboBox.SelectedItem as ComboBoxItem).Tag as int? ?? 0;
            DateTime orderDate = OrderDatePicker.SelectedDate ?? DateTime.Now;
            string status = (OrderStatusComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            string deliveryAddress = OrderDeliveryAddressTextBox.Text;

            using (SqlConnection connection = new SqlConnection(lineConnection))
            {
                string query = "INSERT INTO CustomerOrders (CustomerID, OrderDate, OrderStatus, DeliveryAddress) VALUES (@CustomerID, @OrderDate, @OrderStatus, @DeliveryAddress)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CustomerID", customerID);
                command.Parameters.AddWithValue("@OrderDate", orderDate);
                command.Parameters.AddWithValue("@OrderStatus", status);
                command.Parameters.AddWithValue("@DeliveryAddress", deliveryAddress);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Метод для обновления статуса заказа
        private void UpdateOrderStatusButton_Click(object sender, RoutedEventArgs e)
        {
            int orderID = int.Parse(OrderCustomerComboBox.Text); // Предполагается, что ID заказа вводится в поле OrderCustomerComboBox
            string status = (OrderStatusComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            using (SqlConnection connection = new SqlConnection(lineConnection))
            {
                string query = "UPDATE CustomerOrders SET OrderStatus = @OrderStatus WHERE OrderID = @OrderID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@OrderStatus", status);
                command.Parameters.AddWithValue("@OrderID", orderID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Метод для поиска товаров
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = SearchTextBox.Text;

            using (SqlConnection connection = new SqlConnection(lineConnection))
            {
                string query = "SELECT * FROM StoreProducts WHERE ProductName LIKE @SearchTerm OR ProductDescription LIKE @SearchTerm OR ProductPrice LIKE @SearchTerm";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                SearchResultsDataGrid.ItemsSource = dataTable.DefaultView;
            }
        }

        // Метод для генерации отчета по статусу заказов
        private void GenerateOrderStatusReportButton_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(lineConnection))
            {
                string query = "SELECT * FROM CustomerOrders WHERE OrderStatus = @OrderStatus";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@OrderStatus", "Ожидание оплаты"); // Пример статуса
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                ReportDataGrid.ItemsSource = dataTable.DefaultView;
            }
        }

        // Метод для генерации отчета по популярным товарам
        private void GeneratePopularProductsReportButton_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(lineConnection))
            {
                string query = @"
                    SELECT p.ProductName, SUM(oi.Quantity) AS TotalQuantity
                    FROM OrderItems oi
                    JOIN StoreProducts p ON oi.ProductID = p.ProductID
                    GROUP BY p.ProductName
                    ORDER BY TotalQuantity DESC";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                ReportDataGrid.ItemsSource = dataTable.DefaultView;
            }
        }

        // Метод для генерации отчета по активным клиентам
        private void GenerateActiveCustomersReportButton_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(lineConnection))
            {
                string query = @"
                    SELECT c.CustomerFullName, COUNT(o.OrderID) AS TotalOrders
                    FROM CustomerOrders o
                    JOIN StoreCustomers c ON o.CustomerID = c.CustomerID
                    GROUP BY c.CustomerFullName
                    ORDER BY TotalOrders DESC";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                ReportDataGrid.ItemsSource = dataTable.DefaultView;
            }
        }

        // Метод для генерации отчета по общей выручке по месяцам
        private void GenerateRevenueReportButton_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(lineConnection))
            {
                string query = @"
                    SELECT YEAR(o.OrderDate) AS Year, MONTH(o.OrderDate) AS Month, SUM(oi.Quantity * p.ProductPrice) AS TotalRevenue
                    FROM CustomerOrders o
                    JOIN OrderItems oi ON o.OrderID = oi.OrderID
                    JOIN StoreProducts p ON oi.ProductID = p.ProductID
                    GROUP BY YEAR(o.OrderDate), MONTH(o.OrderDate)
                    ORDER BY Year, Month";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                ReportDataGrid.ItemsSource = dataTable.DefaultView;
            }
        }
    }
}

