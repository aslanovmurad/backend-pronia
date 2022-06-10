using System.ComponentModel.DataAnnotations;

namespace AP204_Pronia.ViewModels
{
    public class EditVM
    {
        [Required, StringLength(maximumLength: 15)]
        public string FirstName { get; set; }
        [Required, StringLength(maximumLength: 15)]
        public string UserName { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, StringLength(maximumLength: 20)]

        public string LastName { get; set; }
        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
