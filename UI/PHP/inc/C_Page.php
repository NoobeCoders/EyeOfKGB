<?php


class C_Page extends C_Base
{

    public function action_index()
    {
        $this->title = "list";
        $this->menu = $this->template("view/menu.php");
        $this->content = $this->template("view/total.php");
    }
}