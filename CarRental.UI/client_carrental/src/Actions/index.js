import Streams from "../api/Streams";
import History from "../history";
import {
    CREATE_CAR,
    GET_CARS,
    UPDATE_CAR,
    DELETE_CAR,
    GET_CAR,
} from "./carActionTypes";

export const userUpdate = (userValues) => {
    return async (dispatch, getState) => {
        const { Token } = getState().auth;
        const response = await Streams.put(
            `./api/User/`,
            { ...userValues },
            {
                headers: { Authorization: `Bearer ${Token}` },
            }
        );
        dispatch({ type: "USER_UPDATE", payload: response.data });
        History.push("/booking");
    };
};

export const userGet = () => {
    return async (dispatch, getState) => {
        const { Token } = getState().auth;
        const response = await Streams.get(`./api/User/`, {
            headers: {
                Authorization: `Bearer ${Token}`,
            },
        });
        console.log(response.data);
        dispatch({ type: "USER_UPDATE", payload: response.data });
    };
};

export const logIn = (LoginDetails) => {
    return async (dispatch, getState) => {
      try{
        const response = await Streams.post(
            "/api/Account/login",

            { ...LoginDetails }
        );
        console.log(response);
        dispatch({ type: "LOG_IN", payload: response.data });

        History.push("/booking");}
        
        catch(error){
            console.log(error.response);
            dispatch({ type: "LOGIN_ERROR", payload: error.response });
        }
    };
};

export const signOut = () => {
    return { type: "SIGN_OUT" };
};

export const register = (FormValues) => {
    return async (dispatch, getState) => {
        const response = await Streams.post(
            "/api/Account/register",

            { ...FormValues }
        );

        dispatch({ type: "SIGN_IN", payload: response.data });
        History.push("/booking");
    };
};

export const createCar = (FormValues) => {
    return async (dispatch, getState) => {
        const { Token } = getState().auth;
        const response = await Streams.post("/api/Cars", {
            carId: 0,
            model: FormValues.model,
            pricePerDay: FormValues.pricePerDay,
            image: FormValues.image,
            numberPlate: FormValues.numberPlate,
            locationId: 0,
            location: {
                locationId: 0,
                city: FormValues.city,
                province: FormValues.province,
            },
        }, {

            headers: {
                Authorization: `Bearer ${Token}`,
            }
        });
        dispatch({ type: CREATE_CAR, payload: response.data });
        return response.data;
    };
};

export const getCars = () => {
    return async (dispatch, getState) => {
        const { Token } = getState().auth;
        const response = await Streams.get("/api/Cars", {

            headers: {
                Authorization: `Bearer ${Token}`,
            }
        });
        dispatch({ type: GET_CARS, payload: response.data });
    };
};

export const getCar = (id) => {
    return async (dispatch, getState) => {
        const { Token } = getState().auth;
        const response = await Streams.get(`/api/Cars/${id}`, {

            headers: {
                Authorization: `Bearer ${Token}`,
            }
        });
        dispatch({ type: GET_CAR, payload: response.data });
    };
};

export const updateCar=(id,FormValues)=>{
    return async(dispatch, getState)=>{
        const { Token } = getState().auth;
        const response = await Streams.put(`/api/Cars/${id}`, 
        {
            carId: FormValues.carId,
            model: FormValues.model,
            pricePerDay: FormValues.pricePerDay,
            image: FormValues.image,
            numberPlate: FormValues.numberPlate,
            locationId:0,
            location:{
                 locationId:0,
                 city:FormValues.city,
                 province:FormValues.province
            }
        }, {

            headers: {
                Authorization: `Bearer ${Token}`,
            }
        });
        dispatch({type:UPDATE_CAR,payload:response.data})
        return response.data
    }
}

export const deleteCar = (id) => {
    return async (dispatch, getState) => {
        const { Token } = getState().auth;
        const response = await Streams.delete(`/api/Cars/${id}`, {

            headers: {
                Authorization: `Bearer ${Token}`,
            }
        });
        dispatch({ type: DELETE_CAR, payload: response.data });
    };
};

export const GetBookings = () => {
    return async (dispatch, getState) => {
        const { Token } = getState().auth;
        const response = await Streams.get(`/api/Trip`, {

            headers: {
                Authorization: `Bearer ${Token}`,
            }
        });
        
        dispatch({ type: "GET_BOOKINGS", payload: response.data });
    }


};

export const GetCarsForBookings = (BookingValues) => { //(/api/Booking/BookingReservation?city=v1&startDate=v2&endDate=v3&model=v4)
    return async (dispatch, getState) => {
        const { Token } = getState().auth;
        const response = await Streams.get(`/api/Booking/BookingReservation?city=${BookingValues.city}&startDate=${BookingValues.startDate}&endDate=${BookingValues.endDate}&model=${BookingValues.model}`, {
            headers: {
                Authorization: `Bearer ${Token}`,
            }
           
        });
       
        dispatch({ type: "GET_FILTER_CARS", payload: response.data });
    }


};

export const PostConfirmBookings = (ConfirmValues) => { //(/api/Booking/ConfirmTrip

    return async (dispatch, getState) => {
        const { Token } = getState().auth;
       
        const response = await Streams.post(`/api/Booking/ConfirmTrip`,
            { ...ConfirmValues }, {
            headers: {
                Authorization: `Bearer ${Token}`,
            }
            //JsonConfirmValue
        });
       
        dispatch({ type: "POST_CONFIRM_CAR", payload: response.data });
    }
};

export const DeleteCarFormBookings = (deletedValues) => { //(/api/Booking/CancelTrip
       
    console.log(deletedValues);
    const{tripId,userId,startDate,endDate,carId,totalAmount,numberPlate}=deletedValues;
   

            


   
   
        return async (dispatch, getState) => {
            const { Token } = getState().auth;
            
          
            const response = await Streams.delete(`/api/Booking/CancelTrip`,
           
                 {
                headers: {
                    Authorization: `Bearer ${Token}`,
                },
                data: {
                    tripId: tripId,
                    startDate: startDate,
                    numberPlate:numberPlate,
                    endDate: endDate,
                    totalAmount: totalAmount,
                    carId: carId,
                    userId:userId
                  }


            }); 
           
            // if (response.data == 200) {
            //     alert("Trip is cancelled successfully. Please click on Home..");
            // }
            dispatch({ type: "Delete_Old_Trip", payload: response.data });
       
            dispatch({ type: "DELETE_RESERVATION", payload: response.data })
        }


};



export const postfeedback = (FeedbackValues) => {
    return async (dispatch, getState) => {
        
        const response = await Streams.post(
            "/api/Review",

            { ...FeedbackValues }
        );

        dispatch({ type: "ADDREVIEW", payload: response.data });
    };
};


export const getfeedback = () => {
    return async (dispatch, getState) => {
        
        const response = await Streams.get("/api/Review", {

        });
        dispatch({ type: "GETREVIEW", payload: response.data });
    };
};