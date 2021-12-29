using System.Collections.Generic;

namespace RegExpService
{
    /// <summary>Azonos találatok csoportosítását leíró osztály.</summary>
    public class MatchGroupedInfo
    {
        /// <summary>Találat értéke.</summary>
        public string Value { get; }

        /// <summary>A csoport darabszáma.</summary>
        public int Count { get { return Indexes.Count; } }

        /// <summary>Konstruktor adat injektálással.</summary>
        /// <param name="pValue">Adott találat eredmény karakterlánca.</param>
        /// <param name="pIndex">Index ami leírja, hogy az eredeti karakterláncban melyik karakteren kezdődik az adott találat.</param>
        public MatchGroupedInfo(string pValue, int pIndex)
        {
            Value = pValue;
            Indexes.Add(pIndex);
        }

        /// <summary>Az összes index lekérése.</summary>
        /// <returns>Eredeti karakterláncba lévő indexek.</returns>
        public int[] GetIndexes()
        {
            return Indexes.ToArray();
        }

        /// <summary>Találat növelése.</summary>
        /// <param name="pIndex">Index ami leírja, hogy az eredeti karakterláncban melyik karakteren kezdődik az adott találat.</param>
        internal void Add(int pIndex)
        {
            Indexes.Add(pIndex);
        }


        #region Privát terület!

        readonly List<int> Indexes = new();

        #endregion

    }
}