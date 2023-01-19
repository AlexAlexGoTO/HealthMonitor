import React, { useState } from 'react';
import { useEffect } from 'react';

const API_URL = 'https://localhost:7289'

const EditObservation = ({ id }) => {
    const [observation, setObservation] = useState()

    const getObservation = async () => {
        const response = await fetch(`${API_URL}/observations/${id}`);
        const data = await response.json();

        setObservation(data);
    }

    const submitForm = () => {

        const requestOptions = {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(observation)
        };

        fetch(`${API_URL}/observations`, requestOptions);
    }

    useEffect(() => {
        if (id) {
            getObservation();
        } else{
            setObservation({
                id: "",
                patientId: 0,
                name: "",
                type: 0,
                value: "",
                description: ""
            })
        }
    }, [id])

    return (
        <div className="update-observation-form">
            {observation != null &&
                <div>
                    {observation.id &&
                            <div>
                                <div class="form-group">
                                <label for="id">ObservationID</label>
                                <input type="id" class="form-control" id="id" value={observation.id} disabled />
                            </div>
                        </div>
                    }
                    <form>
                        <div class="form-group">
                            <label htmlFor="patientId">PatientID</label>
                            <input type="number" class="form-control" id="patientId" placeholder="Enter patientID" value={observation.patientId} onChange={(event) => setObservation(prevSate => ({ ...prevSate, patientId: +event.target.value }))} />
                        </div>
                        <div class="form-group">
                            <label htmlFor="namep">Name</label>
                            <input type="text" class="form-control" id="namep" placeholder="Enter name" value={observation.name} onChange={(event) => setObservation(prevSate => ({ ...prevSate, name: +event.target.value }))} />
                        </div>
                        <div class="form-group">
                            <label htmlFor="type">Type</label>
                            <select id="type" class="form-control" value={observation.type} onChange={(event) => setObservation(prevSate => ({ ...prevSate, type: event.target.value }))} >
                                <option defaultValue>Choose type</option>
                                <option value="1">Weight</option>
                                <option value="2">BloodPressure</option>
                                <option value="3">Pulse</option>
                                <option value="4">Steps</option>
                                <option value="5">BloodOxygenSaturation</option>
                            </select>
                        </div>
                        <div class="form-group">
                            <label htmlFor="ovalue">Value</label>
                            <input type="number" class="form-control" id="ovalue" placeholder="Enter value" value={observation.value} onChange={(event) => setObservation(prevSate => ({ ...prevSate, value: event.target.value }))} />
                        </div>
                        <div class="form-group">
                            <label htmlFor="description">Description</label>
                            <textarea type="text" class="form-control" id="description" placeholder="Enter description" value={observation.description} onChange={(event) => setObservation(prevSate => ({ ...prevSate, description: event.target.value }))} />
                        </div>
                    </form>
                    <button onClick={submitForm} class="btn btn-primary">Submit</button>
                </div>
            }
        </div>
    )
}

export default EditObservation;