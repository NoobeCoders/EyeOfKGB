<?php

 class SQL
{

     private static $instanse;
    private static $link;

    private function __construct()
    {
        setlocale(LC_ALL, "ru_RU.UTF-8");
        mb_internal_encoding("UTF-8");
        try
        {
            self::$link = new PDO('mysql:host='.HOST.';dbname='.DB_NAME, DB_USER, PASSWORD);
        } catch (PDOException $e)
        {
           echo $e->getMessage();
        }
    }
     public static function GetInstance()
     {
         if(self::$instanse == null)
             self::$instanse = new self;

         return self::$instanse;
     }

    public function Select($query)
    {
        $result = self::$link->query($query);

        $row = [];
        while($r = $result->fetchAll(PDO::FETCH_ASSOC))
            $row[] = $r;

        return $row[0];
    }
}