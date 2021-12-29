using System.Linq;

namespace RegExpService
{
    /// <summary>Az eredmény információit tároló osztály.</summary>
    public class MatchesInfo
    {
        /// <summary>A bemeneti karakterlánc hossza.</summary>
        public int InputLength { get; }

        /// <summary>A találatok darabszáma.</summary>
        public int Count { get; }

        /// <summary>Találati lista elemeinek elérése..</summary>
        /// <param name="idx">Az elérendő egyed indexe.</param>
        /// <returns>A kért indexű egyed.</returns>
        public MatchInfo this[int idx] => matchInfos[idx];

        /// <summary>Konstruktor adat injektálással.</summary>
        /// <param name="pMatchInfos">A találatok listája.</param>
        /// <param name="pInputLength">A bemeneti karakterlánc hossza.</param>
        public MatchesInfo(MatchInfo[] pMatchInfos, int pInputLength)
        {
            matchInfos = pMatchInfos;
            InputLength = pInputLength;
            Count = matchInfos.Length;
        }

        /// <summary>Találatok kimenet szerint csoportosított listáját adja vissza.</summary>
        /// <returns>Kimenet szerint csoportosított lista.</returns>
        public MatchGroupedInfo[] GetMatchGroupedInfo()
        {
            MatchGroupedInfoList matchGroupedInfoList = new();
            foreach(MatchInfo matchInfo in matchInfos)
                matchGroupedInfoList.Set(matchInfo.Value, matchInfo.Index);
            return matchGroupedInfoList.GetMatchGroupedInfos();
        }

        /// <summary>Találati lista lekérdezése.</summary>
        /// <returns>Találati lista.</returns>
        public MatchInfo[] GetMatchesInfo()
        {
            return matchInfos.OfType<MatchInfo>().ToArray();
        }


        #region Privát terület!

        private MatchInfo[] matchInfos { get; }

        #endregion
    }
}