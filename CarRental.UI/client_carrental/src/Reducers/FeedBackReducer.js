const Initial_State={
    feedbacks :[]
};

export default (state=Initial_State,action)=>{
    const {type,payload}=action
switch(action.type){
case  'ADDREVIEW':
    return{...state,Title:action.payload.Title,Description:action.payload.Description};
case 'GETREVIEW':
    return payload;

default:
     return state;   


}



};