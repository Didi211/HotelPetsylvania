//In this file, we are going to implement our Axios calls to the backend
//and it is recommended to create separate API service files 
//for every entity in our database or RESTful resource.

//ovo pratim sajt https://code-maze.com/vuejs-axios-http-environment-files/ i lika na youtube-u
import axios from 'axios';

export default () => {
    return axios.create({
        baseURL: 'https://localhost:5001',
        withCredentials: false,
        headers: {
            Accept: "application/json",
            "Content-Type": "application/json"
        }
    });
}