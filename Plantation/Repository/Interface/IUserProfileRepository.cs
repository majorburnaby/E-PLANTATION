using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IUserProfileRepository
    {
        List<UserProfile> GetAll();
        UserProfile Find(int? id);
        UserProfile Add(UserProfile UserProfile);
        UserProfile Update(UserProfile UserProfile);
        void Remove(int id);
    }
}