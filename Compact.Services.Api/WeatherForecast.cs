using System;

namespace Compact.Services.Api
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }

    /* https://elanderson.net/2019/12/log-requests-and-responses-in-asp-net-core-3/ */
}
