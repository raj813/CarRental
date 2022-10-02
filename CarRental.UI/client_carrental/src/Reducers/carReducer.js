import {CREATE_CAR,GET_CARS,UPDATE_CAR,DELETE_CAR} from '../Actions/carActionTypes'


const Initial_State={
    cars:[]
}

const carReducer=(cars=Initial_State,action)=>{
    const {type,payload}=action
    switch(type){
        case CREATE_CAR:
            Initial_State.cars.push(payload)
            return{...cars,payload};
        case GET_CARS:
            return payload;
        case UPDATE_CAR:
            return cars.map((car) => {
                if (car.carId === payload.carId) {
                  return {
                    ...car,
                    ...payload,
                  };
                } else {
                  return car;
                }
              });
        case DELETE_CAR:
            return cars.filter(({ carId }) => carId !== payload.carId)
        default:
            return cars
    }
}
    
export default carReducer