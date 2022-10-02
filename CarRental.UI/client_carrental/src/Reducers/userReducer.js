const Initial_State = {
  userId: "",
  passwordHash: "",
  firstName: "",
  lastName: "",
  phone: "",
  email: "",
};

export default (state = Initial_State, action) => {
  if (action.type === "USER_UPDATE") {
    return {
      ...state,
      userId: action.payload.userId,
      passwordHash: action.payload.passwordHash,
      firstName: action.payload.firstName,
      lastName: action.payload.lastName,
      phone: action.payload.phoneNumber,
      email: action.payload.email,
    };
  }
  return state;
};
