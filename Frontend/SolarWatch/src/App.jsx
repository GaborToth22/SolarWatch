import React, { useState, } from 'react';
import './App.css'
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import HomePage from "./Components/HomePage.jsx";
import LoginPage from './Components/LoginPage.jsx';
import RegistrationPage from './Components/RegistrationPage.jsx';


function App() {  
  const [loggedUser, setLoggedUser] = useState(undefined);

  return (
    <Router>
      <Routes>
        <Route path="/" element={<HomePage {...{ loggedUser, setLoggedUser }}/>} />
        <Route path="/login" element={<LoginPage {...{ loggedUser, setLoggedUser }}/>} />
        <Route path="/registration" element={<RegistrationPage />} />
      </Routes>
    </Router>
  )
}

export default App
