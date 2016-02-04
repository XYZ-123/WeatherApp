import {createAction} from 'redux-actions';
import actionTypes from '../constants/WeatherActionTypes';

export const loadWeather = createAction(actionTypes.LOAD_WEATHER);

export const setDateRange = createAction(actionTypes.SET_DATE_RANGE, (fromDate, toDate) => {fromDate,toDate});

export const setWeatherProvider = createAction(actionTypes.SET_DATE_RANGE, (provider) => {provider});

export const startLoadWeather = createAction(actionTypes.START_LOAD_WEATHER);

export const loadWeatherSuccess = createAction(actionTypes.LOAD_WEATHER_SUCCESS, (forecast, forecastType)=>{forecast, forecastType});

export const loadWeatherError = createAction(actionTypes.LOAD_WEATHER_ERROR, (error)=>{error});

export default {loadWeather, setDateRange,setWeatherProvider, startLoadWeather, loadWeatherSuccess, loadWeatherError};
