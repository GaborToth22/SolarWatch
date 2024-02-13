import { useState, useEffect } from "react";

function HomePage() {
    const [logged, setlogged] = useState(false);
    const [adsData, setAdsData] = useState([]);
        
    return (
        <>
            <div className="homepage-container">                    
                <div className="content-area">
                        Sanyi
                </div>
            </div>
        </>
    );
}

export default HomePage;