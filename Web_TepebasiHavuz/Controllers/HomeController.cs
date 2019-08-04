using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web_TepebasiHavuz.Models;

namespace Web_TepebasiHavuz.Controllers
{
    public class HomeController : Controller
    {


        private IRepository repository;
        public static Users activeUser;
        public static string user_TC;


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
            if (ModelState.IsValid)
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
                        user_TC = user.TC;
                       activeUser = repository.findUser(user.TC);
                        return View("StudentRegistration", activeUser);
                    }
                }
                else
                {
                    return View();
                }
            }

               

                else return View();

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
            activeUser = repository.findUser(user_TC);
            return View(repository.PoolData.Where(p => p.AgeInfo == activeUser.Age ));
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
        public ViewResult StudentReservationSuccess() => View();

        [HttpPost]
        public IActionResult AddPool(PoolDB p)
        {


            repository.AddPool(p);
            return RedirectToAction(nameof(AdminPoolList), p);
        }


        public ViewResult AdminReservationList()
        {
            return View(repository.ReservationData);
        }

        public IActionResult StudentReservationDetail(int key)
        {
            ViewData["UserInfo"] = activeUser.Name + " " +activeUser.Surname;
            return View(repository.GetPool(key));
        }

        [HttpPost]
        public IActionResult StudentReservationDetail(PoolDB pool)
        {
            Reservation r1 = new Reservation();
            pool.BookingStatus = "Dolu";
            r1.UserID = activeUser.UserID;
            r1.PoolID = pool.PoolID;

            repository.UpdatePool(pool);
            repository.AddReservation(r1);
            //return RedirectToAction(nameof(AdminView));
            return RedirectToAction(nameof(StudentReservationSuccess));
        }


        [HttpPost]
        public IActionResult DeleteReservation(Reservation reservation)
        {
            PoolDB p1 = repository.GetPool(reservation.PoolID);
            p1.BookingStatus = "";
            repository.UpdatePool(p1);
            repository.DeleteReservation(reservation);
            //return RedirectToAction(nameof(AdminView));
            return RedirectToAction(nameof(AdminReservationList));
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
