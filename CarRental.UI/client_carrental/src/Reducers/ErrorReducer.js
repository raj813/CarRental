


export default (state = [], action) => {
    
    switch (action.type) {
        case "LOGIN_ERROR":
        return[...state,action.payload]
        default:
            return state;
    }
}