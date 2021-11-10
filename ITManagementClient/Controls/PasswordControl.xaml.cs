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
    public partial class PasswordControl : UserControl
    {
        public PasswordControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SecurePasswordProperty = DependencyProperty.Register(
            "SecurePassword", typeof(string), typeof(PasswordControl), new PropertyMetadata(default(string)));

        public string SecurePassword
        {
            get { return (string)GetValue(SecurePasswordProperty); }
            set
            {
                SetValue(SecurePasswordProperty, value);
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
            SecurePassword = ((PasswordBox)sender).Password;
        }
    }
}
