import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { connect } from "react-redux";
import { getCars, deleteCar, updateCar } from "../../Actions";

import { makeStyles } from "@material-ui/core/styles";
import Card from "@material-ui/core/Card";
import CardActionArea from "@material-ui/core/CardActionArea";
import CardActions from "@material-ui/core/CardActions";
import CardContent from "@material-ui/core/CardContent";
import CardMedia from "@material-ui/core/CardMedia";
import Button from "@material-ui/core/Button";
import Typography from "@material-ui/core/Typography";
import { provinceOptions,modelOptions } from "./options";

const CarList = (props) => {
  const [isDeleteClicked, setisDeleteClicked] = useState(false)

  const onDeleteCar = (id) => {
    props.deleteCar(id).then(()=>{
      setisDeleteClicked(true)
    });    
    console.log(`car with id ${id} has now been deleted`);
  };

  useEffect(() => {
    props.getCars();
  }, [isDeleteClicked]);
  

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

  const carList = () => {
    if (props.cars.length > 1) {
      console.log(props.cars);
      return props.cars.map((x) => {
        return (
          <Card className={classes.root} key={x.carId}>
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
                  Located in {x.location.city}, {provinceOptions[x.location.province].label}
                </Typography>
              </CardContent>
            </CardActionArea>
            <CardActions>
              <Button size="small" color="secondary" onClick={(e)=>{onDeleteCar(x.carId)}}>
                Delete
              </Button>
              <Link to={`/updateCar/${x.carId}`} style={{ textDecoration: 'none' }}>
              <Button size="small" color="primary">
                Update
              </Button>
              </Link>
            </CardActions>
          </Card>
        );
      });
    }
  };
  return (
    <div style={{margin:"10%"}}>
      <Link to={`/addCar`} className={'custom'}><button>Add Car</button></Link>
      <h2 style={{textAlign:"center",color:"burlywood"}}> CAR LIST</h2>
      <div className={classes.flex}>{carList()}</div>
    </div>
  );
};

const mapStateToProps = (state, ownProps) => {
  return { cars: state.cars };
};

export default connect(mapStateToProps, { getCars, deleteCar, updateCar })(
  CarList
);
