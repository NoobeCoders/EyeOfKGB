<?php

    class Base {

        private static $instance;

        private function __construct ($host, $user, $pass) {
            //подключаем БД
            try {
                $this->link = new PDO($host, $user, $pass);
                $this->link->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
                $this->link->exec("set names utf8");
            } catch (PDOException $e) {

                echo 'Connection failed: ' . $e->getMessage();
            }

        }
        //Подключаем приватный конструктор
        public static function getDB($host, $user, $pass) {
            if (self::$instance == null) {

                self::$instance = new Base($host, $user, $pass);

            }
            return self::$instance;
        }
        // Делаем вставку
         public function insert($login, $password, $name, $firstname, $email) {
             $defaultroleid = 2;
             $query = "INSERT INTO users (login, password, name, firstname, email, roleid) VALUES ('$login','$password', '$name', '$firstname', '$email', '$defaultroleid')";
             $count = $this->link->exec($query);
             return $count;
        }
        // Делаем выборку
        public function select($query) {

            $rows = $this->link->query($query)->fetch(PDO::FETCH_ASSOC);
            return($rows);

        }

    }