using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ShoppingListControl.xaml
    /// </summary>
    public partial class ShoppingListControl : UserControl
    {
        public ShoppingListControl()
        {
            InitializeComponent();
        }

        private void AddItemClick(object sender, RoutedEventArgs args)
        {
            var item = new LineItem()
            {
                Name = Input.Text
            };
            if (DataContext is ICollection<LineItem> list)
            {
                list.Add(item);
            }
        }
    }
}
