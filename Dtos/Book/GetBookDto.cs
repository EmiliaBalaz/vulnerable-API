using System.ComponentModel.DataAnnotations;
public class GetBookDto
    {
        public int Id {get; set;}
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string? Name {get; set;}
        [Required]
        [StringLength(50, MinimumLength = 1)]
        [RegularExpression("[a-zA-Z]+[ a-zA-Z]*", ErrorMessage="Numerics and spec characters are not allowed.")]
        public string? Author {get; set;}
    }



    
