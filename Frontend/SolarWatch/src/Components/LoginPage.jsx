import { useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import RegistrationPage from "./RegistrationPage";

function LoginPage() {
    
    const [username, setUsername] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [isLoggedIn, setIsLoggedIn] = useState(false);
    const [loginError, setLoginError] = useState(null);
    const navigate = useNavigate();

    const handleLoginSubmit = async (e) => {
        e.preventDefault();

        try {
            const response = await fetch('api/Auth/Login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    username: username,
                    password: password,
                }),
            });

            if (response.ok) {
                const data = await response.json();
                setIsLoggedIn(true);
                setUsername(data.userName)
                console.log('Login successful! Welcome, '+ data.userName);
                setTimeout(() => {
                    navigate('/');
                }, 2000);
            } else {
                setLoginError('Invalid username or password. Please try again.');
                console.error('Login failed!');
            }
        } catch (error) {
            setLoginError('An error occurred during login. Please try again later.');
            console.error('An error occurred during login:', error);
        }
    }    
   
    return (
        <>
            <div className="login-page">
                {isLoggedIn ? (
                    <p>Welcome, {username}</p>
                ) : (
                    <>
                        <p>Please log in to access the full website.</p>
                        {loginError && <p>{loginError}</p>}
                        <div className="login-form">
                            <input type="text" placeholder="Username" value={username} onChange={(e) => setUsername(e.target.value)} />
                            <input type="password" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)} />
                            <button onClick={handleLoginSubmit}>Log in</button>
                            <p>Not a member yet? 
                                <Link to="/registration">
                                    <span style={{ textDecoration: "underline", cursor: "pointer" }}>Click here to Register</span>
                                </Link>
                            </p>
                        </div>
                    </>
                )}
                <div className="cancel-button">
                    <Link to="/">
                        <button>Home</button>
                    </Link>
                </div>
            </div>
        </>
    )
}

export default LoginPage;