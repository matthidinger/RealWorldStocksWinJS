using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;

namespace RealWorldStocks.Client.Core.Models
{
    public abstract class JsonSerializedCollection<TCollection, TItem> : ObservableCollection<TItem> 
        where TCollection : JsonSerializedCollection<TCollection, TItem>, new() 
        where TItem : class
    {
        public abstract string StorageFilename { get; }

        public static void Initialize()
        {
            if (Current == null)
            {
                Current = new TCollection();
                Current.LoadFromCache();
            }
        }

        public static TCollection Current { get; private set; }
        protected abstract IEnumerable<TItem> DefaultItems { get; }

        public void LoadFromCache()
        {
            using (var appStorage = IsolatedStorageFile.GetUserStoreForApplication())
            using (var reader = new StreamReader(appStorage.OpenFile(string.Format(StorageFilename), FileMode.OpenOrCreate, FileAccess.ReadWrite)))
            {
                var json = reader.ReadToEnd();
                if (string.IsNullOrEmpty(json))
                {
                    foreach (var item in DefaultItems)
                    {
                        Add(item);
                    }
                }
                else
                {
                    try
                    {
                        var cached = SerializationHelper.Deserialize<ObservableCollection<TItem>>(json);
                        if (cached == null || cached.Count == 0)
                            throw new Exception();

                        foreach (var item in cached)
                        {
                            Add(item);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            "Unfortuately something has caused your data to become corrupt. We are going to have to revert to application defaults.",
                            "Data Corrupt", MessageBoxButton.OK);
#if !DEBUG
                        appStorage.DeleteFile(StorageFilename);
#endif
                    }
                }
            }
        }

        public void PersistData()
        {
            using (var appStorage = IsolatedStorageFile.GetUserStoreForApplication())
            using (var writer = new StreamWriter(appStorage.OpenFile(string.Format(StorageFilename), FileMode.Create, FileAccess.ReadWrite)))
            {
                try
                {
                    var json = SerializationHelper.Serialize(this);
                    writer.Write(json);
                }
                catch (Exception)
                {
                    Debug.WriteLine(string.Format("Unable to properly serialize to {0}!", StorageFilename));
                }
            }
        }

        public void Delete()
        {
            Clear();
            using (var appStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                appStorage.DeleteFile(StorageFilename);
            }
        }
    }
}