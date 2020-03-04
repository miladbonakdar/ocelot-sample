using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Middleware.Multiplexer;

namespace Gateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddOcelot(Configuration)
                .AddSingletonDefinedAggregator<UserAndOrderCustomAggregator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            await app.UseOcelot();
        }
    }

    public class UserAndOrderCustomAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<DownstreamContext> responses)
        {
            var userTask = responses.First(r => r.DownstreamReRoute.Key == "User").DownstreamResponse.Content
                .ReadAsStringAsync();
            var orderTask = responses.First(r => r.DownstreamReRoute.Key == "Order").DownstreamResponse.Content
                .ReadAsStringAsync();

            var res = await Task.WhenAll(userTask, orderTask);
            var user = res[0];
            var order = res[1];

            var contentBuilder = new StringBuilder();
            contentBuilder.Append("{\"User\":");
            contentBuilder.Append(user);
            contentBuilder.Append(",\"Order\":");
            contentBuilder.Append(order);
            contentBuilder.Append("}");

            var stringContent = new StringContent(contentBuilder.ToString())
            {
                Headers = {ContentType = new MediaTypeHeaderValue("application/json")}
            };

            return new DownstreamResponse(stringContent, HttpStatusCode.OK,
                new List<KeyValuePair<string, IEnumerable<string>>>()
                , "OK");
        }
    }
}