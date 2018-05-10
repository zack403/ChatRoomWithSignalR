using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatroomBusiness.EF;
using System.Data.Entity.Migrations;

namespace ChatroomBusiness.Repositories
{
    public class UserService
    {
        private readonly FRIENDSCHATEntities _db;

        public UserService()
        {
            _db = new FRIENDSCHATEntities();
        }

        public IQueryable<Users> GetAllUsers()
        {
            return _db.Users.AsQueryable();
        }
        public Users GetUsersById(string id)
        {

            var user = _db.Users.Where(x => x.Id == id).SingleOrDefault();
            return user;
        }
        public Users CreateUsers( Users model)
        {
            model.Id = Guid.NewGuid().ToString();
            var user = _db.Users.Add(model);
            _db.SaveChanges();
            return (user);
        }
        public Users UpdateUsers(Users model)
        {
            var user = _db.Users.Find(model.Id);
            if(user == null)
            {
                return null;
            }
            _db.Users.AddOrUpdate(model);
            _db.SaveChanges();
            return model;
        }
        public void DeleteUser(string id)
        {
            var user = _db.Users.Find(id);
            if (user == null)
            {
                return;
            }
            _db.Users.Remove(user);
            _db.SaveChanges();

        }
    }
}
