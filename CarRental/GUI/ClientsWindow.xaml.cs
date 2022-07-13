using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using CarRental.DBObjects;
using CarRental.DBTools;
using CarRental.Utils;
namespace CarRental.GUI
{
    /// <summary>
    /// Interaction logic for ClientsWindow.xaml
    /// </summary>
    public partial class ClientsWindow : Window
    {
        CarRentalDbContext dbContext;
        Client tempClient;
        /// <summary>
        /// Constructor of ClientsWindow Class
        /// </summary>
        public ClientsWindow()
        {
            InitializeComponent();
            dbContext = new CarRentalDbContext();
            ClientsDataGrid.ItemsSource = dbContext.Clients.ToList();
        }

        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] textBoxes = { FirstNameTextBox, LastNameTextBox, IdNumberTextBox, PhoneNumberTextBox, LicenseNumberTextBox };
            if (CarRentalUtils.CheckTextBoxes(textBoxes))
            {
                AddClientToDatabase();
            }
            else
            {
                MessageBox.Show("Nie wypełniono wszystkich wymaganych pól", "Brak wymaganych danych");
            }
        }

        private void UpdateClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (tempClient!=null)
            {
                TextBox[] textBoxes = { FirstNameTextBox, LastNameTextBox, IdNumberTextBox, PhoneNumberTextBox, LicenseNumberTextBox };
                if (CarRentalUtils.CheckTextBoxes(textBoxes))
                {
                    UpdateTempClientValues();
                }
                else
                {
                    MessageBox.Show("Nie wypełniono wszystkich wymaganych pól", "Brak wymaganych danych");
                }
            }
        }

        private void RemoveClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (tempClient!=null)
            {
                dbContext.Clients.Remove(tempClient);
                dbContext.SaveChanges();
                tempClient = null;
                ClientsDataGrid.ItemsSource = dbContext.Clients.ToList();
            }
        }

        private void ClientsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTempClient();
            if (tempClient!=null)
            {
                SetClientTextBoxes();
            }
            else
            {
                TextBox[] textBoxes = { FirstNameTextBox, LastNameTextBox, IdNumberTextBox, PhoneNumberTextBox, LicenseNumberTextBox };
                CarRentalUtils.ClearTextBoxes(textBoxes);
            }
        }

        private void UpdateTempClient()
        {
            try {
                tempClient = (Client)ClientsDataGrid.SelectedItem;
            }catch(Exception e)
            {
                tempClient = null;
            }
        }

        private void UpdateTempClientValues()
        {
            tempClient.FirstName = FirstNameTextBox.Text;
            tempClient.LastName = LastNameTextBox.Text;
            tempClient.IdNumber = IdNumberTextBox.Text;
            tempClient.PhoneNumber = PhoneNumberTextBox.Text;
            tempClient.LicenseNumber = LicenseNumberTextBox.Text;
            dbContext.Update(tempClient);
            dbContext.SaveChanges();
            ClientsDataGrid.ItemsSource = dbContext.Clients.ToList();
        }

        private void SetClientTextBoxes()
        {
            FirstNameTextBox.Text = tempClient.FirstName;
            LastNameTextBox.Text = tempClient.LastName;
            IdNumberTextBox.Text = tempClient.IdNumber;
            PhoneNumberTextBox.Text = tempClient.PhoneNumber;
            LicenseNumberTextBox.Text = tempClient.LicenseNumber;
        }

        private void AddClientToDatabase()
        {
            Client client = new Client();
            client.FirstName = LastNameTextBox.Text;
            client.LastName = LastNameTextBox.Text;
            client.IdNumber = IdNumberTextBox.Text;
            client.PhoneNumber = PhoneNumberTextBox.Text;
            client.LicenseNumber = LicenseNumberTextBox.Text;
            dbContext.Clients.Add(client);
            dbContext.SaveChanges();
            ClientsDataGrid.ItemsSource = dbContext.Clients.ToList();
        }
    }
}
