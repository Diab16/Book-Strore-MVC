using Book_Store_MVC.Models;
using Day2.Models;
using Day2.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Book_Store_MVC.Validation
{
    public class UniqueEmailAttribute:ValidationAttribute
    {
        //private readonly BookStoreContext bookStore;

        //public UniqueEmailAttribute(BookStoreContext bookStore)
        //{
        //    this.bookStore = bookStore;
        //}
        public UniqueEmailAttribute()
        {
        }


        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            // the name that we gonna make check on 

            string? Email = value?.ToString();


            // get Course  from the requst 
            var db = (BookStoreContext)validationContext.GetService(typeof(BookStoreContext));
            

            // get the course from dbcontext and compare

            //  InitialCompanyContext context = new InitialCompanyContext();
            ApplicationUser? userFromDb = db.Users.FirstOrDefault(u => u.Email == Email);

            if (userFromDb == null) return ValidationResult.Success;
            else return new ValidationResult($"{Email} Email is Already Exists in Users , Enter Different Email");
        }
    }
}
