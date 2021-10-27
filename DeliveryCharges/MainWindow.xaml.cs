// Author : Chavela Rios
// Date   : 2020-09-30
// Write a program called DeliveryCharges for a package delivery service. 
// The program should use an array that holds at least 10 zip codes of areas 
// to which the company makes deliveries. Create a parallel array that contains 
// delivery charges for the zip codes. It is assumed that the delivery charge 
// increases for zip codes that are further from the delivery service home base. 

using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Media; // Used for audio

namespace DeliveryCharges
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Declare variables
        // Array holding prices
        double[] arrayZipCodePrices = { 4.50, 4.25, 4.75, 3.00, 4.00, 5.00, 5.25, 5.50, 5.25, 5.25 };
        // Array holding zipcodes as strings (heard that is the practice)
        string[] arrayZipCodes = { "55321", "55325", "55336", "55350", "55355", "56201", "56228", "56243", "56253", "56288" };
        // Variable to hold index of array to match the string to the price
        int num;
        // Variable to hold the price of the array once index is matched
        double price;
        // Variables to make sure the parcel options are selected
        Boolean letter=false, box=false;

        // SoundPlayer objects
        SoundPlayer AudioLtr = new SoundPlayer(DeliveryCharges.Properties.Resources.Letter_SFX);
        SoundPlayer AudioBox = new SoundPlayer(DeliveryCharges.Properties.Resources.Box_SFX);

        public MainWindow()
        {
            InitializeComponent();
        }

        // Initializing variables when window is first loaded
        private void FrmMain_Loaded(object sender, RoutedEventArgs e)
        {
            New();
        }

        // When Letter Button is clicked
        private void ImgLetter_Click(object sender, RoutedEventArgs e)
        {
            letter = true;
            box = false;
        }

        // When Box Button is clicked
        private void ImgBox_Click(object sender, RoutedEventArgs e)
        {
            box = true;
            letter = false;
        }

        // When Submit Button is clicked
        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            // Takes input and first verifies is Letter or Box was selected

            // If yes
            if (letter || box)
            {
                // Will go on with rest of program
                DeliveryRate();
            }
            else // Otherwise will ask them to pick
            {
                TxtResult.Text = "Please choose a parcel rate.";
            }

            // After program is run resets Letter and Box button selections
            Reset();
        }

        // Re-initializes variables
        private void New()
        {
            TxtZipCode.Text = "";
            TxtResult.Text = "";
            letter = false;
            box = false;
        }

        // Clears result textbox after re-typing in input textbox
        private void TxtZipCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            TxtResult.Text = "";
        }

        // Re-initializes Letter and Box buttons
        private void Reset()
        {
            letter = false;
            box = false;
        }

        // After making sure Letter or Box was selected, takes input and verifies information 
        private void DeliveryRate()
        {
            // Declares variable that initailizes with text that is input
            string myZip = TxtZipCode.Text;

            // If .Contains returns true the input matches an index in the string zipcode array
            if (arrayZipCodes.Contains(myZip))
            {
                // Finds the index where the input matches in the array
                num = Array.IndexOf(arrayZipCodes, myZip);
                // Shows the value in the matching index in the zip code price array
                price = arrayZipCodePrices[num];

                // If Letter button is selected:
                if (letter)
                {
                    // Takes the price and halves it, as postage for letters is cheaper
                    price = price / 2;
                    // Plays audio for letter
                    AudioLtr.Play();
                    // Displays results
                    TxtResult.Text = String.Format("We can deliver to that Zip. Cost per letter is ${0:0.00}",
                    price);
                }
                else if (box) // Otherwise, if Box button is selected:
                {
                    // Takes the price and doubles it, as postage for bigger parcels is expensive
                    price = price * 2;
                    // Plays audio for box
                    AudioBox.Play();
                    // Displays results
                    TxtResult.Text = String.Format("We can deliver to that Zip. Cost per package is ${0:0.00}",
                    price);
                }
            }
            else if (myZip.Length > 5) // If the input doesn't match any in the array, it will check the length for 5 char
            {
                // If longer than 5 char then doesn't match the format we are asking for input
                // Displays further instruction
                TxtResult.Text = "Please enter a 5 digit Zip Code";
            }
            else // If input does have 5 char but doesn't match any in the array
            {
                // Displays that we are unable to service user
                TxtResult.Text = "We do not deliver to that area.";
            }
        }
    }
}
