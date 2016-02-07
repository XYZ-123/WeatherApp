import { combineReducers } from 'redux';
import weatherReducer from './weather';

const rootReducer = combineReducers({
  weatherState: weatherReducer
});

export default rootReducer;
