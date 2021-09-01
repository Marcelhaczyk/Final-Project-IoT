
const urlik = 'alldata.json';
var sampleTimeMsec = 100;	
var sampleTimeSec = sampleTimeMsec/1000;

var czyDodana  = true;
var data;
var dataSize;

function updateTable(){
	var table = document.getElementById("TABLE");
}

function addRow() {
          
    var myName = document.getElementById("name");
    var age = document.getElementById("age");
    var table = document.getElementById("myTableData");
 
    var rowCount = table.rows.length;
    var row = table.insertRow(rowCount);
 
    row.insertCell(0).innerHTML= '<input type="button" value = "Delete" onClick="Javacsript:deleteRow(this)">';
    row.insertCell(1).innerHTML= myName.value;
    row.insertCell(2).innerHTML= age.value;
 
}
 function ajaxJSON() {
	$.ajax({
        url: urlik,
		type: 'GET', dataType: 'json',
		success: function(responseJSON, status, xhr) {
			// assign names and units to labels
			var nowaTablica = new Array(0);
			data = responseJSON;
			
			addTable();
			
		
		}
	});
}
 
 
function deleteRow(obj) {
      
   
    var table = document.getElementById("TABLE");
    
    
    for (i = 0; i < 6; i++)
	{
		table.deleteRow(i);
	}
    
    
}
 
function addTable() {
      
    var myTableDiv = document.getElementById("myDynamicTable");
    
    dataSize = data.length;
    var table = document.createElement('TABLE');
    table.border='1';
    
    var tableBody = document.createElement('TBODY');
    table.appendChild(tableBody);
      
    for (var i=0; i<dataSize; i++){
       var tr = document.createElement('TR');
       tableBody.appendChild(tr);
       
       var columns = 2;
	   var keys =  Object.keys(data[i]);
	   var helpArray = new Array(3);
	   
	   var td = document.createElement('TD');
           td.width='400';
       if(Array.isArray(data[i][keys[0]])) {
		   var dlugosc = data[i][keys[0]].length;
		   columns += (dlugosc-1);
		   helpArray = data[i][keys[0]];
		   for (var j=0; j<columns; j++){
           
           if(j==0){
				td.appendChild(document.createTextNode(String(keys[0]).concat(" ["+String(data[i][keys[1]])+"] : ")));
           }
           else {
			   var helping  = [Object.keys(helpArray[0]), Object.keys(helpArray[1]),Object.keys(helpArray[2])];
			   td.appendChild(document.createTextNode(" ("+String(helpArray[j-1][helping[j-1]])+")"));
		   }
           tr.appendChild(td);
       }
		   
		   
		   }
		   else {
			   
			   td.appendChild(document.createTextNode(String(keys[0]).concat("["+String(data[i].Unit)+"] : ")));
			   tr.appendChild(td);
			   td.appendChild(document.createTextNode(String(data[i][keys[0]])));
			   tr.appendChild(td);
		   }
       
       
       
    }
    var childer = myTableDiv.childNodes[0];
	myTableDiv.replaceChild(table, childer);
    //myTableDiv.appendChild(table);
    
}
function startTimer(){
	timer = setInterval(ajaxJSON, sampleTimeMsec);
}

function stopTimer(){
	clearInterval(timer);
} 
 
function load() {
	$.ajax(urlik, {
		type: 'GET', dataType: 'json',
		success: function(responseJSON, status, xhr) {
			dataSize = responseJSON.length;
			data = responseJSON;
		
	
	startTimer();
		},
	}); 
    
    console.log("Page load finished");
 
}
function tableInit(){
	
	
}
$(document).ready(() => { 
	
	
	
	
	
	
	
});
