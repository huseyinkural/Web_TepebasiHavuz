using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Web_TepebasiHavuz.Models
{
    public class DataRepository : IRepository
    {
        private DataContext context;

        public DataRepository(DataContext ctx)
        {
            context = ctx;

        }



        public IEnumerable<Users> UserData => context.Users.ToArray();
        public IEnumerable<PoolDB> PoolData => context.PoolDB.ToArray();
        public IEnumerable<Reservation> ReservationData => context.Reservation.Include(r => r.User).Include(r=>r.Pool).ToList();


        public Users GetUser(int key) => context.Users.Find(key);

        public Users findUser(string key) => context.Users.Where(u => u.TC == key).FirstOrDefault();

        public PoolDB GetPool(int key) => context.PoolDB.Find(key);

        public bool CheckUser(Users user)
        {
       
            
            var usr = this.context.Users.Where(u => u.TC == user.TC && u.Pass == user.Pass).FirstOrDefault();
            if (usr != null)
            {
                return true;
            }
            return false;
        }

        public void UpdateUser(Users user)
        {
            //Users u = GetUser(user.UserID);
            Users u = this.context.Users.Where(a => a.TC == user.TC).FirstOrDefault();
            u.Name = user.Name;
            u.Surname = user.Surname;
            u.Surname = user.Surname;
            u.Pass = user.Pass;
            u.Age = user.Age;
            u.Sex = user.Sex;
            u.Degree = user.Degree;
            context.SaveChanges();
        }

        public void AddUser(Users user)
        {
            this.context.Users.Add(user);
            this.context.SaveChanges();
        }

        public void UpdatePool(PoolDB pool)
        {
            PoolDB p = GetPool(pool.PoolID);
            p.PoolName = pool.PoolName;
            p.KulvarNo = pool.KulvarNo;
            p.AgeInfo = pool.AgeInfo;
            p.BookingStatus = pool.BookingStatus;
            p.DayInfo = pool.DayInfo;
            p.Degree = pool.Degree;
            p.TimePeriod = pool.TimePeriod;
            context.SaveChanges();
        }

        public void AddPool(PoolDB pool)
        {
            this.context.PoolDB.Add(pool);
            this.context.SaveChanges();
        }

        
        public void AddReservation(Reservation reservation)
        {
            this.context.Reservation.Add(reservation);
            this.context.SaveChanges();
        }

        public void DeleteReservation(Reservation reservation)
        {
            this.context.Reservation.Remove(reservation);
            this.context.SaveChanges();
        }

    }
}
