using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ShoppingData
{
    public class ShoppingListItems : ICollection<LineItem>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        private List<LineItem> _items = new List<LineItem>();

        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        //should be in the base class
        public event PropertyChangedEventHandler? PropertyChanged;

        //helper method to invoke PropertyChanged
        //can put this method in the base class
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //we have some code we want to execute whenever IsFound property changes
        //within LineItem
        //this code will be executed when IsFound changes. We want to announce
        //that FoundItemCount and NotFoundItemCount are also changing
        private void HandleItemPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsFound")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NotFoundItemCount)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FoundItemCount)));

                //could do this from the child class:
                //OnPropertyChanged(nameof(NotFoundItemCount));
            }
        }

        public int FoundItemCount
        {
            get
            {
                int count = 0;
                foreach (LineItem item in _items)
                {
                    if (item.IsFound) count++;
                }

                return count;
            }
        }

        public int NotFoundItemCount
        {
            get
            {
                return Count - FoundItemCount;
            }
        }

        #region collectionImplementation

        public int Count => _items.Count;

        public bool IsReadOnly => ((ICollection<LineItem>)_items).IsReadOnly;

        public void Add(LineItem item)
        {
            _items.Add(item);

            //attach HandleItemPropertyChanged to the new item's PropertyChanged event handler
            item.PropertyChanged += HandleItemPropertyChanged;

            //At this point, Count, FoundItemCount, NotFoundItemCount could change
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FoundItemCount)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NotFoundItemCount)));

            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        public void Clear()
        {
            foreach (LineItem item in _items)
            {
                item.PropertyChanged -= HandleItemPropertyChanged;
            }

            _items.Clear();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FoundItemCount)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NotFoundItemCount)));
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public bool Contains(LineItem item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(LineItem[] array, int arrayIndex)
        {
            ((ICollection<LineItem>)_items).CopyTo(array, arrayIndex);
        }

        public IEnumerator<LineItem> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public bool Remove(LineItem item)
        {
            int index = _items.IndexOf(item);
            if (index == -1)
            {
                _items.Remove(item);
                item.PropertyChanged -= HandleItemPropertyChanged;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FoundItemCount)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NotFoundItemCount)));
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, index));
                return true;
            }
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        #endregion
    }
}
