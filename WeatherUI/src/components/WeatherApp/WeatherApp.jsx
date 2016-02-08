import React, {PropTypes} from 'react';
import DatePicker from 'react-datepicker';
import moment from "moment";
import "react-datepicker/dist/react-datepicker.css";

import forecastTypes from '../../constants/ForecastTypes';
import ResultsView from './ResultsView';
import Error from './Error';
import './WeatherApp.scss';

export class WeatherApp extends React.Component {

  constructor(props)
  {
    super(props);
  }

  handleForecastTypeChange()
  {
    if(this.props.weatherState.currentForecastType == forecastTypes.CurrentWeather)
      this.props.actions.setForecastType(forecastTypes.Daily);
    else
      this.props.actions.setForecastType(forecastTypes.CurrentWeather);
  }

  handleWeatherRequest()
  {
      this.props.actions.loadWeather();
  }
  handleToDateChange(date)
  {
    console.log("toDate",date);
    this.props.actions.setEndDate(date);

  }
  handleFromDateChange(date)
  {
    console.log("fromDate",date);
    this.props.actions.setStartDate(date);
  }
  render()
  {
    let state = this.props.weatherState;
    return(<div>
      <div className="WeatherApp--calendar">
            <p>Pick start date</p>
           <DatePicker dateFormat="DD-MM-YYYY" selected={state.fromDate} disabled={state.currentForecastType==forecastTypes.CurrentWeather} minDate={moment()} maxDate={moment().add(5,'days')} placeholderText="Pick Start date" onChange={this.handleFromDateChange.bind(this)} />
      </div>
      <div className="WeatherApp--calendar">
        <p>Pick end date</p>
        <DatePicker dateFormat="DD-MM-YYYY" selected={state.toDate} disabled={state.currentForecastType==forecastTypes.CurrentWeather} minDate={moment()} maxDate={moment().add(5,'days')} placeholderText="Pick End date" onChange={this.handleToDateChange.bind(this)} />
      </div>
                <p> Get Current Weather<input type="checkbox" checked={state.currentForecastType == forecastTypes.CurrentWeather} onClick={this.handleForecastTypeChange.bind(this)}/></p>
                <button onClick={this.handleWeatherRequest.bind(this)}>Get Forecast</button>
                {state.loading? <span>Loading....</span>:null}
                <ResultsView forecastType={state.currentForecastType} forecast={state.forecast}/>
                <Error error={state.errorText}/>

          </div>);
  }
}

WeatherApp.propTypes = {
  actions: PropTypes.object.isRequired,
  weatherState: PropTypes.object.isRequired
};

export default WeatherApp;
