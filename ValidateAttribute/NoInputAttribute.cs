using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AccountBooks.ValidateAttribute
{
    /*
    撰寫自定驗證注意事項
    ▪ 必須將此類別宣告為 sealed class 
    ▪ 需要繼承 ValidationAttribute 抽象類別
    ▪ 必須覆寫 IsValid 方法
    ▪ GetClientValidationRules的ValidationType一 定要小寫
    */
   //MVC验证-自定义验证规则、禁止输入某些值 http://www.cnblogs.com/darrenji/p/3580253.html
    public sealed class NoInputAttribute : ValidationAttribute, IClientValidatable
    {
        /// <summary>
        /// 禁止输入的值
        /// </summary>
        public string[] Input { get; set; }

        public NoInputAttribute(string input)//如果输入的字符串有逗号分隔
        {
           //檢查是否有包含分隔符號
            if (input.IndexOf(",") > -1)
                //把字符分隔成数组赋值给Input
                this.Input = input.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            else
                //没有逗号，就构建一个数组赋值给Input
                this.Input = new string[] { input };
        }

        public override bool IsValid(object value)
        {
            //要不要輸入與此驗證無關
            if (value == null)
            {
                return true;
            }

            //如果輸入的值是字串才做判斷
            if (value is string)
            {
                if (string.Join(",", Input).Contains(value.ToString()))
                {
                    return false;
                }
            }
            return true;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule rule = new ModelClientValidationRule
            {
                ValidationType = "noinput",  //GetClientValidationRules的ValidationType一 定要小寫
                ErrorMessage=FormatErrorMessage(metadata.GetDisplayName())
            };

            //此參數一定要是小寫！前台rule.ValidationParameters["input"]存储的应该是string类型，所以保存的时候要把Input数组元素join起来。
            rule.ValidationParameters["input"] = string.Join(",", Input);
            yield return rule;
        }
    }
}