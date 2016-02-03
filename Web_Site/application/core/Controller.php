<?php

/**
+ класс главного контроллера View /
+ dev - engineer /
+ project - business card site "быдлокодеры.рф" /
*/

    class Controller {
	
        public $model;
        public $view;
	
        function __construct(){
            
            // Вызывается главная модель Model с подключением к БД(исправлю)
            // Вызывается метод подключения getConnect()
            // Вызывается главный класс представления View
            $this->model = new Model();
            $this->model->getConnect();
            $this->view = new View();
        }
	
        function action_index(){
		// todo	
        }
    }
