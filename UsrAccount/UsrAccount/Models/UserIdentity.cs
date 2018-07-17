using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UsrAccount.Models
{
    public class UserIdentity
    {
      
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "아이디")]
        public string UserId { get; set; }

        [Display(Name = "패스워드")]
        [DataType(DataType.Password)]
        [Required, StringLength(5, MinimumLength = 2, ErrorMessage = "2자에서 5자 이내입니다")]
        public string Password { get; set; }

        [Display(Name = "별명")]
        public string NicName { get; set; }
        
        [Display(Name = "사용자소속")]
        public string UserGroup { get; set; }

        [Display(Name = "사용자권한")]
        public string Role { get; set; }

    
        [Display(Name = "사용")]
        public bool InActivity { get; set; }
    }
}
