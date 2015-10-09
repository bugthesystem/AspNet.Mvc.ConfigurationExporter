# AspNet.Mvc.ConfigurationExporter
Provides server side configurations to available on client side

[![Build status](https://ci.appveyor.com/api/projects/status/fechy6cptv9t79g8?svg=true)](https://ci.appveyor.com/project/ziyasal/aspnet-mvc-configurationexporter)

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
 <section name="exportConfigr" type="AspNet.Mvc.ConfigurationExporter.Section.ConfigrSectionHandler,
 AspNet.Mvc.ConfigurationExporter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" />
 ```

  Add configuration section values to web.config;
  ```xml
   <exportConfigr>
    <appSettings>
      <add key="AKey" value="true" />
      <add key="BKey" value="false" />
      <add key="CKey" value="12" />
      <add key="DKey" value="Github" />
      <add key="EKey" value="Look ma!" />
    </appSettings>
  </exportConfigr>
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

###Export property values from custom types

**Suppose that we have a type and we'd like to make its properties available on client side which marked with `ConfigrExported`**
```csharp

public interface ITestConfiguration
  {
      int TestInt { get; }
      string TestString { get; }
      int TestProperty { get; }
  }

  public class TestConfiguration : ITestConfiguration
  {
      public int TestInt { get { return 20; } }

      [ConfigrExported]
      public string TestString { get { return "https://github.com/PanteonProject"; } }

      [ConfigrExported(Name = "testName")]
      public int TestProperty { get { return 10; } }

  }
```
Register type to exporter;  

```csharp

Exporter.Instance.RegisterType<ITestConfiguration>(type => 
                    DependencyResolver.Current.GetService(type));
//OR
Exporter.Instance.RegisterType<ITestConfiguration>(type => 
                    (ITestConfiguration)new TestConfiguration());
```


Add following script tag to your page;
```html
<script type="text/javascript" src="~/configr"></script>
```

Access from js;
```js
console.log(window.configuration.AKey);
```
**Custom JS Namespace**  

 Alternatively you can declare a namespace in App.config like this.
   ```xml
  <add key="configr:Namespace" value="github.config" />
 ```
 and access from js like this :
 ```js
console.log(github.config.AKey);
```
