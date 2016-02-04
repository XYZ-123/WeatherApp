import {take, put, call, fork} from 'redux-saga';
import 'fetch';

import actionTypes from '../constants/WeatherActionTypes';
import action from '../actions/weatherActions';

function* doWeatherRequest(getState)
{
    while(yield take(actionTypes.LOAD_WEATHER))
    {
        try{
          let provider = getState().weather.provider;
          let fromDate = getState().weather.fromDate;
          let toDate = getState().weather.toDate;

          if(!provider || !fromDate || !toDate)
            throw new Error();
        }
    }
}
