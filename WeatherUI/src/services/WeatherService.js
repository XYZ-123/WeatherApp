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
      debugger;
      let fromDateFormatted = fromDate.format("DD-MM-YYYY");
      let toDateFormatted = toDate.format("DD-MM-YYYY");
      switch(forecastType)
      {
          case forecastTypes.CurrentWeather:
              return this.getCurrentWeather(provider);
          case forecastTypes.Daily:
              return this.getDailyWeather(provider, fromDateFormatted, toDateFormatted);
        default:
              throw "Unrecognized request";
      }
    }

    getCurrentWeather(provider)
    {
        let forecastRequest = {units:"Celsium",location:{city:'London',country:'uk'}};
        let fetch = this.fetch || window.fetch;
        return fetch(`${baseUrl}api/forecast/currentweather?weatherprovider=${provider}`,{method:"post",
          headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
          },
          body:JSON.stringify(forecastRequest)}).then( response => {console.log(response);
          return response.json(); }).catch(error=> console.log(error));
    }

    getDailyWeather(provider, fromDate, toDate)
    {

        let forecastRequest = {units:0,location:{city:'London',country:'uk'}};
        let fetch = this.fetch || window.fetch;
        return fetch(`${baseUrl}api/forecast/dailyforecast/${fromDate}/${toDate}?weatherprovider=${provider}`,{method:"POST",
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        },body:JSON.stringify(forecastRequest)}).then( response => response.json());
    }

}
