using System.ComponentModel.DataAnnotations;

namespace ArticleProject.Core
{
    public class Category
    {
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "المعرف")]
        public int Id { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "اسم الصنف")]
        [MaxLength(50, ErrorMessage = "اعلى قمية للادخال هي 50 حرف")]
        [MinLength(2, ErrorMessage = "ادنى قيمة للادخال هي حرفان")]
        [DataType(DataType.Text)]
        public string Name { get; set; }



        //NAvigation
        public virtual List<autherPost>? AutherPosts { get; set; }

    }
}
