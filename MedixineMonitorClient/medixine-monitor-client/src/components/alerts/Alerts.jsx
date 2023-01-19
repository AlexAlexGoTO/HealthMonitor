import React, { useState } from 'react';
import { useEffect } from 'react';

import * as signalR from '@microsoft/signalr'

const API_URL = 'https://localhost:7289'

const Alerts = () => {
    const [alerts, setAlerts] = useState([]);

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
        let connection = new signalR.HubConnectionBuilder()
        .withUrl(`${API_URL}/alertNotifications`, {
            skipNegotiation: true,
            transport: signalR.HttpTransportType.WebSockets
        })
        .build();

        connection.start()

        connection.on("channel", (data) => {
            setAlerts(prevSate => ([ ...prevSate, data]));
        })

        getAlerts();
    }, [])

    return (
        <>
            <table className="table">
                <thead className="thead-dark">
                    <tr>
                    <th scope="col">PatientId</th>
                    <th scope="col">Message</th>
                    </tr>
                </thead>
                <tbody>
                    {alerts.map((alert) => (
                        <tr key={alert.id}>
                            <td>{alert.patientId}</td>
                            <td>{alert.message}</td>
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