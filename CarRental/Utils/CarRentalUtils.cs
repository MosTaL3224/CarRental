using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
namespace CarRental.Utils
{
    /// <summary>
    /// Class with static util methods
    /// </summary>
    public class CarRentalUtils
    {
        /// <summary>
        /// Try parse input text to integer
        /// </summary>
        public static bool CheckValueIsInteger(string value)
        {
            try
            {
                Int32.Parse(value);
                return true;
            }catch(Exception e)
            {

            }
            return false;
        }
        /// <summary>
        /// Try parse input text to double
        /// </summary>
        public static bool CheckValueIsDouble(string value)
        {
            try
            {
                Double.Parse(value);
                return true;
            }
            catch (Exception e)
            {

            }
            return false;
        }
        /// <summary>
        /// Check text in array of textboxes, if any have empty value returns false
        /// </summary>
        public static bool CheckTextBoxes(TextBox[] textBoxes)
        {
            foreach(var tb in textBoxes)
            {
                if (tb.Text==null || tb.Text.Trim().Equals(""))
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Clear values
        /// </summary>
        public static void ClearTextBoxes(TextBox[] textBoxes)
        {
            foreach (var tb in textBoxes)
            {
                tb.Text = "";
            }
        }

        public static List<int> PrepareDaysList(int min, int days)
        {
            List<int> daysList = new List<int>();
            for (int i=min; i<=days;i++)
            {
                daysList.Add(i);
            }
            return daysList;
        }

        public static double GetRentPrice(double carRentPrice, double insurancePrice, int days)
        {
            return (carRentPrice + insurancePrice) * days;
        }

        public static bool CheckComboBoxes(ComboBox[] comboBoxes)
        {
            foreach (var cb in comboBoxes)
            {
                if (cb.SelectedItem == null)
                {
                    return false;
                }
            }
            return true;
        }

        public static void ClearComboBoxesSelection(ComboBox[] comboBoxes)
        {
            foreach(var cb in comboBoxes)
            {
                cb.SelectedItem = null;
            }
        }

        public static bool CheckDatePicker(DatePicker datePicker)
        {
            if (datePicker.SelectedDate==null)
            {
                return false;
            }
            return true;
        }
        
    }
}
