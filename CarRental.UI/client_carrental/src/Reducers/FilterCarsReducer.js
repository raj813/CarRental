export default(state=[],action)=>{
switch(action.type){
case "GET_FILTER_CARS":
    return[...action.payload];
default:
   return state;    




}



}