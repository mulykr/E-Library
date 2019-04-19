﻿using System;
using LiBook.Data.Entities;

namespace LiBook.Services.DTO
{
    public class WishListItemDto
    {
        public string BookId { get; set; }
        public Book Book { get; set; }
        public string UserId { get; set; }
        public UserProfile User { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
