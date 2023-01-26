import React, { useState } from 'react';
import { useEffect } from 'react';

const API_URL = 'https://localhost:7289'

const EditPatient = ({ id }) => {
    const [patient, setPatient] = useState()

    const getPatient = async () => {
        const response = await fetch(`${API_URL}/patients/${id}`);
        const data = await response.json();

        setPatient(data);
    }

    const submitForm = () => {

        const requestOptions = {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(patient)
        };

        fetch(`${API_URL}/patients`, requestOptions);
    }

    useEffect(() => {
        if (id) {
            getPatient();
        } else{
            setPatient({
                id: 0,
                name: "",
                age: 0,
                address: ""
            })
        }
    }, [id])

    return (
        <div className="update-item-form">
            {patient != null &&
                <div>
                    {patient.id != 0 &&
                        <div>
                            <div class="form-group">
                                <label for="id">PatientID</label>
                                <input type="id" class="form-control" id="id" value={patient.id} disabled />
                            </div>
                        </div>
                    }
                    <form>
                        <div class="form-group">
                            <label htmlFor="namep">Name</label>
                            <input type="text" class="form-control" id="namep" placeholder="Enter name" value={patient.name} onChange={(event) => setPatient(prevSate => ({ ...prevSate, name: event.target.value }))} />
                        </div>
                        <div class="form-group">
                            <label htmlFor="age">Age</label>
                            <input type="number" class="form-control" id="age" placeholder="Enter age" value={patient.age} onChange={(event) => setPatient(prevSate => ({ ...prevSate, age: +event.target.value }))} />
                        </div>
                        <div class="form-group">
                            <label htmlFor="address">Address</label>
                            <textarea type="text" class="form-control" id="address" placeholder="Enter address" value={patient.address} onChange={(event) => setPatient(prevSate => ({ ...prevSate, address: event.target.value }))} />
                        </div>
                    </form>
                    <button onClick={submitForm} class="btn btn-primary">Submit</button>
                </div>
            }
        </div>
    )
}

export default EditPatient;