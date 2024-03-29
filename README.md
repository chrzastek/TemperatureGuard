# TemperatureGuard
.NET Core Console App designed to be running on Raspberry Pi device. <br/>
Program allows to display local temperature and humidity on LCD display. Also it has built-in guard, that will trigger the alarm
if the temperature will exceed given threshold.

<h2>Hardware:</h2>
<ul>
  <li>Raspberry Pi</li>
  <li>Breadboard</li>
  <li>Connection cables</li>
  <li>Microswitch</li>
  <li>Buzzer 5V</li>
  <li>DHT11 sensor</li>
  <li>DS18B20+ sensor</li>
  <li>LCD 2x16 Display</li>
  <li>Diode</li>
  <li>Resistors: 10k Ohm, 2x 4,7k Ohm and 220 Ohm</li>
</ul>

<h2>Software on Rpi</h2>
<ul>
  <li>Raspbian OS</li>
  <li>.NET Core Runtime 2.0 or newer</li>
</ul>

<h2>Algorithm graph</h2>
<img src="https://github.com/chrzastek/TemperatureGuard/blob/master/algorithm.png?raw=true"/>

<h2>Photos</h2>

<h4>Whole</h4>
<img src="https://github.com/chrzastek/TemperatureGuard/blob/master/photos/whole.jpg?raw=true"/>

<h4>Alarm</h4>
<img src="https://github.com/chrzastek/TemperatureGuard/blob/master/photos/alarm.jpg?raw=true"/>

<h4>Waiting</h4>
<img src="https://github.com/chrzastek/TemperatureGuard/blob/master/photos/waiting.jpg?raw=true"/>
