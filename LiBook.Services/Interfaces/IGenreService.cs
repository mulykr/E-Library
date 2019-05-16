using LiBook.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiBook.Services.Interfaces
{
    public interface IGenreService: IDisposable
    {
        GenreDTO Get(string id);
        IEnumerable<GenreDTO> GetList();
        //IEnumerable<GenreDTO> GetByBook(BookDto book);
        void Update(GenreDTO item);
        void AddToGenre(GenreDTO genreDto);
        void DeleteFromGenre(string id);
        //bool IsInGenre(BookDto bookDTO);
    }
}
