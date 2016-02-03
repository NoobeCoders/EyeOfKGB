<?php
    
    // Подключение основных классов Controller, Model, View и Router
    require_once 'core/Controller.php';
    require_once 'core/Model.php';
    require_once 'core/View.php';
    require_once 'core/Router.php';
	
	// Вызываем метод маршрутизатора  start() 
	Router::start();