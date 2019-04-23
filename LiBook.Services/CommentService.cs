﻿using AutoMapper;
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
                .OrderBy(i => i.TimaStamp)
                .Reverse()
                .Select(i=>_mapper.Map<Comment, CommentDto>(i));
        }

        public IEnumerable<CommentDto> GetByUser(ClaimsPrincipal user)
        {
            return _repository.Get(i =>
                    i.UserId == user.GetUserId())
                .OrderBy(i => i.TimaStamp)
                .Reverse()
                .Select(i => _mapper.Map<Comment, CommentDto>(i));
        }

        public void AddComment(CommentDto commentDto)
        {
            var item = _mapper.Map<CommentDto, Comment>(commentDto);
            item.TimaStamp = DateTime.Now;
            _repository.Create(item);
            _repository.Save();
        }

        public void DeleteComment(CommentDto commentDto)
        {
            var item = _repository.Get(i => i.BookId == commentDto.BookId && i.UserId == commentDto.UserId).First();
            if(item!=null)
            {
                _repository.Delete(item.Id);
                _repository.Save();

            }
        }

    }
}
