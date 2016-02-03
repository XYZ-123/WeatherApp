import {createAction} from 'redux-actions';
import actionTypes from '../constants/WeatherActionTypes';

export loadWeather = function()
{
  return createAction(actionTypes.LOAD_WEATHER);
}

export setDateRange = function(fromDate, toDate)
{
  return createAction(actionTypes.SET_DATE_RANGE, (fromDate,toDate) => {fromDate,toDate});
}
