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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShoppingList
{
    /// <summary>
    /// Interaction logic for CountBox.xaml
    /// </summary>
    public partial class CountBox : UserControl
    {
        public uint Count
        {
            get
            {
                return (uint)GetValue(CountProperty);
            }
            set
            {
                SetValue(CountProperty, value);
            }
        }

        public static readonly DependencyProperty CountProperty = DependencyProperty.Register(nameof(Count), typeof(uint), typeof(CountBox), new PropertyMetadata(1u));

        public CountBox()
        {
            InitializeComponent();
        }

        private void HandleIncrement(object sender, RoutedEventArgs e)
        {
            if (Count < uint.MaxValue)
            {
                Count++;
            }
        }

        private void HandleDecrement(object sender, RoutedEventArgs e)
        {
            if (Count > 0)
            {
                Count--;
            }
        }
    }
}
