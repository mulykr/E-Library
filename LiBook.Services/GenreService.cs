using System;
using AutoMapper;
using LiBook.Data.Entities;
using LiBook.Data.Interfaces;
using LiBook.Services.DTO;
using LiBook.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace LiBook.Services
{
    public class GenreService : IGenreService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Genre> _repository;

        public GenreService(IRepository<Genre> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Create(GenreDTO genreDto)
        {
            var genre = _mapper.Map<GenreDTO, Genre>(genreDto);
            _repository.Create(genre);
            _repository.Save();
        }

        public void Delete(string id)
        {
            _repository.Delete(id);
            _repository.Save();
        }

        public GenreDTO Get(string id)
        {
            return _mapper.Map<Genre, GenreDTO>(_repository.Get(id));
        }

        public void Update(GenreDTO item)
        {
            var genre = _mapper.Map<GenreDTO, Genre>(item);
            _repository.Update(genre);
            _repository.Save();
        }

        public IEnumerable<GenreDTO> GetList()
        {
            return _repository.GetList().Select(item => _mapper.Map<Genre, GenreDTO>(item));
        }


        #region IDisposable Support
        private bool _disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _repository.Dispose();
                }

                _disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
