# DotNetCoreOptionsBinder

## Options binder that will allow you to stop inject `IOptions<OptionsModel>`.

Are you tired of injecting IOptions<OptionsModel> to your service's constructor? Worry no more, this library will resolve this problem. Usage is fairly simple:
  
1. Install this library from Nuget repository: https://www.nuget.org/packages/OptionsBinder.Configuration/1.0.0
2. Invoke extension in Startup.cs class:
```
public void ConfigureServices(IServiceCollection services)
{
    services.AddModelBasedOptions(Configuration);
}
```
3. Use IOptionsModel interface on your model class:
  ```
public class SomeConfiguration : IOptionsModel
{
    public string SomeProp { get; set; }
}
  ```
4. Add settings to your preferred configuration provider. In this example it is appsettings.json:
```
{
...
"SomeConfiguration": {
    "SomeProp":  "SomeValue" 
}
```
5. Voilà, you can use now your registered options in any constructor:
```
public class ValuesController : Controller
{
    private readonly SomeConfiguration _someConfiguration;

    public ValuesController(SomeConfiguration someConfiguration)
    {
        _someConfiguration = someConfiguration;
    }

    [HttpGet]
    public string Get()
    {
        return $"Value read from configuration: {_someConfiguration.SomeProp}";
    }
}
```