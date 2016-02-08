<div id="content">
    <h2>Ежедневная статистека</h2>
    <form action="" method="post">
        Сайт:
            <select name="site">
                <?php foreach($site as $value):?>
                    <option value="<?=$value['id']?>"><?=$value['name']?></option>
                <?php endforeach;?>

            </select>
        <br>
        Личность:
            <select name="persons">
                <?php foreach($persons as $value):?>
                    <option value="<?=$value['id']?>"><?=$value['name']?></option>
                <?php endforeach; ?>
            </select>
        <input type="submit" name="sub" value="показать статистеку">
        <br>
        <em>Период:</em>
        <br>
        <table id="date">
            <tr>
                <td>
                    <em>От:</em>
                </td>
                <td>
                    <input  name="from_date" class="date" value="yy-mm-dd">
                </td>
            </tr>
            <tr>
                <td>
                    <em>До:</em>
                </td>
                <td>
                    <input  name="to_date" class="date" value="yy-mm-dd">
                </td>
            </tr>
    </form>
    <table class="table">
        <tr>
            <th>Дата</th>
            <th>Результать</th>
        </tr>
        <?php foreach($daily as $value):?>
            <tr>
                <td><?=$value['lastscandate'];?></td>
                <td><?=$value['rank'];?></td>
            </tr>
        <?php endforeach;?>
    </table>
</div>