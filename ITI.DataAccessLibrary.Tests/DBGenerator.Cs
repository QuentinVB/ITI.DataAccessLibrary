﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using ITI.DataAccessLibrary.Model;

namespace ITI.DataAccessLibrary.Tests
{
    public class DBGenerator
    {
        readonly string _path;
        readonly string _fileName = "database.sqlite";
        readonly string _dbPath;
        Random _randomSource;

        public const int DEFAULT_HARBOR_COUNT = 10;
        public const int DEFAULT_SHIP_COUNT = 5;
        public const int DEFAULT_CONTAINER_COUNT = 100;

        List<Harbor> _harbors;
        List<ContainerShip> _containerShips;
        List<Container> _containers;

        public List<Harbor> Harbors { get => _harbors; private set => _harbors = value; }
        public List<ContainerShip> ContainerShips { get => _containerShips; private set => _containerShips = value; }
        public List<Container> Containers { get => _containers; set => _containers = value; }
        public Random RandomSource { get => _randomSource; set => _randomSource = value; }

        public DBGenerator()
        {
            //_path = new FileInfo(Assembly.GetEntryAssembly().Location).Directory.ToString();
            _path = Environment.CurrentDirectory.ToString();
            _dbPath = $"{_path}\\{_fileName}";
            _randomSource = new Random();
            _harbors = new List<Harbor>();
            _containerShips = new List<ContainerShip>();
            _containers = new List<Container>();
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
                CreateContainerTable(connexion);
                connexion.Close();
            }    
        }

        void CreateHarbourTable(SQLiteConnection connexion,int count= DEFAULT_HARBOR_COUNT)
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
                    for (int i = 1; i <= count; i++)
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

                        command.CommandText = "insert into Harbor" +
                            "(Name, Country,Latitude,Longitude) " +
                            "values(" +
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

        void CreateShipTable(SQLiteConnection connexion, int count = DEFAULT_SHIP_COUNT)
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
                                        "Origin int," +
                                        "Destination int," +
                                        "DepartureTime text," +
                                        "ArrivalTime text," +
                                        "Crew int," +
                                        "MaxWeight int,"+
                                        "MaxSpeed real," +
                                        "MaxWidth int," +
                                        "MaxHeight int,"+
                                        "MaxLength int"+                                       
                                    ")";
                    command.ExecuteNonQuery();

                    //generate ships
                    ContainerShip containerShip;
                    for (int i = 1; i <= count; i++)
                    {
                        var truc = _randomSource.Next(1, _harbors.Count);
                        Harbor originHarbor = Harbors.Where(
                            x=> x.Id == truc
                            ).First();
                        var machin = _randomSource.Next(1, _harbors.Count);
                        Harbor destinationHarbor = Harbors.Where(x=> x.Id == machin).First();
                        DateTime departure = DateTime.Today.AddDays(-_randomSource.Next(15, 35));

                        containerShip = new ContainerShip()
                        {
                            Id= i,
                            ATISCode = Guid.NewGuid().ToString(),
                            Name = GetRandomName(),
                            Origin = originHarbor,
                            Destination = destinationHarbor,
                            DepartureTime = departure,
                            ArrivalTime = departure.AddDays(_randomSource.Next(15, 35)),
                            Crew = _randomSource.Next(1,20),
                            MaxSpeed = _randomSource.NextDouble() * 20,
                            MaxWidth = _randomSource.Next(6,19),
                            MaxHeight = _randomSource.Next(6, 10),
                            MaxLength = _randomSource.Next(10, 24)

                        };
                        containerShip.MaxWeight = (int)(containerShip.MaxWidth * containerShip.MaxHeight * containerShip.MaxLength * _randomSource.NextDouble() * 10);
                        
                        ContainerShips.Add(containerShip);

                        command.CommandText = "insert into Ship(ATISCode, Name,Origin,Destination,DepartureTime,ArrivalTime,Crew,MaxWeight,MaxSpeed,MaxWidth,MaxHeight,MaxLength) values(" +
                            $"'{containerShip.ATISCode}', " +
                            $"'{containerShip.Name}', " +
                            $"{containerShip.Origin.Id}, " +
                            $"{containerShip.Destination.Id}, " +
                            $"'{containerShip.DepartureTime.ToString("yyyy-MM-dd HH:mm:ss.fff")}'," +
                            $"'{containerShip.ArrivalTime.ToString("yyyy-MM-dd HH:mm:ss.fff")}'," +
                            $"{containerShip.Crew}, " +
                            $"{containerShip.MaxWeight}, " +
                            $"{containerShip.MaxSpeed.ToString(CultureInfo.InvariantCulture)}," +
                            $"{containerShip.MaxWidth}, " +
                            $"{containerShip.MaxHeight}, " +
                            $"{containerShip.MaxLength} " +
                            ")";
                        command.ExecuteNonQuery();
                    }
                }
                transaction.Commit();
            }
        }
        void CreateContainerTable(SQLiteConnection connexion, int count = DEFAULT_CONTAINER_COUNT)
        {
            if (Harbors.Count < 1 || ContainerShips.Count < 1) throw new InvalidOperationException();

            using (SQLiteTransaction transaction = connexion.BeginTransaction())
            {
                using (SQLiteCommand command = connexion.CreateCommand())
                {
                    //create the table
                    command.CommandText = "create table Container " +
                                    "(" +
                                        "Id INTEGER PRIMARY KEY," +
                                        "Reference text," +
                                        "Content text," +
                                        "CurrentShip int," +
                                        "Origin int," +
                                        "Destination int," +
                                        "IsOpenTop int," +
                                        "EmptyWeigth int," +
                                        "Weight int," +
                                        "X int," +
                                        "Y int," +
                                        "Z int" +
                                    ")";
                    command.ExecuteNonQuery();

                    //generate Container
                    Container container ;
                    for (int i = 1; i <= count; i++)
                    {
                        int shipId= _randomSource.Next(1, _containerShips.Count);
                        ContainerShip currentShip = ContainerShips.Where(
                            x => x.Id == shipId
                            ).First();

                        container = new Container()
                        {
                            Id = i,
                            Reference = Guid.NewGuid().ToString().Substring(0, 8),
                            Content = GetRandomContent(),
                            CurrentShip=currentShip,
                            Origin = currentShip.Origin,
                            Destination = currentShip.Destination,
                            IsOpenTop = _randomSource.NextDouble()>0.5 ,
                            EmptyWeigth = _randomSource.Next(10,30),
                            X = _randomSource.Next(0, currentShip.MaxWidth),
                            Y = _randomSource.Next(0, currentShip.MaxHeight),
                            Z = _randomSource.Next(0, currentShip.MaxLength),
                        };
                        container.Weight = (int)(container.EmptyWeigth + _randomSource.NextDouble() * 100);

                        //TODO : Be consistent and store the container at real place on ships (not randomly)

                        Containers.Add(container);

                        //insert to DB
                        command.CommandText = "insert into " +
                            "Container(Reference,Content,CurrentShip,Origin,Destination,IsOpenTop,EmptyWeigth,Weight,X,Y,Z) values(" +
                            $"'{container.Reference.Substring(0,8)}', " +
                            $"'{container.Content}', " +
                            $"{container.CurrentShip.Id}, " +
                            $"{container.Origin.Id}, " +
                            $"{container.Destination.Id}, " +
                            $"{(container.IsOpenTop? 1 : 0 )}," +
                            $"{container.EmptyWeigth}," +
                            $"{container.Weight}, " +
                            $"{container.X}, " +
                            $"{container.Y}, " +
                            $"{container.Z} " +
                            ")";
                        command.ExecuteNonQuery();
                    }
                }
                transaction.Commit();
            }
        }
        public string GetRandomName()
        {
            return GenerateName(_randomSource.Next(5,10));
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
        public static string GetRandomContent()
        {
            Random r = new Random();
            switch (r.Next(0,12))
            {
                case 0: return "Wheat";
                case 1: return "Playstation 4";
                case 2: return "Vegetable";
                case 3: return "Toys";
                case 4: return "Metal plates";
                case 5: return "Wood planks";
                case 6: return "Electronic components";
                case 8: return "Car components";
                case 9: return "Cloths";
                case 10: return "Books";
                case 11: return "Scooters";
                case 12: return "Ponies"; //<-srsly ?
                default: return "somethings";
            }
        }
    }
}
