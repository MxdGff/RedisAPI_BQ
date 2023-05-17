namespace RedisAPI_BQ
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "RedisAPI_BQ";
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
