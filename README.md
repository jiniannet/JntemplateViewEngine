# 关于JntemplateViewEngine
JntemplateViewEngine是基于Jntemplate的ASP.NET MVC视图引擎，简单来说它可以让你在ASP.NET MVC中更方便的使用Jntemplate.

Jntemplate源码可以从下面的地址获取

GITEE（国内）：https://gitee.com/jiniannet/jntemplate
GITHUB：https://github.com/jiniannet/jntemplate

Jntemplate 文档：http://docs.jiniannet.com/

# 快速上手
- 模板使用新后缀.jnt 或者 .html

## 配置视图引擎
- 打开 `Startup.cs`，在ConfigureServices方法中增加`AddJntemplateViewEngine`,如下如示
```
        public void ConfigureServices(IServiceCollection services)
        {
            //...其它代码
            //Add Jntemplate ViewEngine
            services.AddJntemplateViewEngine();
        }

```

- 在`Configure`方法中增加`UseJntemplate`,如下如示
```
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //...其它代码
            //Use Jntemplate
            app.UseJntemplate(jntemplateConfig =>
            {
                //在这里你也可以进行其它参数的配置
                jntemplateConfig.ContentRootPath = env.ContentRootPath;
            });
        }

```

## 增加视图
在Views\Home下增加一个Index.html 或者Index.jnt 内容如下：
```html
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>${Site.Name}</title> 
</head>
<body>
    <h1>Welcome to ${Site.Name}!</h1>
    <p>Engine Version:${Site.Version} &copy;${Now.Year}</p>
</body>
</html>
```

## 增加Action
打开HomeController，增加一个Index的Action
```
        public IActionResult Index()
        {
            this.Set("Site", new SiteViewModel
            {
                Name = "Jntemplate",
                Version = JinianNet.JNTemplate.Engine.Version
            });
            this.Set("Now", DateTime.Now);
            return View();
        }
```
具体可参考演示项目JntemplateViewEngineDemo
 
 