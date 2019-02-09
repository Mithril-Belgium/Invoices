![Build Status](https://dev.azure.com/Mithril-Belgium/Mithril.Invoices/_apis/build/status/Mithril-Belgium.Invoices?branchName=master)

# Mithril invoices
Basically a small project to manage some invoices.


## Getting started
This project use some well known patterns, architectures and practices.

### Onion
This project is structured with a classic onion architecture.
* Mithril.Invoices.Domain: contain all the domain logic. This project should be as pure as possible and should not have any dependency.
* Mithril.Invoices.Infrastructure: contain all the infrastructure logic such as connection to external system (database, cache, ...).
* Mithril.Invoices.Application: contain the use case. Act as a coordinator for simple or complexe scenarii. Should only depends on the domain project.
* Mithril.Invoices.WebApi: contain the HTTP facade. Should only depends on the application project. Currently also references other projects for the dependency injection. Maybe in the future I could put this configuration in another project.

Ensure the startup project is Mithril.Invoices.WebApi.

### Domain Driven Design
This project try to apply domain driven design patterns:
* Rich domain models
* Entities/Value objects
* Aggregate
* Repositories
* ...

### CQRS
The goal of this project was to totally separate commands and query.
You can see this in the Mithril.Invoices.Application project.

### Event sourcing
All events are stored and the projections are built on top of these.
The object states are only stored for performance purposes.

## Dependencies

You can gather all dependencies in the docker-compose.yml.

You can launch all of them with that command:
```
docker stack deploy -c docker-compose.yml mithril_invoices
```
When you finished you can stop the stack:
```
docker stack rm mithril_invoices
```

### EventStore

[EventStore](https://eventstore.org/) is used to store events. 
