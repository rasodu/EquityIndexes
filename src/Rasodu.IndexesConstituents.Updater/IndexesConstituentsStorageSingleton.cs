﻿using System;
using System.Collections.Generic;

namespace Rasodu.IndexesConstituents.Updater
{
    internal delegate void EquityIndexUpdated(List<Equity> equities);
    internal sealed class IndexesConstituentsStorageSingleton
    {
        private IndexesConstituentsStorageSingleton()
        {
            equityIndexHt = new Dictionary<string, IList<Equity>>();
            observers = new Dictionary<string, EquityIndexUpdated>();
        }
        private static readonly Lazy<IndexesConstituentsStorageSingleton> _lazy = new Lazy<IndexesConstituentsStorageSingleton>(() => new IndexesConstituentsStorageSingleton());
        internal static IndexesConstituentsStorageSingleton Instance
        {
            get
            {
                return _lazy.Value;
            }
        }
        internal IDictionary<string, IList<Equity>> equityIndexHt;
        internal IDictionary<string, EquityIndexUpdated> observers;
        internal void OnEquityIndexUpdated(string equityIndexName, EquityIndexUpdated eventDelegate)
        {
            if (!observers.ContainsKey(equityIndexName))
            {
                observers[equityIndexName] = eventDelegate;
            }
            else
            {
                observers[equityIndexName] += eventDelegate;
            }
        }
        internal void SetDataForIndex(string equityIndexName, List<Equity> equitiesInIndex)
        {
            equityIndexHt[equityIndexName] = equitiesInIndex;
            if (observers.ContainsKey(equityIndexName))
            {
                observers[equityIndexName](equitiesInIndex);
            }
        }
    }
}
