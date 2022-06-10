using System.ComponentModel.DataAnnotations;

namespace AP204_Pronia.ViewModels
{
    public class LoginVM
    {
        [Required,StringLength(maximumLength:20)]
        public string UserName { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
