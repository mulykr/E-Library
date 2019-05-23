using LiBook.Services.DTO;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace LiBook.Services.Interfaces
{
    public interface ICommentService : IDisposable
    {
        CommentDto Get(string id);
        IEnumerable<CommentDto> GetByBook(BookDto book);
        IEnumerable<CommentDto> GetByUser(ClaimsPrincipal user);
        void AddComment(CommentDto commentDto);
        void DeleteComment(CommentDto commentDto);
        void Like(string commentId, ClaimsPrincipal user);
    }
}
