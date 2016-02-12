<?php
include_once ("info/info.php");
include_once ("classes/class_base.php");
include_once ("classes/class_security.php");

    $connect = Base::getDB($host,$user,$pass);

    // проверка наличия пароля и логина в базе даннызх
    if (isset($_POST['login']) && isset($_POST['password'])) {
        $login = $_POST['login'];
        $password = $_POST['password'];
        $hash_password = security::hash($password,$salt);
        $query = "SELECT * FROM users WHERE login = '$login' AND password = '$hash_password'";
        $autorization = $connect->select($query);

        if($autorization) {
            echo 'Вы вошли как '.$autorization['login'];

        } else {
            return false;
        }

    }
    // регистрация нового пользователя
    if (isset($_POST['registration_login']) && isset($_POST['registration_password'])) {
        $login = $_POST['registration_login'];
        $password = $_POST['registration_password'];
        $name = $_POST['registration_name'];
        $firstname = $_POST['registration_firstname'];
        $email = $_POST['registration_email'];
        $hash_password = security::hash($password,$salt);
        $registration = $connect->insert($login, $hash_password, $name, $firstname, $email);
        if($registration == 1) {
            echo 'Регистрация прошла успешно';
        }
    }






