 import React, { useEffect, useState } from "react";
 import { Link } from "react-router-dom";
 import { connect } from "react-redux";
 import { makeStyles } from "@material-ui/core/styles";
 import Card from "@material-ui/core/Card";
 import CardActionArea from "@material-ui/core/CardActionArea";
 import CardActions from "@material-ui/core/CardActions";
 import CardContent from "@material-ui/core/CardContent";
 import CardMedia from "@material-ui/core/CardMedia";
 import Button from "@material-ui/core/Button";
 import Typography from "@material-ui/core/Typography";
 import {getfeedback} from "../Actions";


 const FeedBackList = (props) => {
 const isSubmitClicked = props.isSubmitClicked;
 console.log("Is submit is clicked")
 console.log(isSubmitClicked) 
    useEffect(()=>{
         props.getfeedback();
         console.log("hitting use effect ");
         console.log(props);
         console.log("in the use effect  ");
     },[isSubmitClicked]);
     console.log("main method is here ");
     const useStyles = makeStyles({
         flex:{
           display:'flex',
           flexWrap:'wrap',
           justifyContent:'space-between'
         },
         root: {
           width: 345,
           padding:10,
           margin:20
  
         },
         media: {
           height: 140,
         },
       });
       const classes = useStyles();
       const feedbackList=()=>{
         if (props.review.length > 1) {
             console.log(props.review);
             return props.review.map((x,i) => {
               return (
                 <Card className={classes.root} key={i}>
                   <CardActionArea>
                 
                     <CardContent>
                       <Typography gutterBottom variant="h5" component="h2">
                         Title : {x.title}
    
                       </Typography>
                       <Typography variant="body2" color="textSecondary" component="p">
                         Description: {x.description}<br></br>
                       </Typography>
                     </CardContent>
                   </CardActionArea>
            
                 </Card>
               );
       });
     }
 };
 return (
     <div style={{margin:"10%"}}>
   
       <h2 style={{textAlign:"center",color:"burlywood"}}> Reviews</h2>
       <div className={classes.flex}>{feedbackList()}</div>
     </div>
   );
 };

 const mapStateToProps = (state, ownProps) => {
    return { review: state.feedback };
  };

 export default connect(mapStateToProps,{getfeedback})(
   FeedBackList
 );
