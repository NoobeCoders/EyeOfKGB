<?php

/**
+ класс Контроллер страницы Техническая помощь /
+ dev - engineer /
+ project - business card site "быдлокодеры.рф" /
*/

	class Controller_Faq extends Controller{

		function action_index(){
			
			$this->view->generate('faq_view.php', 'template_view.php', $data);
		}
	}

