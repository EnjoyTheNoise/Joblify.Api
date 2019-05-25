using System;

namespace Joblify.Core.Users.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Birthdate { get; set; }
        public string Description { get; set; }
        public string Experience { get; set; }
        public string FieldOfInterest { get; set; }
        public string PhotoUrl { get; set; }
    }
}
