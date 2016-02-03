<?php

/**
+ класс Контроллер страницы 404 /
+ dev - engineer /
+ project - business card site "быдлокодеры.рф" /
*/

    class Controller_404 extends Controller{
	
        function action_index(){
            
            $this->view->generate('404_view.php', 'template_view.php');
        }

    }
