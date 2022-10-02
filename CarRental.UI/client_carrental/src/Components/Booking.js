import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { GetCarsForBookings } from '../Actions';
import { PostConfirmBookings } from '../Actions'
import History from '../history';
import { useState } from 'react';
import { makeStyles } from "@material-ui/core/styles";
import Card from "@material-ui/core/Card";
import CardActionArea from "@material-ui/core/CardActionArea";
import CardActions from "@material-ui/core/CardActions";
import CardContent from "@material-ui/core/CardContent";
import CardMedia from "@material-ui/core/CardMedia";
import Button from "@material-ui/core/Button";
import Typography from "@material-ui/core/Typography";
import '../CSS/form.css';
import {modelOptions as modelOptions}  from './Car/options'


//component for booking form and list 



const useStyles = makeStyles({
    flex:{
      display:'flex'
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
  //component
const Booking = (props) => {
    const classes = useStyles();
    useEffect(() => {
        if (props.auth !== true) {
            History.push('/');
        }
    })
    const [BookingFilters, SetBookingFilters] = useState({
        city: "",
        startDate: "",
        endDate: "",
        model: 0
    })
    const onsubmit=(event)=>{
     event.preventDefault();
        console.log(BookingFilters);
        props.GetCarsForBookings(BookingFilters);
    
    }

    //const onsubmitConfrim = (event, x) => {
    //    event.preventDefault();
    //    x.userId = 2 ///delete this later
    //    x = {...x}; 
    //    props.PostConfirmBookings(x);
    //   /* console.log(x);*/

    //}
    const onConfirmBooking=(x)=>{
       
   props.PostConfirmBookings(x);
    }
    const helperMethod=(x)=>{
        
        console.log("hi im in here");
        if(props.bookedcar.length>=1){

       let result=props.bookedcar.find(({ carId })=>carId===x.carId);
        if(result){
            return(<div>Bookingconfirmed</div>);

        }

        else{
            return(<Button size="small" color="primary" onClick={()=>onConfirmBooking(x)}>
            ConfirmBooking
          </Button>);
       }
        
       
        
        }

        else {
            return(<Button size="small" color="primary" onClick={()=>onConfirmBooking(x)}>
            ConfirmBooking
          </Button>);   
        }
    }

      const Loadlist=()=>{

            if(props.carlist){
                return props.carlist.map((x,i)=>{
                    return(
                        <Card className={classes.root} key={i}>
                        <CardActionArea>
                          <CardMedia
                            className={classes.media}
                            image={x.image}
                            title="Image of car"
                          />
                          <CardContent>
                            <Typography gutterBottom variant="h5" component="h2">
                              Model : {modelOptions[x.model].label}
                            </Typography>
                            <Typography variant="body2" color="textSecondary" component="p">
                              Car Price : {x.pricePerDay} per day <br></br>
                              Numberplate {x.numberPlate}, 
                            </Typography>
                          </CardContent>
                        </CardActionArea>
                        <CardActions>
                            {
                                helperMethod(x)
                              

                            }
                               
             
             
                        </CardActions>
                      </Card>
 
 );

    })
    
}


      }
/*console.log(BookingFilters);*/
    return (<div>
        <div>
        
        <form onSubmit={(event)=>onsubmit(event)}>
        <h2>Car Booking</h2>
            <label>City</label>
            <input
                type="text"
                value={BookingFilters.city}
                onChange={(event) => SetBookingFilters((obj)=>({

                    ...obj,
                    city:event.target.value
         
                   }))} />
            
            <br />
            <label>startDate</label>
            <input
                type="date"
                value={BookingFilters.startDate}
                onChange={(event) => SetBookingFilters((obj)=>({

                    ...obj,
                    startDate:event.target.value
         
                   }))} />
            <br />
            <label>endDate</label>
            <input
                type="date"
                value={BookingFilters.endDate}
                onChange={(event) => SetBookingFilters((obj)=>({

                    ...obj,
                    endDate:event.target.value
         
                   }))} />
            <br />
            <label>Model</label>

          
            <select  value={BookingFilters.model}
                onChange={(event) => SetBookingFilters((obj)=>({

                    ...obj,
                    model:event.target.value
         
                   }))}>
          
          
          {modelOptions.map((option,i) => (
            <option value={option.value} key={i}>{option.label}</option>
          ))}
          
          
        </select>
            <br />
            <button onClick={(event)=>onsubmit(event)}>Search</button>
          
           
        </form>
        
        </div>
        <div>
           
        {Loadlist()}
       
        </div>
      
        

    </div>);

}

const mapStateToProps = (state) => {
    console.log(state);
    return { auth: state.auth.isSignedIn,
        carlist: state.filtercar,
        bookedcar: state.confirmcar


    }
}
export default connect(mapStateToProps, { GetCarsForBookings, PostConfirmBookings})(Booking);