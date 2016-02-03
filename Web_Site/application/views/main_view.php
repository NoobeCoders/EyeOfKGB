<?php

/**
+ Представление главной страницы /
+ dev - engineer /
+ project - business card site "быдлокодеры.рф" /
*/

    echo "<h1>О компании</h1>";
            echo "<div class='container'>";
                echo "<p class='comp'>Если в кратце...Наша команда разработала продукт, который поможет Вам спарсить любой контент :)</p>";
			echo "</div>";
            
    echo "<h1 class='h_aligne'>Наша команда</h1>";
		foreach($data as $row){
			echo "<div class='container'>";
            echo "<img class='alignleft shad' src='images/team/" . $row['src'] . "'><br>";
            echo "Имя: <span class='text_shad'>" .  $row['first_name'] . "</span><br>";
            echo "Фамилия: <span class='text_shad'>" . $row['last_name'] . "</span><br>";
            echo "Язык: <span class='text_shad'>" .  $row['prog_language'] . "</span><br>";
            echo "Модуль: <span class='text_shad'>" . $row['module'] . "</span>";
			echo "</div>";
		}
?>
