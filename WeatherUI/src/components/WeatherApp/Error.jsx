import React from 'react';

export const Error = (props) =>
{
    return (<div> props.error ? <div style="color:red;"> {props.error}</div> : null</div>);
};
export default Error;
