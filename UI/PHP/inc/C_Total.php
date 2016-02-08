<?php



class C_Total extends C_Base
{
    private $total;

    public function __construct()
    {
       $this->total = M_Total::GetInstance();
    }

    public function action_index()
    {
        $site = $this->total->Get_Site();


        if(isset($_POST['sub']))
            $total = $this->total->Get_Total($_POST['id']);
        else
            $total = $this->total->Get_Total(1);


        $this->title = "list";
        $this->menu = $this->template("view/menu.php");
        $this->content = $this->template("view/total.php", ['total' => $total, 'site' => $site]);
    }

}