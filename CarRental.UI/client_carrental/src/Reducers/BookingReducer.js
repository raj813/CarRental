const Initial_State={
    Bookings:[]
}
export default (state=[],action)=>{
    switch(action.type){
case 'GET_BOOKINGS':{
return[...action.payload]

}
case "Delete_Old_Trip":
   
    return state.filter(({ tripId }) => tripId !== action.payload.deletedTrip.tripId)
default:
    return state;
    }


}