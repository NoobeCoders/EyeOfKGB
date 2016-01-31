<?php

abstract class C_Base extends Controller
{
    protected $title;
    protected $content;
    protected $menu;

    function __construct()
    {

    }

    public function before()
    {
        header("Content-type: text/html; charset=utf-8");
        $this->title = "MyTitle";
        $this->content = "MyContent";
    }

    public function render()
    {
        $params = ["title" => $this->title, "menu" => $this->menu, "content" => $this->content];
        $page = $this->template("view/main.php", $params);

        echo $page;
    }
}