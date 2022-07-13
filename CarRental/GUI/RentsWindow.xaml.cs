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
using System.Diagnostics;
using CarRental.DBObjects;
using CarRental.DBTools;
using CarRental.Utils;
using CarRental.Containers;
namespace CarRental.GUI
{
    /// <summary>
    /// Interaction logic for RentsWindow.xaml
    /// </summary>
    public partial class RentsWindow : Window
    {
        CarRentalDbContext dbContext;
        List<RentContainer> gridData;
        DbUtils dbUtils;
        RentContainer tempRentContainer;
        /// <summary>
        /// Constructor of RentsWindow Class
        /// </summary>
        public RentsWindow()
        {
            InitializeComponent();
            dbContext = new CarRentalDbContext();
            dbUtils = new DbUtils(dbContext);
            UpdateGridData();
            SetComboBoxesItems();
        }



        private void UpdateGridData()
        {
            gridData = dbUtils.GetRentContainers();
            RentsDataGrid.ItemsSource = gridData;
        }

        private void RentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTempRentContainer();
            if (tempRentContainer!=null)
            {
                UpdateComboBoxes();
                UpdateDatePicker();
            }
            else
            {
                ComboBox[] comboBoxes = { ClientComboBox, CarComboBox, RentLocationComboBox, ReturnLocationComboBox, InsuranceComboBox, DaysComboBox };
                CarRentalUtils.ClearComboBoxesSelection(comboBoxes);
                ClearDatePicker();
            }
        }


        private void UpdateTempRentContainer()
        {
            try
            {
                tempRentContainer = (RentContainer)RentsDataGrid.SelectedItem;
            }catch (Exception e)
            {
                tempRentContainer = null; 
            }
        }

        private void SetComboBoxesItems()
        {
            ClientComboBox.ItemsSource = dbContext.Clients.ToList();
            CarComboBox.ItemsSource = dbContext.Cars.ToList();
            RentLocationComboBox.ItemsSource = dbContext.Localizations.ToList();
            ReturnLocationComboBox.ItemsSource = dbContext.Localizations.ToList();
            InsuranceComboBox.ItemsSource = dbContext.Insurances.ToList();
            DaysComboBox.ItemsSource = CarRentalUtils.PrepareDaysList(1, 30);
        }

        private void UpdateComboBoxes()
        {
            ClientComboBox.SelectedItem = tempRentContainer.Client;
            CarComboBox.SelectedItem = tempRentContainer.CarContainer.Car;
            RentLocationComboBox.SelectedItem = tempRentContainer.LocalizationRent;
            ReturnLocationComboBox.SelectedItem = tempRentContainer.LocalizationReturn;
            InsuranceComboBox.SelectedItem = tempRentContainer.Insurance;
            DaysComboBox.SelectedItem = tempRentContainer.Rent.RentDays;
        }

        private void UpdateDatePicker()
        {
            RentDatePicker.SelectedDate = tempRentContainer.Rent.RentDate;
        }

        private void ClearDatePicker()
        {
            RentDatePicker.SelectedDate = null;
        }

        private void AddLocalizationButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBox[] comboBoxes = { ClientComboBox, CarComboBox, RentLocationComboBox, ReturnLocationComboBox, InsuranceComboBox, DaysComboBox };
            if (CarRentalUtils.CheckComboBoxes(comboBoxes) && CarRentalUtils.CheckDatePicker(RentDatePicker))
            {
                AddRentToDataBase();
            }
            else
            {
                MessageBox.Show("Nie wypełniono wszystkich wymaganych pól", "Brak wymaganych danych");
            }
        }

        private void RemoveLocalizationButton_Click(object sender, RoutedEventArgs e)
        {
            if (tempRentContainer!=null)
            {
                dbContext.Rents.Remove(tempRentContainer.Rent);
                dbContext.SaveChanges();
                tempRentContainer = null;
                UpdateGridData();
            }
        }

        private void UpdateLocalizationButton_Click(object sender, RoutedEventArgs e)
        {
            if (tempRentContainer != null)
            {
                ComboBox[] comboBoxes = { ClientComboBox, CarComboBox, RentLocationComboBox, ReturnLocationComboBox, InsuranceComboBox, DaysComboBox };
                if (CarRentalUtils.CheckComboBoxes(comboBoxes) && CarRentalUtils.CheckDatePicker(RentDatePicker))
                {
                    UpdateRent();
                }
                else
                {
                    MessageBox.Show("Nie wypełniono wszystkich wymaganych pól", "Brak wymaganych danych");
                }
            }
        }

        private void AddRentToDataBase()
        {
            Rent rent = new Rent();
            rent.ClientId = ((Client)ClientComboBox.SelectedItem).ClientId;
            rent.CarId = ((Car)CarComboBox.SelectedItem).CarId;
            rent.InsuranceId = ((Insurance)InsuranceComboBox.SelectedItem).InsuranceId;
            rent.RentLocalizationId = ((DBObjects.Localization)RentLocationComboBox.SelectedItem).LocalizationId;
            rent.ReturnLocalizationId = ((DBObjects.Localization)ReturnLocationComboBox.SelectedItem).LocalizationId;
            rent.RentDate = (DateTime) RentDatePicker.SelectedDate;
            rent.RentDays = (int) DaysComboBox.SelectedItem;
            dbContext.Rents.Add(rent);
            dbContext.SaveChanges();
            UpdateGridData();
        }

        private void UpdateRent()
        {
           tempRentContainer.Rent.ClientId = ((Client)ClientComboBox.SelectedItem).ClientId;
           tempRentContainer.Rent.CarId = ((Car)CarComboBox.SelectedItem).CarId;
           tempRentContainer.Rent.InsuranceId = ((Insurance)InsuranceComboBox.SelectedItem).InsuranceId;
           tempRentContainer.Rent.RentLocalizationId = ((DBObjects.Localization)RentLocationComboBox.SelectedItem).LocalizationId;
           tempRentContainer.Rent.ReturnLocalizationId = ((DBObjects.Localization)ReturnLocationComboBox.SelectedItem).LocalizationId;
           tempRentContainer.Rent.RentDate = (DateTime)RentDatePicker.SelectedDate;
           tempRentContainer.Rent.RentDays = (int)DaysComboBox.SelectedItem;
           dbContext.Update(tempRentContainer.Rent);
           dbContext.SaveChanges();
           UpdateGridData();
        }


    }
}
