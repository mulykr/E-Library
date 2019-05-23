using AutoMapper;
using LiBook.Data.Entities;
using LiBook.Data.Interfaces;
using LiBook.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using LiBook.Services.Extensions.Identity;
using LiBook.Services.Interfaces;

namespace LiBook.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _repository;
        private readonly IMapper _mapper;

        public CommentService(IRepository<Comment> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        public CommentDto Get(string id)
        {
            return _mapper.Map<Comment, CommentDto>(_repository.Get(id));
        }

        public IEnumerable<CommentDto> GetByBook(BookDto book)
        {
            return _repository
                .Get(i => i.BookId == book.Id)
                .OrderBy(i => i.TimeStamp)
                .Reverse()
                .Select(i=>_mapper.Map<Comment, CommentDto>(i));
        }

        public IEnumerable<CommentDto> GetByUser(ClaimsPrincipal user)
        {
            return _repository.Get(i =>
                    i.UserId == user.GetUserId())
                .OrderBy(i => i.TimeStamp)
                .Reverse()
                .Select(i => _mapper.Map<Comment, CommentDto>(i));
        }

        public void AddComment(CommentDto commentDto)
        {
            var item = _mapper.Map<CommentDto, Comment>(commentDto);
            item.TimeStamp = DateTime.Now;
            _repository.Create(item);
            _repository.Save();
        }

        public void DeleteComment(CommentDto commentDto)
        {
            var item = _repository.Get(commentDto.Id);
            if (item != null)
            {
                _repository.Delete(item.Id);
                _repository.Save();
            }
        }

        public void Like(string commentId, ClaimsPrincipal user)
        {
            var comment = _repository.Get(commentId);
            if (comment != null)
            {
                if (comment.CommentLikes.Any(i => i.UserProfileId == user.GetUserId()))
                {
                    var like = comment.CommentLikes.First(i => i.UserProfileId == user.GetUserId());
                    comment.CommentLikes.Remove(like);
                }
                else
                {
                    comment.CommentLikes.Add(new CommentLike
                    {
                        CommentId = commentId,
                        UserProfileId = user.GetUserId(),
                        Liked = true
                    });
                }

                _repository.Update(comment);
                _repository.Save();
            }
            else
            {
                throw new ArgumentException("Invalid comment Id.");
            }
        }
    }
}
