<?php

/**
+ класс главного контроллера View /
+ dev - engineer /
+ project - business card site "быдлокодеры.рф" /
*/

    class View{
	
        //$data - массив с контентом страницы.
        function generate($content_view, $template_view, $data = null){
            
            if(is_array($data)) {
                
                extract($data);
            }
            
            include 'application/views/'.$template_view;
        }
    }
