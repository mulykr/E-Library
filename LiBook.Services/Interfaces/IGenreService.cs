using LiBook.Services.DTO;
using System;
using System.Collections.Generic;

namespace LiBook.Services.Interfaces
{
    public interface IGenreService: IDisposable
    {
        GenreDTO Get(string id);
        IEnumerable<GenreDTO> GetList();
        void Update(GenreDTO item);
        void Create(GenreDTO genreDto);
        void Delete(string id);
    }
}
