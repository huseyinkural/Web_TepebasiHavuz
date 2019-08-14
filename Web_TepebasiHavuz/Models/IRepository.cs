using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_TepebasiHavuz.Models
{
   public interface IRepository
    {
        IEnumerable<Users> UserData { get; }
       // IEnumerable<Users> RezData { get; }

        IEnumerable<Reservation> ReservationData { get; }
        IEnumerable<PoolDB> PoolData { get; }
        Users GetUser(int key);
        Reservation GetReservation(int key);
        PoolDB GetPool(int key);

        void AddUser(Users user);
        void AddReservation(Reservation reservation);
        Users findUser(string key);
        bool CheckUser(Users user);
        bool haveReservation(Users user);
        void UpdateUser(Users user);
        void DeleteUser(Users u);
        void DeletePool(PoolDB p);


        void UpdatePool(PoolDB pool);
        void AddPool(PoolDB pool);
        void DeleteReservation(Reservation reservation);
        IEnumerable<Users> RezData(int key);
    }
}
