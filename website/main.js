	var text = 'Добро пожаловать на официальный сайт БыдлоКОдеров!';
	i = 0;
	function type(){
		i++;
		if( i <= text.length )
			document.getElementById("greetings").innerHTML = text.substr(0, i);

		setTimeout( type, 50 );
		}
	type();


