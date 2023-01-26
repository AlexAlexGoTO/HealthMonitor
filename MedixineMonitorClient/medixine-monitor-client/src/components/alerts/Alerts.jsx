import React, { useState, useContext } from 'react';
import { useEffect } from 'react';
import { AlertContext } from '../../alert-context';

const API_URL = 'https://localhost:7289'

const Alerts = () => {
    const [alerts, setAlerts] = useState([]);
    const alertContext = useContext(AlertContext);

    const getAlerts = async () => {
        const response = await fetch(`${API_URL}/alerts`);
        const data = await response.json();

        setAlerts(data);
    }

    const removeAlert = async (id) => {
        const requestOptions = {
            method: 'DELETE',
            headers: { 'Content-Type': 'application/json' },
        };

        await fetch(`${API_URL}/alerts/${id}`, requestOptions);

        const data = alerts.filter(function(alert) { 
            return alert.id !== id 
        })

        setAlerts(data);
    }

    useEffect(() => {
        if(alertContext.alerts.length > 0){
           setAlerts(prevState => [...prevState, alertContext.alerts[alertContext.alerts.length - 1]]);
        }
    }, [alertContext.alerts])

    useEffect(() => {
        alertContext.resetBadgeNumber();
        
        getAlerts();
    }, [])

    return (
        <>
            <table className="table">
                <thead className="thead-dark">
                    <tr>
                    <th scope="col">PatientId</th>
                    <th scope="col">Message</th>
                    <th scope="col">Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {alerts.map((alert) => (
                        <tr key={alert.id}>
                            <td>{alert.patientId}</td>
                            <td>{alert.message}</td>
                            <td><button onClick={() => removeAlert(alert.id)} class="btn btn-dark">Remove</button></td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </>
    )
}

export default Alerts;