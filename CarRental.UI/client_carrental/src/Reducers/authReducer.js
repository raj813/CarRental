const Initial_State={
    isSignedIn: null,
    Token: null,
    Email:null,
    UserType:null
};

export default (state=Initial_State,action)=>{
switch(action.type){
case  'SIGN_IN':
    return{...state,isSignedIn:true,Token:action.payload.token,Email:action.payload.email,UserType:action.payload.userType};
case 'LOG_IN':    
return{...state,isSignedIn:true,Token:action.payload.token,Email:action.payload.email,
    UserType:action.payload.userType};


default:
     return state;   


}



};