<?php
$countryL=$_POST["countryL"];
$reportno=$_POST["reportno"];
$dateofreportday=$_POST["dateofreportday"];
$dateofreportmonth=$_POST["dateofreportmonth"];
$dateofreportyear=$_POST["dateofreportyear"];
$dataplacetimeday=$_POST["dataplacetimeday"];
$dataplacetimemonth=$_POST["dataplacetimemonth"];
$dataplacetimeyear=$_POST["dataplacetimeyear"];
$nameofcoldstore=$_POST["nameofcoldstore"];
$preadvice=$_POST["preadvice"];
$awb=$_POST["awbr"];
$packagelist=$_POST["packagelist"];

$dateofreportforDB=$dateofreportyear."-".$dateofreportmonth."-".$dateofreportday;
$dataplacetimeforDB=$dataplacetimeyear."-".$dataplacetimemonth."-".$dataplacetimeday;

echo 
"Country: ".$countryL."<br>".
"Report No: ".$reportno."<br>".
"Date of report: ".$dateofreportday." / ".$dateofreportmonth." - ".$dateofreportyear." (".$dateofreportforDB.")<br>".
"Place, Date and Time of Inspection: ".$dataplacetimeday." / ".$dataplacetimemonth."- ".$dataplacetimeyear." (".$dataplacetimeforDB.")<br>".
"Name of cold store, date and time vaccines entered into cold store: ".$nameofcoldstore."<br>".
"Pre-advice: ".$preadvice."<br>".
"Copy airway bill (AWB): ".$awb."<br>".
"Copy of package list: ".$packagelist."<br>";

mysql_connect('localhost', 'user', 'password') or die(mysql_error());
mysql_select_db('database') or die(mysql_error());
$result = mysql_query("INSERT INTO arrivals (country, reportnumber, dateofreport, inspectiontime, coldstore, preadvice, awb, packagelist) VALUES ('$countryL','$reportno','$dateofreportforDB','$dataplacetimeforDB', '$nameofcoldstore', '$preadvice', '$awb', '$packagelist')") or die(mysql_error());

if($result){
	echo "<br><b>Data was correctly inserted in the database.</b>";
} else {
	echo "<br><b>An error occured when trying to insert data into the database</b>";
}


$recipient = "unicef@denix.dk";
		
$subject = "New vaccine arrival report";
$message = "Dear $recipient: \n\n
A new vaccine arrival report has been added.".
"Country: ".$countryL."<br>".
"Report No: ".$reportno."<br>".
"Date of report: ".$dateofreportday." / ".$dateofreportmonth." - ".$dateofreportyear." (".$dateofreportforDB.")<br>".
"Place, Date and Time of Inspection: ".$dataplacetimeday." / ".$dataplacetimemonth."- ".$dataplacetimeyear." (".$dataplacetimeforDB.")<br>".
"Name of cold store, date and time vaccines entered into cold store: ".$nameofcoldstore."<br>".
"Pre-advice: ".$preadvice."<br>".
"Copy airway bill (AWB): ".$awb."<br>".
"Copy of package list: ".$packagelist."<br>";
					
$header = "from:noreply@uniceflogisticks.com";

if(mail($recipient, $subject, $message, $header)){
	echo "<br><br><b>An email has been send to ".$recipient."</b>";
} else {
	echo "<br><br><b>An error occured when trying to send an email to ".$recipient."</b>";
}

?>
