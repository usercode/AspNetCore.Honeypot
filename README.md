# AspNetCore.Honeypot

It's a simple honeypot implementation for ASP.NET Core to detect bot posts.

https://www.nuget.org/packages/AspNetCore.Honeypot

## How to use it ##

1. Register honeypot service.

```cs
 public void ConfigureServices(IServiceCollection services)
 {
      services.AddHoneypot();
 }
```

2. Add tag helper to _ViewImports.cshtml

```cs
@addTagHelper *, AspNetCore.Honeypot
```

3. Place honeypot tag to a form with a custom name (e.g. "name", "email" or "city") and use *your* css class to hide it.

```html
<honeypot name="email" class="hide"></honeypot>
```

4. Place the honeypot attribute to your action method.

```cs
  [Honeypot]
  [HttpPost]
  public async Task<IActionResult> PostRegister(RegisterViewModel registerData)
  {
      //..
  }
```

