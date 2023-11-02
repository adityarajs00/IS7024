# Neighborhood Watch
-----
**Design Document**


Aditya Raj Singhal


Ishaan Chaudhari


Paresh Natarajan


Shivani Pilli


Yash Shukla


## Icon
<img src="NW%20(2).png" alt="Neighborhood Watch Logo" style="width:200px;"/>

## Introduction
 You are new to Cincinnati, you need advice on which neighborhood to live in. Who do you turn to? *Neighborhood Watch* is an application that filters safe neighborhoods based on crime statistics provided by the police department.

- Attributes safe based on crime reports (reported and handled-closed or not)
- Based on your gender
- Based on age
- Based on violent and non-violent crime  (Weapons and incident type)
- Based on response time

## Data Feed 

We'll be joining these datasets by the cpd_neighborhood column


- [Crime Incidents](https://data.cincinnati-oh.gov/resource/k59e-2pvf.json)


- [Calls for Response](https://data.cincinnati-oh.gov/resource/gexm-h6bt.json)


## Functional Requirements


#### Requirement 1: Search for safety attributes of a particular neighborhood

##### Scenario: 

As a user, I want to be able to search for safety by neighborhood, so that I can decide where to live.


##### Dependencies:

Neighborhood crime incidents data and police call for service data

##### Assumptions:

- Neighborhood is in Cincinnati


- Response time is in minutes

##### Example 1.1:

**Given** the crime & call response data is available

**When** I search for "Clifton"

**Then** I should receive at least one result with these attributes:

neighborhood: Clifton


Safety Index: Safe


Response Time Index: High


Safe Time to Visit: 10 AM to 5 PM




##### Example 1.2:

**Given** the crime & call response data is available

**When** I search for "Female"

**Then** I should receive at least one result with these attributes:

neighborhood: Clifton


Safety Index: Safe


Response Time Index: High


Safe Time to Visit: 10 AM to 5 PM


Female Safety Index: High







##### Example 1.3:

**Given** the crime & call response data is available

**When** I search for "C"

**Then** I should receive results with neighbourhoods that start with "C" with these attributes:

neighborhood: Clifton


Safety Index: Safe


Response Time Index: High


Safe Time to Visit: 10 AM to 5 PM



neighborhood: Calhoun


Safety Index: Safe


Response Time Index: Moderate


Safe Time to Visit: 9 AM to 8 PM



neighborhood: Corryville


Safety Index: Moderately Safe


Response Time Index: High


Safe Time to Visit: 10 AM to 3 PM




##### Example 1.4:

**Given** crime & call response data is available

**When** I search for “asuhashdoahfoiaehf”

**Then** I should receive zero results (an empty list)



#### Requirement 2: Provide neighborhood safety review 

##### Scenario: 

As a user, I want to be able to provide safety review about my neighborhood


##### Dependencies:

- The device has an internet connection


##### Assumptions:

- The user has lived or is living in the neighborhood


##### Example 2.1:


**Given** crime & call response data is available

**When** I add a review about a neighborhood and hit save

**Then** the data should be saved and when I go to the reviews page I should be able to see the review

##### Example 2.2:


**Given** crime & call response data is available

**When** I add a review about a neighborhood and don't hit save

**Then** the data should not be saved and when I go to the reviews page I should not be able to see my review

##### Example 2.3:


**Given** crime & call response data is available

**When** I add a photo for a review of a neighborhood and hit save

**Then** the data should be saved and when I go to the reviews page I should be able to see the review with the photo




## Scrum Roles
- Frontend : Aditya Raj Singhal
- Integration developer: Ishaan Chaudhari
- Scrum Master: Shivani Pilli
- Product owner: Yash Shukla
- DevOps: Paresh Natarajan

## Weekly meetings

Friday: 5:30 PM on Teams