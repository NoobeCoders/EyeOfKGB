<?php


abstract class Controller
{
    abstract function render();

    abstract function before();

    public function request($action)
    {
        $this->before();
        $this->$action();
        $this->render();
    }

    protected function isGet()
    {
        return $_SERVER['REQUEST_METHOD'] == "GET";
    }

    protected function isPost()
    {
        return $_SERVER['REQUEST_METHOD'] == "POST";
    }

    protected function template($file, $params = [])
    {
        foreach ($params as $k => $v)
        {
            $$k = $v;
        }

        ob_start();
        include("$file");
        return ob_get_clean();
    }

    public function __call($method, $params)
    {
        die("Unknown method");
    }
}