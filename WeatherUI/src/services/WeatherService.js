import forecastTypes from '../constants/ForecastTypes';

var baseUrl = 'http://localhost:5000/';

export default class WeatherService
{
    constructor(fetch)
    {
      this.fetch = fetch;
    }

    getWeather(provider, forecastType, fromDate, toDate)
    {
      switch(forecastType)
      {
          case forecastTypes.CurrentWeather:
              return getCurrentWeather(provider);
          case forecastTypes.Daily:
              return getDailyWeather(provider, fromDate, toDate);
        default:
              throw new Error("Unrecognized request");
      }
    }

    getCurrentWeather(provider)
    {
        let forecastRequest = {units:0,location:{city:'London',country:'uk'}};
        return this.fetch(`${baseUrl}api/forecast/currentweather?weatherprovider=${provider}`,{method:"POST",
          headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
          },body:JSON.stringify(forecastRequest)}).then( response => response.json());
    }

    getDailyWeather(provider, fromDate, toDate)
    {
        let forecastRequest = {units:0,location:{city:'London',country:'uk'}};
        return this.fetch(`${baseUrl}api/forecast/dailyforecast/${fromDate}/${toDate}?weatherprovider=${provider}`,{method:"POST",
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        },body:JSON.stringify(forecastRequest)}).then( response => response.json());
    }

}
