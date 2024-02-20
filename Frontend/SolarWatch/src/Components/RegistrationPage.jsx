import { Link } from "react-router-dom";
import { useState } from 'react';

function RegistrationPage() {    
    const [email, setEmail] = useState('');
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [registrationMessage, setRegistrationMessage] = useState('');

    const handleSubmit = async (e) =>{
        e.preventDefault();

        try {
            const response = await fetch('api/Auth/Register', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    email,
                    username,
                    password,
                }),
            });

            if (response.ok) {
                console.log('Success registration!');  
                setRegistrationMessage('Success registration!');              
            } else {
                console.error('Registration failed!');
                setRegistrationMessage('Registration failed! Please try again.');                
            }
        } catch (error) {
            console.error('An error occured during the:', error);
        }
    }
    
    return (
        <div className="background-container">
        <div className="registration-popup">
            <div className="registration-content">                
            <form onSubmit={handleSubmit}>
                <input type="text" placeholder="Email" value={email} onChange={(e) => setEmail(e.target.value)} /><br />
                <input type="text" placeholder="Username" value={username} onChange={(e) => setUsername(e.target.value)} /><br />
                <input type="password" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)} /><br />
                <button className="button" type="submit">Register</button>                
            </form>
            <div>{registrationMessage}</div>
            <br />
            <Link to="/login">
                <button className="button">Login</button>
            </Link>
            
            </div>         
        </div>
        </div>
        
    );
}

export default RegistrationPage;