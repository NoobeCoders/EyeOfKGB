

$(document).ready(function () {
    $("#registration").hide();                  //
    $("#registrationbutton").click(function(){  //  Делаем
        $("#registration").show('slow');        //  небольшую
        $("#autorization").hide('slow');        // анимацию окон
    });
    $("#back").click(function(){
        $("#registration").hide('slow');         //
        $("#autorization").show('slow');        //
    });
    //Валидируем форму и если все хорошо отправляем через ajax
    $("#registrationform").validate({
        rules: {
            registration_login: {
                required: true,
                minlength: 4,
                maxlength: 16,
                remote:{
                    url: 'src/checklogin.php',
                    type: 'POST'
                }
            },
            registration_password: {
                required: true,
                minlength: 6,
                maxlength: 16
            },
            registration_password_repeat: {
                required: true,
                minlength: 6,
                maxlength: 16,
                equalTo: "#registration_password"
            }

        },
        messages: {
            registration_login: {
                required: "Поле Обязательно для заполнения",
                minlength: "Длина логина минимально 4 символа",
                maxlength: "Слишком длинный Логин",
                remote: "Такой логин уже существыует"
            },
            registration_password: {
                required: "Поле Обязательно для заполнения",
                minlength: "Длина логина минимально 6 символа",
                maxlength: "Слишком длинный Логин"
            },
            registration_password_repeat: {
                required: "Поле Обязательно для заполнения",
                minlength: "Длина логина минимально 6 символа",
                maxlength: "Слишком длинный Логин",
                equalTo: "Пароли должны совпадать"
            }
        },
        submitHandler: function() {
            $.ajax({
                type: 'POST',
                url:'src/index.php',
                data: $("#registrationform").serialize(),
                dataType:'text',
                success: function(data) {
                    $('#result').append('<div class="result">'+data+'</div>');
                    $("#registration").hide('slow');
                    $("#autorization").show('slow');
                    $("#registrationform").trigger("reset");



                }
            })
            }
    });
    //Проверяем чтобы не оставляли пустыми поля login и password при вводе
    $("#loginpassword").validate({
            rules:{
                login:{
                    required: true
                },
                password:{
                    required: true
                }
            },
            messages:{
                login:{
                    required: 'Введите Логин'
                },
                password:{
                    required: 'Введите Пароль'
                }
            },
            submitHandler: function() {
                $.ajax({
                    type: 'POST',
                    url:'./src/index.php',
                    data: $("#loginpassword").serialize(),
                    dataType:'text',
                    success: function(data) {
                        console.log(data);
                        if (!data) {
                            $('#result').append('<div class="result">Неправильный логин или пароль</div>');
                        } else {
                        $('#result').append('<div class="result">Вы вошли как '+data+'. Перенаправление на сайт через 4 секунды</div>');
                       setTimeout(function(){
                            window.location.href = "view/nextstage.html";
                        },4000)
                    }}
                })
            }
        });
    $('#backend').click(function(){
        window.location.href = "../index.html";
    });

});
