<?php

/**
+ класс Контроллер страницы Продукт /
+ dev - engineer /
+ project - business card site "быдлокодеры.рф" /
*/

	class Controller_Product extends Controller{

		function action_index(){
			
			$this->view->generate('product_view.php', 'template_view.php', $data);
		}
	}
