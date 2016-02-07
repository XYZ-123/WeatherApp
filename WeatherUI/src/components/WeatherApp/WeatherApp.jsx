import React, {PropTypes} from 'react';
import forecastTypes from '../../constants/ForecastTypes';

import ResultsView from './ResultsView';
import Error from './Error';

export class WeatherApp extends React.Component {
  constructor(props)
  {
    super(props);
    this.state = {};
  }

  handleForecastTypeChange()
  {
    if(this.props.weatherState.forecastType == forecastTypes.CurrentWeather)
      this.props.actions.setForecastType(forecastTypes.Daily);
    else
      this.props.actions.setForecastType(forecastTypes.CurrentWeather);
  }

  handleWeatherRequest()
  {
      this.props.actions.startLoadWeather();
  }

  render()
  {
    let state = this.props.weatherState;
    return(<div>
                <input ref="fromDate" disabled={state.forecastType==forecastTypes.CurrentWeather}/>
                <input ref="toDate" disabled={state.forecastType==forecastTypes.CurrentWeather}/>
                <input type="checkbox" checked={state.forecastType == forecastTypes.CurrentWeather} onClick={this.handleForecastTypeChange.bind(this)}/>
                <button onClick={this.handleWeatherRequest.bind(this)}>Get Forecast</button>
                {state.loading? <span>Loading....</span>:null}
                <ResultsView forecastType={state.forecastType} forecast={state.forecast}/>
                <Error error={state.errorText}/>

          </div>);
  }
}
WeatherApp.propTypes = {
  actions: PropTypes.object.isRequired,
  weatherState: PropTypes.object.isRequired
};

export default WeatherApp;
