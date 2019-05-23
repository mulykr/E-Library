using AutoMapper;
using LiBook.Data.Entities;
using LiBook.Services.DTO;

namespace LiBook.Models.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentDto>();
            CreateMap<CommentDto, Comment>();
            CreateMap<CommentDto, CommentViewModel>();
            CreateMap<CommentViewModel, CommentDto>();
            CreateMap<CommentLike, CommentLikeDto>();
            CreateMap<CommentLikeDto, CommentLike>();
            CreateMap<CommentLikeDto, CommentLikeViewModel>();
            CreateMap<CommentLikeViewModel, CommentLikeDto>();
        }
    }
}
