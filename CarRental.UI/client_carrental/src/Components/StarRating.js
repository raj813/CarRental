// import React,{useState} from 'react';
// import ReactDOM, { render } from 'react-dom';
// import StarRatingComponent from 'react-star-rating-component';
// import {connect} from 'react-redux';

// const StarRating=(props)=>{

//     const[Rating,SetRating] = useState({
//         Rating : 1
//     })

//     const onStarClick = (nextValue, prevValue, name)=>{
//         SetRating:nextValue
//     }

//     return(
//         <div>
//            <h2>Rating from state: {rating}</h2>
//             <StarRatingComponent 
//                 name="rate1" 
//                 starCount={5}
//                 value={rating}
//                 onStarClick={onStarClick}
//             /> 
//         </div>
//     )
// }


// export default connect(null) (StarRating);