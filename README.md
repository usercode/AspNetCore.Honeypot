# AspNetCore.Honeypot

It's a simple honeypot implementation for ASP.NET Core to detect bot posts.

https://www.nuget.org/packages/AspNetCore.Honeypot  
  
[![nuget](https://img.shields.io/nuget/v/AspNetCore.Honeypot.svg)](https://www.nuget.org/packages/AspNetCore.Honeypot)
  
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

3. Place any honeypot tag to a form with a custom name (e.g. "name", "email" or "city") and use *your* css class to hide it.

```html
<honeypot-field name="email" class="hide" />
<honeypot-field name="name" class="hide" />
<honeypot-field name="city" class="hide" />
```

4. Place one time-based honeypot tag. 
```html
<honeypot-time />
```

6. Bot detection handling

4.1 Place the honeypot attribute to your action method for automatic bot detection handling.

```cs
  [Honeypot]
  [HttpPost]
  public async Task<IActionResult> PostRegister(RegisterViewModel registerData)
  {
      //..
  }
```

4.2 Use the extension method to handle bot detection by yourself.
```cs
  [HttpPost]
  public async Task<IActionResult> PostRegister(RegisterViewModel registerData)
  {
      if (HttpContext.IsHoneypotTrapped())
      {
          ModelState.Clear();
          ModelState.AddModelError("", "bot detection");

          //log

          return View("Register", new RegisterViewModel());
      }
  }
```


