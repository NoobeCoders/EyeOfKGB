<?php

/**
+ класс Контроллер страницы Контакты /
+ dev - engineer /
+ project - business card site "быдлокодеры.рф" /
*/

    class Controller_Contacts extends Controller{
	
        function action_index(){
            
            $this->view->generate('contacts_view.php', 'template_view.php', $data);
        }
    }
