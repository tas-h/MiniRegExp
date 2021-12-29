using System.Text.RegularExpressions;

namespace RegExpService
{
    /// <summary>Reguláris kifejezéshez tartozó információk tárolója.</summary>
    public class RegexInfo
    {
        /// <summary>A kifejezés rövid elnevezése.</summary>
        public string Name { get; }

        /// <summary>A kifejezéshez hozzáfűzött megjegyzés.</summary>
        public string Description { get; }

        /// <summary>A bemeneti karakterlánc.</summary>
        public string Input { get; }

        /// <summary>A reguláris minta.</summary>
        public string Pattern { get; }

        /// <summary>Reguláris kifejezés opciói.</summary>
        public RegexOptions RegexOptions { get; }

        /// <summary>Csere esetén a cseréhez szükséges minta.</summary>
        public string Replacement { get; }

        /// <summary>Konstruktor adat injektálással.</summary>
        /// <param name="pName">A kifejezés rövid elnevezése.</param>
        /// <param name="pDescription">A kifejezéshez hozzáfűzött megjegyzés.</param>
        /// <param name="pInput">A bemeneti karakterlánc.</param>
        /// <param name="pPattern">A reguláris minta.</param>
        /// <param name="pRegexOptions">Reguláris kifejezés opciói.</param>
        /// <param name="pReplacement">Csere esetén a cseréhez szükséges minta.</param>
        public RegexInfo(string pName, string pDescription, string pInput, string pPattern, RegexOptions pRegexOptions, string pReplacement)
        {
            Name = pName;
            Description = pDescription;
            Input = pInput;
            Pattern = pPattern;
            RegexOptions = pRegexOptions;
            Replacement = pReplacement;
        }
    }
}