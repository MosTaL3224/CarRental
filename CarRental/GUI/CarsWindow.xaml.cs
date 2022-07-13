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
using CarRental.Containers;

namespace CarRental.GUI
{
    /// <summary>
    /// Interaction logic for CarsWindow.xaml
    /// </summary>
    public partial class CarsWindow : Window
    {
        CarRentalDbContext dbContext;
        DbUtils dbUtils;
        CarContainer tempCarContainer;
        /// <summary>
        /// Constructor of CarsWindow Class
        /// </summary>
        public CarsWindow()
        {
            InitializeComponent();
            dbContext = new CarRentalDbContext();
            dbUtils = new DbUtils(dbContext);
            InitializeDataGrids();
            UpdateCarComboBoxes();
        }



        private void InitializeDataGrids()
        {
            CarsDataGrid.ItemsSource = dbUtils.GetCarContainers();
            CarTypeDataGrid.ItemsSource = dbContext.CarTypes.ToList();
            FuelDataGrid.ItemsSource = dbContext.FuelTypes.ToList();
            DriveTypeDataGrid.ItemsSource = dbContext.DriveTypes.ToList();
        }

        private void UpdateCarComboBoxes()
        {
            CarTypeComboBox.ItemsSource = dbContext.CarTypes.ToList();
            FuelComboBox.ItemsSource = dbContext.FuelTypes.ToList();
            DriveTypeComboBox.ItemsSource = dbContext.DriveTypes.ToList();
        }

        private void AddCarTypeButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] textBoxes = { CarTypeTextBox };
            if (CarRentalUtils.CheckTextBoxes(textBoxes))
            {
                CarType carType = new CarType();
                carType.Type = CarTypeTextBox.Text;
                dbContext.CarTypes.Add(carType);
                dbContext.SaveChanges();
                CarTypeDataGrid.ItemsSource = dbContext.CarTypes.ToList();
                CarTypeComboBox.ItemsSource = dbContext.CarTypes.ToList();
            }
            else
            {
                MessageBox.Show("Nie wprowadzono nazwy dla nowego typu auta", "Brak wymaganych danych");
            }
        }

        private void AddFuelButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] textBoxes = { FuelTypeTextBox };
            if (CarRentalUtils.CheckTextBoxes(textBoxes))
            {
                FuelType fuelType = new FuelType();
                fuelType.FuelName = FuelTypeTextBox.Text;
                dbContext.FuelTypes.Add(fuelType);
                dbContext.SaveChanges();
                FuelDataGrid.ItemsSource = dbContext.FuelTypes.ToList();
                FuelComboBox.ItemsSource = dbContext.CarTypes.ToList();
            }
            else
            {
                MessageBox.Show("Nie wprowadzono nazwy dla nowego typu paliwa", "Brak wymaganych danych");
            }
        }

        private void AddDriveTypeButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] textBoxes = { DriveTypeTextBox };
            if (CarRentalUtils.CheckTextBoxes(textBoxes))
            {
                DriveType driveType = new DriveType();
                driveType.Type = DriveTypeTextBox.Text;
                dbContext.DriveTypes.Add(driveType);
                dbContext.SaveChanges();
                DriveTypeDataGrid.ItemsSource = dbContext.DriveTypes.ToList();
                DriveTypeComboBox.ItemsSource = dbContext.DriveTypes.ToList();
            }
            else
            {
                MessageBox.Show("Nie wprowadzono nazwy dla nowego typu napędu", "Brak wymaganych danych");
            }
        }

        private void AddCarButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] textBoxes = { CarNameTextBox, CarPriceTextBox, CarRegNumberTextBox };
            ComboBox[] comboBoxes = { CarTypeComboBox, DriveTypeComboBox, FuelComboBox };
            if (CarRentalUtils.CheckTextBoxes(textBoxes) && CarRentalUtils.CheckComboBoxes(comboBoxes))
            {
                if (CarRentalUtils.CheckValueIsDouble(CarPriceTextBox.Text))
                {
                    AddCarToDataBase();
                }
                else
                {
                    MessageBox.Show("Wprowadzona wartość w polu ceny jest nieprawidłowa", "Zła wartość");
                }
            }else
            {
                MessageBox.Show("Nie wypełniono wszystkich wymaganych pól", "Brak wymaganych danych");
            }
        }

        private void RemoveCarButton_Click(object sender, RoutedEventArgs e)
        {
            if (tempCarContainer!=null)
            {
                dbContext.Cars.Remove(tempCarContainer.Car);
                dbContext.SaveChanges();
                tempCarContainer = null;
                CarsDataGrid.ItemsSource = dbUtils.GetCarContainers();
            }
        }

        private void UpdateCarButton_Click(object sender, RoutedEventArgs e)
        {
            if (tempCarContainer!=null)
            {
                TextBox[] textBoxes = { CarNameTextBox, CarPriceTextBox, CarRegNumberTextBox };
                ComboBox[] comboBoxes = { CarTypeComboBox, DriveTypeComboBox, FuelComboBox };
                if (CarRentalUtils.CheckTextBoxes(textBoxes) && CarRentalUtils.CheckComboBoxes(comboBoxes))
                {
                    if (CarRentalUtils.CheckValueIsDouble(CarPriceTextBox.Text))
                    {
                        UpdateCar();
                    }
                    else
                    {
                        MessageBox.Show("Wprowadzona wartość w polu ceny jest nieprawidłowa", "Zła wartość");
                    }
                }
                else
                {
                    MessageBox.Show("Nie wypełniono wszystkich wymaganych pól", "Brak wymaganych danych");
                }
            }
        }

        private void AddCarToDataBase()
        {
            Car car = new Car();
            car.CarName = CarNameTextBox.Text;
            car.CarPrice = Double.Parse(CarPriceTextBox.Text);
            car.RegistrationNumber = CarRegNumberTextBox.Text;
            car.CarType = ((CarType)CarTypeComboBox.SelectedItem).CarTypeId;
            car.FuelType = ((FuelType)FuelComboBox.SelectedItem).FuelId;
            car.DriveType = ((DriveType)DriveTypeComboBox.SelectedItem).DriveTypeId;
            dbContext.Cars.Add(car);
            dbContext.SaveChanges();
            CarsDataGrid.ItemsSource = dbUtils.GetCarContainers();
        }

        private void UpdateCar()
        {
            tempCarContainer.Car.CarName = CarNameTextBox.Text;
            tempCarContainer.Car.CarPrice = Double.Parse(CarPriceTextBox.Text);
            tempCarContainer.Car.RegistrationNumber = CarRegNumberTextBox.Text;
            tempCarContainer.Car.CarType = ((CarType)CarTypeComboBox.SelectedItem).CarTypeId;
            tempCarContainer.Car.FuelType = ((FuelType)FuelComboBox.SelectedItem).FuelId;
            tempCarContainer.Car.DriveType = ((DriveType)DriveTypeComboBox.SelectedItem).DriveTypeId;
            dbContext.Update(tempCarContainer.Car);
            dbContext.SaveChanges();
            CarsDataGrid.ItemsSource = dbUtils.GetCarContainers();
        }

        private void CarsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTempCarContainer();
            if (tempCarContainer!=null)
            {
                SetTextBoxesAndCombosOnSelect();
            }
            else
            {
                TextBox[] textBoxes = { CarNameTextBox, CarPriceTextBox, CarRegNumberTextBox };
                ComboBox[] comboBoxes = { CarTypeComboBox, DriveTypeComboBox, FuelComboBox };
                CarRentalUtils.ClearTextBoxes(textBoxes);
                CarRentalUtils.ClearComboBoxesSelection(comboBoxes);
            }
        }

        private void UpdateTempCarContainer()
        {
            try
            {
                tempCarContainer = (CarContainer)CarsDataGrid.SelectedItem;
            }
            catch (Exception e)
            {
                tempCarContainer = null;
            }
        }

        private void SetTextBoxesAndCombosOnSelect()
        {
            CarNameTextBox.Text = tempCarContainer.Car.CarName;
            CarPriceTextBox.Text = tempCarContainer.Car.CarPrice.ToString();
            CarRegNumberTextBox.Text = tempCarContainer.Car.RegistrationNumber;
            CarTypeComboBox.SelectedItem = tempCarContainer.CarType;
            FuelComboBox.SelectedItem = tempCarContainer.FuelType;
            DriveTypeComboBox.SelectedItem = tempCarContainer.DriveType;
        }

    }
}
