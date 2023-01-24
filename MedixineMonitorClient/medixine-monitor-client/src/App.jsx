import { useEffect, useState } from 'react';
import Navbar from './components/navbar/Navbar';
import 'bootstrap/dist/css/bootstrap.min.css'
import './base.css'
import {
  BrowserRouter as Router,
  Routes,
  Route,
  Link
} from "react-router-dom";
import Observations from './components/observations/Observations';
import Alerts from './components/alerts/Alerts';
import { AlertContext, AlertContextProvider } from './alert-context';
import { Alert } from 'react-bootstrap';


const App = () => {
  const [showAlerts, setShowAlerts] = useState(true);

  return (
    <>
      <AlertContextProvider>
        <Navbar />
        <div className="container">
            <Routes>
                <Route path="/observations" element={<Observations />}/>
                <Route path="/alerts" element={<Alerts />}/>
                <Route path="/"/>
            </Routes>
        </div>
        <div className='custom-alerts col-md-12'>
          <AlertContext.Consumer>
              {api => {
                return api.alerts.map((alert) => {
                      return <Alert key={alert.id} show={api.visibility[alert.id] !== false} variant={'info'} onClick={() => api.hide(alert.id)} dismissible>{alert.message}</Alert>
                  })
                }
              } 
          </AlertContext.Consumer>
        </div>
      </AlertContextProvider>
     </>
  );
}

export default App;
