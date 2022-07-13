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
    /// Interaction logic for InsurancesAndLocalizationsWindow.xaml
    /// </summary>
    public partial class InsurancesAndLocalizationsWindow : Window
    {
        CarRentalDbContext dbContext;
        DBObjects.Localization tempLocalization;
        Insurance tempInsurance;
        /// <summary>
        /// Constructor of InsurancesAndLocalizationsWindow Class
        /// </summary>
        public InsurancesAndLocalizationsWindow()
        {
            InitializeComponent();
            dbContext = new CarRentalDbContext();
            UpdateDataGrids();
        }

        private void UpdateDataGrids()
        {
            InsuranceDataGrid.ItemsSource = dbContext.Insurances.ToList();
            LocalizationDataGrid.ItemsSource = dbContext.Localizations.ToList();
        }


        #region Insurance Region

        private void InsuranceDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTempInsurance();
            if (tempInsurance!=null)
            {               
                SetInsuranceTextBoxes();
            }
            else
            {
                TextBox[] textBoxes = { InsuranceNameTextBox, InsurancePriceTextBox };
                CarRentalUtils.ClearTextBoxes(textBoxes);
            }
        }

        private void SetInsuranceTextBoxes()
        {
           
            InsuranceNameTextBox.Text = tempInsurance.InsuranceName;
            InsurancePriceTextBox.Text = tempInsurance.InsurancePrice.ToString();
        }

        private void AddInsuranceButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] textBoxes = { InsuranceNameTextBox, InsurancePriceTextBox };
            if (CarRentalUtils.CheckTextBoxes(textBoxes))
            {
                if (CarRentalUtils.CheckValueIsDouble(InsurancePriceTextBox.Text))
                {
                    AddInsuranceToDataBase();
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

        private void RemoveInsuranceButton_Click(object sender, RoutedEventArgs e)
        {
            if (tempInsurance!=null)
            {
                dbContext.Insurances.Remove(tempInsurance);
                dbContext.SaveChanges();
                tempInsurance = null;
                InsuranceDataGrid.ItemsSource = dbContext.Insurances.ToList();
            }
        }

        private void UpdateInsuranceButton_Click(object sender, RoutedEventArgs e)
        {
            if (tempInsurance!=null)
            {
                TextBox[] textBoxes = { InsuranceNameTextBox, InsurancePriceTextBox };
                if (CarRentalUtils.CheckTextBoxes(textBoxes))
                {
                    if (CarRentalUtils.CheckValueIsDouble(InsurancePriceTextBox.Text))
                    {
                        UpdateTempInsuranceValues();
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

        private void AddInsuranceToDataBase()
        {
            Insurance insurance = new Insurance();
            insurance.InsuranceName = InsuranceNameTextBox.Text;
            insurance.InsurancePrice = Double.Parse(InsurancePriceTextBox.Text);
            dbContext.Insurances.Add(insurance);
            dbContext.SaveChanges();
            InsuranceDataGrid.ItemsSource = dbContext.Insurances.ToList();
        }

        private void UpdateTempInsurance()
        {
            try
            {
                tempInsurance = (Insurance)InsuranceDataGrid.SelectedItem;
            }catch(Exception e)
            {
                tempInsurance = null;
            }
        }

        private void UpdateTempInsuranceValues()
        {
            tempInsurance.InsuranceName= InsuranceNameTextBox.Text;
            tempInsurance.InsurancePrice = Double.Parse(InsurancePriceTextBox.Text);
            dbContext.Update(tempInsurance);
            dbContext.SaveChanges();
            InsuranceDataGrid.ItemsSource = dbContext.Insurances.ToList();
        }

        #endregion

        #region Localization Region

        private void LocalizationDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTempLocalization();
            if (tempLocalization != null)
            {
                SetLocalizationTextBoxes();
            }
            else
            {
                TextBox[] textBoxes = { CityTextBox, ProvinceTextBox, AddressTextBox };
                CarRentalUtils.ClearTextBoxes(textBoxes);
            }
        }

        private void SetLocalizationTextBoxes()
        {
            CityTextBox.Text = tempLocalization.City;
            ProvinceTextBox.Text = tempLocalization.Province;
            AddressTextBox.Text = tempLocalization.Address;
        }

        private void AddLocalizationButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] textBoxes = { CityTextBox, ProvinceTextBox, AddressTextBox };
            if (CarRentalUtils.CheckTextBoxes(textBoxes))
            {
                AddLocalizationToDataBase();
            }
            else
            {
                MessageBox.Show("Nie wypełniono wszystkich wymaganych pól", "Brak wymaganych danych");
            }
        }

        private void RemoveLocalizationButton_Click(object sender, RoutedEventArgs e)
        {
            if (tempLocalization!=null)
            {
                dbContext.Localizations.Remove(tempLocalization);
                dbContext.SaveChanges();
                tempLocalization = null;
                LocalizationDataGrid.ItemsSource = dbContext.Localizations.ToList();
            }
        }

        private void UpdateLocalizationButton_Click(object sender, RoutedEventArgs e)
        {
            if (tempLocalization!=null)
            {
                TextBox[] textBoxes = { CityTextBox, ProvinceTextBox, AddressTextBox };
                if (CarRentalUtils.CheckTextBoxes(textBoxes))
                {
                    UpdateTempLocalizationValues();
                }
                else
                {
                    MessageBox.Show("Nie wypełniono wszystkich wymaganych pól", "Brak wymaganych danych");
                }
            }
        }

        private void AddLocalizationToDataBase()
        {
            DBObjects.Localization localization = new DBObjects.Localization();
            localization.City = CityTextBox.Text;
            localization.Province = ProvinceTextBox.Text;
            localization.Address = AddressTextBox.Text;
            dbContext.Localizations.Add(localization);
            dbContext.SaveChanges();
            LocalizationDataGrid.ItemsSource = dbContext.Localizations.ToList();
        }

        private void UpdateTempLocalization()
        {
            try
            {
                tempLocalization = (DBObjects.Localization)LocalizationDataGrid.SelectedItem;
            }
            catch (Exception e)
            {
                tempLocalization = null;
            }
        }

        private void UpdateTempLocalizationValues()
        {
            tempLocalization.City = CityTextBox.Text;
            tempLocalization.Province = ProvinceTextBox.Text;
            tempLocalization.Address = AddressTextBox.Text;
            dbContext.Update(tempLocalization);
            dbContext.SaveChanges();
            LocalizationDataGrid.ItemsSource = dbContext.Localizations.ToList();
        }
        #endregion

    }
}
