import React from 'react';

const Observation = ({observation}) => {
    return (
        <React.Fragment>
            <td>{observation.patientId}</td>
            <td>{observation.name}</td>
            <td>{observation.type}</td>
            <td>{observation.value}</td>
            <td>{observation.description}</td>
        </React.Fragment>
    )
}

export default Observation;