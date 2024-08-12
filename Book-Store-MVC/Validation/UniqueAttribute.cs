using Book_Store_MVC.Models;
using System.ComponentModel.DataAnnotations;

namespace Book_Store_MVC.Validation
{
    public class UniqueAttribute:ValidationAttribute
    {
        private readonly BookStoreContext bookStore;

        public UniqueAttribute( BookStoreContext bookStore )
        {
            this.bookStore = bookStore;
        }
        public UniqueAttribute()
        {
            
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            // the name that we gonna make check on 

            string? Title = value?.ToString();


            // get Course  from the requst 

            Book book = (Book)validationContext.ObjectInstance;

            // get the course from dbcontext and compare

            //  InitialCompanyContext context = new InitialCompanyContext();
            Book? courseFromDb = bookStore.Books.FirstOrDefault(c =>c.Title == Title && c.CategoryId == book.CategoryId);

            if (courseFromDb == null) return ValidationResult.Success;
            else return new ValidationResult($"{Title} Title is Already Exist in The Category Selected  ");
        }

    }
}
