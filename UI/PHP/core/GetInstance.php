<?php
 class GetInstance
{
    protected static $instanse;

    protected function __construct()
    {
    }

     public static function GetInstance()
    {
        if(self::$instanse == null)
            self::$instanse = new self;

        return self::$instanse;
    }

}