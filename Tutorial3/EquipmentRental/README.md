# Project description
A console application in C# that supports a university equipment rental service was implemented. The system
allows a user to create/rent/return 3 types of equipment: laptop, camera and projector. The generation of report was implemented
along with different methods for supporting this feature.

## Project structure
The project is split by directories:
* Models - containing classes needed for initialization of user/equipment/rent objectin the system\
  ( These classes represent the problem domain and contain only essential data and minimal logic )
* Services - directory that contains all app`s services\
  ( All business rules are located here instead of being spread across the application )
* Program.cs - containing all cases for testing the system based on use cases and scenarios from
assigment file for this tutorial.

## Design decision
Each class has a single, clear responsibility where:
* Models classes represent data
* Services handle business logic
* Program handles interaction
