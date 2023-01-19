import React, { useState } from 'react';
import { useEffect } from 'react';
import EditObservation from './EditObservation';
import Observation from './Observation';
import './observations.css'

const API_URL = 'https://localhost:7289'

const Observations = () => {
    const [selectedRow, setSelectedRow] = React.useState("-1");
    const [observations, setObservations] = useState([])

    const getObservations = async () => {
        const response = await fetch(`${API_URL}/observations`);
        const data = await response.json();

        setObservations(data);
        console.log(data)
    }
    
    useEffect(() => {
        getObservations();
    }, [])

    return (
        <div className="row">
            <div className="observationList col-md-6">
                <table className="table">
                    <thead className="thead-dark">
                        <tr>
                        <th scope="col">PatientId</th>
                        <th scope="col">Name</th>
                        <th scope="col">Type</th>
                        <th scope="col">Value</th>
                        <th scope="col">Description</th>
                        </tr>
                    </thead>
                    <tbody>
                        {observations.map((observation) => (
                            <tr className="data" key={observation.id} onClick={() => setSelectedRow(observation.id)}>
                                <Observation observation={observation}/>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
            <div className="editObservation col-md-6">
                <button onClick={() => setSelectedRow("")} className="btn btn-primary create-observation">
                    Create New
                </button>
                {selectedRow != -1 && <EditObservation id={selectedRow}/> }
            </div>
        </div>
    )
}

export default Observations;