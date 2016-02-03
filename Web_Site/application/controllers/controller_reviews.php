<?php

/**
+ класс Контроллер страницы Отзывы /
+ dev - engineer /
+ project - business card site "быдлокодеры.рф" /
*/

    class Controller_Reviews extends Controller{
	
        function action_index(){
		
            $this->view->generate('reviews_view.php', 'template_view.php', $data);
        }
    }
