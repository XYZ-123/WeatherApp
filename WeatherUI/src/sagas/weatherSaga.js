import {take, put, call, fork} from 'redux-saga';
import 'whatwg-fetch';

import actionTypes from '../constants/WeatherActionTypes';
import actions from '../actions/weatherActions';
import forecastTypes from '../constants/ForecastTypes';
import WeatherService from '../services/WeatherService';

export default function* doWeatherRequest(getState)
{
  console.log("Do Weather REquest saga loaded");
    while(yield take(actionTypes.LOAD_WEATHER))
    {
      console.log("REceived Load WEather REquest");
        try{
          let provider = getState().weatherState.provider;
          let fromDate = getState().weatherState.fromDate;
          let toDate = getState().weatherState.toDate;
          let forecastType = getState().weatherState.currentForecastType;

          if(!provider || ((!fromDate || !toDate) && forecastType !== forecastTypes.CurrentWeather))
            throw new Error("Invalid error");

          console.log("Starting to load weather");

          yield put(actions.startLoadWeather());

          var weatherService = new WeatherService();
          console.log("weather service",weatherService);
          let forecast = yield weatherService.getWeather(provider, forecastType, fromDate, toDate);
          console.log("Received forecast:", forecast);
          yield put(actions.loadWeatherSuccess(forecast, forecastType));

        }
      catch(e){
        yield put(actions.loadWeatherError(e));
      }
    }
  console.log("Finished saga");
}
