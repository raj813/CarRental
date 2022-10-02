import React,{useState} from 'react';
import {connect} from 'react-redux';
import{postfeedback} from '../Actions';


import { makeStyles } from "@material-ui/core/styles";
import Card from "@material-ui/core/Card";
import CardActionArea from "@material-ui/core/CardActionArea";
import CardActions from "@material-ui/core/CardActions";
import CardContent from "@material-ui/core/CardContent";
import CardMedia from "@material-ui/core/CardMedia";
import Button from "@material-ui/core/Button";
import Typography from "@material-ui/core/Typography";
import StarRating from './StarRating';
import FeedBackList from './FeedBackList';


const FeedBack=(props)=>{
    const[FeedbackValues,SetFeedbackValues] = useState({
        Title : "",
        Description:"",
        
    })
    const [isSubmitClicked, setisSubmitClicked] = useState(false)
    const onFeedBackSubmit = (event) =>{
        event.preventDefault();
 
        props.postfeedback(FeedbackValues).then(()=>{
          onCancle();
          setisSubmitClicked(true)
          
        });
      
      
    }

    const onCancle = () =>{
      SetFeedbackValues((obj)=>({
        Description :"",
        Title:""

      }))
      
  }


return (
<div>
    <form onSubmit={(event)=>onFeedBackSubmit(event)} >
        <h2>FeedBack Form</h2>
        {/* <StarRating>

        </StarRating> */}
        <label>Title</label>
<input
            type="text"
          value={FeedbackValues.Title}
          onChange={(event) => SetFeedbackValues((obj)=>({

           ...obj,
           Title  :event.target.value

          }))} />
            <br/>
            <label> Description </label>
<input
            type="text"
          value={FeedbackValues.Description}
          onChange={(event) => SetFeedbackValues((obj)=>({

            ...obj,
            Description :event.target.value

          }))} />
            <br/> <br/>

            <button onClick={(event)=>onFeedBackSubmit(event)}>Submit</button>
            <button  onClick={onCancle}>Cancel</button>
    </form>

   <FeedBackList isSubmitClicked = {isSubmitClicked} >
  </FeedBackList> 

</div>
);

};


const mapStateToProps = (state, ownProps) => {
  return { review: state.feedback };
};
export default  connect(mapStateToProps,{postfeedback}) (FeedBack);