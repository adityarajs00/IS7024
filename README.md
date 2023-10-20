# CrimeWatch
-----
**Design Document**


Aditya Raj Singhal


Ishaan Chaudhari


Paresh Natarajan


Shivani Pilli


Yash Shukla


## Introduction
 You are new to Cincinnati, you need advice on which neighborhood to live in. Who do you turn to? *CrimeWatch* is an application that filters safe neighborhoods based on crime statistics provided by the police department.

- Attributes safe based on crime reports (reported and handled-closed or not)
- Based on your gender
- Based on age
- Based on violent and non-violent crime  (Weapons and incident type)
- Based on response time

## Data Feed 
- [Crime Incidents](https://data.cincinnati-oh.gov/resource/k59e-2pvf.json)


- [Calls for Response](https://data.cincinnati-oh.gov/resource/gexm-h6bt.json)


## Functional Requirements


#### Requirement: Search for safe neighborhoods

##### Scenario: 

A user wants to know if a particular neighborhood is safe based on age or gender or response time


##### Dependencies:

Neighborhood crime incidents data and police call for service data

##### Assumptions:

- Age is in years


- Response time is in minutes

##### Example:

**Given** the data available

**When** I give the Age or Gender or response time

**Then** I should get the neighborhood name which has fastest response time, lowest incidents reported and highest incident close rate

## Scrum Roles
- Frontend : Aditya Raj Singhal
- Integration developer: Ishaan Chaudhari
- Scrum Master: Shivani Pilli
- Product owner: Yash Shukla
- DevOps: Paresh Natarajan

## Weekly meetings

Friday: 5:30 PM on Teams
