<?php
require_once "core/Controller.php";
require_once "core/SQL.php";


function __autoload($classname)
{
    require_once "inc/$classname.php";
}
$arr = [];
if(isset($_GET['q']))
    $arr = explode('/', $_GET['q']);

$params = [];

foreach ($arr as $value)
{
    if($value != '')
        $params[] = $value;
}

$action = "action_";
$action .= isset($params[1]) ? $params[1] : "index";

$c = "page";

if(isset($params[0]))
    $c = $params[0];

switch ($c)
{
    case 'page': $controller = new C_Page();
        break;
    default: $controller = new C_Page();
}

$controller->request($action);
