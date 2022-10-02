
import React,{useState} from 'react';
import {connect} from 'react-redux';
import{register} from '../Actions';
import '../CSS/form.css';


//component for registration form 
const FormDisplay=(props)=>{
const[FormValues,SetFormValues]= useState({ PasswordHash :"",
FirstName:"",
 LastName:"",
Email:"",
PhoneNumber:""})
const onFormSubmit=(event)=>{
  
event.preventDefault();


props.register(FormValues);
}

return(
<div>
  
    <form onSubmit={(event)=>onFormSubmit(event)}>
    <h2>Register</h2>
    <label>PasswordHash</label>
    <input
            type="text"
          value={FormValues.PasswordHash}
          onChange={(event) => SetFormValues((obj)=>({

           ...obj,
           PasswordHash:event.target.value

          }))} />
          <br/>

<label>FirstName</label>
<input
            type="text"
          value={FormValues.FirstName}
          onChange={(event) => SetFormValues((obj)=>({

           ...obj,
            FirstName  :event.target.value

          }))} />
            <br/>
            <label> LastName</label>
<input
            type="text"
          value={FormValues. LastName}
          onChange={(event) => SetFormValues((obj)=>({

            ...obj,
            LastName :event.target.value

          }))} />
            <br/>
<label>Email</label>
<input
            type="text"
          value={FormValues.Email}
          onChange={(event) => SetFormValues((obj)=>({

            ...obj,
            Email :event.target.value

          }))} />
            <br/>
<label>PhoneNumber</label>
<input
            type="text"
          value={FormValues.PhoneNumber}
          onChange={(event) => SetFormValues((obj)=>({

            ...obj,
            PhoneNumber :event.target.value

          }))} /> <br/>
         <button onClick={(event)=>onFormSubmit(event)}>Register</button>
        </form>
    
   </div> 
    
    
   
);
};
export default connect(null,{register}) (FormDisplay);