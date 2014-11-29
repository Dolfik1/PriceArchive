using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceArchive.Model
{
    public partial class SqlRepository
    {
        public IQueryable<UserRole> UserRoles
        {
            get
            {
                return Db.UserRole;
            }
        }

        public bool CreateUserRole(UserRole instance)
        {
            if (instance.ID == 0)
            {
                Db.UserRole.InsertOnSubmit(instance);
                Db.UserRole.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateUserRole(UserRole instance)
        {
            UserRole cache = Db.UserRole.Where(p => p.ID == instance.ID).FirstOrDefault();
            if (cache != null)
            {
                //TODO : Update fields for UserRole
                Db.UserRole.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveUserRole(int idUserRole)
        {
            UserRole instance = Db.UserRole.Where(p => p.ID == idUserRole).FirstOrDefault();
            if (instance != null)
            {
                Db.UserRole.DeleteOnSubmit(instance);
                Db.UserRole.Context.SubmitChanges();
                return true;
            }

            return false;
        }

    }
}
