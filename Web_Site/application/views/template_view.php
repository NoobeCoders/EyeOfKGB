<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<meta http-equiv="content-type" content="text/html; charset=utf-8" />
		<meta name="description" content="" />
		<meta name="keywords" content="" />
		<title>быдлокодеры.рф</title>
		<link href="http://fonts.googleapis.com/css?family=Open+Sans" rel="stylesheet" type="text/css" />
		<link href="http://fonts.googleapis.com/css?family=Kreon" rel="stylesheet" type="text/css" />
		<link rel="stylesheet" type="text/css" href="/css/style.css" />
		<script src="/js/jquery-1.6.2.js" type="text/javascript"></script>
	</head>
	<body>
    <video autoplay loop muted class="bgvideo" id="bgvideo">
        <source src="images/video.mp4" type="video/mp4"></source>
    </video>
		<div id="wrapper">
			<div id="header">
				<div id="logo">
					<a href="/"><img src="images/logo2.png"></a>
				</div>

				<!--<div id="menu">
					<ul>
						<li class="first active"><a href="/">О компании</a></li>
						<li><a href="/">Продукт</a></li>
						<li><a href="/">Техническая помощь</a></li>
						<li><a href="/">Контакты</a></li>
						<li class="last"><a href="/">Отзывы</a></li>
					</ul>
					<br class="clearfix" />
				</div>-->
			</div>
			<div id="page">
            <div class="sidebar_wraper">
				<div id="sidebar">
					<div class="side-box">
						<ul class="list">
							<li class="first active"><a href="/">О компании</a></li>
						<li><a href="/product">Продукт</a></li>
						<li><a href="/faq">Техническая помощь</a></li>
						<li><a href="/contacts">Контакты</a></li>
						<li class="last"><a href="/reviews">Отзывы</a></li>
						</ul>
						<hr class="bold"/>
					</div>
					<div class="side-box">
						<div class="login">
		
						<form action="" method="post">
							<label>Логин:</label><br>
						  <input name="login" type="text" size="15" maxlength="15"><br>
							<label>Пароль:</label><br>
						  <input name="password" type="password" size="15" maxlength="15"><br><br>
						  <input type="submit" value="войти"><br>
						</form>
						Здравствуйте <font color="green">гость</font>!
						</div>
					</div>
				</div>
				</div>
				<div id="content">
					<div class="box">
						<?php include 'application/views/'.$content_view; ?>
					</div>
					<br class="clearfix" />
				</div>
				<br class="clearfix" />
			</div>
			<div id="page-bottom">
				<div id="page-bottom-sidebar">
					<h3>Наши контакты</h3>
					<ul class="list">
						<li class="first">icq: 329207183</li>
						<li>skypeid: cheponya1</li>
						<li class="last">email: info@быдлокодеры.рф</li>
					</ul>
				</div>
				<div id="page-bottom-content">
					<h3></h3>
					<p>
						Наша команда принимает заказы любой сложности!!!<br>
						И что самое важное - Мы работаем за еду :)
						<hr>
							Никаких прав на материал на данном сайте нет и быть не может!<br>
							Копируйте хоть до потери пульса.
						<hr>
							
					</p>
				</div>
				<br class="clearfix" />
			</div>
		</div>
		<div id="footer">
			<a href="/">быдлокодеры.рф</a> &copy; 2015</a>
		</div>
	</body>
</html>