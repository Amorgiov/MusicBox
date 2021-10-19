namespace MusicBox.Domain.UserUpdateDto
{
    public class EditPasswordViewModel
    {
        public string Id { get; set; }
        
        public string Email { get; set; }

        public string NewPassword { get; set; }
    }
}