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
    public partial class RepeatPasswordControl : UserControl
    {
        public RepeatPasswordControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty RepeatSecurePasswordProperty = DependencyProperty.Register(
            "RepeatSecurePassword", typeof(string), typeof(RepeatPasswordControl), new PropertyMetadata(default(string)));

        public string RepeatSecurePassword
        {
            get { return (string)GetValue(RepeatSecurePasswordProperty); }
            set
            {
                SetValue(RepeatSecurePasswordProperty, value);
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
            RepeatSecurePassword = ((PasswordBox)sender).Password;
        }
    }
}
