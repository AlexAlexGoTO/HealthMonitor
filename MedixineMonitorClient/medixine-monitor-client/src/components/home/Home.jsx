import React, { useState, useContext } from 'react';

const Home = () => {
    return (
        <>
            <div class="list-group">
  <a href="#" class="list-group-item list-group-item-action flex-column align-items-start">
    <div class="d-flex w-100 justify-content-between">
      <h5 class="mb-1">Application</h5>
      <small>today</small>
    </div>
    <p class="mb-1">This is real time application</p>
    <p class="mb-2">Every time when we change/add patient or observation all users will see those changes in real time.</p>
    <p class="mb-3">Every time the indicators exceed the allowable values we receive a notification-alert on our screen.</p>
  </a>
  <a href="#" class="list-group-item list-group-item-action flex-column align-items-start">
    <div class="d-flex w-100 justify-content-between">
      <h5 class="mb-1">Front-End</h5>
    </div>
    <p class="mb-1">Was written in simple way with React with few libraries like Bootstrap and SignalR for WebSockets that I'm using for notification-alerts and updating all ui data in real time.</p>
  </a>
  <a href="#" class="list-group-item list-group-item-action flex-column align-items-start">
    <div class="d-flex w-100 justify-content-between">
      <h5 class="mb-1">Back-End</h5>
      <small class="text-muted">today</small>
    </div>
    <p class="mb-1">Was written on .NET vesion 6. SQL server express LocalDB as database. Architecture based on Clean Architecture pattern + CQRS + MediatR.</p>
    <p class="mb-1">Patients run as separate Microservice and using same DB but different Schema. It's "Views between database schemas" approach are used to expose data between schemas (i.e. microservices).
    This strategy is a good approach for migrating from a monolith to microservices. Communication implemented throught REST.</p>
  </a>
</div>
        </>
    )
}

export default Home;