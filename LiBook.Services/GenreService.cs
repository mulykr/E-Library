using AutoMapper;
using LiBook.Data.Entities;
using LiBook.Data.Interfaces;
using LiBook.Services.DTO;
using LiBook.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiBook.Services
{
    public class GenreService : IGenreService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Genre> _repository;
        private readonly IAppConfiguration _appConfiguration;

        public GenreService(IRepository<Genre> repository,
            IMapper mapper,
            IAppConfiguration appConfiguration)
        {
            _repository = repository;
            _mapper = mapper;
            _appConfiguration = appConfiguration;
        }

        public void AddToGenre(GenreDTO genreDto)
        {
            var genre = _mapper.Map<GenreDTO, Genre>(genreDto);
            _repository.Create(genre);
            _repository.Save();
        }

        public void DeleteFromWGenre(string id)
        {
            var book = Get(id);
            _repository.Delete(id);
            _repository.Save();
        }

        public GenreDTO Get(string id)
        {
            return _mapper.Map<Genre, GenreDTO>(_repository.Get(id));
        }

        public IEnumerable<GenreDTO> GetList()
        {
            return _repository.GetList().Select(item => _mapper.Map<Genre, GenreDTO>(item));
        }


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~GenreService() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
