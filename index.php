<?php
      define("dbserver","127.0.0.1");
				define("dbuser","drechlu14");
				define("dbpass","drechlu14!");
				define("dbname","drechlu14");
				$db = new PDO(
					"mysql:host=" .dbserver.";dbname=".dbname,dbuser,dbpass,
					array(
						PDO::MYSQL_ATTR_INIT_COMMAND => "SET NAMES utf8",
						PDO::MYSQL_ATTR_INIT_COMMAND => "SET CHARACTER SET utf8"

					)
					);

if($_POST["TypOperace"]==0){
		if(isset($_GET["Surname"])){
			$Surname = $_GET["Surname"];
			$result = $db->prepare("SELECT * FROM databaze_osob WHERE Surname LIKE '%$Surname%'");
			$db2 = $result->execute();
	    	header('Content-Type: text/json');
			echo json_encode($result->fetchAll(PDO::FETCH_ASSOC));
		} else {
			$result = $db->query("SELECT * FROM databaze_osob");
	    	header('Content-Type: text/json');
			echo json_encode($result->fetchAll(PDO::FETCH_ASSOC));
		}
}
	
if($_POST["TypOperace"]==1){
		parse_str(file_get_contents("php://input"),$data);
		if(!empty($data)){
			extract ($data);
			$sql = $db->prepare("INSERT INTO `databaze_osob`(`Id`, `Name`, `Surname`, `RC1`, `RC2`, `BirthDate`) VALUES ('','$Name','$Surname','$RC1','$RC2','$BirthDate')");
  			$db2 = $sql->execute();
  			if($db2){
            echo 'Hotovo.' ;        		
        	}
        	elseif($sql->errorCode() == 23000){
        		echo 'Záznam je již v db.' ;
        	}
        	 else{
        		echo 'Error' ;
        	}
		}
}

if($_POST["TypOperace"]==3){
		parse_str(file_get_contents("php://input"),$data);
		if(!empty($data)){
			extract ($data);
			$sql = $db->prepare("UPDATE databaze_osob SET Name = '$Name', Surname = '$Surname', RC1 = '$RC1', RC2 = '$RC2', BirthDate = '$BirthDate' WHERE Id = '$Id'");
  			$db2 = $sql->execute();
  			if($db2){
        		echo 'Upraveno.' ;
        	} else{
        		echo 'Error' ;
        	}
  		}
}

if($_POST["TypOperace"]==2){
		if(!empty($_POST['Id'])){
			$number = $_POST['Id'];
        	$sql = $db->prepare('DELETE FROM databaze_osob WHERE Id = :Id');
			$db2 = $sql->execute(array(':Id' => $number));
			if($db2){
        		echo 'Smazáno.' ;
        	} else{
        		echo 'Error' ;
        	}
		}
}

?>