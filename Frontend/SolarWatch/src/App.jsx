import './App.css'
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import HomePage from "./Components/HomePage.jsx";
import LoginPage from './Components/LoginPage.jsx';
import RegistrationPage from './Components/RegistrationPage.jsx';


function App() {  

  return (
    <Router>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/registration" element={<RegistrationPage />} />
      </Routes>
    </Router>
  )
}

export default App
