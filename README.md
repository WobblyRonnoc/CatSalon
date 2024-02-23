# What is this?

##### This is my capstone project of the Web Development & Internet Applications program at Algonquin College. 

##### It's a web application made using the C# .NET Core MVC & Entity Framework Core!

## ![Landing Page](https://github.com/WobblyRonnoc/CatSalon/blob/main/Configuration%20Files/Landing%20Page.gif)

# What does it do?

Most grooming service web pages are a single form with no way to save your info for repeated booking. 

This one allows you to save your cat's info to a profile and repeatedly use it for booking appointments.  It uses a login system to save pet information and allows users book and manage appointments for multiple cats at once.



### 														User System

##### Logging in

#### ![Login](https://github.com/WobblyRonnoc/CatSalon/blob/main/Configuration%20Files/Login.gif)

##### Editing & deleting account 

![Edit and Delete Account](https://github.com/WobblyRonnoc/CatSalon/blob/main/Configuration%20Files/Edit%20and%20Delete%20Account.gif)

### 													Cat Profile System 

##### Adding a new cat profile

![Add Cat](https://github.com/WobblyRonnoc/CatSalon/blob/main/Configuration%20Files/Add%20Cat.gif)

##### Editing and Deleting a cat profile

![Edit and Delete](https://github.com/WobblyRonnoc/CatSalon/blob/main/Configuration%20Files/Edit%20and%20Delete.gif)

### 												Appointment Management

##### Booking an appointment

![Make Appointment](https://github.com/WobblyRonnoc/CatSalon/blob/main/Configuration%20Files/Make%20Appointment.gif)

##### Cancelling an appointment

![Making an Appointment](https://github.com/WobblyRonnoc/CatSalon/blob/main/Configuration%20Files/Cancel%20Appointment.gif)

### Service Listing

![Service listing](https://github.com/WobblyRonnoc/CatSalon/blob/main/Configuration%20Files/Services.png)

These are all pulled from the database. No hardcoding here!



### Form Validation

###### 									

<img src="https://github.com/WobblyRonnoc/CatSalon/blob/main/Configuration%20Files/Signup-form.png" alt="image-20240222143833102" style="zoom:150%;" />

###### 													Checking the database for existing contact information

<img src="https://github.com/WobblyRonnoc/CatSalon/blob/main/Configuration%20Files/DB-screenshot.png" alt="image-20240222143856832" style="zoom:150%;" />







## The Objective

##### - Create a dynamic web application capable of database interactions and CRUD operations.

##### - Design and implement a database 

##### - Scaffold the application using EntityFramework Core 



##### Demonstrate my knowledge of:

- ##### MVC: Views, Layouts, Controllers, Models, Routes

- ##### Model Validation 

- ##### EntityFramework Core

- ##### Ajax

- ##### Database Design

- ##### SQL

- ##### Authentication

- ##### DataType conversion 

- ##### LINQ





## How I did it

#### Choosing a Topic

I researched a common service, the first one that came to mind being **animal grooming**. I found that most websites, at least locally, were single appointments bookings with no user system. 

I saw a potential scenario where a user would not want to re-input their animal's details for every appointment, especially in the case of a high maintenance animal requiring frequent bookings.

So I conceptualized a fictional business that provides cat grooming services.  



#### Making a Plan

From there I created a **User Story** to outline the reasoning for the web app to be made:

```	
As a cat owner I want to keep my cat well groomed so that it is healthy and clean.
As a cat owner I want to make an appointment so my cat is groomed.
As someone that wants to regularly book appointments, I want to avoid repetitive data entry.
```

and a **User Case** to outline the possible flow of the application from the user's perspective. 



**User Case**

**Title:** Make Appointment 

**Actors:** Client & System

**Scenario**:

1. Client arrives at website

2. Client logs in

3. System confirms user info exists

4. System displays user dashboard page

5. Client selects make appointment 

6. System finds no cat profiles under Client account

7. System prompts Client to fill new cat profile form 

8. Client completes new cat profile form 

9. System prompts Client to select cat for appointment from available cat profiles

10. Client selects new cat

11. System prompts client to select from available time and date

12. Client selects time and date

13. System prompts Client to select from available services

14. Client browses and adds services

15. System prompts Client for final confirmation 

16. Client confirms submission

17. System displays thank you message and notifies user of confirmation email

 **Pre-condition:** Client has existing account and no cat profiles



#### Designing the Database

Using the user stories and scenarios to back up database design reasoning and make firm decisions, I drafted an ERD.

after testing queries to confirm the execution of my logic, I finalized my <a href="https://github.com/WobblyRonnoc/CatSalon/blob/main/CatSalonDBBuilder.sql">database builder file</a>

![ERD](https://github.com/WobblyRonnoc/CatSalon/blob/main/Configuration%20Files/ERD.png)

```
*Appointments are limited to one cat, as having multiple cats in one appointment complicates the database structure exponentially. Requiring the addition of several associative entities and ternary relationships.

From the user’s perspective, it is still possible to book two cats in simultaneously, but it will be expressed in the database by two separate appointments hosted by two different employees happening in the same time block. 

```



#### Using the Entity Framework

The main challenge of the assignment was using the **Entity Framework Core** to create the application scaffolding using the database I had created. 

After building the database, I used the Entity Framework Core CLI to generate the models, database context, views, and controllers for the entire .NET MVC Application.

#### The Associative Entity problem

There was one part of my design that did not translate from my database to the generated code: **Associative entities are not automatically implemented.* by Entity Framework Core

I had to tweak the generated code and manually create the models, and the context to actually interface it with the rest of the entity framework's system. 

This forced me to gain a **MUCH** deeper understanding of how the Entity Framework works under the hood, and actually allowed me to become more proficient with the surface level manipulation.





 

