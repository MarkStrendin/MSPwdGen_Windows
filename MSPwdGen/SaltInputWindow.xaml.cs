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
            txtSaltInput.Text = Storage.getSalt();
        }

        private void btn_SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Storage.setSalt(txtSaltInput.Text);
            Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
