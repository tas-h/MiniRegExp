using System.Collections.Generic;

namespace RegExpService
{
    /// <summary>Kimenet szerintt csoportosítást leíró osztály.</summary>
    internal class MatchGroupedInfoList
    {
        /// <summary>Érték/index pár beállítása.</summary>
        /// <param name="pValue">Talált karakterlánc érték.</param>
        /// <param name="pIndex">Kezdőpozíció az eredeti karakterláncban.</param>
        internal void Set(string pValue, int pIndex)
        {
            MatchGroupedInfo matchGroupedInfo = matchGroupedInfoList.Find(x => x.Value == pValue);
            if (matchGroupedInfo == null)
            {
                matchGroupedInfoList.Add(new(pValue, pIndex));
                return;
            }
            matchGroupedInfo.Add(pIndex);
        }

        /// <summary>Az összes azonos kimenetű találat információinak lekérdezése.</summary>
        /// <returns>Azonos kimenetű találatok információit tartalmazó lista.</returns>
        internal MatchGroupedInfo[] GetMatchGroupedInfos()
        {
            return matchGroupedInfoList.ToArray();
        }


        #region Privát terület!

        readonly List<MatchGroupedInfo> matchGroupedInfoList = new();

        #endregion
    }
}