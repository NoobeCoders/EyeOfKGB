<?php
include_once ("info/info.php");
include_once ("classes/class_base.php");

    //проверяем Наличие Логина в базе данных
    $login = $_POST['registration_login'];
    $connect = Base::getDB($host,$user,$pass);
    $query = "SELECT * FROM users WHERE login = '$login'";
    $select = $connect->select($query);
    $bool = (bool)$select;



        if (!$bool) {
            echo 'true';

        } else {

            echo 'false';
        }

