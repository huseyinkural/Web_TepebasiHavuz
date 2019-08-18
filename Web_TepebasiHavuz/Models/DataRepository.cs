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
        public IEnumerable<OnKayit> OnKayitData => context.OnKayit.ToArray();

        /*
        public IEnumerable<Users> RezData => context.Users
            .FromSql(
                "select * From Users u Where u.UserID IN (Select r.UserID from Reservation r, PoolDB p Where r.PoolID = p.PoolID And p.PoolID=1)")
            .ToList();
*/
        public Users GetUser(int key) => context.Users.Find(key);

        public Users findUser(string key) => context.Users.Where(u => u.TC == key).FirstOrDefault();

        public PoolDB GetPool(int key) => context.PoolDB.Find(key);

        public OnKayit GetOnKayit(int key) => context.OnKayit.Find(key);

        public Reservation GetReservation(int key) => context.Reservation.Where(u => u.UserID == key).FirstOrDefault();
        public bool CheckUser(Users user)
        {
       
         
            var usr = this.context.Users.Where(u => u.TC == user.TC).FirstOrDefault();
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
            u.FullName = user.FullName;
            u.BloodGroup = user.BloodGroup;
            u.DateOfBirth = user.DateOfBirth;
            u.Degree = user.Degree;
            u.IllnessDetail = user.IllnessDetail;
            u.ParentInfo = user.ParentInfo;
            u.Sex = user.Sex;
            u.User_Address = user.User_Address;
            u.PillDetail = user.PillDetail;
          
            context.SaveChanges();
        }

        public void AddUser(Users user)
        {
            this.context.Users.Add(user);
            this.context.SaveChanges();
        }

        public void AddOnKayit(OnKayit o)
        {
            this.context.OnKayit.Add(o);
            this.context.SaveChanges();
        }

        public void UpdatePool(PoolDB pool)
        {
            PoolDB p = GetPool(pool.PoolID);
            p.PoolName = pool.PoolName;
            p.KulvarNo = pool.KulvarNo;
            p.AgeInfo = pool.AgeInfo;
            p.Limit = pool.Limit;
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
            var p = GetPool(reservation.PoolID);
            p.BookingStatus = "";
            UpdatePool(p);

            this.context.Reservation.Remove(reservation);
            this.context.SaveChanges();
        }
        public void DeleteUser(Users u)
        {
            this.context.Users.Remove(u);
            this.context.SaveChanges();
        }

        public void DeletePool(PoolDB p)
        {
            this.context.PoolDB.Remove(p);
            this.context.SaveChanges();
        }


        public void DeleteOnKayit(OnKayit o)
        {
            this.context.OnKayit.Remove(o);
            this.context.SaveChanges();
        }

        public IEnumerable<Users> RezData(int key)
        {
            string query =
                "select * From Users u Where u.UserID IN (Select r.UserID from Reservation r, PoolDB p Where r.PoolID = p.PoolID And p.PoolID=" +
                key.ToString() + ")";
            IEnumerable<Users> r = this.context.Users
                .FromSql(query)
                .ToList();

            return r;
        }

        public bool haveReservation(Users user)
        {
            var rez = this.context.Reservation.Where(u => u.UserID == user.UserID).FirstOrDefault();

            if (rez != null)
            {
                return true;
            }
            return false;
        }

    }
}
