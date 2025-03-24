using System.ComponentModel;

namespace ServiceB.Models
{
    public enum WeatherType
    {
        [Description("гроза с небольшим дождём")]
        thunderstorm_with_light_rain,

        [Description("гроза с дождём")]
        thunderstorm_with_rain,

        [Description("гроза с сильным дождём")]
        thunderstorm_with_heavy_rain,

        [Description("лёгкая гроза")]
        light_thunderstorm,

        [Description("гроза")]
        thunderstorm,

        [Description("сильная гроза")]
        heavy_thunderstorm,

        [Description("рваная гроза")]
        ragged_thunderstorm,

        [Description("гроза с мелкой моросью")]
        thunderstorm_with_light_drizzle,

        [Description("гроза с моросью")]
        thunderstorm_with_drizzle,

        [Description("гроза с сильной моросью")]
        thunderstorm_with_heavy_drizzle,

        [Description("лёгкая морось")]
        light_intensity_drizzle,

        [Description("морось")]
        drizzle,

        [Description("сильная морось")]
        heavy_intensity_drizzle,

        [Description("лёгкая морось с дождём")]
        light_intensity_drizzle_rain,

        [Description("морось с дождём")]
        drizzle_rain,

        [Description("сильная морось с дождём")]
        heavy_intensity_drizzle_rain,

        [Description("ливень с моросью")]
        shower_rain_and_drizzle,

        [Description("сильный ливень с моросью")]
        heavy_shower_rain_and_drizzle,

        [Description("морось с ливнем")]
        shower_drizzle,

        [Description("лёгкий дождь")]
        light_rain,

        [Description("умеренный дождь")]
        moderate_rain,

        [Description("сильный дождь")]
        heavy_intensity_rain,

        [Description("очень сильный дождь")]
        very_heavy_rain,

        [Description("экстремальный дождь")]
        extreme_rain,

        [Description("ледяной дождь")]
        freezing_rain,

        [Description("лёгкий ливень")]
        light_intensity_shower_rain,

        [Description("ливень")]
        shower_rain,

        [Description("сильный ливень")]
        heavy_intensity_shower_rain,

        [Description("рваный ливень")]
        ragged_shower_rain,

        [Description("лёгкий снег")]
        light_snow,

        [Description("снег")]
        snow,

        [Description("сильный снег")]
        heavy_snow,

        [Description("мокрый снег")]
        sleet,

        [Description("лёгкий мокрый снег")]
        light_shower_sleet,

        [Description("мокрый снег с ливнем")]
        shower_sleet,

        [Description("лёгкий дождь со снегом")]
        light_rain_and_snow,

        [Description("дождь со снегом")]
        rain_and_snow,

        [Description("лёгкий снег с ливнем")]
        light_shower_snow,

        [Description("снег с ливнем")]
        shower_snow,

        [Description("сильный снег с ливнем")]
        heavy_shower_snow,

        [Description("туман")]
        mist,

        [Description("дым")]
        smoke,

        [Description("дымка")]
        haze,

        [Description("песчаные/пыльные вихри")]
        sand_dust_whirls,

        [Description("туман")]
        fog,

        [Description("песок")]
        sand,

        [Description("пыль")]
        dust,

        [Description("вулканический пепел")]
        volcanic_ash,

        [Description("шквалы")]
        squalls,

        [Description("торнадо")]
        tornado,

        [Description("ясное небо")]
        clear_sky,

        [Description("небольшая облачность: 11-25%")]
        few_clouds,

        [Description("рассеянная облачность: 25-50%")]
        scattered_clouds,

        [Description("разорванная облачность: 51-84%")]
        broken_clouds,

        [Description("пасмурно: 85-100%")]
        overcast_clouds
    }
}

