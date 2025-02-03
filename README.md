# Small demo system for monitoring patient measurements and generating alerts based on measurement deviations
1. Patients microservice (sync communication)
2. Main service
3. Small react app for front-end

# HealthMonitor
1. Architecture Based on Clean Architecutre pattern + CQRS + MediatR
2. SignalR for alerts
3. Added few UnitTests
4. Added few Integration test with InMemoryDatabase for testing purposes
5. Used EntityFramework ORM

TODO: 
1. FRONT-END: move web-sockets hub connection to contex, to make it able to receive alerts on any screen.
2. BACK-END: change inMemoryStore saving for last observations and save only average number
3. RabbitMQ for communication

UPD: 24.01
1. Fixed alerts in UI, now it's global, added popups and badge counter.
2. Changed "average" calculation for last 3 observations

UPD: 25.01
1. Added Patients Microservice

UPD: 26.01
1. Converted to real-time app
