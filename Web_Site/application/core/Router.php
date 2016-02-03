<?php

/**
+ класс маршрутизатора запросов /
+ dev - engineer /
+ project - business card site "быдлокодеры.рф" /
*/

    class Router{
        
       
		
        //Начало работы маршрутизатора
        static function start(){
            //Значения по умолчанию для контроллера и метода(главная страница)
            $controllerName = 'Main';
            $actionName= 'index';
            
            //Улавливаем запрос адресной строки и разбиваем на массив
            $routes = explode('/', $_SERVER['REQUEST_URI']);
            
            //Проверяем элементы массива, присваиваем $controllerName и $actionName значения если элементы !пусты
            if(!empty($routes[1])){
                $controllerName = $routes[1];
            }
            if(!empty($routes[2])){
                $actionName = $routes[2];
            }
            
            //Добавляем префиксы к названиям для подключения файлов с классами и экшенов
            $modelName = 'Model_' . $controllerName;
			$controllerName = 'Controller_' . $controllerName;
            $actionName = 'action_' . $actionName;
            
            //Записываем путь по умолчанию, дописываем расширение файла
            ///Model
            $modelFile = strtolower($modelName) . '.php';
            $modelPath = "application/models/" . $modelFile;
            ///Проверка на наличе файла с классом который нужно подключить маршрутизатору. Если существует - подключаем. Иначе отправляем на страницу 404
            if(file_exists($modelPath)){
                include "application/models/" . $modelFile;
            }
			
			//Записываем путь по умолчанию, дописываем расширение файла
            ///Controller
            $controllerFile = strtolower($controllerName) . '.php';
            $controllerPath = "application/controllers/" . $controllerFile;
            ///Проверка на наличе файла с классом который нужно подключить маршрутизатору. Если существует - подключаем. Иначе отправляем на страницу 404
            if(file_exists($controllerPath)){
                include "application/controllers/" . $controllerFile;
            }else{
                Router::ErrorPage404();
            }
            
            //Создаем объект класса подключенного контроллера и присваиваем экшену значение $ctionName
            $controller = new $controllerName;
            $action = $actionName;
            
            //Проверяем существует ли метод(экшн) в вызваном контроллере. Если существует - вызываем метод, иначе отправляем на страницу 404
            if(method_exists($controller, $action)){
                $controller->$action();
            }else{
                Router::ErrorPage404();
            }
		}
		 //Редирект на страницу 404
        public function ErrorPage404(){
        
            $host = 'http://'.$_SERVER['HTTP_HOST'].'/';
            header('HTTP/1.1 404 Not Found');
            header("Status: 404 Not Found");
            header('Location:'.$host.'404');
        }
        
    }