<?php

class M_Daily
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

    public function Get_Daily($id_persons, $id_site, $founddatetime = "0000-00-00", $lastscandate = "2500-01-01")
    {
        $query = "SELECT `pages`.`lastscandate`, `personpagerank`.`rank`, `persons`.`name`
                  FROM `pages`
                  INNER JOIN `personpagerank` ON `pages`.`id` = `personpagerank`.`pageid`
                  INNER JOIN `persons` ON `persons`.`id` = `personpagerank`.`personid`
                  INNER JOIN `sites` ON `sites`.`id` = `pages`.`siteid`
                  WHERE (`personpagerank`.`personid` = $id_persons AND `pages`.`siteid` = $id_site)
                  AND (`pages`.`lastscandate` BETWEEN  '$founddatetime' AND  '$lastscandate' ) ";
        $result = $this->Sql->Select($query);
        return $result;
    }

        public function Get_Persons()
    {
        $query = "SELECT * FROM `persons`";

        $result = $this->Sql->Select($query);

        return $result;
    }

}