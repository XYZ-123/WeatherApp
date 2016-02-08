import {createAction} from 'redux-actions';
import actionTypes from '../constants/WeatherActionTypes';

export const loadWeather = createAction(actionTypes.LOAD_WEATHER);

export const setStartDate = createAction(actionTypes.SET_START_DATE, fromDate => fromDate);

export const setEndDate = createAction(actionTypes.SET_END_DATE, toDate => toDate);

export const setWeatherProvider = createAction(actionTypes.SET_WEATHER_PROVIDER, (provider) => ({provider}));

export const startLoadWeather = createAction(actionTypes.START_LOAD_WEATHER);

export const loadWeatherSuccess = createAction(actionTypes.LOAD_WEATHER_SUCCESS, (forecast, forecastType)=>({forecast, forecastType}));

export const loadWeatherError = createAction(actionTypes.LOAD_WEATHER_ERROR, error => error);

export const setForecastType = createAction(actionTypes.SET_FORECAST_TYPE, forecastType => ({forecastType}));

export default {loadWeather, setStartDate, setEndDate, setWeatherProvider, startLoadWeather, loadWeatherSuccess, loadWeatherError, setForecastType};
