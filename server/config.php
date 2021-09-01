<?php
if(!empty($_POST)){
	$configData = $_POST["data"];
	$file = fopen("configData.json", "w");
	fwrite($file, $configData);
	fclose($file);
	echo "It Works";
	//exec("./ledzisko.py");
}else{
	echo "It doesnt work";
	
	//exec("./ledzisko.py");
	//echo exec("ledzisko.py");
}


?>
