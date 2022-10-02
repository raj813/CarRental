import React from 'react';
import ReactDOM from 'react-dom';
import App from './Components/App';
import Thunk from 'redux-thunk';
import {Provider} from 'react-redux';
import{createStore,applyMiddleware,compose} from 'redux';
import reducers from './Reducers'

//const store = createStore(reducers, applyMiddleware(Thunk));
const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;
ReactDOM.render(
    //<Provider store={store}>
<Provider store={createStore(reducers,composeEnhancers(applyMiddleware(Thunk)))}>


<App/>

</Provider>
,document.querySelector('#root'));