using System;
using System.Collections.Generic;
using Model_staticDB;
using Connection;
using Npgsql;
using System.Data.Common;

namespace Repository
{
    /*
     * TEntity - тип модели; 
     * DType - дефолтный тип передаваемого параметра
     */
    public interface IRepository<TEntity, DType>
        where TEntity : class
    {
        void AddNew(TEntity entity);
        void Edit(TEntity entity);
        TEntity GetOne(DType condition);
        List<TEntity> GetList(DType condition);
        void Delete(DType condition);
    }

    public interface IDriverRepository<TEntity, DType> : IRepository<TEntity, DType>
        where TEntity : class
    {

    }

    public interface IWayRepository<TEntity, DType> : IRepository<TEntity, DType>
        where TEntity : class
    {

    }
    public interface IWaystationRepository<TEntity, DType> : IRepository<TEntity, DType>
        where TEntity : class
    {
        public void AddNewOnWay(TEntity entity, DType condition);
        public List<TEntity> GetAll();
        public TEntity GetOneLast();
        public void AddOnWay(TEntity entity, DType condition);
    }
    public interface IAutoRepository<TEntity, DType> : IRepository<TEntity, DType>
        where TEntity : class
    {
        public TEntity GetOneByNumber(DType condition);
        public TEntity GetOneByDriverId(DType condition);
        public List<TEntity> GetListWhereDriverIdIsNull(DType condition);
        public void EditStatusByNumber(DType number, DType newStatus);
        public void DeleteByNumber(DType condition);
        public void EditDriverIdByNumber(DType number, int driver_id);
    }
    public interface IOwnerRepository<TEntity, DType> : IRepository<TEntity, DType>
        where TEntity : class
    {

    }
    public interface IWorktimeRepository<TEntity, DType> : IRepository<TEntity, DType>
        where TEntity : class
    {
        public List<TEntity> GetAll();
        public void AddWorktimeToDriver(DType id, DType driver_id);
    }
    public class DriverDBRepository : IDriverRepository<Driver, int>
    {
        private IConnection<NpgsqlConnection> Connect { get; set; }

        public DriverDBRepository(IConnection<NpgsqlConnection> connect)
        {
            this.Connect = connect;
        }
        public void AddNew(Driver entity)
        {
            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("INSERT INTO driver(name, birthday, passport, place, phonenumber) " +
                "VALUES(\'" + entity.Name + "\', \'" + entity.Birthday + "\', \'" + entity.Passport + "\', \'" + 
                entity.Place + "\', \'" + entity.Phonenumber + "\');",
                    Connect.Db);

            if (npgSqlCommand.ExecuteNonQuery() == 0)
            {
                Console.WriteLine("Bad add driver query!");
            }
            else
            {
                Console.WriteLine("Add new driver");
            }
            Connect.Close();
        }

        public void Delete(int condition)
        {
            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("DELETE FROM driver WHERE id = " + condition.ToString() + ";",
                    Connect.Db);

            if (npgSqlCommand.ExecuteNonQuery() == 0)
            {
                Console.WriteLine("Bad delete driver query!");
            }
            else
            {
                Console.WriteLine("Delete driver");
            }
            Connect.Close();
        }

        public void Edit(Driver entity)
        {
            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("UPDATE driver" +
                " SET name = \'" + entity.Name +   "\', birthday = \'" + entity.Birthday + "\', " +
                "passport = \'" + entity.Passport + "\', place = \'" + entity.Place + "\'" +
                ", phonenumber = \'" + entity.Phonenumber + "\' " +
                "WHERE id = " + entity.Id.ToString() + ";", Connect.Db);

            if (npgSqlCommand.ExecuteNonQuery() == 0)
            {
                Console.WriteLine("Bad edit driver query!");
            }
            else
            {
                Console.WriteLine("Edit driver");
            }
            Connect.Close();
        }

        public List<Driver> GetList(int condition)
        {
            List<Driver> drivers = new List<Driver>();

            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("SELECT driver.id, driver.name, driver.birthday, driver.place, driver.passport, driver.phonenumber " +
                " FROM way JOIN auto ON way.number = auto.way_number JOIN driver ON auto.driver_id = driver.id " +
                "WHERE way.number = \'" + condition.ToString() + "\';",
                    Connect.Db);

            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();

            if (npgSqlDataReader.HasRows)
            {
                foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
                {
                    Driver driver = new Driver();
                    driver.Id = Convert.ToInt32(dbDataRecord["id"].ToString());
                    driver.Name = dbDataRecord["name"].ToString();
                    driver.Birthday = dbDataRecord["birthday"].ToString();
                    driver.Passport = dbDataRecord["passport"].ToString();
                    driver.Phonenumber = dbDataRecord["phonenumber"].ToString();
                    driver.Place = dbDataRecord["place"].ToString();
                    drivers.Add(driver);
                }
            }
            else
                Console.WriteLine("Запрос не вернул строк");

            Connect.Close();
            return drivers;
        }

        public Driver GetOne(int condition)
        {
            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("SELECT * FROM driver WHERE id = \'" + condition.ToString() + "\';",
                    Connect.Db);

            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();
            Driver driver = new Driver();

            if (npgSqlDataReader.HasRows)
            {
                foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
                {
                    driver.Id = Convert.ToInt32(dbDataRecord["id"].ToString());
                    driver.Name = dbDataRecord["name"].ToString();
                    driver.Birthday = dbDataRecord["birthday"].ToString();
                    driver.Passport = dbDataRecord["passport"].ToString();
                    driver.Phonenumber = dbDataRecord["phonenumber"].ToString();
                    driver.Place = dbDataRecord["place"].ToString();
                }
            }
            else
                Console.WriteLine("Запрос не вернул строк");

            Connect.Close();
            return driver;
        }
    }
    public class OwnerDBRepository : IOwnerRepository<Owner, int>
    {
        private IConnection<NpgsqlConnection> Connect { get; set; }

        public OwnerDBRepository(IConnection<NpgsqlConnection> connect)
        {
            this.Connect = connect;
        }

        public void AddNew(Owner entity)
        {
            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("INSERT INTO owner(name) VALUES(\'" + entity.Name + "\');",
                    Connect.Db);

            if(npgSqlCommand.ExecuteNonQuery() == 0)
            {
                Console.WriteLine("Bad add owner query!");
            }
            else
            {
                Console.WriteLine("Add new owner");
            }
            Connect.Close();
        }

        public void Delete(int condition)
        {
            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("DELETE FROM owner WHERE id = " + condition.ToString() + ";",
                    Connect.Db);

            if (npgSqlCommand.ExecuteNonQuery() == 0)
            {
                Console.WriteLine("Bad delete owner query!");
            }
            else
            {
                Console.WriteLine("Delete owner");
            }
            Connect.Close();
        }

        public void Edit(Owner entity)
        {
            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("UPDATE owner" +
                " SET name = \'" + entity.Name + "\' " +
                "WHERE id = " + entity.Id.ToString() + ";", Connect.Db);

            if (npgSqlCommand.ExecuteNonQuery() == 0)
            {
                Console.WriteLine("Bad edit owner query!");
            }
            else
            {
                Console.WriteLine("Edit owner");
            }
            Connect.Close();
        }

        //не реализована
        public List<Owner> GetList(int condition)
        {
            //реализация не имеет смысла
            throw new NotImplementedException();
        }

        public Owner GetOne(int condition)
        {
            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("SELECT * FROM owner WHERE id = \'" + condition.ToString() + "\';",
                    Connect.Db);

            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();
            Owner owner = new Owner();
            
            if (npgSqlDataReader.HasRows)
            {
                foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
                {
                    owner.Id = Convert.ToInt32(dbDataRecord["id"].ToString());
                    owner.Name = dbDataRecord["name"].ToString();
                }
            }
            else
                Console.WriteLine("Запрос не вернул строк");

            Connect.Close();
            return owner;
        }
    }
    public class WayDBRepository : IWayRepository<Way, int>
    {
        private IConnection<NpgsqlConnection> Connect { get; set; }

        public WayDBRepository(IConnection<NpgsqlConnection> connect)
        {
            this.Connect = connect;
        }
        public void AddNew(Way entity)
        {
            Way way = new Way();
            way = GetOne(entity.Number);

            if (way.OwnerId != 0)
            {
                Connect.Open();
                NpgsqlCommand npgSqlCommand = new NpgsqlCommand("INSERT INTO way(number, price, owner_id) VALUES(\'" + entity.Number.ToString() + "\'" +
                    ", \'" + entity.Price.ToString() + "\', \'" + entity.OwnerId.ToString() + "\');",
                        Connect.Db);

                if (npgSqlCommand.ExecuteNonQuery() == 0)
                {
                    Console.WriteLine("Bad add way query!");
                }
                else
                {
                    Console.WriteLine("Add new way");
                }
                Connect.Close();
            }
            else 
            {
                Console.WriteLine("Не удалось добавить маршрут! Маршрут с таким номером уже существует!");
            }
        }

        public void Delete(int condition)
        {
            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("DELETE FROM way WHERE number = " + condition.ToString() + ";",
                    Connect.Db);

            if (npgSqlCommand.ExecuteNonQuery() == 0)
            {
                Console.WriteLine("Bad delete way query!");
            }
            else
            {
                Console.WriteLine("Delete way");
            }
            Connect.Close();
        }

        public void Edit(Way entity)
        {
            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("UPDATE way" +
                " SET number = \'" + entity.Number.ToString() + "\', price = \'" + entity.Price.ToString() + "\', owner_id = \'" + entity.OwnerId.ToString() + "\' " +
                "WHERE number = " + entity.Number.ToString() + ";", Connect.Db);

            if (npgSqlCommand.ExecuteNonQuery() == 0)
            {
                Console.WriteLine("Bad edit owner query!");
            }
            else
            {
                Console.WriteLine("Edit owner");
            }
            Connect.Close();
        }

        public List<Way> GetList(int condition)
        {
            List<Way> ways = new List<Way>();

            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("SELECT way.number, way.price, way.owner_id FROM owner" +
                "JOIN way ON owner.id = way.owner_id WHERE owner.id = " + condition.ToString() + ";",
                    Connect.Db);

            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();

            if (npgSqlDataReader.HasRows)
            {
                foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
                {
                    Way way = new Way();
                    way.Number = Convert.ToInt32(dbDataRecord["number"].ToString());
                    way.Price = Convert.ToInt32(dbDataRecord["price"].ToString());
                    way.OwnerId = Convert.ToInt32(dbDataRecord["owner_id"].ToString());
                    ways.Add(way);
                }
            }
            else
                Console.WriteLine("Запрос не вернул строк");

            Connect.Close();
            return ways;
        }

        public Way GetOne(int condition)
        {
            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("SELECT * FROM way WHERE number = \'" + condition.ToString() + "\';",
                    Connect.Db);

            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();
            Way way = new Way();

            if (npgSqlDataReader.HasRows)
            {
                foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
                {
                    way.Number = Convert.ToInt32(dbDataRecord["number"].ToString());
                    way.Price = Convert.ToInt32(dbDataRecord["price"].ToString());
                    way.OwnerId = Convert.ToInt32(dbDataRecord["owner_id"].ToString());
                }
            }
            else
                Console.WriteLine("Запрос не вернул строк");

            Connect.Close();
            return way;
        }

    }
    public class WaystationDBRepository : IWaystationRepository<Waystation, int>
    {
        private IConnection<NpgsqlConnection> Connect { get; set; }

        public WaystationDBRepository(IConnection<NpgsqlConnection> connect)
        {
            this.Connect = connect;
        }
        public void AddNew(Waystation entity)
        {
            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("INSERT INTO waystation(name, place) VALUES(\'" + entity.Name + "\', \'" + entity.Place + "\');",
                    Connect.Db);

            if (npgSqlCommand.ExecuteNonQuery() == 0)
            {
                Console.WriteLine("Bad add waystation query!");
            }
            else
            {
                Console.WriteLine("Add new waystation");
            }
            Connect.Close();
        }

        public void Delete(int condition)
        {
            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("DELETE FROM waystation WHERE id = " + condition.ToString() + ";",
                    Connect.Db);

            if (npgSqlCommand.ExecuteNonQuery() == 0)
            {
                Console.WriteLine("Bad delete waystation query!");
            }
            else
            {
                Console.WriteLine("Delete waystation");
            }
            Connect.Close();
        }

        public void Edit(Waystation entity)
        {
            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("UPDATE waystation" +
                " SET name = \'" + entity.Name + "\', place = \'" + entity.Place + "\' " +
                "WHERE id = " + entity.Id.ToString() + ";", Connect.Db);

            if (npgSqlCommand.ExecuteNonQuery() == 0)
            {
                Console.WriteLine("Bad edit waystation query!");
            }
            else
            {
                Console.WriteLine("Edit waystation");
            }
            Connect.Close();
        }

        public List<Waystation> GetList(int condition)
        {
            List<Waystation> waystations = new List<Waystation>();

            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("SELECT waystation.id, waystation.name, waystation.place FROM way" +
                "JOIN way_waystation ON way.number = way_waystation.way_number JOIN waystation ON way_waystation.waystation_id = waystation.id WHERE way.number = " + condition.ToString() + ";",
                    Connect.Db);

            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();

            if (npgSqlDataReader.HasRows)
            {
                foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
                {
                    Waystation waystation = new Waystation();
                    waystation.Id = Convert.ToInt32(dbDataRecord["id"].ToString());
                    waystation.Name = dbDataRecord["name"].ToString();
                    waystation.Place = dbDataRecord["place"].ToString();
                    waystations.Add(waystation);
                }
            }
            else
                Console.WriteLine("Запрос не вернул строк");

            Connect.Close();
            return waystations;
        }

        public Waystation GetOne(int condition)
        {
            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("SELECT * FROM waystation WHERE id = \'" + condition.ToString() + "\';",
                    Connect.Db);

            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();
            Waystation waystation = new Waystation();

            if (npgSqlDataReader.HasRows)
            {
                foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
                {
                    waystation.Id = Convert.ToInt32(dbDataRecord["id"].ToString());
                    waystation.Name = dbDataRecord["name"].ToString();
                    waystation.Place = dbDataRecord["place"].ToString();
                }
            }
            else
                Console.WriteLine("Запрос не вернул строк");

            Connect.Close();
            return waystation;
        }

        public void AddNewOnWay(Waystation entity, int condition)
        {
            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("INSERT INTO waystation(name, place) VALUES(\'" + entity.Name + "\', \'" + entity.Place + "\');",
                    Connect.Db);

            if (npgSqlCommand.ExecuteNonQuery() == 0)
            {
                Console.WriteLine("Bad add waystation query!");
            }
            else
            {
                Console.WriteLine("Add new waystation");
            }
            Connect.Close();

            Waystation waystation = new Waystation();
            waystation = GetOneLast();

            Connect.Open();

            NpgsqlCommand npgSqlCommand2 = new NpgsqlCommand("INSERT INTO way_waystation(way_number, waystation_id) VALUES(\'" + condition.ToString() + "\', \'" + waystation.Id.ToString() + "\');",
                    Connect.Db);

            if (npgSqlCommand2.ExecuteNonQuery() == 0)
            {
                Console.WriteLine("Bad add way_waystation query!");
            }
            else
            {
                Console.WriteLine("Add new way_waystation");
            }

            Connect.Close();
        }

        public List<Waystation> GetAll()
        {
            List<Waystation> waystations = new List<Waystation>();

            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("SELECT * FROM waystation;", Connect.Db);

            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();

            if (npgSqlDataReader.HasRows)
            {
                foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
                {
                    Waystation waystation = new Waystation();
                    waystation.Id = Convert.ToInt32(dbDataRecord["id"].ToString());
                    waystation.Name = dbDataRecord["name"].ToString();
                    waystation.Place = dbDataRecord["place"].ToString();
                    waystations.Add(waystation);
                }
            }
            else
                Console.WriteLine("Запрос не вернул строк");

            Connect.Close();
            return waystations;
        }

        //функция должна работать внутри класса, контроллер не должен использовать ее
        public Waystation GetOneLast()
        {
            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("SELECT * FROM waystation ORDER BY id DESC LIMIT 1;",
                    Connect.Db);

            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();
            Waystation waystation = new Waystation();

            if (npgSqlDataReader.HasRows)
            {
                foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
                {
                    waystation.Id = Convert.ToInt32(dbDataRecord["id"].ToString());
                    waystation.Name = dbDataRecord["name"].ToString();
                    waystation.Place = dbDataRecord["place"].ToString();
                }
            }
            else
                Console.WriteLine("Запрос не вернул строк");
            Connect.Close();
            return waystation;
        }

        public void AddOnWay(Waystation entity, int condition)
        {
            Connect.Open();

            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("INSERT INTO way_waystation(way_number, waystation_id) VALUES(\'" + condition.ToString() + "\', \'" + entity.Id.ToString() + "\');",
                    Connect.Db);

            if (npgSqlCommand.ExecuteNonQuery() == 0)
            {
                Console.WriteLine("Bad add way_waystation query!");
            }
            else
            {
                Console.WriteLine("Add new way_waystation");
            }

            Connect.Close();
        }
    }
    public class AutoDBRepository : IAutoRepository<Auto, String>
    {
        private IConnection<NpgsqlConnection> Connect { get; set; }

        public AutoDBRepository(IConnection<NpgsqlConnection> connect)
        {
            this.Connect = connect;
        }

        public void AddNew(Auto entity)
        {
            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("INSERT INTO auto(number, brand, model, status, capacity, release_date, driver_id, way_number) " +
                "VALUES(\'" + entity.Number + "\', \'" + entity.Brand + "\', \'" + entity.Model + "\', \'" +
                entity.Status + "\', " + entity.Capacity.ToString() + ", \'" + entity.ReleaseDate + "\', NULL, " + entity.WayNumber.ToString() + ");",
                    Connect.Db);

            if (npgSqlCommand.ExecuteNonQuery() == 0)
            {
                Console.WriteLine("Bad add auto query!");
            }
            else
            {
                Console.WriteLine("Add new auto");
            }
            Connect.Close();
        }

        public void Delete(string condition)
        {
            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("DELETE FROM auto WHERE id = " + condition + ";",
                    Connect.Db);

            if (npgSqlCommand.ExecuteNonQuery() == 0)
            {
                Console.WriteLine("Bad delete auto query!");
            }
            else
            {
                Console.WriteLine("Delete auto");
            }
            Connect.Close();
        }

        public void Edit(Auto entity)
        {
            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("UPDATE auto" +
                " SET number = \'" + entity.Number + "\', brand = \'" + entity.Brand + "\', " +
                "model = \'" + entity.Model + "\', status = \'" + entity.Status + "\'" +
                ", capacity = " + entity.Capacity.ToString() + ", release_date = \'" + entity.ReleaseDate + "\', driver_id = NULL, way_number = " + entity.WayNumber.ToString() + " " +
                "WHERE id = " + entity.Id.ToString() + ";", Connect.Db);

            if (npgSqlCommand.ExecuteNonQuery() == 0)
            {
                Console.WriteLine("Bad edit auto query!");
            }
            else
            {
                Console.WriteLine("Edit auto");
            }
            Connect.Close();
        }

        public List<Auto> GetList(string condition)
        {
            List<Auto> cars = new List<Auto>();

            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("SELECT * FROM auto WHERE way_number = " + condition + ";",
                    Connect.Db);

            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();

            if (npgSqlDataReader.HasRows)
            {
                foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
                {
                    Auto auto = new Auto();
                    auto.Id = Convert.ToInt32(dbDataRecord["id"].ToString());
                    auto.Number = dbDataRecord["number"].ToString();
                    auto.Brand = dbDataRecord["brand"].ToString();
                    auto.Model = dbDataRecord["model"].ToString();
                    auto.Status = dbDataRecord["status"].ToString();
                    auto.Capacity = Convert.ToInt32(dbDataRecord["capacity"].ToString());
                    auto.ReleaseDate = dbDataRecord["release_date"].ToString();
                    if (dbDataRecord["driver_id"].ToString() == "")
                    {
                        auto.DriverId = 0;
                    }
                    else
                    {
                        auto.DriverId = Convert.ToInt32(dbDataRecord["driver_id"].ToString());
                    }
                   
                    auto.WayNumber = Convert.ToInt32(dbDataRecord["way_number"].ToString());
                    cars.Add(auto);
                }
            }
            else
                Console.WriteLine("Запрос не вернул строк");

            Connect.Close();
            return cars;
        }

        public Auto GetOne(string condition)
        {
            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("SELECT * FROM auto WHERE id = " + condition + ";",
                    Connect.Db);

            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();
            Auto auto = new Auto();

            if (npgSqlDataReader.HasRows)
            {
                foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
                {                  
                    auto.Id = Convert.ToInt32(dbDataRecord["id"].ToString());
                    auto.Number = dbDataRecord["number"].ToString();
                    auto.Brand = dbDataRecord["brand"].ToString();
                    auto.Model = dbDataRecord["model"].ToString();
                    auto.Status = dbDataRecord["status"].ToString();
                    auto.Capacity = Convert.ToInt32(dbDataRecord["capacity"].ToString());
                    auto.ReleaseDate = dbDataRecord["release_date"].ToString();
                    if (dbDataRecord["driver_id"].ToString() == "") auto.DriverId = 0;
                    else auto.DriverId = Convert.ToInt32(dbDataRecord["driver_id"].ToString());
                    auto.WayNumber = Convert.ToInt32(dbDataRecord["way_number"].ToString());
                }
            }
            else
                Console.WriteLine("Запрос не вернул строк");

            Connect.Close();
            return auto;
        }

        public Auto GetOneByNumber(string condition)
        {
            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("SELECT * FROM auto WHERE number = \'" + condition + "\';",
                    Connect.Db);

            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();
            Auto auto = new Auto();

            if (npgSqlDataReader.HasRows)
            {
                foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
                {
                    auto.Id = Convert.ToInt32(dbDataRecord["id"].ToString());
                    auto.Number = dbDataRecord["number"].ToString();
                    auto.Brand = dbDataRecord["brand"].ToString();
                    auto.Model = dbDataRecord["model"].ToString();
                    auto.Status = dbDataRecord["status"].ToString();
                    auto.Capacity = Convert.ToInt32(dbDataRecord["capacity"].ToString());
                    auto.ReleaseDate = dbDataRecord["release_date"].ToString();
                    if (dbDataRecord["driver_id"].ToString() == "") auto.DriverId = 0;
                    else auto.DriverId = Convert.ToInt32(dbDataRecord["driver_id"].ToString());
                    auto.WayNumber = Convert.ToInt32(dbDataRecord["way_number"].ToString());
                }
            }
            else
                Console.WriteLine("Запрос не вернул строк");

            Connect.Close();
            return auto;
        }

        public Auto GetOneByDriverId(string condition)
        {
            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("SELECT * FROM auto WHERE driver_id = " + condition + ";",
                    Connect.Db);

            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();
            Auto auto = new Auto();

            if (npgSqlDataReader.HasRows)
            {
                foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
                {
                    auto.Id = Convert.ToInt32(dbDataRecord["id"].ToString());
                    auto.Number = dbDataRecord["number"].ToString();
                    auto.Brand = dbDataRecord["brand"].ToString();
                    auto.Model = dbDataRecord["model"].ToString();
                    auto.Status = dbDataRecord["status"].ToString();
                    auto.Capacity = Convert.ToInt32(dbDataRecord["capacity"].ToString());
                    auto.ReleaseDate = dbDataRecord["release_date"].ToString();
                    auto.DriverId = Convert.ToInt32(dbDataRecord["driver_id"].ToString());
                    auto.WayNumber = Convert.ToInt32(dbDataRecord["way_number"].ToString());
                }
            }
            else
                Console.WriteLine("Запрос не вернул строк");

            Connect.Close();
            return auto;
        }

        public List<Auto> GetListWhereDriverIdIsNull(string condition)
        {
            List<Auto> cars = new List<Auto>();

            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("SELECT * FROM auto WHERE way_number = " + condition + " AND driver_id IS NULL;",
                    Connect.Db);

            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();

            if (npgSqlDataReader.HasRows)
            {
                foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
                {
                    Auto auto = new Auto();
                    auto.Id = Convert.ToInt32(dbDataRecord["id"].ToString());
                    auto.Number = dbDataRecord["number"].ToString();
                    auto.Brand = dbDataRecord["brand"].ToString();
                    auto.Model = dbDataRecord["model"].ToString();
                    auto.Status = dbDataRecord["status"].ToString();
                    auto.Capacity = Convert.ToInt32(dbDataRecord["capacity"].ToString());
                    auto.ReleaseDate = dbDataRecord["release_date"].ToString();
                    auto.DriverId = 0;
                    auto.WayNumber = Convert.ToInt32(dbDataRecord["way_number"].ToString());
                    cars.Add(auto);
                }
            }
            else
                Console.WriteLine("Запрос не вернул строк");

            Connect.Close();
            return cars;
        }

        public void EditStatusByNumber(string number, string newStatus)
        {
            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("UPDATE auto" +
                " SET status = \'" + newStatus + "\' " +
                "WHERE number = \'" + number + "\';", Connect.Db);

            if (npgSqlCommand.ExecuteNonQuery() == 0)
            {
                Console.WriteLine("Bad edit auto query!");
            }
            else
            {
                Console.WriteLine("Edit auto");
            }
            Connect.Close();
        }

        public void DeleteByNumber(string condition)
        {
            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("DELETE FROM auto WHERE number = \'" + condition + "\';",
                    Connect.Db);

            if (npgSqlCommand.ExecuteNonQuery() == 0)
            {
                Console.WriteLine("Bad delete auto query!");
            }
            else
            {
                Console.WriteLine("Delete auto");
            }
            Connect.Close();
        }

        public void EditDriverIdByNumber(string number, int driver_id)
        {
            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("UPDATE auto" +
                " SET driver_id = " + driver_id.ToString() + " " +
                "WHERE number = \'" + number + "\';", Connect.Db);

            if (npgSqlCommand.ExecuteNonQuery() == 0)
            {
                Console.WriteLine("Bad edit auto query!");
            }
            else
            {
                Console.WriteLine("Edit auto");
            }
            Connect.Close();
        }
    }
    public class WorktimeDBRepository : IWorktimeRepository<Worktime, int>
    {
        private IConnection<NpgsqlConnection> Connect { get; set; }

        public WorktimeDBRepository(IConnection<NpgsqlConnection> connect)
        {
            this.Connect = connect;
        }

        public void AddNew(Worktime entity)
        {
            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("INSERT INTO worktime(weekday, start_time, finish_time) VALUES(\'" + entity.Weekday + "\', \'" + entity.StartTime + "\', \'" + entity.FinishTime + "\');",
                    Connect.Db);

            if (npgSqlCommand.ExecuteNonQuery() == 0)
            {
                Console.WriteLine("Bad add worktime query!");
            }
            else
            {
                Console.WriteLine("Add new worktime");
            }
            Connect.Close();
        }

        public void Delete(int condition)
        {
            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("DELETE FROM worktime WHERE id = " + condition.ToString() + ";",
                    Connect.Db);

            if (npgSqlCommand.ExecuteNonQuery() == 0)
            {
                Console.WriteLine("Bad delete worktime query!");
            }
            else
            {
                Console.WriteLine("Delete worktime");
            }
            Connect.Close();
        }

        public void Edit(Worktime entity)
        {
            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("UPDATE worktime" +
                " SET weekday = \'" + entity.Weekday + "\', start_time = \'" + entity.StartTime + "\', finish_time = \'" + entity.FinishTime + "\' " +
                "WHERE id = " + entity.Id.ToString() + ";", Connect.Db);

            if (npgSqlCommand.ExecuteNonQuery() == 0)
            {
                Console.WriteLine("Bad edit worktime query!");
            }
            else
            {
                Console.WriteLine("Edit worktime");
            }
            Connect.Close();
        }

        public List<Worktime> GetList(int condition)
        {
            List<Worktime> worktimes = new List<Worktime>();

            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("SELECT worktime.id, worktime.weekday, worktime.start_time, worktime.finish_time FROM driver" +
                " JOIN driver_worktime ON driver.id = driver_worktime.driver_id JOIN worktime ON driver_worktime.worktime_id = worktime.id WHERE driver.id = " + condition.ToString() + ";",
                    Connect.Db);

            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();

            if (npgSqlDataReader.HasRows)
            {
                foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
                {
                    Worktime worktime = new Worktime();
                    worktime.Id = Convert.ToInt32(dbDataRecord["id"].ToString());
                    worktime.Weekday = dbDataRecord["weekday"].ToString();
                    worktime.StartTime = dbDataRecord["start_time"].ToString();
                    worktime.FinishTime = dbDataRecord["finish_time"].ToString();
                    worktimes.Add(worktime);
                }
            }
            else
                Console.WriteLine("Запрос не вернул строк");

            Connect.Close();
            return worktimes;
        }

        public Worktime GetOne(int condition)
        {
            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("SELECT * FROM worktime " +
                "WHERE id = " + condition.ToString() + ";",
                    Connect.Db);

            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();
            Worktime worktime = new Worktime();
            if (npgSqlDataReader.HasRows)
            {
                foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
                {
                    worktime.Id = Convert.ToInt32(dbDataRecord["id"].ToString());
                    worktime.Weekday = dbDataRecord["weekday"].ToString();
                    worktime.StartTime = dbDataRecord["start_time"].ToString();
                    worktime.FinishTime = dbDataRecord["finish_time"].ToString();
                }
            }
            else
                Console.WriteLine("Запрос не вернул строк");

            Connect.Close();
            return worktime;
        }

        public List<Worktime> GetAll()
        {
            List<Worktime> worktimes = new List<Worktime>();

            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("SELECT * FROM worktime;",
                    Connect.Db);

            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();

            if (npgSqlDataReader.HasRows)
            {
                foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
                {
                    Worktime worktime = new Worktime();
                    worktime.Id = Convert.ToInt32(dbDataRecord["id"].ToString());
                    worktime.Weekday = dbDataRecord["weekday"].ToString();
                    worktime.StartTime = dbDataRecord["start_time"].ToString();
                    worktime.FinishTime = dbDataRecord["finish_time"].ToString();
                    worktimes.Add(worktime);
                }
            }
            else
                Console.WriteLine("Запрос не вернул строк");

            Connect.Close();
            return worktimes;
        }

        public void AddWorktimeToDriver(int id, int driver_id)
        {
            Connect.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("INSERT INTO driver_worktime(driver_id, worktime_id) VALUES(" + driver_id.ToString() + ", " + id.ToString() + ");",
                    Connect.Db);

            if (npgSqlCommand.ExecuteNonQuery() == 0)
            {
                Console.WriteLine("Bad add driver_worktime query!");
            }
            else
            {
                Console.WriteLine("Add new driver_worktime");
            }
            Connect.Close();
        }
    }
}
 