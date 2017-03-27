using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AccountBooks.Models
{
    [MetadataType(typeof(ChargeModelsMetaData))]
    public partial class ChargeModels
    {
        public class ChargeModelsMetaData
        {
            [Required]
            public Guid Id { get; set; }

            [Required]
            [Display(Name = "類別")]
            public string Category { get; set; }

            [Required]
            [Display(Name = "金額")]
            [RegularExpression(@"^[0-9]*[1-9][0-9]*$", ErrorMessage = "金額只能輸入正整數 ")]
         //   [Range(1, int.MaxValue, ErrorMessage = "金額必需大於0元")]
            public int Amount { get; set; }

            [Required]
            [Display(Name = "日期")]
            public DateTime Date { get; set; }

            [Required]
            [Display(Name = "備註")]
            [StringLength(100)]  //以字串长度进行条件设置，建构函式预设指定最大长度
            public string Remarks { get; set; }
        }
    }
}