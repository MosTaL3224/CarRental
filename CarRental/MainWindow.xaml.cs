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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CarRental.GUI;
namespace CarRental
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Constructor of MainWindow Class
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RentButton_Click(object sender, RoutedEventArgs e)
        {
            RentsWindow rentsWindow = new RentsWindow();
            rentsWindow.ShowDialog();
        }

        private void ClientsButton_Click(object sender, RoutedEventArgs e)
        {
            ClientsWindow clientsWindow = new ClientsWindow();
            clientsWindow.ShowDialog();
        }

        private void CarsButton_Click(object sender, RoutedEventArgs e)
        {
            CarsWindow carsWindow = new CarsWindow();
            carsWindow.ShowDialog();
        }

        private void InsurLocationButton_Click(object sender, RoutedEventArgs e)
        {
            InsurancesAndLocalizationsWindow window = new InsurancesAndLocalizationsWindow();
            window.ShowDialog();
        }
    }
}
