import { useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";

function LoginPage({ loggedUser, setLoggedUser }) {
    
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
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
                await setLoggedUser(data);
                console.log(data)                
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
        <div className="background-container">
            <div className="login-page">
                {loggedUser ? (
                    <p>Welcome, {loggedUser.userName}</p>
                ) : (
                    <>
                        <p>Please log in to access the SolarWatch.</p>
                        {loginError && <p>{loginError}</p>}
                        <div className="login-form">
                            <input type="text" placeholder="Username" value={username} onChange={(e) => setUsername(e.target.value)} />
                            <input type="password" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)} /><br></br>
                            <button className="button" onClick={handleLoginSubmit}>Log in</button>      <br></br>                      
                                <Link to="/registration" style={{ textDecoration: "underline", cursor: "pointer", color: "silver"  }}>
                                    Click here to Register
                                </Link>
                                <br />
                            <div className="cancel-button">                                
                            </div>
                        </div>
                    </>
                )}
            </div>
            
        </div>
    )
}

export default LoginPage;