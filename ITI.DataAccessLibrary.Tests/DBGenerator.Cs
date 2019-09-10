using ITI.DataAccessLibrary.Correction.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
/// <summary>
/// http://zetcode.com/db/sqlitecsharp/trans/
/// http://www.tsjensen.com/blog/post/2012/11/10/SQLite-on-Visual-Studio-with-NuGet-and-Easy-Instructions
/// </summary>
namespace ITI.DataAccessLibrary.Tests
{
    public class DBGenerator
    {
        readonly string _path;
        readonly string _fileName = "database.sqlite";
        readonly string _dbPath;
        Random _randomSource;

        List<Harbor> _harbors;
        List<ContainerShip> _containerShips;

        public List<Harbor> Harbors { get => _harbors; private set => _harbors = value; }
        public List<ContainerShip> ContainerShips { get => _containerShips; private set => _containerShips = value; }

        public DBGenerator()
        {
            //_path = new FileInfo(Assembly.GetEntryAssembly().Location).Directory.ToString();
            _path = Environment.CurrentDirectory.ToString();
            _dbPath = $"{_path}\\{_fileName}";
            _randomSource = new Random();
            _harbors = new List<Harbor>();
        }

        public void CreateDatabase()
        {
            //clean actual data
            if(File.Exists(_dbPath)) File.Delete(_dbPath);            
            _harbors.Clear();

            //regenerate the data
            SQLiteConnection.CreateFile(_dbPath);

            using (SQLiteConnection connexion = new SQLiteConnection($"Data Source={_fileName};Version=3;"))
            {
                connexion.Open();
                CreateHarbourTable(connexion);
                CreateShipTable(connexion);
                connexion.Close();
            }    
        }

        void CreateHarbourTable(SQLiteConnection connexion,int count = 100)
        {
            using (SQLiteTransaction transaction = connexion.BeginTransaction())
            {
                using (SQLiteCommand command = connexion.CreateCommand())
                {
                    //create the table
                    command.CommandText = "create table Harbor " +
                                    "(" +
                                        "Id INTEGER PRIMARY KEY," +
                                        "Name text," +
                                        "Country text, " +
                                        "Latitude real," +
                                        "Longitude real" +
                                    ")";
                    command.ExecuteNonQuery();

                    //generate harbors
                    Harbor harbor;
                    for (int i = 1; i < count; i++)
                    {
                        harbor = new Harbor()
                        {
                            Id = i,
                            Name = GetRandomName(),
                            Country = GenerateName(_randomSource.Next(3)),
                            Latitude = (_randomSource.NextDouble() * 180) - 90,
                            Longitude = (_randomSource.NextDouble() * 180) - 90
                        };

                        Harbors.Add(harbor);

                        command.CommandText ="insert into Harbor(Name, Country,Longitude,Latitude) values(" +
                            $"'{harbor.Name}', " +
                            $"'{harbor.Country}'," +
                            $"{harbor.Latitude.ToString(CultureInfo.InvariantCulture)}, " +
                            $"{harbor.Longitude.ToString(CultureInfo.InvariantCulture)}" +
                            ")";
                        command.ExecuteNonQuery();
                    }
                }
                transaction.Commit();
            }
        }

        void CreateShipTable(SQLiteConnection connexion, int count = 100)
        {
            if (Harbors.Count < 1) throw new InvalidOperationException();

            using (SQLiteTransaction transaction = connexion.BeginTransaction())
            {
                using (SQLiteCommand command = connexion.CreateCommand())
                {
                    //create the table
                    command.CommandText = "create table Ship " +
                                    "(" +
                                        "Id INTEGER PRIMARY KEY," +
                                        "Name text," +
                                        "ATISCode text," +
                                        "DepartureTime text," +
                                        "ArrivalTime text," +
                                        "Origin int," +
                                        "Destination int," +
                                        "Crew int," +
                                        "MaxWeight int," +
                                        "MaxWidth int," +
                                        "MaxLength int," +
                                        "MaxHeight int," +
                                        "MaxSpeed real" +
                                    ")";
                    command.ExecuteNonQuery();


                    //generate ships
                    ContainerShip containerShip;
                    for (int i = 1; i < count; i++)
                    {
                        Harbor originHarbor = Harbors.Single(x=> x.Id == _randomSource.Next(1, _harbors.Count));
                        Harbor destinationHarbor = Harbors.Single(x=> x.Id == _randomSource.Next(1, _harbors.Count ) && x.Id != originHarbor.Id);
                        DateTime departure = DateTime.Today.AddDays(-_randomSource.Next(15, 35));

                        containerShip = new ContainerShip()
                        {
                            Id= i,
                            ATISCode = Guid.NewGuid().ToString(),
                            Destination = destinationHarbor,
                            Origin = originHarbor,
                            DepartureTime = departure,
                            ArrivalTime = departure.AddDays(_randomSource.Next(15, 35)),
                            Crew = _randomSource.Next(1,20),
                            MaxWeight = (int)(_randomSource.NextDouble()*1000),
                            MaxWidth = _randomSource.Next(6,19),
                            MaxLength = _randomSource.Next(10, 24),
                            MaxHeight = _randomSource.Next(6, 10),
                            MaxSpeed = _randomSource.NextDouble()*10,
                        };

                        //date : "YYYY-MM-DD HH:MM:SS.SSS"

                        ContainerShips.Add(containerShip);

                        command.CommandText = "insert into Ship(ATISCode, Name,Destination,Origin,DepartureTime,ArrivalTime,Crew,MaxWeight,MaxWidth,MaxLength,MaxHeight,MaxSpeed) values(" +
                            $"'{containerShip.ATISCode}', " +
                            $"'{containerShip.Name}', " +
                            $"'{containerShip.Destination.Id}', " +
                            $"'{containerShip.Origin.Id}', " +
                            $"'{containerShip.DepartureTime}'," +
                            $"'{containerShip.ArrivalTime}'," +
                            $"{containerShip.Crew}, " +
                            $"{containerShip.MaxWeight}, " +
                            $"{containerShip.MaxWidth}, " +
                            $"{containerShip.MaxLength}, " +
                            $"{containerShip.MaxHeight}, " +
                            $"{containerShip.MaxSpeed.ToString(CultureInfo.InvariantCulture)}" +
                            ")";
                        command.ExecuteNonQuery();
                    }
                }
                transaction.Commit();
            }
        }


        string GetRandomName()
        {
            //todo : generate real names instead of guid
            return GenerateName(_randomSource.Next(5,10));
            //return Guid.NewGuid().ToString();
        }
        public static string GenerateName(int len)
        {
            Random r = new Random();
            string consonants = "bcdfghjklmlnpqrstvwx";
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            StringBuilder Name = new StringBuilder();
            Name.Append(char.ToUpper(consonants[r.Next(consonants.Length)]));
            Name.Append(vowels[r.Next(vowels.Length)]);
            int b = 2; 
            while (b < len)
            {
                Name.Append(consonants[r.Next(consonants.Length)]);
                b++;
                Name.Append(vowels[r.Next(vowels.Length)]);
                b++;
            }

            return Name.ToString();
        }
    }
}
