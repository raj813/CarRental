import React, { useState, useEffect } from "react";
import History from "../history";
import { Link } from "react-router-dom";
import { connect } from "react-redux";
import { logIn } from "../Actions";
import { signOut } from "../Actions";

import { makeStyles } from "@material-ui/core/styles";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import IconButton from "@material-ui/core/IconButton";
import { green, lightGreen, pink, red } from "@material-ui/core/colors";
import "../CSS/menuitem.css";
import { grey } from "@material-ui/core/colors";

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
  },
  menuButton: {
    marginRight: theme.spacing(2),
  },
  title: {
    flexGrow: 1,
  },
  Typography: {
    fontFamily: "Raleway, Arial",
  },
  header: {
    padding: "0px,0px,0px,0px",
    backgroundColor: "burlywood;",
    color: "black",
  },
}));

const Header = (props) => {
  const classes = useStyles();
  const [menu, setMenu] = useState("false");

  const myFunction = () => {
    setMenu(!menu);
  };
  const username = () => {
    if (props.email) {
      return props.email;
    } else return "Guest";
  };

  const onLogout = () => {
    setMenu(!menu);
    props.signOut();
    History.push("/");
  };

  const onHome=()=>{
    if (props.signedin){
return(<Link to="/booking" className='custom'><button>Home</button></Link>);

    }
else{
  return(<Link to="/" className='custom'><button>Home</button></Link>);
}
  }
  const menuitem = () => {
    if (props.signedin && props.type === "Admin") {
      return (
        <div className="dropdown">
          <button onClick={() => myFunction()} className="dropbtn">
            {" "}
            {username()}{" "}
          </button>
          <div
            className={
              menu === true ? "dropdown-content show" : "dropdown-content"
            }
          >
            <Link to={`/carList`} onClick={()=>myFunction()}>Car List</Link>
            <Link to={`/addCar`} onClick={()=>myFunction()}>Add Car</Link>
            <Link
              onClick={() => myFunction()}
              to={`/userprofile/${props.email}`}
            >
              UserProfile
            </Link>
            <button onClick={() => onLogout()}>Logout</button>
          </div>
        </div>
      );
    } else if (props.signedin && props.type === "User") {
      return (
        <div className="dropdown">
          <button onClick={() => myFunction()} className="dropbtn">
            {" "}
            {username()}{" "}
          </button>
          <div
            className={
              menu === true ? "dropdown-content show" : "dropdown-content"
            }
          >
          <Link to={`/feedback`} onClick={()=>myFunction()}className='custom'>Feedback</Link> 
            <Link to={"/bookinghistory"} onClick={()=>myFunction()}>Booking History</Link>
            <Link
              onClick={() => myFunction()}
              to={`/userprofile/${props.email}`}
            >
              User Profile
            </Link>
            <button onClick={() => onLogout()}>Logout</button>
          </div>
        </div>
      );
    } else
      return (
        <div className="dropdown">
          <button onClick={() => myFunction()} className="dropbtn">
            {" "}
            {username()}{" "}
          </button>
          <div
            className={
              menu === true ? "dropdown-content show" : "dropdown-content"
            }
          >
            <Link to={"/register"} onClick={()=>myFunction()}className='custom'><button>Register</button></Link>
          </div>
        </div>
      );
  };

  return (
    <div >
      <AppBar position="fixed" className={classes.header}>
        <Toolbar>
          <Typography variant="h6" className={classes.title}>
            {onHome()}
            
          </Typography>

          <Typography variant="h6" className={classes.title}>
            <Link to={"/feedback"} className='custom'><button>Feedback</button></Link>
          </Typography>

          {menuitem()}
        </Toolbar>
      </AppBar>
    </div>
  );
};
const mapStateToProps = (state) => {
  return {
    user: state.user,
    email: state.auth.Email,
    signedin: state.auth.isSignedIn,
    type: state.auth.UserType,
  };
};

export default connect(mapStateToProps, { signOut })(Header);
