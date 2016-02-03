<?php

/**
+ класс главного контроллера Model /
+ dev - engineer /
+ project - business card site "быдлокодеры.рф" /
*/

    class Model{
        // Данные для подключения к БД
        public $hostname = "mysql.hostinger.ru";
        public $password = "";
        public $user = "u455076900_admin";
        public $dbname = "u455076900_eye";
        public $db;
    
        // Подключение к базе данных при создании объекта(исправлю)
        public function __construct(){
            try {
                $this->db = new PDO('mysql:host='.$this->hostname.'; dbname='.$this->dbname, $this->user, $this->password); 
                $this->db->query('SET NAMES utf8');
                return true;
            }
            // Отловили ошибку записали в файл error.log 
            catch(PDOException $e) {
                $fp = fopen("error.log", "a");
                $mytext = date('l jS \of F Y h:i:s A') . "\r\n" . $e->getMessage() . "\r\n\r\n";
                $test = fwrite($fp, $mytext);
            }
        }
        
        public function getConnect(){
            return $this->db;
        }
}	