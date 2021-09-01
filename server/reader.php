<?php
if(!empty($_POST)){
	$configData = $_POST["data"];
	$file = fopen("config.json", "w");
	fwrite($file, $configData);
	fclose($file);
	echo "It Works";
	}else{
	echo "It doesnt work";
	
	}


?>
