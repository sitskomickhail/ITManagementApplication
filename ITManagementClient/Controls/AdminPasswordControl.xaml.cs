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

namespace ITManagementClient.Controls
{
    /// <summary>
    /// Interaction logic for PasswordControl.xaml
    /// </summary>
    public partial class AdminPasswordControl : UserControl
    {
        public AdminPasswordControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty AdminSecurePasswordProperty = DependencyProperty.Register(
            "AdminSecurePassword", typeof(string), typeof(AdminPasswordControl), new PropertyMetadata(default(string)));

        public string AdminSecurePassword
        {
            get { return (string)GetValue(AdminSecurePasswordProperty); }
            set
            {
                SetValue(AdminSecurePasswordProperty, value);
                if (value == String.Empty)
                {
                    var content = this.Content as Grid;
                    var passwordBox = content.Children[0] as PasswordBox;

                    passwordBox.Clear();
                }
            }
        }

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            AdminSecurePassword = ((PasswordBox)sender).Password;
        }
    }
}
