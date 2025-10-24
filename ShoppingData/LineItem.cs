using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingData
{
    /// <summary>
    /// A class representing an item on a shopping list, 
    /// with a quantity and a flag to indicate if it has
    /// been found yet while shopping at the store.
    /// </summary>
    public class LineItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// The name of the item needed
        /// </summary>
        public string Name { get; set; }

        private Category _location = Category.Other;
        public Category Location
        {
            get => _location;
            set
            {
                _location = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Location)));
            }
        }


        private uint _quantity = 1;

        /// <summary>
        /// The quantity of the item needed
        /// </summary>
        public uint Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Quantity)));
            }
        }

        private bool _isFound = false;

        /// <summary>
        /// If the item has been found
        /// </summary>
        public bool IsFound
        {
            get => _isFound;
            set
            {
                _isFound = value;

                //IsFound just changed! Invoke our event handler.
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsFound)));
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
