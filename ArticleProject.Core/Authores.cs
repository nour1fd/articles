using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ArticleProject.Core
{
    public class Authores
    {
        [Required]
        [DisplayName("المعرف")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "  المعرف الخاص بالمستخدم")]

        public string UserId { get; set; }
        [Required]
        [DisplayName("اسم المستخدم")]

        public string UserName { get; set; }
        [Required]
        [DisplayName("الاسم الكامل")]

        public string FullName { get; set; }






        [AllowNull]
        [DisplayName("الصورة")]

        public string? ProfilImgUrl { get; set; }
        [AllowNull]

        [DisplayName("البايو")]

        public string? Bio { get; set; }
        [AllowNull]

        [DisplayName("فيسبوك")]

        public string? Facebook { get; set; }
        [AllowNull]

        [DisplayName("انستغرام")]

        public string? Instagram { get; set; }
        [AllowNull]

        [DisplayName("تويتر")]

        public string? Twitter { get; set; }
        //Navigation
        public virtual List<autherPost>? AutherPosts { get; set; }


    }
}
