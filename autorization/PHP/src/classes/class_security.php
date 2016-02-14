<?php


class Security
{   // Очищаем строку от разной гадости
    static public function clear_string ($str) {

        $text = htmlspecialchars($str, ENT_QUOTES, 'UTF-8');
        $st = preg_replace('|^[A-Z0-9]+$|i','', $text);
        return $st;
    }
    //Превращшаем пароль в нечто неудоваримое
    static public function hash ($string, $salt) {

        return crypt($string,$salt);
    }
}