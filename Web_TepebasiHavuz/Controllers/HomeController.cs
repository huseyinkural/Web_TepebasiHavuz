using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web_TepebasiHavuz.Models;
using Microsoft.AspNetCore.Http;

namespace Web_TepebasiHavuz.Controllers
{
    public class HomeController : Controller
    {


        private IRepository repository;
        private static object Lock = new object();


        public HomeController(IRepository repo)
        {
            repository = repo;

            
        }
        public IActionResult Index()
        {
            return View();
        }

        public ViewResult Login()
        {
            return View();
        }




        [HttpPost]
        public ViewResult Login(Users user)
        {
           
            bool isUser = repository.CheckUser(user);
            if (isUser)
            {
                if (user.TC.Equals("123"))
                {
                    return View("AdminView");
                }
                else
                {
                    
                    
                    HttpContext.Session.SetString("UserTC", user.TC);
                    var u = repository.findUser(user.TC);

                    
                    return View("StudentRegistration", u);
                    
                    
                }
            }
            else
            {
                return View();
            }


        }
       

        public IActionResult StudentRegistration()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult StudentRegistration(Users user)
        {
            repository.UpdateUser(user);
            return RedirectToAction(nameof(StudentReservationList));
            //return View();
        }

        public IActionResult StudentReservationList()
        {

            var u = repository.findUser(HttpContext.Session.GetString("UserTC"));
            int age = DateTime.Now.Year - u.DateOfBirth.Year;
            
           // return View();
            return View(repository.PoolData.Where(p => p.AgeInfo == age && p.Degree == u.Degree ));
        }



        public ViewResult AdminView() => View();
       
       

        public ViewResult UserList()
        {
            return View(repository.UserData);
        }

        public IActionResult UpdateUsers(int key)
        {
          
            return View(repository.GetUser(key));
        }

        [HttpPost]
        public IActionResult UpdateUsers(Users user)
        {
            repository.UpdateUser(user);
            //return RedirectToAction(nameof(AdminView));
            return View();
        }
        public ViewResult AddUser() => View();

        [HttpPost]
        public IActionResult AddUser(Users u)
        {
          
           
            repository.AddUser(u);
            return RedirectToAction(nameof(UserList), u);
        }



        public ViewResult AdminPoolList()
        {
            return View(repository.PoolData);
        }

        public IActionResult UpdatePool(int key)
        {

            return View(repository.GetPool(key));
        }

        [HttpPost]
        public IActionResult UpdatePool(PoolDB pool)
        {
            repository.UpdatePool(pool);
            //return RedirectToAction(nameof(AdminView));
            return RedirectToAction(nameof(AdminPoolList), pool);
        }


        public ViewResult AddPool() => View();

        public ViewResult StudentReservationSuccess(PoolDB p)
        {
           
            if (repository.RezData(p.PoolID).Count() == p.Limit)
            {
                p.BookingStatus = "Dolu";
                repository.UpdatePool(p);
            }

            return View();
        } 
        public ViewResult StudentReservationFailed() => View();

        [HttpPost]
        public IActionResult AddPool(PoolDB p)
        {


            repository.AddPool(p);
            return RedirectToAction(nameof(AdminPoolList), p);
        }


        public ViewResult AdminReservationList()
        {
            return View(repository.PoolData);
        }

        public IActionResult StudentReservationDetail(int key)
        {
            var u = repository.findUser(HttpContext.Session.GetString("UserTC"));
            ViewData["UserInfo"] = u.FullName;
            return View(repository.GetPool(key));
        }

        [HttpPost]
        public IActionResult StudentReservationDetail(PoolDB pool)
        {

            lock (Lock)
            {
                if (repository.RezData(pool.PoolID).Count() != pool.Limit)
                {
                    Reservation r1 = new Reservation();
                    var u = repository.findUser(HttpContext.Session.GetString("UserTC"));
                    r1.UserID = u.UserID;
                    r1.PoolID = pool.PoolID;
                    repository.AddReservation(r1);
                    return RedirectToAction(nameof(StudentReservationSuccess), pool);
                }

                else
                {
                    return RedirectToAction(nameof(StudentReservationFailed));
                }

            }

            

           
           
        }

     


      
        public IActionResult DeleteReservation(int key)
        {
            Reservation r = repository.GetReservation(key);
            var p = repository.GetPool(r.PoolID);
            p.BookingStatus = "";
            repository.UpdatePool(p);
            repository.DeleteReservation(r);
            //return RedirectToAction(nameof(AdminView));
            return RedirectToAction(nameof(AdminReservationList));
        }

        public IActionResult RezervationStuList(int key)
        {
            var p = repository.GetPool(key);
            ViewData["PName"] = p.PoolName + " " + p.DayInfo + " "+ p.TimePeriod + "\r\n" + "Kulvar No: "+p.KulvarNo+ " "+"Kontenjan: "+p.Limit  ;
            ViewData["Doluluk Oranı"] = repository.RezData(key).Count() +" / " + p.Limit;
            return View(repository.RezData(key));
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
