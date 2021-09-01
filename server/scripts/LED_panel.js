class RGB{ 
	
	constructor(i,a1,a2,a3,rgb){
		this.col = rgb;
		this.r=a1;
		this.g=a2;
		this.b=a3;
		this.x = parseInt(i/8);
		this.y = i%8;
	}
}
var LED = [];
var LEDjson;
var LEDcreeper = [{"x":0,"y":0,"r":255,"g":255,"b":255},{"x":1,"y":0,"r":255,"g":255,"b":255},{"x":2,"y":0,"r":105,"g":105,"b":105},{"x":3,"y":0,"r":105,"g":105,"b":105},{"x":4,"y":0,"r":255,"g":255,"b":255},{"x":5,"y":0,"r":255,"g":255,"b":255},{"x":6,"y":0,"r":255,"g":255,"b":255},{"x":7,"y":0,"r":105,"g":105,"b":105},{"x":0,"y":1,"r":255,"g":255,"b":255},{"x":1,"y":1,"r":105,"g":105,"b":105},{"x":2,"y":1,"r":105,"g":105,"b":105},{"x":3,"y":1,"r":105,"g":105,"b":105},{"x":4,"y":1,"r":105,"g":105,"b":105},{"x":5,"y":1,"r":255,"g":255,"b":255},{"x":6,"y":1,"r":105,"g":105,"b":105},{"x":7,"y":1,"r":105,"g":105,"b":105},{"x":0,"y":2,"r":105,"g":105,"b":105},{"x":1,"y":2,"r":105,"g":105,"b":105},{"x":2,"y":2,"r":178,"g":34,"b":34},{"x":3,"y":2,"r":105,"g":105,"b":105},{"x":4,"y":2,"r":105,"g":105,"b":105},{"x":5,"y":2,"r":105,"g":105,"b":105},{"x":6,"y":2,"r":105,"g":105,"b":105},{"x":7,"y":2,"r":105,"g":105,"b":105},{"x":0,"y":3,"r":105,"g":105,"b":105},{"x":1,"y":3,"r":105,"g":105,"b":105},{"x":2,"y":3,"r":105,"g":105,"b":105},{"x":3,"y":3,"r":105,"g":105,"b":105},{"x":4,"y":3,"r":211,"g":211,"b":211},{"x":5,"y":3,"r":105,"g":105,"b":105},{"x":6,"y":3,"r":105,"g":105,"b":105},{"x":7,"y":3,"r":105,"g":105,"b":105},{"x":0,"y":4,"r":105,"g":105,"b":105},{"x":1,"y":4,"r":105,"g":105,"b":105},{"x":2,"y":4,"r":105,"g":105,"b":105},{"x":3,"y":4,"r":105,"g":105,"b":105},{"x":4,"y":4,"r":211,"g":211,"b":211},{"x":5,"y":4,"r":105,"g":105,"b":105},{"x":6,"y":4,"r":105,"g":105,"b":105},{"x":7,"y":4,"r":105,"g":105,"b":105},{"x":0,"y":5,"r":105,"g":105,"b":105},{"x":1,"y":5,"r":105,"g":105,"b":105},{"x":2,"y":5,"r":178,"g":34,"b":34},{"x":3,"y":5,"r":105,"g":105,"b":105},{"x":4,"y":5,"r":105,"g":105,"b":105},{"x":5,"y":5,"r":105,"g":105,"b":105},{"x":6,"y":5,"r":105,"g":105,"b":105},{"x":7,"y":5,"r":105,"g":105,"b":105},{"x":0,"y":6,"r":255,"g":255,"b":255},{"x":1,"y":6,"r":105,"g":105,"b":105},{"x":2,"y":6,"r":105,"g":105,"b":105},{"x":3,"y":6,"r":105,"g":105,"b":105},{"x":4,"y":6,"r":105,"g":105,"b":105},{"x":5,"y":6,"r":255,"g":255,"b":255},{"x":6,"y":6,"r":105,"g":105,"b":105},{"x":7,"y":6,"r":105,"g":105,"b":105},{"x":0,"y":7,"r":255,"g":255,"b":255},{"x":1,"y":7,"r":255,"g":255,"b":255},{"x":2,"y":7,"r":105,"g":105,"b":105},{"x":3,"y":7,"r":105,"g":105,"b":105},{"x":4,"y":7,"r":255,"g":255,"b":255},{"x":5,"y":7,"r":255,"g":255,"b":255},{"x":6,"y":7,"r":255,"g":255,"b":255},{"x":7,"y":7,"r":105,"g":105,"b":105}]
var state =0;
const url = 'reader.php';


function presetHandler(){
	for(i=0; i<64;i++){
	document.getElementById('l'+i).style.background = "rgb("+LEDcreeper[i].r+", "+LEDcreeper[i].g+", "+LEDcreeper[i].b+")";
	}
	LEDjson = JSON.stringify(LEDcreeper);
 sendData(url, LEDjson);
}

function ledHandler(e){
	if(state == 0){
	const value = document.getElementById('colorpicker').value;
	document.getElementById(e).style.background = value;
}else if(state == 1){
		document.getElementById(e).style.background = "";
}
for (i=0; i<64; i++){
		
		var a = document.getElementById('l'+i).style.background;
		
		var x = a.toRGB();
		temp = new RGB(i,x.r,x.g,x.b,a);
		LED[i] = temp;  
}
LEDjson = JSON.stringify(LED);
 sendData(url, LEDjson);
}



function resetOneHandler(){
	if(state == 0) {state = 1;}
	else if(state == 1) {state = 0;}
}



function resetAllHandler(){
	 for (i=0; i<64; i++){
		
		document.getElementById('l'+i).style.background="";
		temp = new RGB(i,0,0,0);
		LED[i] = temp;  
}
	 LEDjson = JSON.stringify(LED);
 sendData(url, LEDjson);
 }



function sendData(rurl, rdata){
	$.ajax({
		url: rurl,
		data: {"data" : rdata},
		type: 'POST',
		dataType: 'text/json',
		success: function(response){console.log(response);},
		failure: function(response){console.log(response);}
		});
	
	
}



String.prototype.toRGB = function() {

  var rgb = this.split( ',' ) ;
  this.r=parseInt( rgb[0].substring(4) ) ; // skip rgb(
  this.g=parseInt( rgb[1] ) ; // this is just g
  this.b=parseInt( rgb[2] ) ; // parseInt scraps trailing )
	return this;
}


