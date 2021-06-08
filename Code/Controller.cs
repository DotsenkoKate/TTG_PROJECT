using System;
using System.Collections.Generic;
using System.Text;
using Connection;
using Repository;
using Model_staticDB;
using Npgsql;

namespace MainController
{
    public class Controller
    {
        public IConnection<NpgsqlConnection> Connect { get; set; }
        public Controller(String connect)
        {
            Connect = new ConnectionPSQL(connect);
        }
        public void AddNewOwner(Owner owner)
        {
            IOwnerRepository<Owner, int> Repository = new OwnerDBRepository(Connect);

            Repository.AddNew(owner);
        }
        public void DeleteOwner(int id)
        {
            IOwnerRepository<Owner, int> Repository = new OwnerDBRepository(Connect);

            Repository.Delete(id);
        }
        public Owner GetOwnerById(int id)
        {
            IOwnerRepository<Owner, int> Repository = new OwnerDBRepository(Connect);

            return Repository.GetOne(id);
        }
        public void UpdateOwner(Owner owner)
        {
            IOwnerRepository<Owner, int> Repository = new OwnerDBRepository(Connect);

            Repository.Edit(owner);
        }
        public void AddNewDriver(Driver driver)
        {
            IDriverRepository<Driver, int> Repository = new DriverDBRepository(Connect);

            Repository.AddNew(driver);
        }
        public void UpdateDriver(Driver driver)
        {
            IDriverRepository<Driver, int> Repository = new DriverDBRepository(Connect);

            Repository.Edit(driver);
        }
        public List<Driver> GetAllDriversOnWay(int way_number)
        {
            IDriverRepository<Driver, int> Repository = new DriverDBRepository(Connect);

            return Repository.GetList(way_number);
        }
        public Driver GetDriverById(int id)
        {
            IDriverRepository<Driver, int> Repository = new DriverDBRepository(Connect);

            return Repository.GetOne(id);
        }
        public void DeleteDriver(int id)
        {
            IDriverRepository<Driver, int> Repository = new DriverDBRepository(Connect);

            Repository.Delete(id);
        }
        public void AddNewWay(Way way)
        {
            IWayRepository<Way, int> repository = new WayDBRepository(Connect);

            repository.AddNew(way);
        }
        public Way GetWayByWayNumber(int way_number)
        {
            IWayRepository<Way, int> repository = new WayDBRepository(Connect);

            return repository.GetOne(way_number);
        }
        public void DeleteWay(int way_number)
        {
            IWayRepository<Way, int> repository = new WayDBRepository(Connect);

            repository.Delete(way_number);
        }
        public void UpdateWay(Way way)
        {
            IWayRepository<Way, int> repository = new WayDBRepository(Connect);

            repository.Edit(way);
        }
        public List<Way> GetAllWaysByOwner(int owner_id)
        {
            IWayRepository<Way, int> repository = new WayDBRepository(Connect);

            return repository.GetList(owner_id);
        }
        public void AddNewWaystation(Waystation waystation)
        {
            IWaystationRepository<Waystation, int> repository = new WaystationDBRepository(Connect);

            repository.AddNew(waystation);
        }
        public void UpdateWaystation(Waystation waystation)
        {
            IWaystationRepository<Waystation, int> repository = new WaystationDBRepository(Connect);

            repository.Edit(waystation);
        }
        public Waystation GetWaystationById(int id)
        {
            IWaystationRepository<Waystation, int> repository = new WaystationDBRepository(Connect);

            return repository.GetOne(id);
        }
        public List<Waystation> GetAllWaystationsByWaynumber(int way_number)
        {
            IWaystationRepository<Waystation, int> repository = new WaystationDBRepository(Connect);

            return repository.GetList(way_number);
        }
        public List<Waystation> GetWaystations()
        {
            IWaystationRepository<Waystation, int> repository = new WaystationDBRepository(Connect);

            return repository.GetAll();
        }
        public void AddNewWaystationOnWay(Waystation waystation, int way_number)
        {
            IWaystationRepository<Waystation, int> repository = new WaystationDBRepository(Connect);

            repository.AddNewOnWay(waystation, way_number);
        }
        public void AddWaystationOnWay(Waystation waystation, int way_number)
        {
            IWaystationRepository<Waystation, int> repository = new WaystationDBRepository(Connect);

            repository.AddOnWay(waystation, way_number);
        }
        public void AddNewAuto(Auto auto)
        {
            IAutoRepository<Auto, string> repository = new AutoDBRepository(Connect);

            repository.AddNew(auto);
        }
        public void UpdateAuto(Auto auto)
        {
            IAutoRepository<Auto, string> repository = new AutoDBRepository(Connect);

            repository.Edit(auto);
        }
        public Auto GetAutoById(int id)
        {
            IAutoRepository<Auto, string> repository = new AutoDBRepository(Connect);

            return repository.GetOne(id.ToString());
        }
        public void DeleteAuto(int id)
        {
            IAutoRepository<Auto, string> repository = new AutoDBRepository(Connect);

            repository.Delete(id.ToString());
        }
        public List<Auto> GetAllCarsByWaynumber(int way_number)
        {
            IAutoRepository<Auto, string> repository = new AutoDBRepository(Connect);

            return repository.GetList(way_number.ToString());
        }
        public Auto GetAutoByNumber(string number)
        {
            IAutoRepository<Auto, string> repository = new AutoDBRepository(Connect);

            return repository.GetOneByNumber(number);
        }
        public Auto GetAutoByDriverId(int driver_id)
        {
            IAutoRepository<Auto, string> repository = new AutoDBRepository(Connect);

            return repository.GetOneByDriverId(driver_id.ToString());
        }
        public List<Auto> GetAllCarsWithoutDriverByWaynumber(int way_number)
        {
            IAutoRepository<Auto, string> repository = new AutoDBRepository(Connect);

            return repository.GetListWhereDriverIdIsNull(way_number.ToString());
        }
        public void ChangeStatusAuto(string number, string newStatus)
        {
            IAutoRepository<Auto, string> repository = new AutoDBRepository(Connect);

           repository.EditStatusByNumber(number, newStatus);
        }
        public void DeleteAutoByNumber(string number)
        {
            IAutoRepository<Auto, string> repository = new AutoDBRepository(Connect);

            repository.DeleteByNumber(number);
        }
        public void UpdateAutoDriverIdByNumber(string number, int driver_id)
        {
            IAutoRepository<Auto, string> repository = new AutoDBRepository(Connect);

            repository.EditDriverIdByNumber(number, driver_id);
        }
        public void AddNewWorktime(Worktime worktime)
        {
            IWorktimeRepository<Worktime, int> repository = new WorktimeDBRepository(Connect);

            repository.AddNew(worktime);
        }
        public void DeleteWorktimeById(int id)
        {
            IWorktimeRepository<Worktime, int> repository = new WorktimeDBRepository(Connect);

            repository.Delete(id);
        }
        public void UpdateWorktime(Worktime worktime)
        {
            IWorktimeRepository<Worktime, int> repository = new WorktimeDBRepository(Connect);

            repository.Edit(worktime);
        }
        public Worktime GetWorktimeById(int id)
        {
            IWorktimeRepository<Worktime, int> repository = new WorktimeDBRepository(Connect);

            return repository.GetOne(id);
        }
        public List<Worktime> GetAllWorktimesByDriverId(int driver_id)
        {
            IWorktimeRepository<Worktime, int> repository = new WorktimeDBRepository(Connect);

            return repository.GetList(driver_id);
        }
        public List<Worktime> GetAllWokrtimes()
        {
            IWorktimeRepository<Worktime, int> repository = new WorktimeDBRepository(Connect);

            return repository.GetAll();
        }
        public void AddWorktimeToDriver(int worktime, int driver_id)
        {
            IWorktimeRepository<Worktime, int> repository = new WorktimeDBRepository(Connect);

            repository.AddWorktimeToDriver(worktime, driver_id);
        }


    }
}
