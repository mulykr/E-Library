using LiBook.Services.DTO;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace LiBook.Services.Interfaces
{
    public interface ICommentService: IDisposable
    {
        CommentDTO Get(string id);
        IEnumerable<CommentDTO> GetByBook(BookDto book);
        void AddComment(CommentDTO CommentDto);
        void DeleteComment(CommentDTO CommentDto);
    }
}
