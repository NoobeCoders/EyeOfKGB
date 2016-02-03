<?php

/**
+ класс Контроллер главной страницы /
+ dev - engineer /
+ project - business card site "быдлокодеры.рф" /
*/

	class Controller_Main extends Controller{
		

		public function __construct(){
			
			$this->model = new Model_main();
			$this->view = new View();
		}
		public function action_index(){
			
			$data = $this->model->get_command_list();
			$this->view->generate('main_view.php', 'template_view.php', $data);
		}
	}
