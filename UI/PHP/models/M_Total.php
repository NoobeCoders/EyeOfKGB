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

    private function Site($id = 1)
    {
        $query = "SELECT * FROM sites WHERE `id` = $id";

        $result = $this->Sql->Select($query);

        return $result;
    }



    public function Get_Total($id = 1)
    {

        $persons = $this->Get_Persons();
        $site = $this->Site($id);
        foreach($persons as $v)
        {
            $t[] = $v['id'];

        }
        foreach($t as $k)
        {
            $total = $this->Total($k, $site[0]['id']);
            $p[] = $total[0];
        }

        return $p;
    }



    public function Total($id, $siteid = 1)
    {
        $query = "SELECT SUM(  `rank` ) AS  `rank` ,  `persons`.`name` ,  `personpagerank`.`personid` ,  `pages`.`id`
                  FROM  `personpagerank`
                  INNER JOIN  `pages` ON  `pages`.`id` =  `personpagerank`.`pageid`
                  INNER JOIN  `persons` ON  `persons`.`id` =  `personpagerank`.`personid`
                  INNER JOIN  `sites` ON  `sites`.`id` =  `pages`.`siteid`
                  WHERE  `pages`.`siteid` = $siteid
                  AND  `personpagerank`.`personid` =$id" ;
        $result = $this->Sql->Select($query);
        return $result;

    }

    private function Get_Persons()
    {
        $query = "SELECT * FROM `persons`";

        $result = $this->Sql->Select($query);

        return $result;
    }

}