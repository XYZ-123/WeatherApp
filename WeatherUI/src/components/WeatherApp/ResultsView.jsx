import React from 'react';

export const ResultsView = (props)=> {
      return (<div>{props.forecastType}<br/><div>{props.forecast}</div></div>)
};

export default ResultsView;
