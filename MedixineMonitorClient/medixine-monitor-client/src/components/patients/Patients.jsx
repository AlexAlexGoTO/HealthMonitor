import React, { useState } from 'react';
import { useEffect } from 'react';
import Patient from './Patient';
import EditPatient from './EditPatient';
import * as signalR from "@microsoft/signalr";

const API_URL = 'https://localhost:7289'

const Patients = () => {
    const [selectedRow, setSelectedRow] = React.useState("-1");
    const [patients, setPatients] = useState([])

    const getPatients = async () => {
        const response = await fetch(`${API_URL}/patients`);
        const data = await response.json();

        setPatients(data);
        console.log(data)
    }
    
    useEffect(() => {
        getPatients();

        let connection = new signalR.HubConnectionBuilder()
            .withUrl(`${API_URL}/monitor-updates`, {
                skipNegotiation: true,
                transport: signalR.HttpTransportType.WebSockets,
            })
            .build();

        connection.start();

        connection.on("patients-update", (data) => {
            setPatients(prevState => 
                {
                    const index = prevState.findIndex(p => p.id === data.id);
                    if(index > -1) {
                        return prevState.map(item => 
                                    item.id === data.id 
                                    ? {...item, 
                                        name : data.name, 
                                        age: data.age,
                                        address: data.address
                                    } 
                                    : item )
                    } else {
                        return [...prevState, data];
                    }
                }
            )
        });
    }, [])

    return (
        <div className="row">
            <div className="patientList col-md-6">
                <table className="table">
                    <thead className="thead-dark">
                        <tr>
                        <th scope="col">Id</th>
                        <th scope="col">Name</th>
                        <th scope="col">Age</th>
                        <th scope="col">Adress</th>
                        <th scope="col">IsDeleted</th>
                        </tr>
                    </thead>
                    <tbody>
                        {patients.map((patient) => (
                            <tr className="data" key={patient.id} onClick={() => setSelectedRow(patient.id)}>
                                <Patient patient={patient}/>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
            <div className="col-md-6">
                <button onClick={() => setSelectedRow("")} className="btn btn-primary create-item">
                    Create New
                </button>
                <button onClick={() => setSelectedRow(-1)} className="btn btn-dark cancel-item">
                    Cancel
                </button>
                {selectedRow != -1 && <EditPatient id={selectedRow}/> }
            </div>
        </div>
    )
}

export default Patients;