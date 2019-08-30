using DAL.Entities;
using System;
using System.Linq.Expressions;

namespace DAL.Repositories.Filters
{
    public class UserFilter
    {
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string HashedPassword { get; set; }
        public string UserEmail { get; set; }
        public DateTime UserBirthDate { get; set; }
        public string UserAvatar { get; set; }
        public int UserRoleId { get; set; }

        public Expression<Func<User, bool>> PropertiesFilter()
        {
            var chainOfConditions = PredicateBuilder.True<User>();

            if (!string.IsNullOrEmpty(UserName))
                chainOfConditions = chainOfConditions.And(i => i.Name == UserName);

            if (!string.IsNullOrEmpty(UserSurname))
                chainOfConditions = chainOfConditions.And(i => i.Surname == UserSurname);

            if (!string.IsNullOrEmpty(UserEmail))
                chainOfConditions = chainOfConditions.And(i => i.Email == UserEmail);

            if (!string.IsNullOrEmpty(UserAvatar))
                chainOfConditions = chainOfConditions.And(i => i.Avatar == UserAvatar);

            if (!string.IsNullOrEmpty(HashedPassword))
                chainOfConditions = chainOfConditions.And(i => i.Password == HashedPassword);

            if (UserRoleId != default)
                chainOfConditions = chainOfConditions.And(i => i.RoleId == UserRoleId);

            if (UserBirthDate != default)
                chainOfConditions = chainOfConditions.And(i => i.BirthDate == UserBirthDate);

            return chainOfConditions;
        }
    }
}
