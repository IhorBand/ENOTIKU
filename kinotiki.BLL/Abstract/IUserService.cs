using kinotiki.BLL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kinotiki.BLL.Abstract
{
    public interface IUserService
    {
        //TODO:
        List<User> GetUsers();

        void Create(User user);

        bool Remove(string login);
        bool RemoveByMail(string email);

        bool Update(User user);

        User Find(string login);
        User Find(string login, string password);
        User FindByMail(string email);
    }
}
