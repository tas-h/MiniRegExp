using System;
using System.Data.SQLite;
using System.Text.RegularExpressions;

namespace RegExpService
{
    /// <summary>A program sqlite adatbázishoz való csatlakozását és kezelését végző osztály.</summary>
    internal class SQLiteDB : System.IDisposable
    {
        /// <summary>Konstruktor, ami csatlakozik az adatbázis fájlhoz, amennyiben nem létezik, akkor létrehozza, ill.
        /// a szükséges táblát is létrehozza, amennyiben az nem létezik.</summary>
        public SQLiteDB()
        {
            dbConnection = new SQLiteConnection("Data Source=RegExpService.sqlite;Version=3;");
            dbConnection.Open();
            InitDB();
        }

        /// <summary>Regex információk elmentése az adatbázisba.</summary>
        /// <param name="pRegexInfo">Elmentendő regex információk.</param>
        public void Save(RegexInfo pRegexInfo)
        {
            CheckData(pRegexInfo);
            SQLiteCommand existCommand = new($"SELECT {c_ID} FROM {c_RegexInfoTableName} WHERE {c_NameFieldName} = @{c_NameFieldName}", dbConnection);
            existCommand.Parameters.AddWithValue(c_NameFieldName, pRegexInfo.Name);
            SQLiteDataReader reader = existCommand.ExecuteReader();
            SQLiteCommand saveCommand;
            if (reader.HasRows)
            {
                reader.Read();
                Int64 id = (Int64)reader[c_ID];
                saveCommand = new($@"
                    UPDATE 
                        {c_RegexInfoTableName} 
                    SET 
                        {c_DescriptionFieldName} = @{c_DescriptionFieldName},
                        {c_InputFieldName} = @{c_InputFieldName},
                        {c_PatternFieldName} = @{c_PatternFieldName},
                        {c_RegexOptionsFieldName} = @{c_RegexOptionsFieldName},
                        {c_ReplacementFieldName} = @{c_ReplacementFieldName}
                    WHERE 
                        {c_ID} = {id}", dbConnection);
            }
            else
            {
                saveCommand = new($@"
                    INSERT INTO {c_RegexInfoTableName} (
                        {c_NameFieldName},
                        {c_DescriptionFieldName},
                        {c_InputFieldName},
                        {c_PatternFieldName},
                        {c_RegexOptionsFieldName},
                        {c_ReplacementFieldName} )
                    VALUES (
                        @{c_NameFieldName},
                        @{c_DescriptionFieldName},
                        @{c_InputFieldName},
                        @{c_PatternFieldName},
                        @{c_RegexOptionsFieldName},
                        @{c_ReplacementFieldName} )
                    ", dbConnection);
                saveCommand.Parameters.AddWithValue(c_NameFieldName, pRegexInfo.Name);
            }
            saveCommand.Parameters.AddWithValue(c_DescriptionFieldName, pRegexInfo.Description);
            saveCommand.Parameters.AddWithValue(c_InputFieldName, pRegexInfo.Input);
            saveCommand.Parameters.AddWithValue(c_PatternFieldName, pRegexInfo.Pattern);
            saveCommand.Parameters.AddWithValue(c_RegexOptionsFieldName, pRegexInfo.RegexOptions);
            saveCommand.Parameters.AddWithValue(c_ReplacementFieldName, pRegexInfo.Replacement);
            saveCommand.ExecuteNonQuery();
        }

        /// <summary>Regex információk betöltése adatbázisból.</summary>
        /// <param name="pName">Betöltendő regex információk neve.</param>
        /// <returns>Regex információk.</returns>
        public RegexInfo Load(string pName)
        {
            SQLiteCommand loadCommand = new(@$"
                SELECT 
                    {c_DescriptionFieldName},
                    {c_InputFieldName},
                    {c_PatternFieldName},
                    {c_RegexOptionsFieldName},
                    {c_ReplacementFieldName} 
                FROM 
                    {c_RegexInfoTableName} 
                WHERE 
                    {c_NameFieldName} = @{c_NameFieldName}", dbConnection);
            loadCommand.Parameters.AddWithValue(c_NameFieldName, pName);
            SQLiteDataReader reader = loadCommand.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                return new(
                    pName,
                    reader[c_DescriptionFieldName].ToString(),
                    reader[c_InputFieldName].ToString(),
                    reader[c_PatternFieldName].ToString(),
                    (RegexOptions)reader[c_RegexOptionsFieldName],
                    reader[c_ReplacementFieldName].ToString());
            }
            else
                return new(Service.C_DefaultName, string.Empty, string.Empty, string.Empty, RegexOptions.None, string.Empty);
        }

        #region Privát tertület!

        const string c_RegexInfoTableName = "RegexInfo";
        const string c_ID = "ID";
        const string c_NameFieldName = "Name";
        const string c_DescriptionFieldName = "Description";
        const string c_InputFieldName = "InputText";
        const string c_PatternFieldName = "Pattern";
        const string c_RegexOptionsFieldName = "RegexOptions";
        const string c_ReplacementFieldName = "Replacement";
        readonly SQLiteConnection dbConnection;

        private void InitDB()
        {
            SQLiteCommand existCommand = new($"SELECT 1 FROM sqlite_master WHERE type = 'table' AND name = '{c_RegexInfoTableName}'", dbConnection);
            SQLiteDataReader reader = existCommand.ExecuteReader();
            if (!reader.HasRows)
            {
                SQLiteCommand createTableCommand = new ($@"
                    CREATE TABLE {c_RegexInfoTableName} (
                        {c_ID} INTEGER PRIMARY KEY AUTOINCREMENT,
                        {c_NameFieldName} VARCHAR(150) NOT NULL UNIQUE,
                        {c_DescriptionFieldName} VARCHAR(255),
                        {c_InputFieldName} TEXT,
                        {c_PatternFieldName} VARCHAR(512) NOT NULL,
                        {c_RegexOptionsFieldName} INT NOT NULL,
                        {c_ReplacementFieldName})", dbConnection);
                createTableCommand.ExecuteNonQuery();
            }
        }

        private void CheckData(RegexInfo pRegexInfo)
        {
            if (pRegexInfo.Name.Length > 150)
                throw new($"Az elnevezés mező túl hosszú! Elfogadott maximális méret 150, a megadott elnevezés hossza: {pRegexInfo.Name.Length}.");
            if (pRegexInfo.Description.Length > 255)
                throw new($"A megjegyzés mező túl hosszú! Elfogadott maximális méret 255, a megadott megjegyzés hossza: {pRegexInfo.Description.Length}.");
            if (pRegexInfo.Pattern.Length > 512)
                throw new($"A reguláris minta mező túl hosszú! Elfogadott maximális méret 255, a megadott reguláris minta hossza: {pRegexInfo.Pattern.Length}.");
        }

        public void Dispose()
        {
            dbConnection.Close();
        }

        #endregion
    }
}