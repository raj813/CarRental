import React,{useEffect} from 'react';
import{connect} from 'react-redux';
import { GetBookings } from '../Actions';
import { DeleteCarFormBookings } from '../Actions'
import History from '../history';


import { makeStyles } from '@material-ui/core/styles';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import Divider from '@material-ui/core/Divider';
import ListItemText from '@material-ui/core/ListItemText';
import ListItemAvatar from '@material-ui/core/ListItemAvatar';
import Avatar from '@material-ui/core/Avatar';
import Typography from '@material-ui/core/Typography';
//controller for displaying list of bookings user have
const useStyles = makeStyles((theme) => ({
  root: {
    width: '100%',
    maxWidth: '100ch',
    backgroundColor: theme.palette.background.paper,
  },
  inline: {
    display: 'inline',
  },
  rowContainer:{width:'150%'}
}));



const UserBookings=(props)=>{
    const classes = useStyles();
useEffect(()=>{
props.GetBookings();
},[])

const onClickFeedback=()=>{
  
History.push("/feedback");

}

const bookinglist=()=>{
return props.bookings.map((x)=>{
 let today= new Date();
 let day= today.getDate();
 let month= (today.getMonth()+1);
 const date= x.startDate;
 const enddate= x.endDate;
const startdate= parseInt(date.substr(8,2));
const end= parseInt(enddate.substr(8,2));
console.log("bye" + startdate + end);

const onCancelTrip = ( x) => {
  debugger;
    console.log(x);
    
    props.DeleteCarFormBookings(x);
    
    /*console.log(props.tripDelete);*/
    
   
    }

    const helperMethod = () => {
       

        console.log("hi im in Alert");
        if (props.tripDelete.length >= 1) {
            alert("Trip is cancelled successfully. Click on Home..");
        }
    }

 if(end>=day){
    return(
    
        <List className={classes.root}>
        <ListItem alignItems="flex-start">
          <ListItemAvatar>
            <Avatar alt="Remy Sharp"  className={classes.rowContainer}src={x.image} />
          </ListItemAvatar>
          <ListItemText
            primary={x.tripId}
            secondary={
              <React.Fragment>
                <Typography
                  component="span"
                  variant="body2"
                  className={classes.inline}
                  color="textPrimary"
                >
               <label> TotalAmount{x.totalAmount}</label>
                </Typography>
                <label> startdate {x.startDate}</label>
                <label> enddate {x.endDate}</label>  

                    <button onClick={() => onCancelTrip(x)}>cancel</button>
              </React.Fragment>
            }
          />
        </ListItem>
        <Divider variant="inset" component="li" />
    
       
    </List>
    
    
    
   ); }
else {
    return(
        
        <List className={classes.root}>
        <ListItem alignItems="flex-start">
          <ListItemAvatar>
            <Avatar alt="Remy Sharp"className={classes.rowContainer} src={x.image} />
          </ListItemAvatar>
          <ListItemText
            primary={x.tripId}
            secondary={
              <React.Fragment>
                <Typography
                  component="span"
                  variant="body2"
                  className={classes.inline}
                  color="textPrimary"
                >
               <label> TotalAmount{x.totalAmount}</label>
                </Typography>
                <label> startdate {x.startDate}</label>
                <label> enddate {x.endDate}</label>  
                  
                    <button onClick={() => onClickFeedback()}>FeedBack</button>
                    {/*<div>helperMethod()</div>;*/}
              </React.Fragment>
            }
          />
        </ListItem>
        <Divider variant="inset" component="li" />
    
       
    </List>

    );
}
   
})

}
    return(<ul>{bookinglist()}</ul>)
}
const mapStateToProps=(state)=>{
    return {
        bookings: state.Booking,
        tripDelete: state.deletetrip
    };
}
export default connect(mapStateToProps, { GetBookings, DeleteCarFormBookings}) (UserBookings);