<?php

class M_Total
{
    private static $instance;
    private $Sql;

    function __construct()
    {
        $this->Sql = SQL::GetInstance();
    }

    public static function Instance()
    {
        if(self::$instance == null)
            self::$instance = new M_Total();
        return self::$instance;
    }

    public function Get_Total()
    {
        $query = "";
    }
}