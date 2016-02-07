//This file merely configures the store for hot reloading.
//This boilerplate file is likely to be the same for each project that uses Redux.
//With Redux, the actual stores are in /reducers.

import { createStore, applyMiddleware } from 'redux';
import rootReducer from '../reducers';
import sagaMiddleware from 'redux-saga';

import weatherSaga from '../sagas/weatherSaga';

const createStoreWithMiddleWare = applyMiddleware(sagaMiddleware(weatherSaga))(createStore);

export default function configureStore() {

  const store = createStoreWithMiddleWare(rootReducer);
  console.log("Initial state",store.getState());

  if (module.hot) {
    // Enable Webpack hot module replacement for reducers
    module.hot.accept('../reducers', () => {
      const nextReducer = require('../reducers');
      store.replaceReducer(nextReducer);
    });
  }

  return store;
}
