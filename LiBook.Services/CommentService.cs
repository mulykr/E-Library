using AutoMapper;
using LiBook.Data.Entities;
using LiBook.Data.Interfaces;
using LiBook.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiBook.Services.Interfaces
{
    public class CommentService:ICommentService
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

        public CommentDTO Get(string id)
        {
            return _mapper.Map<Comment, CommentDTO>(_repository.Get(id));
        }

        public IEnumerable<CommentDTO> GetByBook(BookDto book)
        {
            return _repository
                .Get(i => i.BookId == book.Id)
                .Select(i=>_mapper.Map<Comment, CommentDTO>(i));
        }

        public void AddComment(CommentDTO CommentDto)
        {
            var item = _mapper.Map<CommentDTO, Comment>(CommentDto);
            item.TimaStamp = DateTime.Now;
            _repository.Create(item);
            _repository.Save();
        }

        public void DeleteComment(CommentDTO CommentDto)
        {
            var item = _repository.Get(i => i.BookId == CommentDto.BookId && i.UserId == CommentDto.UserId).First();
            if(item!=null)
            {
                _repository.Delete(item.Id);
                _repository.Save();

            }
        }

    }
}
