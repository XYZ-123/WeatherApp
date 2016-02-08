import actionTypes from '../constants/WeatherActionTypes';

export default function weatherState(state = {provider:"OpenWeather",currentForecastType:"DAILY"}, action={})
{
  switch(action.type)
  {
          case actionTypes.LOAD_WEATHER_SUCCESS:
              return Object.assign({},state, {forecast: action.payload.forecast, forecastType: action.payload.forecastType, loading:false, errorText: undefined });
          case actionTypes.START_LOAD_WEATHER:
              return Object.assign({}, state, {loading: true, errorText: undefined});
          case actionTypes.LOAD_WEATHER_ERROR:
            return Object.assign({}, state, {errorText: action.payload, loading:false });
          case actionTypes.SET_WEATHER_PROVIDER:
              return Object.assign({}, state, {provider:action.payload.provider});
          case actionTypes.SET_START_DATE:
              return Object.assign({}, state, {fromDate:action.payload});
          case actionTypes.SET_END_DATE:
              return Object.assign({}, state, {toDate:action.payload});
          case actionTypes.SET_FORECAST_TYPE:
              return Object.assign({},state,{currentForecastType: action.payload.forecastType});
          default:
            return state;
  }
}
