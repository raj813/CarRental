import React, { useEffect, useState } from "react";
import { connect } from "react-redux";
import { userUpdate, userGet } from "../Actions";
import classes from "../CSS/UserProfile.module.css";

const isEmpty = (value) => value === "";

const UserDisplay = (props) => {
    const [userValues, setUserValues] = useState({
        UserId: "",
        PasswordHash: "",
        FirstName: "",
        LastName: "",
        Email: "",
        PhoneNumber: "",
    });

    const [formInputValidity, setFormInputValidity] = useState({
        FirstName: true,
        LastName: true,
        Email: true,
        PhoneNumber: true,
    });

    useEffect(() => {
        props.userGet();
    }, []);

    useEffect(() => {
        setUserValues({
            ...userValues,
            UserId: props.user.userId,
            PasswordHash: props.user.passwordHash,
            FirstName: props.user.firstName,
            LastName: props.user.lastName,
            PhoneNumber: props.user.phone,
            Email: props.user.email,
        });
    }, [props.user]);

    const onFormSubmit = (event) => {
        event.preventDefault();

        const validFirstName = !isEmpty(userValues.FirstName);
        const validLastName = !isEmpty(userValues.LastName);
        const validPhone = !isEmpty(userValues.PhoneNumber);
        const validEmail = !isEmpty(userValues.Email);

        setFormInputValidity({
            FirstName: validFirstName,
            LastName: validLastName,
            Email: validEmail,
            PhoneNumber: validPhone,
        });

        const formIsValid =
            validFirstName && validLastName && validPhone && validEmail;

        if (formIsValid) {
            props.userUpdate(userValues);
        }
    };

    const onDisplay = () => {
        if (userValues.UserId === "") {
            return <div>loading</div>;
        } else {
            console.log(userValues);
            return (
                <div>                    
                    <form>
                    <h2>User Profile</h2>
                        <label className={classes.subHeader}>First Name</label>
                        <input
                            type="text"
                            className={classes.text}
                            value={userValues.FirstName}
                            onChange={(event) =>
                                setUserValues((obj) => ({
                                    ...obj,
                                    FirstName: event.target.value,
                                }))
                            }
                        />
                        {!formInputValidity.FirstName && (
                            <p className={classes.invalid}>
                                Please enter a valid first name!
                            </p>
                        )}
                        <br />
                        <label className={classes.subHeader}>Last Name</label>
                        <input
                            type="text"
                            className={classes.text}
                            value={userValues.LastName}
                            onChange={(event) =>
                                setUserValues((obj) => ({
                                    ...obj,
                                    LastName: event.target.value,
                                }))
                            }
                        />
                        {!formInputValidity.LastName && (
                            <p className={classes.invalid}>Please enter a valid last name!</p>
                        )}
                        <br />
                        <label className={classes.subHeader}>Email</label>
                        <input
                            type="text"
                            className={classes.text}
                            value={userValues.Email}
                            onChange={(event) =>
                                setUserValues((obj) => ({
                                    ...obj,
                                    Email: event.target.value,
                                }))
                            }
                        />
                        {!formInputValidity.Email && (
                            <p className={classes.invalid}>Please enter a valid email id!</p>
                        )}
                        <br />
                        <label className={classes.subHeader}>Phone Number</label>
                        <input
                            type="text"
                            className={classes.text}
                            value={userValues.PhoneNumber}
                            onChange={(event) =>
                                setUserValues((obj) => ({
                                    ...obj,
                                    PhoneNumber: event.target.value,
                                }))
                            }
                        />
                        <br />
                        {!formInputValidity.PhoneNumber && (
                            <p className={classes.invalid}>
                                Please enter a valid phone number!
                            </p>
                        )}
                        <button
                            className={classes.button}
                            onClick={(event) => onFormSubmit(event)}
                        >
                            Submit
            </button>
                    </form>
                </div>
            );
        }
    };
    return <div>{onDisplay()}</div>;
};

const mapStateToProps = (state) => {
    return {
        user: state.user,
        email: state.auth.Email,
    };
};

export default connect(mapStateToProps, { userUpdate, userGet })(UserDisplay);
