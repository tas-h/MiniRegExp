using System;
using System.Text.RegularExpressions;

namespace RegExpService
{
    /// <summary>A szolgáltató osztály.</summary>
    public class Service
    {
        /// <summary>Alapértelmezett mentés neve.</summary>
        public const string C_DefaultName = "currentRegex";

        /// <summary>Mintakeresés típusa.</summary>
        public RegExpType RegExpType { get; set; } = RegExpType.Match;

        /// <summary>Keresés rövid megnevezése.</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Kereséshez fűzött leírás.</summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>Bemeneti szöveg.</summary>
        public string Input { get; set; } = string.Empty;

        /// <summary>Reguláris minta.</summary>
        public string Pattern { get; set; } = string.Empty;

        /// <summary>Cseréhez minta.</summary>
        public string Replacement { get; set; } = string.Empty;

        /// <summary>Csere eredménye.</summary>
        public string ReplaceResult { get; set; } = string.Empty;

        /// <summary>Igaz esetén több soros keresési mód lesz beállítva, hamis esetén a teljes szöveget, sortörésektől függetlenül egy sornak tekinti.
        /// Kihatással van a regex kifejezésben a ^ és $ operátorokra. További infóért lásd: https://docs.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-options#multiline-mode </summary>
        public bool Multiline
        {
            get => (regexOptions & RegexOptions.Multiline) == RegexOptions.Multiline;
            set => regexOptions = value ? regexOptions | RegexOptions.Multiline : regexOptions & ~RegexOptions.Multiline;
        }

        /// <summary>Igaz esetén egy soros mód van beállítva. Kihatással van a . operátorra. 
        /// További információért lásd: https://docs.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-options#single-line-mode </summary>
        public bool Singleline
        {
            get => (regexOptions & RegexOptions.Singleline) == RegexOptions.Singleline;
            set => regexOptions = value ? regexOptions | RegexOptions.Singleline : regexOptions & ~RegexOptions.Singleline;
        }


        /// <summary>Igaz esetén a kis és nagy betűk nincsenek megkülönböztetve, hamis esetén ingen.
        /// További információért lásd: https://docs.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-options#case-insensitive-matching </summary>
        public bool IgnoreCase
        {
            get => (regexOptions & RegexOptions.IgnoreCase) == RegexOptions.IgnoreCase;
            set => regexOptions = value ? regexOptions | RegexOptions.IgnoreCase : regexOptions & ~RegexOptions.IgnoreCase;
        }

        /// <summary>Mentés</summary>
        public void Save()
        {
            sqliteDB.Save(new RegexInfo(Name, Description, Input, Pattern, regexOptions, Replacement));
        }

        /// <summary>Betöltés.</summary>
        /// <param name="pName"></param>
        public void Load(string pName)
        {
            RegexInfo regexInfo = sqliteDB.Load(pName);
            Name = regexInfo.Name;
            Description = regexInfo.Description;
            Input = regexInfo.Input;
            Pattern = regexInfo.Pattern;
            regexOptions = regexInfo.RegexOptions;
            Replacement = regexInfo.Replacement;
        }


        /// <summary>A reguláris keresés eredményének lekérése.</summary>
        /// <returns>A reguláris keresés eredménye.</returns>
        public string GetResultText()
        {
            if (RegExpType == RegExpType.Match)
                return Match();
            else
                return Replace();
        }

        /// <summary>Információ lekérdezése a reguláris keresés találatairól.</summary>
        /// <returns>Információ a reguláris keresés találatairól.</returns>
        public MatchesInfo GetMatchInformations()
        {
            MatchInfo[] matchInfos = new MatchInfo[matchCollection.Count];
            for (int i = 0; i < matchCollection.Count; i++)
            {
                GroupInfo[] groupInfo = new GroupInfo[matchCollection[i].Groups.Count];
                for (int j = 0; j < matchCollection[i].Groups.Count; j++)
                    groupInfo[j] = new GroupInfo(
                        matchCollection[i].Groups[j].Success,
                        matchCollection[i].Groups[j].Name,
                        matchCollection[i].Groups[j].Value,
                        matchCollection[i].Groups[j].Index,
                        matchCollection[i].Groups[j].Length);
                matchInfos[i] = new MatchInfo(
                    matchCollection[i].Success,
                    matchCollection[i].Name,
                    matchCollection[i].Value,
                    matchCollection[i].Index,
                    matchCollection[i].Length,
                    groupInfo);
            }
            return new(matchInfos, Input.Length);
        }

        #region Privát terület! 

        /* Bob bácsi kéri, hogy ha csak használod az osztályt, akkor a privát terület tartalmát ne nézegesd, mert óhatatlanul implementációra fogsz fejleszteni!
         * Azt pedig tudjuk, hogy az implementációra fejlesztés olyan függőségeket szül (drót), aminek az eredménye a spagetti kód! */

        private RegexOptions regexOptions = RegexOptions.None;
        private MatchCollection matchCollection = Regex.Matches("","");
        private readonly SQLiteDB sqliteDB = new();

        private string Match()
        {
            matchCollection = Regex.Matches(Input, Pattern, regexOptions);
            string resultText = string.Empty;
            foreach (Match match in matchCollection)
                resultText += $"{match.Value} {Environment.NewLine}";
            return resultText;
        }

        private string Replace()
        {
            matchCollection = Regex.Matches(Input, Pattern, regexOptions);
            ReplaceResult = Regex.Replace(Input, Pattern, Replacement, regexOptions);
            return ReplaceResult;
        }

        #endregion

    }
}
