# AspNet.Mvc.ConfigurationExporter
Provides server side configurations to available on client side


HOW TO USE
---------------------------

To install AspNet.Mvc.ConfigurationExporter;
```
Install-Package AspNet.Mvc.ConfigurationExporter
```

Register configuration exporter route;
```csharp
 RouteTable.Routes.MapConfigExporter();
 ```
 
Set configuration export mode;  
 **Mode: SECTION**  
  ```xml
  <add key="configr:Mode" value="Section" />
 ```
 Add configuration section to web.config;
 ```xml
 <section name="configExporter" type="AspNet.Mvc.ConfigurationExporter.Section.ConfigrSectionHandler,
 AspNet.Mvc.ConfigurationExporter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" />
 ```
 
  Add configuration section values to web.config;
  ```xml
   <configExporter>
    <appSettings>
      <add key="AKey" value="true" />
      <add key="BKey" value="false" />
      <add key="CKey" value="12" />
      <add key="DKey" value="Github" />
      <add key="EKey" value="Look ma!" />
    </appSettings>
  </configExporter>
  ```
**Mode: KEYS**  

Add following settings to appSettings;
```xml
<add key="AKey" value="true" />
<add key="BKey" value="false" />
<add key="CKey" value="12" />
<add key="DKey" value="Github" />
<add key="configr:Keys" value="AKey|BKey|CKey|DKey" />
 <add key="configr:Mode" value="Keys" />
```
Add following script tag to your page;
```html
<script type="text/javascript" src="~/configr"></script>
```

Access from js;
```js
console.log(window.configuration.AKey);
```
 
