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

namespace MSPwdGen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Check to see if a key exists already, and prompt the user to create one if it does not.
            if (!MSPWDStorage.MasterKeyFileExists())
            {
                SaltInputWindow SaltInputWindow1 = new SaltInputWindow();
                SaltInputWindow1.Show();
                SaltInputWindow1.Activate();
            }
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            if (MSPWDStorage.MasterKeyFileExists())
            {
                txtOutput_Alpha.Text = MSPWDCrypto.CreatePassword_Alpha(txtInput.Text);
                txtOutput_Special.Text = MSPWDCrypto.CreatePassword_Special(txtInput.Text);
                enableClipboardButtons(true);
            }
            else
            {
                MessageBox.Show("Master key not present - set the master key before generating passwords");
            }
        }

        private void btn_ConfigureSaltDialog_Click(object sender, RoutedEventArgs e)
        {
            SaltInputWindow SaltInputWindow1 = new SaltInputWindow();
            SaltInputWindow1.Show();
            SaltInputWindow1.Activate();
        }

        private void btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void showMessage(string thisMessage)
        {
            MessageBox.Show(thisMessage, "Clipboard", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void enableClipboardButtons(Boolean thisStatus)
        {
            btn_Alpha_Copy8.IsEnabled = thisStatus;
            btn_Alpha_Copy12.IsEnabled = thisStatus;
            btn_Alpha_Copy15.IsEnabled = thisStatus;
            btn_Alpha_Copy20.IsEnabled = thisStatus;
            btn_Alpha_Copy32.IsEnabled = thisStatus;
            btn_Special_Copy8.IsEnabled = thisStatus;
            btn_Special_Copy12.IsEnabled = thisStatus;
            btn_Special_Copy15.IsEnabled = thisStatus;
            btn_Special_Copy20.IsEnabled = thisStatus;
            btn_Special_Copy32.IsEnabled = thisStatus;
        }

        private void btn_Alpha_Copy8_Click(object sender, RoutedEventArgs e)
        {
            if (txtOutput_Alpha.Text.Length >= 8)
            {
                Clipboard.SetText(txtOutput_Alpha.Text.Substring(0, 8));
                showMessage("First 8 characters copied to clipboard!");
            }
        }

        private void btn_Alpha_Copy12_Click(object sender, RoutedEventArgs e)
        {
            if (txtOutput_Alpha.Text.Length >= 12)
            {
                Clipboard.SetText(txtOutput_Alpha.Text.Substring(0, 12));
                showMessage("First 12 characters copied to clipboard!");
            }
        }

        private void btn_Alpha_Copy15_Click(object sender, RoutedEventArgs e)
        {
            if (txtOutput_Alpha.Text.Length >= 15)
            {
                Clipboard.SetText(txtOutput_Alpha.Text.Substring(0, 15));
                showMessage("First 15 characters copied to clipboard!");
            }
        }

        private void btn_Alpha_Copy20_Click(object sender, RoutedEventArgs e)
        {
            if (txtOutput_Alpha.Text.Length >= 20)
            {
                Clipboard.SetText(txtOutput_Alpha.Text.Substring(0, 20));
                showMessage("First 20 characters copied to clipboard!");
            }
        }

        private void btn_Special_Copy8_Click(object sender, RoutedEventArgs e)
        {
            if (txtOutput_Alpha.Text.Length >= 8)
            {
                Clipboard.SetText(txtOutput_Special.Text.Substring(0, 8));
                showMessage("First 8 characters copied to clipboard!");
            }
        }

        private void btn_Special_Copy12_Click(object sender, RoutedEventArgs e)
        {
            if (txtOutput_Alpha.Text.Length >= 12)
            {
                Clipboard.SetText(txtOutput_Special.Text.Substring(0, 12));
                showMessage("First 12 characters copied to clipboard!");
            }
        }

        private void btn_Special_Copy15_Click(object sender, RoutedEventArgs e)
        {
            if (txtOutput_Alpha.Text.Length >= 15)
            {
                Clipboard.SetText(txtOutput_Special.Text.Substring(0, 15));
                showMessage("First 15 characters copied to clipboard!");
            }
        }

        private void btn_Special_Copy20_Click(object sender, RoutedEventArgs e)
        {
            if (txtOutput_Alpha.Text.Length >= 20)
            {
                Clipboard.SetText(txtOutput_Special.Text.Substring(0, 20));
                showMessage("First 20 characters copied to clipboard!");
            }
        }

        private void btn_Special_Copy32_Click(object sender, RoutedEventArgs e)
        {
            if (txtOutput_Special.Text.Length >= 32)
            {
                Clipboard.SetText(txtOutput_Special.Text.Substring(0, 32));
                showMessage("First 32 characters copied to clipboard!");
            }

        }

        private void btn_Alpha_Copy32_Click(object sender, RoutedEventArgs e)
        {
            if (txtOutput_Alpha.Text.Length >= 32)
            {
                Clipboard.SetText(txtOutput_Alpha.Text.Substring(0, 32));
                showMessage("First 32 characters copied to clipboard!");
            }

        }
    }
}
