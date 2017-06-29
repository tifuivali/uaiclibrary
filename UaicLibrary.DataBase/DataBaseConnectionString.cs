namespace UaicLibrary.DataBase
{
    public static class DataBaseConnectionString
    {
        public const string LocalPgSqlConnection =
            "UserID=admin;Password=admin;Host=localhost;Port=5432;Database=UaicLibrary;Pooling=true;";

        public const string AzurePgSqlConnection =
          "UserID=tifuivali@uaiclibrary-db;Password=ubuntu.w7;Host=uaiclibrary-db.postgres.database.azure.com;Port=5432;Database=UaicLibrary;Pooling=true;SSL Mode=Require; Use SSL Stream=true;Trust Server Certificate=true";
    }
}
