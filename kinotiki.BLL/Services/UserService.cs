using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.SqlClient;
using kinotiki.BLL.Abstract;
using kinotiki.BLL.Entity;
using AutoMapper;

namespace kinotiki.Domain.Concrete
{
    public class UserService : IUserService
    {
        private kinotikiDbContext context = new kinotikiDbContext();
        private IMapper mapper { get; set; }

        public UserService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public List<User> GetUsers()
        {
            var users = context.Users.AsQueryable().ToList();
            var userModels = mapper.Map<List<User>>(users);
            return userModels;
        }
        
        public User Find(string login)
        {
            var user = context.Users.FirstOrDefault(u => u.login == login);
            var userModel = mapper.Map<User>(user);
            return userModel;
        }

        public User Find(string login, string password)
        {
            var user = context.Users.FirstOrDefault(u => u.login == login && u.password == password);
            var userModel = mapper.Map<User>(user);
            return userModel;
        }

        public User FindByMail(string email)
        {
            var user = context.Users.FirstOrDefault(u => u.email == email);
            var userModel = mapper.Map<User>(user);
            return userModel;
        }

        public User FindByVerificationKey(string key)
        {
            var user = context.Users.FirstOrDefault(u => !u.isEmailVerificated && u.verificationKey == key);
            var userModel = mapper.Map<User>(user);
            return userModel;
        }

        public void Create(User userModel)
        {
            var user = mapper.Map<Domain.Entity.User>(userModel);
            context.Users.Add(user);
            context.SaveChanges();
        }

        public bool Remove(string login)
        {
            var user = context.Users.FirstOrDefault(u => u.login == login);
            context.Users.Remove(user);
            context.SaveChanges();
            if (Find(login) == null)
                return true;
            else
                return false;
        }

        public bool RemoveByMail(string email)
        {
            var user = context.Users.FirstOrDefault(u => u.email == email);
            context.Users.Remove(user);
            context.SaveChanges();
            if (FindByMail(email) == null)
                return true;
            else
                return false;
        }
        
        public bool Update(User userModel)
        {
            var userNew = mapper.Map<Entity.User>(userModel);
            var entity = context.Users.Find(userModel.id);
            if(entity != null)
            {
                context.Entry(entity).CurrentValues.SetValues(userNew);
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
