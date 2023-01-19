import { useEffect } from 'react';
import Navbar from './components/navbar/Navbar';
import 'bootstrap/dist/css/bootstrap.min.css'
import {
  BrowserRouter as Router,
  Routes,
  Route,
  Link
} from "react-router-dom";
import Observations from './components/observations/Observations';
import Alerts from './components/alerts/Alerts';

const App = () => {
  return (
    <>
      <Navbar />
      <div className="container">
        {/* <Router> */}
                <Routes>
                    <Route path="/observations" element={<Observations />}/>
                    <Route path="/alerts" element={<Alerts />}/>
                    <Route path="/"/>
                </Routes>
          {/* </Router> */}
      </div>
     </>
  );
}

export default App;
