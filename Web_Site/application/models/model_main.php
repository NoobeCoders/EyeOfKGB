<?php

/**
+ класс Модель главной страницы /
+ dev - engineer /
+ project - business card site "быдлокодеры.рф" /
*/

class Model_Main extends Model{
	

	public function get_description(){	
		
	}
	public function get_command_list(){	
		
		$this->data = $this->db->query('SELECT * from command');
		$this->data->setFetchMode(PDO::FETCH_ASSOC);
		$arr = $this->data;
			return $arr;
		
	}

}
