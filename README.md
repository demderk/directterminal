# Direct Terminal
**Simple COM port terminal. Fast, easy, nothing extra. Have usefull presets.**

# Quick start guide
 * Download the latest version of [.NET Core](https://www.microsoft.com/net/download).
 * Download [last release](https://github.com/demderk/directterminal/releases).
 * Run a terminal in this directory and write "dotnet DirectTerminal.dll"


# Presets

You may create special presets for fast connect to port. 

## Default preset

### About

Main preset.  Сan be only one. If additional presets disabled or not exists, he selecting by default.

### How to create

Create file config.xml in program directory.

### Avaiable [settings](#Types-of-settings)

COMPort **(necessary)**  
BaudRate **(necessary)**  
DisableUserPresets  
Parity  
DataBits  
StopBits 
LineEnding  

### Example

```xml
<?xml version="1.0" encoding="utf-8"?>  
<config>  
<DisableUserPreset>false</DisableUserPreset>
<COMPort>COM5</COMPort>  
<BaudRate>9600</BaudRate>
</config>
```

## Additional presets

### About

Additional presets, сan be used both in addition to the main and separately.

### How to create

Create file by mask "config_*.xml" in program directory.
Words before "config_" is used as name of config.

### Avaiable [settings](#Types-of-settings)

COMPort **(necessary)**  
BaudRate **(necessary)**  
Parity  
DataBits  
StopBits 
LineEnding  


### Example

```xml
<?xml version="1.0" encoding="utf-8"?>  
<config>  
<COMPort>COM5</COMPort>  
<BaudRate>9600</BaudRate>  
<LineEnding>NL</LineEnding>  
</config>
```

## Types of settings

Name | Type | Purpose | Additionally 
--- | --- | --- | --- 
**DisableUserPreset** | boolean | `Disable all presets, excludes default.`
**COMPort** | string | COM port name. | **Attributes:**<br><ul>*Question*<ul><li>`Purpose` : When starting, asks the name of the settings</li><li>`Type` : boolean</li></ul><ul>
**BaudRate** | Int32 | Baud rate of COM port.
**Parity** | Parity (enum) | In depending of value. | **Values**<ul><li>`Even` - Sets the parity bit so that the count of bits set is an even number.</li><li>`Mark` - Leaves the parity bit set to 1.</li><li>`None` - No parity check occurs.</li><li>`Odd` - Sets the parity bit so that the count of bits set is an odd number.</li><li>`Space` - Leaves the parity bit set to 0.</li></ul>
**DataBits** | Int32 | COM port data bits. |
**StopBits** | Int32 | COM port stop bits.
**LineEnding** | LineEnding (enum) | In depending of value. | **Values**<ul><li>`NL` - Sent messages end with "\n".</li><li>`CR` - Sent messages end with "\r".</li><li>`Nope` - Sent messages end in nothing.</li></ul>

