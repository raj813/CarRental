export default (state = [], action) => {
    switch (action.type) {
        case "POST_CONFIRM_CAR":
            return [...state, action.payload];
            case "DELETE_RESERVATION":
     
    return state.filter(({ tripId }) => tripId !== action.payload.deletedTrip.tripId)
        default:
            return state;

    }



}