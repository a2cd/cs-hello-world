using cs_hello_world.Component;
using cs_hello_world.Config;
using cs_hello_world.Const;
using cs_hello_world.Util;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
Cfg.Init(app.Configuration); // 读取配置文件
// BlockingListListener.Listen(); // 监听blocking list

// Configure the HTTP request pipeline.
if (!app.Environment.IsEnvironment(Env.Prd))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/hello", () => new
    {
        Code = 200,
        Data = "hello",
        Msg = "success",
        Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss dddd")
    })
    .WithName("SayHello")
    .WithOpenApi();

app.MapGet("/redis/list/{key}/{val}", (string key, string val) =>
    {
        RedisUtil.Db.ListLeftPush(key, val);
        return new
        {
            Code = 200,
            Data = true,
            Msg = "success",
            Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss dddd")
        };
    })
    .WithName("ListLPush")
    .WithOpenApi();

app.Run();
