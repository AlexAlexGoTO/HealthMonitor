import React from 'react';

const Patient = ({patient}) => {
    return (
        <React.Fragment>
            <td>{patient.id}</td>
            <td>{patient.name}</td>
            <td>{patient.age}</td>
            <td>{patient.address}</td>
            <td>{patient.isDeleted ? 'Yes':'No'}</td>
        </React.Fragment>
    )
}

export default Patient;