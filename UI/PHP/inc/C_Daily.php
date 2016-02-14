<?php


class C_Daily extends C_Base
{
    private $daily;

    public function __construct()
    {
        $this->daily = M_Daily::GetInstance();
    }

    public function action_index()
    {
        print_r($_POST['to_date']);
        if(isset($_POST['sub']) && isset($_POST['from_date']) && isset($_POST['to_date']))
            $daily = $this->daily->Get_Daily($_POST['persons'], $_POST['site'], $_POST['from_date'], $_POST['to_date']);
        else
            $daily = $this->daily->Get_Daily(1, 1);
        $site = $this->daily->Get_Site();
        $persons = $this->daily->Get_Persons();
        $this->menu = $this->template("view/menu.php");
        $this->content = $this->template("view/daily.php", ['daily' => $daily, 'site' => $site, 'persons' => $persons]);
    }
}