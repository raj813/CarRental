import React, { useEffect, useState } from "react";
import Select from "react-select";
import { connect } from "react-redux";
import { getCar, updateCar } from "../../Actions/index";
import { useParams } from "react-router-dom";
import { provinceOptions,modelOptions } from "./options";
import history from '../../history'
import '../../CSS/form.css'

const UpdateCar = (props) => {
    let data={}
    let params=useParams()

    
  const [formValues, setFormValues] = useState({
    carId: 0,
    model: 0,
    pricePerDay: 0,
    image: '',
    numberPlate: '',
    locationId:0,
    city:'',
    province:0
    // location:{
    //     locationId:0,
    //     city:'',
    //     province:0
    // }
  });

  useEffect(()=>{
    //props.getCar(params.id)
    data=props.cars.find((car)=>{
        return car.carId===+params.id
     })
     setFormValues({
        carId: data.carId,
        model: data.model,
        pricePerDay: data.pricePerDay,
        image: data.image,
        numberPlate: data.numberPlate,
        locationId:data.locationId,
        city:data.location.city,
        province:data.location.province
     })
},[])

  const onFormSubmit = (event) => {
    event.preventDefault();
    props.updateCar(params.id,formValues).then(()=>{
      history.push('/carList')
    });
  };

  return (
    <div>      
      <form onSubmit={(e) => onFormSubmit(e)} style={{width:'80%',alignContent:'center'}}>
      <h2>UPDATE CAR</h2>
      <br></br>
        <label>Model</label>
        <Select
          options={modelOptions}
          value={{label:modelOptions[formValues.model].label}}
          defaultValue={formValues.model}
          onChange={(e) =>
            setFormValues((obj) => ({
              ...obj,
              model: e.value,
            }))
          }
        ></Select>

        <br></br>
        <label>Price per day</label>
        <input
          type="number"
          value={formValues.pricePerDay}
          min="0"
          onChange={(e) =>
            setFormValues((obj) => ({
              ...obj,
              pricePerDay: e.target.value,
            }))
          }
        ></input>

        <br></br>
        <label>Image URL</label>
        <input
          type="text"
          value={formValues.image}
          onChange={(e) =>
            setFormValues((obj) => ({
              ...obj,
              image: e.target.value,
            }))
          }
        ></input>

        <br></br>
        <label>Plate Number</label>
        <input
          type="text"
          value={formValues.numberPlate}
          onChange={(e) =>
            setFormValues((obj) => ({
              ...obj,
              numberPlate: e.target.value,
            }))
          }
        ></input>

        <br></br>
        <label>City Name</label>
        <input
          type="text"
          value={formValues.city}
          minLength="2"
          onChange={(e) =>
            setFormValues((obj) => ({
              ...obj,
              city: e.target.value,
            }))
          }
        ></input>
        
        <br></br>
        <label>Province Name</label>
        <Select
          options={provinceOptions}
          value={{label:provinceOptions[formValues.province].label}}
          onChange={(e) =>
             setFormValues((obj) => ({
               ...obj,
               province: e.value,
             }))
          }
        ></Select>

        <button onClick={(event)=>onFormSubmit(event)}>Submit</button>
      </form>
    </div>
  );
};

const mapStateToProps = (state, ownProps) => {
    return { cars: state.cars };
  };

export default connect(mapStateToProps,{updateCar,getCar}) (UpdateCar)