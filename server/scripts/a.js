var sampleTimeMsec = 100;	
var sampleTimeSec = sampleTimeMsec/1000;
var maxSamplesNumber = 100;				

var ydata;// lets check this shiet
var xdata;// lets check this shiet

var dataSize;




var lastTimeStamp; 



var chartsContext; // lets check this shiet
var charts; // lets check this shiet
var dataKeys;
var timer;// lets check this shiet


const urlik = 'alldata.json';// lets check this shiet

const url1 = '192.168.56.101';
const port = '1000';
const url3 = 'h.json';

const url_config = 'configData.json'; // lets check this shiet

function addData(dataArray){
	if(ydata[0].length > maxSamplesNumber)
	{
		removeOldData();
		lastTimeStamp += sampleTimeSec;
		xdata.push(lastTimeStamp.toFixed(4));
	}
	
	for (i = 0; i < dataArray.length; i++)
	{
		ydata[i].push(dataArray[i]);
    
	}
	
	for (i = 0; i < charts.length; i++)
	{
		charts[i].update();
	}
	
}


function removeOldData(){
	xdata.splice(0,1);
    for(i=0; i<ydata.length; i++) {
        ydata[i].splice(0,1);
    }
}







function startTimer(){
	timer = setInterval(ajaxJSON, sampleTimeMsec);
}

function stopTimer(){
	clearInterval(timer);
}





function ajaxJSON() {
	$.ajax({
        url: urlik,
		type: 'GET', dataType: 'json',
		success: function(responseJSON, status, xhr) {
			// assign names and units to labels
			var nowaTablica = new Array(0);
			for (i = 0; i < dataSize; i++)
			{
				var klucz = Object.keys(responseJSON[i]);
				var name = klucz[0];
				
				if(Array.isArray(responseJSON[i][klucz[0]])){
					var nameKlucz = Object.keys(responseJSON[i][name]);
				var iksde = Object.keys(responseJSON[i][klucz[0]][nameKlucz[0]]);
				var iksde1  = Object.keys(responseJSON[i][klucz[0]][nameKlucz[1]]);
				var iksde2 = Object.keys(responseJSON[i][klucz[0]][nameKlucz[2]]);
				
					charts[i].data.datasets[0].label = String(iksde).concat(": "+String(responseJSON[i][klucz[0]][nameKlucz[0]][iksde[0]]) + ", "+String(iksde1)+": "+String(responseJSON[i][klucz[0]][nameKlucz[1]][iksde1[0]])+", "+String(iksde2)+": "+String(responseJSON[i][klucz[0]][nameKlucz[2]][iksde2[0]])+" [", responseJSON[i].Unit, "]");
					nowaTablica.push(new Array(responseJSON[i][klucz[0]][nameKlucz[0]][iksde[0]],responseJSON[i][klucz[0]][nameKlucz[1]][iksde1[0]], responseJSON[i][klucz[0]][nameKlucz[2]][iksde2[0]] ));
				}
				else{
				charts[i].data.datasets[0].label = String(responseJSON[i][klucz[0]]).concat(" [", responseJSON[i].Unit, "]");
				nowaTablica.push(+responseJSON[i][klucz[0]]);
			}
				
			}
			
			// add data to graph
			
			
			
			addData(nowaTablica);
		}
	});
}

function ajaxJSONconfig() {
	$.ajax({
		url: url_config,
		type: 'GET', dataType: 'json',
		success: function(responseJSON, status, xhr) {
			sampleTimeMsec = responseJSON.sampleTime;
			$("#sampletime").text(sampleTimeMsec.toString());
			$("#samplenumber").text(maxSamplesNumber.toString());
		
		}
	});
	
}

function chartsInit()
{
	charts = new Array(0);
	chartsContext = new Array(0);
	// array with consecutive integers: <0, maxSamplesNumber-1>
	xdata = [...Array(maxSamplesNumber).keys()];
	// scaling all values ​​times the sample time
	xdata.forEach(function(p, i) {this[i] = (this[i]*sampleTimeSec).toFixed(4);}, xdata);

	// last value of 'xdata'
	lastTimeStamp = +xdata[xdata.length-1];

	// empty array
	ydata = new Array(dataSize);
	
	// defalut labels array
	
	default_labels = ["Pressure [hPa]", "Temperature [C]","humidity [%]"  , "joystick [pitch, roll, yaw]", "magnetic [XM, YM, ZM]", "acceleration [AX, AY, AZ]"];

	// get chart context from 'canvas' element
	
	for (i = 0; i <	dataSize; i++)
	{
		chartsContext.push($("#chart".concat(i+1))[0].getContext('2d'));
	}
	let chartx;
	for (i = 0; i < dataSize; i++)
	{
		//var klucze = Object.keys(data[i]);
		
		if(Array.isArray(ydata[i])){
			chartx = new Chart(chartsContext[i], {
		// The type of chart: linear plot
		type: 'line',

		// Dataset: 'xdata' as labels, 'ydata' as dataset.data
		data: {
			labels: xdata,
			datasets: [{
      
      data: ydata[i][0][0],
      borderColor: 'blue'
    }, {
      
      data: ydata[i][1][0],
      borderColor: 'red'
    }, {
      
      data: ydata[i][2][0],
      borderColor: 'green'
			}]
		},

		// Configuration options
		options: {
			responsive: true,
			maintainAspectRatio: false,
			animation: false,
			scales: {
				yAxes: [{
					ticks: {
						stepSize: 5
						},
					scaleLabel: {
						display: true,
						labelString: 'Environmental data'
					}
				}],
				xAxes: [{
					ticks: {
						stepSize: 5
						},
					scaleLabel: {
						display: true,
						labelString: 'Time [s]'
					}
				}]
			}
		}
	});
		}
		else {
		chartx = new Chart(chartsContext[i], {
		// The type of chart: linear plot
		type: 'line',

		// Dataset: 'xdata' as labels, 'ydata' as dataset.data
		data: {
			labels: xdata,
			datasets: [{
				fill: false,
				label: default_labels[i],
				backgroundColor: 'rgb(255, 0, 0)',
				borderColor: 'rgb(255, 0, 0)',
				data: ydata[i],
				lineTension: 0
			}]
		},

		// Configuration options
		options: {
			responsive: true,
			maintainAspectRatio: false,
			animation: false,
			scales: {
				yAxes: [{
					ticks: {
						stepSize: 5
						},
					scaleLabel: {
						display: true,
						labelString: 'Environmental data'
					}
				}],
				xAxes: [{
					ticks: {
						stepSize: 5
						},
					scaleLabel: {
						display: true,
						labelString: 'Time [s]'
					}
				}]
			}
		}
	});
}
	charts.push(chartx);
	}

	

	
	
for (i = 0; i < dataSize; i++)
{
	ydata[i] = charts[i].data.datasets[0].data;
	
}
	xdata = charts[0].data.labels;
}




$(document).ready(() => { 
	$.ajax(url_config, {
		type: 'GET', dataType: 'json',
		success: function(responseJSON, status, xhr) {
			sampleTimeMsec = responseJSON.sampleTime;
			maxSamplesNumber = responseJSON.sampleAmount;
			url1 = responseJSON.ipAdress;
			port = responseJSON.port;
			$("#sampletime").text(sampleTimeMsec.toString());
			$("samplenumber").text(maxSamplesNumber.toString());

		
		}
	});
	$.ajax(urlik, {
		type: 'GET', dataType: 'json',
		success: function(responseJSON, status, xhr) {
			dataSize = responseJSON.length;
			dataKeys = responseJSON;
			ajaxJSONconfig();
	chartsInit();
	
	
	
	$("#start").click(startTimer);
	$("#stop").click(stopTimer);
	
	//$("#samplenumber").text(maxSamplesNumber.toString());
		},
	}); 
	
	
	
});
