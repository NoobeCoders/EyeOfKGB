<?php


require "config.php";
require "core/Controller.php";
require "core/GetInstance.php";
require "core/SQL.php";
function __autoload($classname)
{
    require "inc/$classname.php";

}


require 'models/M_Total.php';




$action = "action_";
$action .= isset($_GET['act'])?$_GET['act']:"index";

$c = "total";

if(isset($_GET['c']))
    $c = $_GET['c'];

switch ($c)
{
    case "total": $controller = new C_Total();
        break;
    default: $controller = new C_Total();
}

$controller->request($action);
