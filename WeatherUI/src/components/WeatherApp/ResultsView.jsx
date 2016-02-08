import React from 'react';

export const ResultsView = (props)=> {
      return (<div><br/>{props.forecastType}<br/><div>{JSON.stringify(props.forecast)}</div></div>)
};

export default ResultsView;
