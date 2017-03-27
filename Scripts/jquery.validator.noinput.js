//扩展的方法名与NoInputAttribute保持一致，且是小写
//value是指前端输入的值
//element是指html元素
//parm是指输入的参数，即rule.ValidationParameters["input"]键input对应的值，通过NoInputAttribute的构造函数注入的
$.validator.addMethod("noinput", function (value, element, param) {
    if (value == false) { //如果value没有输入，这里就放行
        return true;
    }

    var validateState = true;
    //param就是自定义特性rule.ValidationParameters["input"]对应的值
    var paramarr = param.split(',');

    //第一个参数是数组元素的索引
    //第二个参数是数组元素
    $.each(paramarr, function (index, ele) {
        if (value == ele) {
            validateState = false;
            return;
        }
    });
    return validateState;
});


//扩展方法注册
//第一个参数就是jquery验证扩展方法名
//第二个参数就是rule.ValidationParameters["input"]的键
$.validator.unobtrusive.adapters.addSingleVal("noinput", "input");