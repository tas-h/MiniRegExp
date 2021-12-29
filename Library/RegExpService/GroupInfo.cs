namespace RegExpService
{
    /// <summary>Az eredmény csoportokat leíró osztály.</summary>
    public class GroupInfo
    {
        /// <summary>Sikeres találat esetén igaz.
        /// További információért lásd: https://docs.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.group.success?view=net-6.0#System_Text_RegularExpressions_Group_Success </summary>
        public bool Success { get; }

        /// <summary>Az aktuális példány által képviselt rögzítési csoport nevét adja vissza.
        /// További információért lásd: https://docs.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.group.name?view=net-6.0#System_Text_RegularExpressions_Group_Name </summary>
        public string Name { get; }

        /// <summary> A rögzített részkarakterláncot adja vissza a bemeneti karakterláncból.
        /// További információért lásd: https://docs.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.capture.value?view=net-6.0#System_Text_RegularExpressions_Capture_Value </summary>
        public string Value { get; }

        /// <summary>A hely az eredeti karakterláncban, ahol a rögzített részkarakterlánc első karaktere található.
        /// További információért lásd: https://docs.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.capture.index?view=net-6.0#System_Text_RegularExpressions_Capture_Index </summary>
        public int Index { get; }

        /// Rögzített részkarakterlánc hosszát adja meg.<summary>
        /// További információért lásd: https://docs.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.capture.length?view=net-6.0#System_Text_RegularExpressions_Capture_Length </summary>
        public int Length { get; }

        /// <summary>Konstruktor adat injektálással.</summary>
        /// <param name="pSuccess">Lásd: <see cref="Success"/>.</param>
        /// <param name="pName">Lásd: <see cref="Name"/>.</param>
        /// <param name="pValue">Lásd: <see cref="Value"/>.</param>
        /// <param name="pIndex">Lásd: <see cref="Index"/>.</param>
        /// <param name="pLength">Lásd: <see cref="Length"/>.</param>
        public GroupInfo(bool pSuccess, string pName, string pValue, int pIndex, int pLength)
        {
            Success = pSuccess;
            Name = pName;
            Value = pValue;
            Index = pIndex;
            Length = pLength;
        }

        /// <summary>Az osztály által tárolt információkat adja vissza.</summary>
        /// <returns>Osztály által tárolt információk.</returns>
        public override string ToString()
        {
            return string.Concat(
                "GROUP\t",
                "Name: ", Name, ", ",
                "Value: ", Value, ", ",
                "Index: ", Index, ", ",
                "Length: ", Length);
        }
    }
}