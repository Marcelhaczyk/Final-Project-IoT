#!/usr/bin/sudo /usr/bin/python3
import sys
import json
import time
from sense_emu import SenseHat
sense = SenseHat()
sense.clear()

import os
import re

licznik1 = 0
licznik2 =0
licznik3 =0









try:
    while True:
        for event in sense.stick.get_events():
            if event.action == "pressed":
                if event.direction == "up":
                    licznik1=licznik1+1

                elif event.direction == "down":
                    licznik1=licznik1-1

                elif event.direction == "left":
                    licznik2=licznik2-1

                elif event.direction == "right":
                    licznik2=licznik2+1
                elif event.direction == "middle":
                    licznik3=licznik3+1



        temperature = sense.get_temperature()
        temperature = round(temperature, 1)
        pressure = sense.get_pressure()
        pressure = round(pressure, 1)
        humidity = sense.get_humidity()
        humidity = round(humidity, 1)
        #PRZYSPIESZENIE
        a = sense.get_accelerometer_raw()
        ax= a['x']
        ay= a['y']
        az= a['z']
        ax=round(ax,4)
        ay=round(ay,4)
        az=round(az,4)
        #Pole Magnetyczne
        magnetic = sense.get_compass_raw()
        xm= magnetic['x']
        ym= magnetic['y']
        zm= magnetic['z']
        xm=round(xm,4)
        ym=round(ym,4)
        zm=round(zm,4)
     #____________________________#
        o = sense.get_orientation()  #Katy pitch roll yaw
        pitch = o["pitch"]
        pitch =round(pitch,2)
        roll = o["roll"]
        roll = round(roll,2)
        yaw = o["yaw"]
        yaw = round(yaw,2)
        data = [{'Pressure':pressure, 'Unit':'hPa'},
        {'Temperature':temperature, 'Unit':'Celsius Degrees'},
        {'Humidity':humidity, 'Unit':'%'},
        {'Swing':[{'Pitch':pitch},{'Roll':roll},{'Yaw':yaw}], 'Unit':'Degrees'},
        {'Joystick': [{'SX': licznik1}, {'SY': licznik2}, {'SS': licznik3}],'Unit':'Clicks'},
        {'Magnetic':[{'XM':xm},{'YM':ym},{'ZM':zm}], 'Unit':'A/m'},
        {'Acceleration':[{'AX':ax},{'AY':ay},{'AZ':az}], 'Unit':'m/s^2'}]
        try:
            with open('alldata.json','w') as outfile:
                json.dump(data,outfile)
        finally:
            outfile.close()
        time.sleep(0.1)

        with open('config.json','r') as file:
            data=file.read()
            json_array=json.loads(data)
            store_list = []
            for item in json_array:
                X = item['x']

                Y = item ['y']

                if item['r'] == None: R = 0
                else: R = item['r']

                if item['g'] == None: G = 0
                else: G = item['g']

                if item['b'] == None: B = 0
                else: B = item['b']

                sense.set_pixel(X,Y,(R,G,B))
        file.close()
        time.sleep(0.2)
except KeyboardInterrupt:
    pass

