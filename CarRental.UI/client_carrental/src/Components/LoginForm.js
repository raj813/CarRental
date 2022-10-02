import React,{useState,useEffect} from 'react';
import {Link} from 'react-router-dom';
import {connect} from 'react-redux';
import { logIn } from '../Actions';
import '../CSS/form.css';
import '../CSS/home.css'
import classes from "../CSS/UserProfile.module.css";
//controller for login form 

const LoginForm=(props)=>{
const[LoginDetails,SetLoginDetails]=useState({Email:"",
PasswordHash :"",
})
const [formInputValidity, setFormInputValidity] = useState({
 
  Email: true,
 passwordhash: true
});
const [error, setError] = useState("");

 useEffect(()=>{
   console.log(props.errormsg);
  setError(props.errormsg)
},[props.errormsg])
const onFormSubmit=(event)=>{
    event.preventDefault();
    console.log(LoginDetails);
    let Emailid= !isEmpty(LoginDetails.Email) ;
    let password= !isEmpty(LoginDetails.PasswordHash ) ;
    setFormInputValidity({
      Email: Emailid,
      passwordhash: password
  });

if (Emailid && password){
try{
  props.logIn(LoginDetails);
}
catch(err){
  console.log(err);
}


}
     
 

 

};
//methods for validations 

const errorFunction=()=>{
  if (error){
    return(<label> {error}</label>)
  }
  
}

const isEmpty = (value) => value === "";


return(<div >
  <div className="left">
    </div >
    <div  className="right">
    

    <form onSubmit={(event)=>onFormSubmit(event)}>
    <h2 style={{color:'grey'}}>Login</h2>
    <label>Email</label>
<input
            type="text"
          value={LoginDetails.Email}
          onChange={(event) => SetLoginDetails((obj)=>({

            ...obj,
            Email :event.target.value

          }))} />

{!formInputValidity.Email && (
                            <p className={classes.invalid}>
                                Please enter emailid
                            </p>
                        )}
            <br/>
    <label>PasswordHash</label>
    <input
            type="text"
          value={LoginDetails.PasswordHash}
          onChange={(event) => SetLoginDetails((obj)=>({

           ...obj,
           PasswordHash:event.target.value

          }))} />
          {!formInputValidity.passwordhash&& (
                            <p className={classes.invalid}>
                                Please enter password
                            </p>
                        )}
          <br/>
          <button onClick={(event)=>onFormSubmit(event)}>Login</button>
          <Link to='/register'><button>Register</button></Link>
        {errorFunction()}
          </form>
         
    </div>
    
   </div>);

};

const mapStateToProps = (state) => {
  
 
    if(state.Error.length>=1){
      return{
    errormsg: state.Error[0].data,}}
  else return{}


  }
export default connect(mapStateToProps,{logIn}) (LoginForm);