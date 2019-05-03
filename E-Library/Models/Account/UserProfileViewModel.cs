using System;
using System.Collections.Generic;

namespace LiBook.Models.Account
{
    public class UserProfileViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime RegistredOn { get; set; }

        public ICollection<WishListItemViewModel> WishListItems { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }
    }
}
