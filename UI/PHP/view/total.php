<div id="content">
    <h2>Общая статистека</h2>
    <form action="" method="post">
        Сайт: <select name="id">
            <?php foreach($site as $value):?>
            <option value="<?=$value['id'];?>">
                <?=$value['name']?>
            </option>
            <?php endforeach;?>
        </select>
        <input type="submit" name="sub" value="показать статистеку">
    <table>
        <tr>
            <th>Личность</th>
            <th>Результать</th>
        </tr>
        <?php foreach($total as $value):?>
        <tr>
            <td>
                <?=$value['name'];?>
            </td>
            <td>
                <?=$value['rank'];?>
            </td>
        </tr>
        <?php endforeach; ?>
    </table>
    </form>
</div>