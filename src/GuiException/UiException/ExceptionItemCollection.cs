﻿// ----------------------------------------------------------------
// ErrorBrowser
// Copyright 2008-2009, Irénée HOTTIER,
// 
// This is free software licensed under the NUnit license, You may
// obtain a copy of the license at http://nunit.org/?p=license&r=2.4
// ----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace NUnit.UiException
{
    public delegate void ItemAddedEventHandler(object sender, ErrorItem item);

    /// <summary>
    /// (formerly named ExceptionItemCollection)
    /// 
    /// Manages an ordered set of ErrorItem.
    /// </summary>
    public class ErrorItemCollection :
        IEnumerable
    {
        /// <summary>
        /// Fired when an item has been added to the collection.
        /// </summary>
        public event ItemAddedEventHandler ItemAdded;

        /// <summary>
        /// Fired when the collection has been cleared.
        /// </summary>
        public event EventHandler CollectionCleared;

        /// <summary>
        /// The underlying item list.
        /// </summary>
        protected List<ErrorItem> _items;

        /// <summary>
        /// Build a new ErrorItemCollection.
        /// </summary>
        public ErrorItemCollection()
        {
            _items = new List<ErrorItem>();

            return;
        }

        /// <summary>
        /// Gets the number of item in this collection.
        /// </summary>
        public int Count {
            get { return (_items.Count); }
        }

        /// <summary>
        /// Gets the ErrorItem at the specified index.
        /// </summary>
        /// <param name="index">The index of the wanted ErrorItem.</param>
        /// <returns>The ErrorItem.</returns>
        public ErrorItem this[int index] {
            get { return (_items[index]); }
        }

        /// <summary>
        /// Appends the given item to the end of the collection.
        /// </summary>
        /// <param name="item">The ErrorItem to be added to the collection.</param>
        public void Add(ErrorItem item)
        {
            UiExceptionHelper.CheckNotNull(item, "item");
            _items.Add(item);

            if (ItemAdded != null)
                ItemAdded(this, item);

            return;
        }

        /// <summary>
        /// Clears all items from this collection.
        /// </summary>
        public void Clear()
        {
            if (_items.Count == 0)
                return;

            _items.Clear();

            if (CollectionCleared != null)
                CollectionCleared(this, new EventArgs());

            return;
        }

        /// <summary>
        /// Checks whether the given item belongs to this collection.
        /// </summary>
        /// <param name="item">The item to be checked.</param>
        /// <returns>True if the item belongs to this collection.</returns>
        public bool Contains(ErrorItem item) {
            return (_items.Contains(item));
        }

        /// <summary>
        /// Reverses the sequence order of this collection.
        /// </summary>
        public void Reverse()
        {
            _items.Reverse();
        }

        #region IEnumerable Membres

        /// <summary>
        /// Gets an IEnumerator able to iterate through all ExceptionItems
        /// managed by this collection.
        /// </summary>
        /// <returns>An iterator to be used to iterator through all items
        /// in this collection.</returns>
        public IEnumerator GetEnumerator() {
            return (_items.GetEnumerator());
        }

        #endregion
    }
}
