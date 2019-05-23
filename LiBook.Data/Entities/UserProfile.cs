using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace LiBook.Data.Entities
{
    public class UserProfile : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime RegistredOn { get; set; }

        public ICollection<WishListItem> WishListItems { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<CommentLike> CommentLikes { get; set; }
    }
}
