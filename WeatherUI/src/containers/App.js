// This file bootstraps the app with the boilerplate necessary
// to support hot reloading in Redux
import React, {PropTypes} from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';


import weatherActions from '../actions/weatherActions';
import WeatherApp from '../components/WeatherApp/WeatherApp';


class App extends React.Component {
  render() {
    const { weatherState, actions } = this.props;

    return (
        <WeatherApp weatherState={weatherState} actions={actions} />
    );
  }
}

App.propTypes = {
  actions: PropTypes.object.isRequired,
  weatherState: PropTypes.object.isRequired
};

function mapStateToProps(state) {
  return {
    weatherState: state.weatherState
  };
}

function mapDispatchToProps(dispatch) {
  return {
    actions: bindActionCreators(weatherActions, dispatch)
  };
}

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(App);
