# 关于Jntemplate View Engine
这是一个基于asp.net core mvc 的 jntemplate视图引擎，简单来说，它可以让你在asp.net core mvc中使用jntemplate
jntemplate是基于dotnet平台的文本解析引擎，支持免费商用，如果您喜欢我们，请给我们一个star

GITHUB地址：https://github.com/jiniannet/jntemplate
码云地址：https://gitee.com/jiniannet/jntemplate

# 快速上手
- 模板使用新后缀.jnt 或者 .html
- 在 Startup.cs 中，
```
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc()
                .AddJntemplateViewEngine();
        }

```


# 警告
当前版本目前还仅属于A版，建议大家等待版本稳定后再正式商用
 