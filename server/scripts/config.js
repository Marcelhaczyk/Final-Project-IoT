class Config{ 
	constructor(st,sa){
		this.sampleTime= parseInt(st);
		this.sampleAmount = parseInt(sa);
	}
}

const url = "config.php";
var TempJson;

function saveHandler(){
	var v = parseInt(document.getElementById('sampleTime').value);
	let sa = parseInt(document.getElementById('sampleAmount').value);
	if(v > 500) {v = 500;}
	temp = new Config(v, sa);
	TempJson = JSON.stringify(temp);
 sendData(url, TempJson);
}

function sendData(rurl, rdata){
	$.ajax({
		url: rurl,
		data: {data : rdata},
		type: 'POST',
		dataType: 'text/json',
		success: function(response){console.log(response);},
		failure: function(response){console.log(response);}
		});
	
	
}
