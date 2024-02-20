import { useState, } from "react";
import {  Link } from "react-router-dom";

function HomePage({ loggedUser, setLoggedUser}) {
    const[cityName, setCityName] = useState("");
    const[date, setDate] = useState("");
    const[solarData, setSolarData] = useState("");
        
    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            const response = await fetch(`/api/SolarWatch?cityName=${cityName}&date=${date}`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${loggedUser.token}`
                },
            });
            if (!response.ok) {
                throw new Error('Failed to fetch data');
            }
            const data = await response.json();
            console.log(data)
            setSolarData(data);
            
        } catch (error) {
            console.error("invalid city or date")
        }
    }
    
    return (
        <div className="background-container">
            { loggedUser ? 
            (solarData === "" ?
            (<div className="homepage-container">                    
                <div className="content-area">
                    <h2 className="title">SolarWatch</h2>
                    <form>
                    <input type="text" placeholder="City Name" value={cityName} onChange={(e) => setCityName(e.target.value)}/> <br />
                    <input type="date" placeholder="date" value={date} onChange={(e) => setDate(e.target.value)}/> <br />
                    <button className="button" onClick={handleSubmit}>Submit</button>
                    </form>
                </div>
            </div>) 
            :
            (<div className="homepage-container">                    
                <div className="content-area">
                   <h2 className="title">SolarWatch</h2>
                   <form>
                   <input type="text" placeholder="City Name" value={cityName} onChange={(e) => setCityName(e.target.value)}/> <br />
                   <input type="date" placeholder="date" value={date} onChange={(e) => setDate(e.target.value)}/> <br />
                    <button className="button" onClick={handleSubmit}>Submit</button>
                    </form>
                </div>
                <div className="solarData">
                    <h2>{cityName}</h2>
                    <h3>Date: {solarData.date.slice(0, 10)}</h3>
                    <div className="sunrise-info">
                        <p>Sunrise: {solarData.sunRise}</p>
                    </div>
                    <div className="sunset-info">
                        <p>Sunset: {solarData.sunSet}</p>
                    </div>
                </div>
            </div>)
            ) 
            :
            (<div className="homepage-container">
                <div className="content-area">
                    <p>You must log in to view this page</p>
                    <Link to="/login">
                        <button className="button">Login</button>
                    </Link>
                </div>
            </div>
            )
            }
        </div>
    );
}

export default HomePage;