using RESTful1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace RESTful1
{
    public class ContactesRepository
    {
        private static Contactes2Entities dataContext = new Contactes2Entities();
        public static List<contacte> GetAllContactes()
        {
            List<contacte> lc = dataContext.contactes.ToList();
            return lc;
        }

        public static contacte GetContacte(int contacteID)
        {
            contacte c = dataContext.contactes.Where(x => x.contacteId == contacteID).SingleOrDefault();
            return c;
        }
        public static telefon GetPhone(int telID)
        {
            telefon t = dataContext.telefons.Where(x => x.telId == telID).SingleOrDefault();
            return t;
        }
        public static email GetEmail(int emailId)
        {
            email e = dataContext.emails.Where(x => x.emailId == emailId).SingleOrDefault();
            return e;
        }
        public static bool contacteHasForeign(int contacteID)
        {
            contacte c = dataContext.contactes.Where(x => x.contacteId == contacteID).SingleOrDefault();

            foreach (email em in dataContext.emails.Where(x => x.contacteId == contacteID))
            {
                return true;
            }

            foreach (telefon t in dataContext.telefons.Where(x => x.contacteId == contacteID))
            {
                return true;
            }

            return false;
        }

        public static List<contacte> SearchContactesByName(string contacteName)
        {
            List<contacte> lc = dataContext.contactes.Where(x => x.nom.Contains(contacteName) || x.cognoms.Contains(contacteName)).ToList();
            return lc;
        }

        public static List<telefon> SearchContactesByPhone(string phone)
        {
            List<telefon> lt = dataContext.telefons.Where(x => x.telefon1.Contains(phone)).ToList();
            return lt;
        }
        public static List<email> SearchContactesByEmail(string email)
        {
            List<email> le = dataContext.emails.Where(x => x.email1.Contains(email)).ToList();
            return le;
        }

        public static contacte InsertContacte(contacte c)
        {
            try
            {
                dataContext.contactes.Add(c);
                dataContext.SaveChanges();
                return GetContacte(c.contacteId);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static contacte UpdateContacte(int id, contacte c)
        {
            try
            {
                contacte c0 = dataContext.contactes.Where(x => x.contacteId == id).SingleOrDefault();
                if (c.nom != null) c0.nom = c.nom;
                if (c.cognoms != null) c0.cognoms = c.cognoms;

                dataContext.SaveChanges();
                return GetContacte(id);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static telefon UpdatePhone(int id, telefon t)
        {
            try
            {
                telefon t0 = dataContext.telefons.Where(x => x.telId == id).SingleOrDefault();
                if (t.tipus!= null) t0.tipus = t.tipus;
                if (t.telefon1 != null) t0.telefon1 = t.telefon1;
                if (t.contacteId != null) t0.contacteId = t.contacteId;

                dataContext.SaveChanges();
                return GetPhone(id);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static email UpdateMail(int id, email e)
        {
            try
            {
                email e0 = dataContext.emails.Where(x => x.emailId == id).SingleOrDefault();
                if (e.tipus!= null) e0.tipus = e.tipus;
                if (e.email1 != null) e0.email1 = e.email1;
                if (e.contacteId != null) e0.contacteId = e.contacteId;

                dataContext.SaveChanges();
                return GetEmail(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static contacte CreateContacte(contacte c)
        {
            try
            {
                dataContext.contactes.Add(c);
                dataContext.SaveChanges();
                return GetContacte(c.contacteId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public static email CreateMail(email e)
        {
            try
            {
                dataContext.emails.Add(e);
                dataContext.SaveChanges();
                return GetEmail(e.emailId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public static telefon CreatePhone(telefon t)
        {
            try
            {
                dataContext.telefons.Add(t);
                dataContext.SaveChanges();
                return GetPhone(t.telId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public static void DeleteContacte(int id)
        {
            contacte c = dataContext.contactes.Where(x => x.contacteId == id).SingleOrDefault();

            dataContext.contactes.Remove(c);
            dataContext.SaveChanges();
        }
        public static void DeleteContacteTot(int id)
        {
            contacte c = dataContext.contactes.Where(x => x.contacteId == id).SingleOrDefault();

            foreach (email em in dataContext.emails.Where(x => x.contacteId == id))
            {
                dataContext.emails.Remove(em);
            }

            foreach (telefon t in dataContext.telefons.Where(x => x.contacteId == id))
            {
                dataContext.telefons.Remove(t);
            }

            dataContext.contactes.Remove(c);
            dataContext.SaveChanges();
        }
        public static void DeleteEmail(int id)
        {
            email e = dataContext.emails.Where(x => x.emailId == id).SingleOrDefault();

            dataContext.emails.Remove(e);
            dataContext.SaveChanges();
        }
        public static void DeletePhone(int id)
        {
            telefon t = dataContext.telefons.Where(x => x.telId == id).SingleOrDefault();

            dataContext.telefons.Remove(t);
            dataContext.SaveChanges();
        }

    }



}