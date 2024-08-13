using AutoMapper;
using Book_Store_MVC.Models;
using Book_Store_MVC.ViewModels;

namespace Book_Store_MVC.MappingProfile
{
    public class BookMapProfile:Profile
    {

        public BookMapProfile()
        {
            CreateMap<Book, BookViewModel>().ReverseMap(); 
            
        }
    }
}
