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
using System.Windows.Shapes;
using System.IO.IsolatedStorage;

namespace MSPwdGen
{
    /// <summary>
    /// Interaction logic for SaltInputWindow.xaml
    /// </summary>
    public partial class SaltInputWindow : Window
    {

        public SaltInputWindow()
        {
            InitializeComponent();

            if (MSPWDStorage.MasterKeyFileExists())
            {
                btnEraseKey.IsEnabled = true;
                btnClose.IsEnabled = true;
            }
        }

        private void btn_SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string newSalt = txtSaltInput.Text.Trim();

            if (string.IsNullOrEmpty(newSalt))
            {
                // Don't let a short key be set
                MessageBox.Show("Key must be longer than 3 characters", "Key error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MSPWDStorage.SetMasterKeyFile(MSPWDCrypto.CreateMasterKey(newSalt));
                Close();
            }            
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnEraseKey_Click(object sender, RoutedEventArgs e)
        {
            if (MSPWDStorage.MasterKeyFileExists())
            {
                MSPWDStorage.DeleteMasterKey();
                MessageBox.Show("Master key file has been deleted");
                btnEraseKey.IsEnabled = false;
                btnClose.IsEnabled = false;
            }

        }
    }
}
