import actionTypes from '../constants/WeatherActionTypes';

export default function(state, action)
{
  switch(action.type)
  {
          case actionTypes.LOAD_WEATHER_SUCCESS:
              return Object.assign({},state, {forecast: action.payload, loading:false });
          case actionTypes.START_LOAD_WEATHER:
              return Object.assign({},state, {loading: true});
          case actionTypes.LOAD_WEATHER_ERROR:
            return Object.assign({}, state, {errorText: action.payload, loading:false });
          case actionTypes.SET_WEATHER_PROVIDER:
              return Object.assign({},state, {provider:action.payload.provider});
          case actionTypes.SET_DATE_RANGE:
              return Object.assign({},state, action.payload);

  }
}