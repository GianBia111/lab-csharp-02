namespace DelegatesAndEvents
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <inheritdoc cref="IObservableList{T}" />
    public class ObservableList<TItem> : IObservableList<TItem>
    {

        private readonly IList<TItem> _elements = new List<TItem>();


        /// <inheritdoc cref="IObservableList{T}.ElementInserted" />
        public event ListChangeCallback<TItem> ElementInserted;

        /// <inheritdoc cref="IObservableList{T}.ElementRemoved" />
        public event ListChangeCallback<TItem> ElementRemoved;

        /// <inheritdoc cref="IObservableList{T}.ElementChanged" />
        public event ListElementChangeCallback<TItem> ElementChanged;

        /// <inheritdoc cref="ICollection{T}.Count" />
        public int Count => this._elements.Count;

        /// <inheritdoc cref="ICollection{T}.IsReadOnly" />
        public bool IsReadOnly => this._elements.IsReadOnly;


        /// <inheritdoc cref="IList{T}.this" />
        public TItem this[int index]
        {
            get {
                return this._elements[index];
            }
            set {
                this.ElementChanged?.Invoke(this, (TItem)value, _elements[index], index);
                this._elements[index] = value;
                
            }
        }

        /// <inheritdoc cref="IEnumerable{T}.GetEnumerator" />
        public IEnumerator<TItem> GetEnumerator()
        {
            return this._elements.GetEnumerator();
        }

        /// <inheritdoc cref="IEnumerable.GetEnumerator" />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <inheritdoc cref="ICollection{T}.Add" />
        public void Add(TItem item)
        {
            this._elements.Add(item);
            this.ElementInserted?.Invoke(this, item, this.IndexOf(item));
        }

        /// <inheritdoc cref="ICollection{T}.Clear" />
        public void Clear()
        {
            var clone = new List<TItem>(this._elements);
            this._elements.Clear();
            for (int i = 0; i < clone.Count; i++)
            {
                this.ElementRemoved?.Invoke(this, clone[i], i);
            }
        }

        /// <inheritdoc cref="ICollection{T}.Contains" />
        public bool Contains(TItem item)
        {
            return this._elements.Contains(item);
        }

        /// <inheritdoc cref="ICollection{T}.CopyTo" />
        public void CopyTo(TItem[] array, int arrayIndex)
        {
            this._elements.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc cref="ICollection{T}.Remove" />
        public bool Remove(TItem item)
        {
            if (this.Contains(item))
            {
                this._elements.Remove(item);
                return true;
            }
            else
            {
                return false;
            }
            
        }

        /// <inheritdoc cref="IList{T}.IndexOf" />
        public int IndexOf(TItem item)
        {
            return this._elements.IndexOf(item);
        }

        public void Insert(int index, TItem item)
        {
            this._elements.Insert(index, item);
            this.ElementInserted?.Invoke(this, item, index);
        }

        /// <inheritdoc cref="IList{T}.RemoveAt" />
        public void RemoveAt(int index)
        {
            var removedItem = this._elements[index];
            this._elements.RemoveAt(index);
            this.ElementRemoved?.Invoke(this, removedItem, index);
        }

        /// <inheritdoc cref="IEquatable{T}.Equals(T)" />
        public bool Equals(ObservableList<TItem> other)
        {
            return this._elements.Equals(other._elements);
        }

        /// <inheritdoc cref="object.Equals(object?)" />
        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (this == obj)
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals(obj as ObservableList<TItem>);
        }

        /// <inheritdoc cref="object.GetHashCode" />
        public override int GetHashCode() => this._elements.GetHashCode();

        /// <inheritdoc cref="object.ToString" />
        public override string ToString()
        {
            return "[" + string.Join(", ", this._elements) + "]";
        }
    }
}
