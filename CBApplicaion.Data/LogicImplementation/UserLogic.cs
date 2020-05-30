using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Models;
using CBApplication.Data.Implementations;
using CBApplication.Data.Interfaces;

namespace CBApplicaion.Data.LogicImplementation
{
    public class UserLogic
    {
        private readonly IUserRepository db = new UserRepository(new ApplicationDbContext());

        public void Save(User user)
        {
            //Saving data into database
            db.Add(user);
            db.Save(user);
        }

        public IEnumerable<User> GetAll()
        {
            //Getting a list of all customers
            var getAll = db.GetAll();
            return getAll;
        }

        public void Update(User user)
        {
            //Updating customer new info
            db.Save(user);
        }

        public string CreateRandpword()
        {
            //Minimum number of characters allowed
            int allow = 5;

            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();

            
            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[allow];
           
            for (int i = 0; i < allow; i++)
            {
                chars[i] = validChars[(int)((validChars.Length)* random.NextDouble())];
            }
            return  new string(chars);
        }

        
        public bool IsEmailExist(string emailId)
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    var b = db.Users.Where(a => a.Email == emailId).FirstOrDefault();
                    return b != null;
                }
            }

        public bool IsUserNameExist(string username)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var b = db.Users.Where(m => m.Username == username).FirstOrDefault();
                return b != null;
            }
        }

        public bool isRoleCheck(object role)
        {
            if (role == null)
            {
                return false;
            }
            else
            {
                var one = (string)role;

                switch (one)
                {
                    case "Admin":
                        return true;
                    case "Teller":
                        return true;
                    default:
                        return false;
                }
            }
        }

        public bool SendingEmail(string email, string password, string username)
        {
            var fromEmail = new MailAddress("arnoldekechi1998@gmail.com", "Arnold CBA app");
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "terriblelizard";
            string subject = "Your login details !!!";

            string body = "<br/><br/>Account created successfully" +
                "Your Username is " + username + " and password is " + password + "";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {

                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                try
                {
                    smtp.Send(message);
                }
                catch (Exception e)
                {
                    return false;
                }
                
            return true;
        }

        public bool checkDetails(string username, string password)
        {
            var details = db.GetByUsernameAndPassword(username, password);

            if (details == null)
            {
                return false;
            }

            return true;

        }

        public User GetByUsername(string username)
        {
            var get = db.GetbyEmail(username);
            return get;
        }

        public User Get(int? id)
        {
            //Getting a customer by ID
            var get = db.Get(id);
            return get;
        }

        public User UserInDb(int id)
        {
            var get = db.GetIdInDb(id);
            return get;
        }

        public int NumberOfTellers()
        {
            var tellers = db.NumberOfTellers();
            return tellers;
        }

        public int NumberOfAdmins()
        {
            var admin = db.NumberOfAdmins();
            return admin;
        }
    }
}
