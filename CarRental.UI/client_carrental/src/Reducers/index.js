import {combineReducers} from 'redux';
import authReducer from './authReducer';
import userReducer from './userReducer';
import carReducer from './carReducer';
import BookingReducer from './BookingReducer';
import FilterCarsReducer from './FilterCarsReducer';
import ConfirmBooking from './ConfrimBooking';
import DeleteTripReducer from './DeleteTripReducer'
import FeedBackReducer from './FeedBackReducer';
import ErrorReducer  from './ErrorReducer'

const appReducer= combineReducers({
    auth: authReducer,
    user: userReducer,
    cars: carReducer,
    Booking:BookingReducer,
    filtercar: FilterCarsReducer,
    confirmcar: ConfirmBooking,
    feedback:FeedBackReducer,
    deletetrip: DeleteTripReducer,
    Error: ErrorReducer
    });

    const rootReducer = (state, action) => {
        // when a logout action is dispatched it will reset redux state
        if (action.type === 'SIGN_OUT') {
          state = undefined;
        }
      
        return appReducer(state, action);
      };
      
      export default rootReducer;