# medixineMonitor-
1. Architecture Based on Clean Architecutre pattern + CQRS + MediatR
2. SignalR for alerts
3. Small react app for front-end
4. Added few UnitTests
5. Added few Integration test with InMemoryDatabase for testing purposes
6. Used EntityFramework ORM

TODO: 
1. FRONT-END: move web-sockets hub connection to contex, to make it able to receive alerts on any screen.
2. BACK-END: change inMemoryStore saving for last observations and save only average number

UPD: 24.01
1. Fixed alerts in UI, now it's global, added popups and badge counter.
2. Changed "average" calculation for last 3 observations