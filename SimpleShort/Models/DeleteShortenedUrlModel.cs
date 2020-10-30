using System.ComponentModel.DataAnnotations;

namespace SimpleShort.Models
{
    public class DeleteShortenedUrlModel
    {
        [Required(ErrorMessage = "ip address is required")]
        [MinLength(7, ErrorMessage = "ip address must be as least 7 characters")]
        [MaxLength(39, ErrorMessage = "ip address can not be longer than 39 characters")]
        // [RegularExpression(@"[\da-fA-F.:]", ErrorMessage = "invalid ip address")]
        public string IpAddress { get; set; }

        [MinLength(2, ErrorMessage = "shortened path must be longer than 2 characters")]
        [MaxLength(2000, ErrorMessage = "shortened path must be shorter than 2000 characters")]
        // [RegularExpression(@"[\w-._~:]", ErrorMessage = "invalid path, valid characters are 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-._~:'")]
        public string Path { get; set; }

        [Required(ErrorMessage = "original url is required")]
        [MinLength(2, ErrorMessage = "original url must be longer than 2 characters")]
        [MaxLength(2000, ErrorMessage = "original url must be shorter than 2000 characters")]
        // [RegularExpression(@"[\w-._~:\/?#[\]@!$&'()*+,;%=%]", ErrorMessage = "invalid url")]
        public string OriginalUrl { get; set; }
    }
}
