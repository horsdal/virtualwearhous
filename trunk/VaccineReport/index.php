<html>
	<head>
		<link rel="stylesheet" type="text/css" href="style.css" >
	</head>
	<body>
	<form action="processReport.php" method="post" name="sd">
		<table cellspacing="0" cellpadding="0" id="maintable" align="center">
			<tr>
				<td colspan="3" style="text-align:center;"><h1>Vaccine arrival report</h1></td>
			</tr>
			<tr>
				<td>
					<table cellspacing="0" cellpadding="0"  class="inline_table" style="vertical-align:bottom;">
						<tr>
							<td width="150px" class="descriptionfieldtop">COUNTRY</td>
							<td width="300px" class="inputfieldtop"><input type="text" name="countryL"></td>
						</tr>
						<tr>
							<td width="150px" class="descriptionfield">REPORT No.</td>
							<td class="inputfield"><input type="text" name="reportno"></td>
						</tr>
					</table>
				</td>
				
				<td style="vertical-align:top;">
					<table cellspacing="0" cellpadding="0"  class="inline_table">
						<tr>
							<td></td>
							<td></td>
							<td></td>
						</tr>
						<tr>
							<td width="50"></td>
							<td width="200px" class="descriptionfield">Date of report (dd/mm-yyyy)</td>
							<td  width="200" class="inputfield">
								<select name="dateofreportday">
									<? for($i=1;$i<=31;$i++){
									echo "<option value='";
										if($i<10){
											echo "0";
										}
									echo $i."'>".$i."</option>";
									}
									?>
								</select> / 
								<select name="dateofreportmonth">
									<? for($i=1;$i<=12;$i++){
									echo "<option value='";
										if($i<10){
											echo "0";
										}
									echo $i."'>".$i."</option>";
									}
									?>
								</select> - 
								<select name="dateofreportyear">
									<? for($i=2010;$i>=2000;$i--){
									echo "<option value='";
										if($i<10){
											echo "0";
										}
									echo $i."'>".$i."</option>";
									}
									?>
								</select>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			
			<tr>
				<td colspan="3">
					<table cellspacing="0" cellpadding="0"  class="inline_table" style="vertical-align:bottom;">
						<tr>
							<td width="450px" class="descriptionfieldtop">Place, Date and Time of Inspection</td>
							<td width="450px"class="inputfieldtop">Name of cold store, date and time vaccines entered into cold store
</td>
						</tr>
						<tr>
							<td class="descriptionfield">
							<select name="dataplacetimeday">
									<? for($i=1;$i<=31;$i++){
									echo "<option value='";
										if($i<10){
											echo "0";
										}
									echo $i."'>".$i."</option>";
									}
									?>
								</select> / 
								<select name="dataplacetimemonth">
									<? for($i=1;$i<=12;$i++){
									echo "<option value='";
										if($i<10){
											echo "0";
										}
									echo $i."'>".$i."</option>";
									}
									?>
								</select> - 
								<select name="dataplacetimeyear">
									<? for($i=2010;$i>=2000;$i--){
									echo "<option value='";
										if($i<10){
											echo "0";
										}
									echo $i."'>".$i."</option>";
									}
									?>
								</select>
							</td>
							<td class="inputfield"><input type="text" name="nameofcoldstore" style="width:420px;"></td>
						</tr>
					</table>
				</td>
			</tr>
			
			<tr>
				<td colspan="3" style=""><h3>PART I-ADVANCE NOTICE<h3></td>
			</tr>
			
			<tr>
				<td colspan="3">
					<table cellspacing="0" cellpadding="0"  class="inline_table" style="vertical-align:bottom;">
						<tr>
							<td width="150px" class="descriptionfieldtop">MAIN DOCUMENTS</td>
							<td width="150px" class="inputfieldtop">Date received by consignee</td>
							<td width="150px" class="inputfieldtop">Copy airway bill (AWB)</td>
							<td width="150px" class="inputfieldtop">Copy of packing list</td>
							<td width="150px" class="inputfieldtop">Copy of invoice</td>
							<td width="150px"class="inputfieldtop">Copy of lot release certificate </td>
						</tr>
						<tr>
							<td class="descriptionfieldmiddle">Pre-advice</td>
							<td class="inputfieldmiddle"><input type="text" name="preadvice"></td>
							<td colspan="4" class="inputfield" style="background-color:black;"></td>
						</tr>
						<tr>
							<td width="150px" class="descriptionfield">Shipping notification</td>
							<td width="150px" class="inputfield"><input type="text" name="shippingnotification"></td>
							<td width="150px" class="inputfield">
								<input type="radio" name="awbr" value="Yes"> Yes 
								<input type="radio" name="awbr" value="No" checked> No
							</td>
							<td width="150px" class="inputfield">
								<input type="radio" name="packagelist" value="Yes"> Yes 
								<input type="radio" name="packagelist" value="No" checked> No
							</td>
							<td width="150px" class="inputfield">
								<input type="radio" name="invoice" value="Yes"> Yes 
								<input type="radio" name="invoice" value="No" checked> No
							</td>
							<td width="150px"class="inputfield">
								<input type="radio" name="certificate" value="Yes"> Yes 
								<input type="radio" name="certificate" value="No" checked> No
							</td>
						</tr>
					</table>
				</td>
			</tr>
			
			<tr>
				<td colspan="3">
					<table cellspacing="0" cellpadding="0"  class="inline_table" style="vertical-align:bottom;">
						<tr>
							<td width="450px" class="descriptionfield">List other documents (if requested)</td>
							<td width="450px"class="inputfield"><textarea name="listofdocuments" style="width:420px;"></textarea></td>
						</tr>
					</table>
				</td>
			</tr>

			<tr>
				<td colspan="3" style=""><h3>PART II- FLIGHT ARRIVAL DETAILS<h3></td>
			</tr>
			
			<tr>
				<td colspan="3">
					<table cellspacing="0" cellpadding="0"  class="inline_table" style="vertical-align:bottom;" width="900px">
						<tr>
							<td rowspan="2"class="descriptionfieldtop">AWB number</td>
							<td rowspan="2" class="inputfieldtop">Airport of destination</td>
							<td rowspan="2" class="inputfieldtop">Flight No</td>
							<td colspan="2" class="inputfieldtop">ETA as per notification</td>
							<td colspan="2" class="inputfieldtop">Actual time of arrival</td>
						</tr>
						<tr>
							<td class="inputfieldmiddle">Date</td>
							<td class="inputfieldmiddle">Date</td>
							<td class="inputfieldmiddle">Date</td>
							<td class="inputfieldmiddle">Date</td>
						</tr>
						<tr>
							<td class="descriptionfield"><input type="text" name="awbnumber" style="width:100px;"></td>
							<td class="inputfield"><input type="text" name="destination" style="width:100px;"></td>
							<td class="inputfield"><input type="text" name="flightnumber" style="width:100px;"></td>
							<td class="inputfield"><input type="text" name="ETA1" style="width:100px;"></td>
							<td class="inputfield"><input type="text" name="ETA2" style="width:100px;"></td>
							<td class="inputfield"><input type="text" name="actualtime1" style="width:100px;"></td>
							<td class="inputfield"><input type="text" name="actualtime2" style="width:100px;"></td>
						</tr>
					</table>
				</td>
			</tr>
			
			<tr>
				<td colspan="3" style="">NAME OF CLEARING AGENT: <input type="text" name="nameofclearingagent" style="width:100px;"> ON BEHALF OF: <input type="text" name="onbehalfof" style="width:100px;"></td>
			</tr>
			
			<tr>
				<td colspan="3" style=""><h3>PART III- DETAILS OF VACCINE SHIPMENT<h3></td>
			</tr>
			
			<tr>
				<td colspan="3">
					<table cellspacing="0" cellpadding="0"  class="inline_table" style="vertical-align:bottom;" width="900px">
						<tr>
							<td class="descriptionfieldtop">Purchase order No.</td>
							<td class="inputfieldtop">Consignee</td>
							<td class="inputfieldtop">Vaccine description (Type and doses/vial) </td>
							<td class="inputfieldtop">Manufacturer</td>
							<td class="inputfieldtop">Country</td>
						</tr>
						<tr>
							<td class="descriptionfield"><input type="text" name="purchaseordernumber" style="width:100px;"></td>
							<td class="inputfield"><input type="text" name="consignee" style="width:100px;"></td>
							<td class="inputfield"><input type="text" name="vaccinedescription" style="width:100px;"></td>
							<td class="inputfield"><input type="text" name="menufacturer" style="width:100px;"></td>
							<td class="inputfield"><input type="text" name="country" style="width:100px;"></td>
						</tr>
					</table>
				</td>
			</tr>
			
			<tr>
				<td colspan="3">
					<table cellspacing="0" cellpadding="0"  class="inline_table" style="vertical-align:bottom;" width="900px">
						<tr>
							<td colspan="4" class="descriptionfieldtop">Vaccine</td>
							<td colspan="4" class="inputfieldtop">Diluent/droppers</td>
						</tr>
						<tr>
							<td class="descriptionfieldmiddle">Lot number</td>
							<td class="inputfieldmiddle">Number of boxes</td>
							<td class="inputfieldmiddle">Number of vials</td>
							<td class="inputfieldmiddle">Expiry date</td>
							<td class="inputfieldmiddle">Lot number</td>
							<td class="inputfieldmiddle">Number of boxes</td>
							<td class="inputfieldmiddle">Number of units</td>
							<td class="inputfieldmiddle">Expiry date</td>
						</tr>
						<tr>
							<td class="descriptionfieldmiddle"><input type="text" name="lot1a" style="width:100px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="numboxes1a" style="width:100px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="numvials1a" style="width:100px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="expirydate1a" style="width:100px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="lot1b" style="width:100px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="numboxes1b" style="width:100px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="numunits1b" style="width:100px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="expirydate1b" style="width:100px;"></td>
						</tr>
						<tr>
							<td class="descriptionfieldmiddle"><input type="text" name="lot2a" style="width:100px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="numboxes2a" style="width:100px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="numvials2a" style="width:100px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="expirydate2a" style="width:100px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="lot2b" style="width:100px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="numboxes2b" style="width:100px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="numunits2b" style="width:100px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="expirydate2b" style="width:100px;"></td>
						</tr>
						<tr>
							<td class="descriptionfieldmiddle"><input type="text" name="lot3a" style="width:100px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="numboxes3a" style="width:100px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="numvials3a" style="width:100px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="expirydate3a" style="width:100px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="lot3b" style="width:100px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="numboxes3b" style="width:100px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="numunits3b" style="width:100px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="expirydate3b" style="width:100px;"></td>
						</tr>
						<tr>
							<td class="descriptionfieldmiddle"><input type="text" name="lot4a" style="width:100px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="numboxes4a" style="width:100px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="numvials4a" style="width:100px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="expirydate4a" style="width:100px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="lot4b" style="width:100px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="numboxes4b" style="width:100px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="numunits4b" style="width:100px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="expirydate4b" style="width:100px;"></td>
						</tr>
						<tr>
							<td class="descriptionfield"><input type="text" name="lot4a" style="width:100px;"></td>
							<td class="inputfield"><input type="text" name="numboxes4a" style="width:100px;"></td>
							<td class="inputfield"><input type="text" name="numvials4a" style="width:100px;"></td>
							<td class="inputfield"><input type="text" name="expirydate4a" style="width:100px;"></td>
							<td class="inputfield"><input type="text" name="lot4b" style="width:100px;"></td>
							<td class="inputfield"><input type="text" name="numboxes4b" style="width:100px;"></td>
							<td class="inputfield"><input type="text" name="numunits4b" style="width:100px;"></td>
							<td class="inputfield"><input type="text" name="expirydate4b" style="width:100px;"></td>
						</tr>
					</table>
				</td>
			</tr>
			
			<tr>
				<td colspan="3" style="">(Continue on separate sheet if necessary)</td>
			</tr>
			
			<tr>
				<td colspan="3">
					<table cellspacing="0" cellpadding="0"  class="inline_table" style="vertical-align:bottom;" width="900px">
						<tr>
							<td class="descriptionfieldtop">&nbsp;</td>
							<td class="inputfieldtop">Yes</td>
							<td class="inputfieldtop">No</td>
							<td class="inputfieldtop">Comments</td>
						</tr>
						<tr>
							<td class="descriptionfieldmiddle">Was quantity received as per shipping notification?</td>
							<td class="inputfieldmiddle"><input type="radio" name="noti" value="Yes"></td>
							<td class="inputfieldmiddle"><input type="radio" name="noti" value="No" checked></td>
							<td class="inputfieldmiddle"><input type="text" name="noticomments" style="width:400px;"></td>
						</tr>
						<tr>
							<td class="descriptionfield">If not, were details of short-shipment provided prior to vaccine arrival?</td>
							<td class="inputfield"><input type="radio" name="priottoarival" value="Yes"></td>
							<td class="inputfield"><input type="radio" name="priottoarival" value="No" checked></td>
							<td class="inputfield"><input type="text" name="priottoarivalcomments" style="width:400px;"></td>
						</tr>
					</table>
				</td>
			</tr>
			
			<tr>
				<td colspan="3">
					<table cellspacing="0" cellpadding="0"  class="inline_table" style="vertical-align:bottom;" width="900px">
						<tr>
							<td width="700px">&nbsp;</td>
							<td class="descriptionfield" width="80px">Report No.</td>
							<td class="inputfield"><input type="text" name="reportnumber" style="width:200px;"></td>
						</tr>
					</table>
				</td>
			</tr>
			
			<tr>
				<td colspan="3" style=""><h3>PART IV-DOCUMENTS ACCOMPANYING THE SHIPMENT</h3></td>
			</tr>
			
			<tr>
				<td colspan="3">
					<table cellspacing="0" cellpadding="0"  class="inline_table" style="vertical-align:bottom;" width="900px">
						<tr>
							<td class="descriptionfieldtop">Invoice</td>
							<td class="inputfieldtop">Packing list</td>
							<td class="inputfieldtop">Lot release certificate</td>
							<td class="inputfieldtop">Vaccine arrival report</td>
							<td class="inputfieldtop">Other</td>
						</tr>
						<tr>
							<td class="descriptionfieldmiddle"><input type="radio" name="invoiceN" value="Yes"> Yes<input type="radio" name="invoiceN" value="No" checked> No</td>
							<td class="inputfieldmiddle"><input type="radio" name="packinglistN" value="Yes"> Yes<input type="radio" name="packinglistN" value="No" checked> No</td>
							<td class="inputfieldmiddle"><input type="radio" name="releasecertificateN" value="Yes"> Yes<input type="radio" name="releasecertificateN" value="No" checked> No</td>
							<td class="inputfieldmiddle"><input type="radio" name="vaccinearrivalN" value="Yes"> Yes<input type="radio" name="vaccinearrivalN" value="No" checked> No</td>
							<td class="inputfieldmiddle"><input type="text" name ="other" width="100px"></td>
						</tr>
						<tr>
							<td class="descriptionfield">Comments</td>
							<td colspan="4" class="inputfield"><input type="text" name="part4comments" style="width:700px;"></td>
						</tr>
					</table>
				</td>
			</tr>
			
			<tr>
				<td colspan="3" style=""><h3>PART V- STATUS OF SHIPPING INDICATORS</h3></td>
			</tr>
			
			<tr>
				<td colspan="3">
					<table cellspacing="0" cellpadding="0"  class="inline_table" style="vertical-align:bottom;" width="900px">
						<tr>
							<td class="descriptionfieldtop">Total number of boxes inspected</td>
							<td class="inputfieldtop">
								<input type="text" name="tnumofboxesinspected" style="width:200px;">
							</td>
						</tr>
						<tr>
							<td class="descriptionfieldmiddle">Coolant type:</td>
							<td class="inputfieldmiddle">
								<input type="radio" name="coolanttyped" value="Dry ice"> Dry ice
								<input type="radio" name="coolanttyped" value="Icepacks" checked> Icepacks
								<input type="radio" name="coolanttyped" value="No coolant"> No coolant
							</td>
						</tr>
						<tr>
							<td class="descriptionfield">Temperature monitors present:</td>
							<td colspan="4" class="inputfield">
								<input type="radio" name="temperaturemonitorpresent" value="VVM"> VVM
								<input type="radio" name="temperaturemonitorpresent" value="Cold chain card"> Cold chain card
								<input type="radio" name="temperaturemonitorpresent" value="Electronic device"> Electronic device
							</td>
						</tr>
					</table>
				</td>
			</tr>
			
			<tr>
				<td colspan="3" style=""><h3>PROVIDE BELOW DETAILS OF STATUS ONLY WHEN PROBLEMS ARE OBSERVED:</h3></td>
			</tr>
			
			<tr>
				<td colspan="3">
					<table cellspacing="0" cellpadding="0"  class="inline_table" style="vertical-align:bottom;" width="900px">
						<tr>
							<td rowspan="2" class="descriptionfieldtop">Box number</td>
							<td rowspan="2" class="inputfieldtop">Lot number</td>
							<td colspan="2" class="inputfieldtop">Alarm in electronic device </td>
							<td colspan="4" class="inputfieldtop">Vaccine vial monitor</td>
							<td colspan="4" class="inputfieldtop">Cold Chain Monitor</td>
							<td rowspan="2" class="inputfieldtop">Date/time of inspection</td>
						</tr>
						<tr>
							<td class="descriptionfieldmiddle">Yes</td>
							<td class="inputfieldmiddle">No</td>
							<td class="inputfieldmiddle">1</td>
							<td class="inputfieldmiddle">2</td>
							<td class="inputfieldmiddle">3</td>
							<td class="inputfieldmiddle">4</td>
							<td class="inputfieldmiddle">A</td>
							<td class="inputfieldmiddle">B</td>
							<td class="inputfieldmiddle">C</td>
							<td class="inputfieldmiddle">D</td>
						</tr>
						<?php
						for($i=1;$i<2;$i++){//i<10
						echo '<tr>
							<td class="descriptionfieldmiddle"><input type="text" name="boxnumber_'.$i.'" style="width:75px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="lotnumberbox_'.$i.'" style="width:150px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="alarmY_'.$i.'" style="width:75px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="alarmN_'.$i.'" style="width:75px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="vaccinemonitor1_'.$i.'" style="width:50px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="vaccinemonitor2_'.$i.'" style="width:50px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="vaccinemonitor3_'.$i.'" style="width:50px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="vaccinemonitor4_'.$i.'" style="width:50px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="vaccinemonitorA_'.$i.'" style="width:50px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="vaccinemonitorB_'.$i.'" style="width:50px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="vaccinemonitorC_'.$i.'" style="width:50px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="vaccinemonitorD_'.$i.'" style="width:50px;"></td>
							<td class="inputfieldmiddle"><input type="text" name="timeofinspection_'.$i.'" style="width:100px;"></td>
						</tr>';
						}
						?>
						<tr>
							<td class="descriptionfield"><input type="text" name="boxnumber_10" style="width:75px;"></td>
							<td class="inputfield"><input type="text" name="lotnumberbox_10" style="width:150px;"></td>
							<td class="inputfield"><input type="text" name="alarmY_10" style="width:75px;"></td>
							<td class="inputfield"><input type="text" name="alarmN_10" style="width:75px;"></td>
							<td class="inputfield"><input type="text" name="vaccinemonitor1_10" style="width:50px;"></td>
							<td class="inputfield"><input type="text" name="vaccinemonitor2_10" style="width:50px;"></td>
							<td class="inputfield"><input type="text" name="vaccinemonitor3_10" style="width:50px;"></td>
							<td class="inputfield"><input type="text" name="vaccinemonitor4_10" style="width:50px;"></td>
							<td class="inputfield"><input type="text" name="vaccinemonitorA_10" style="width:50px;"></td>
							<td class="inputfield"><input type="text" name="vaccinemonitorB_10" style="width:50px;"></td>
							<td class="inputfield"><input type="text" name="vaccinemonitorC_10" style="width:50px;"></td>
							<td class="inputfield"><input type="text" name="vaccinemonitorD_10" style="width:50px;"></td>
							<td class="inputfield"><input type="text" name="timeofinspection_10" style="width:100px;"></td>
						</tr>
					</table>
				</td>
			</tr>
			
			<tr>
				<td colspan="3" style="">(Continue on separate sheet if necessary)</td>
			</tr>
			
			<tr>
				<td colspan="3" style=""><h3>PART VI- GENERAL CONDITIONS OF SHIPMENT</h3></td>
			</tr>
			
						<tr>
				<td colspan="3">
					<table cellspacing="0" cellpadding="0"  class="inline_table" style="vertical-align:bottom;" width="900px">
						<tr>
							<td width="300px" class="descriptionfieldtop">What was the condition of boxes on arrival?</td>
							<td class="inputfieldtop">
								<input type="text" name="boxcondition" style="width:500px;">
							</td>
						</tr>
						<tr>
							<td class="descriptionfieldmiddle">Were necessary labels attached to shipping boxes?</td>
							<td class="inputfieldmiddle">
								<input type="text" name="necessarylabelsattached" style="width:500px;">
							</td>
						</tr>
						<tr>
							<td class="descriptionfield">Other comments:<br>(continued in separate sheet if necessary)
</td>
							<td colspan="4" class="inputfield">
								<textarea width="500" type="text" name="otherscommentspackage" style="width:500px;"></textarea>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			
			<tr>
				<td colspan="3" style=""><h3>PART VI- GENERAL CONDITIONS OF SHIPMENT</h3></td>
			</tr>
			
			<tr>
				<td colspan="3" style=""><input type="submit" value="Send to UNICEF" style="width:500px;height:50px;"></td>
			</tr>
		</table>
	</form>
	</body>
</html>
