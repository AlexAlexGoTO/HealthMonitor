import React from 'react';

const Observation = ({observation}) => {
    const types = {
        "0": "Weight",
        "1": "BloodPressure",
        "2": "Pulse",
        "3": "Steps",
        "4": "BloodOxygenSaturation",
    }

    return (
        <React.Fragment>
            <td>{observation.patientId}</td>
            <td>{observation.name}</td>
            <td>{types[observation.type]}</td>
            <td>{observation.value}</td>
            <td>{observation.description}</td>
        </React.Fragment>
    )
}

export default Observation;