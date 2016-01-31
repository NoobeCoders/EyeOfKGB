<?php

class M_Total
{
    private static $instanse;
    private $Sql;

    protected function __construct()
    {

        $this->Sql = SQL::GetInstance();
    }
    public static function GetInstance()
    {
        if(self::$instanse == null)
            self::$instanse = new self;

        return self::$instanse;
    }

    public function Get_Site()
    {
        $query = "SELECT * FROM sites";

        $result = $this->Sql->Select($query);

        return $result;
    }



    public function Get_Total($id)
    {
        $query = "SELECT  `persons`.`name` ,  `personpagerank`.`rank`
                  FROM  `persons`
                  INNER JOIN  `personpagerank` ON  `persons`.`id` =  `personpagerank`.`personid`
                  WHERE `personpagerank`.`pageid` = $id";
        $result = $this->Sql->Select($query);

        return $result;

    }

}