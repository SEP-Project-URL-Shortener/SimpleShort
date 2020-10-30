﻿using System.ComponentModel.DataAnnotations;

namespace SimpleShort.Models
{
    public class CreateShortenedUrlModel
    {

        [Required(ErrorMessage = "ip address is required")]
        [MinLength(7, ErrorMessage = "ip address must be as least 7 characters")]
        [MaxLength(39, ErrorMessage = "ip address can not be longer than 39 characters")]
        //[RegularExpression(@"[\da-fA-F.:]", ErrorMessage = "invalid ip address")]
        public string IpAddress { get; set; }

        [MinLength(2, ErrorMessage = "shortened path must be longer than 2 characters")]
        [MaxLength(2000, ErrorMessage = "shortened path must be shorter than 2000 characters")]
        // [RegularExpression(@"[\w-._~:]", ErrorMessage = "invalid path")]
        public string Path { get; set; }

        [Required(ErrorMessage = "original url is required")]
        [MinLength(2, ErrorMessage = "original url must be longer than 2 characters")]
        [MaxLength(2000, ErrorMessage = "original url must be shorter than 2000 characters")]
        // [RegularExpression(@"[\w-._~:\/?#[\]@!$&'()*+,;%=%]", ErrorMessage = "invalid url")]
        public string OriginalUrl { get; set; }

        [MinLength(14, ErrorMessage = "expiration must be longer than 14 characters")]
        [MaxLength(22, ErrorMessage = "expiration must be shorter than 22 characters")]
        // [RegularExpression(@"[\d\/:APM ]", ErrorMessage = "invalid date, 01/01/0001 01:00:00 AM")]
        public string Expiration { get; set; }
    }
}

/*
 * {
  "ipAddress": "anything",
  "path": "testing",
  "originalUrl": "testing123",
  "expiration": "01/01/3000 12:00 AM"
}
 */