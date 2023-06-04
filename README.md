# Knila.BookStore
.Net Core Developer Assessment 

**DB Connection String** > 

**BookAPI** > Using n-Tier Architecture which is having Model and Controller

**Domain Layer** > Has the business model w.r.to Database table and stored procedure return type

**Infrastructure Layer** > Has the common services, methods, extensions and logs

**Service Layer** > Has the business Logic also get the data repository and send it to Controller

**Repository Layer** > It communicates with Database using Dapper Micro ORM

**Controller** > Get and Post the data as JSON 

**Database Folder** > Has the mdf file which is having the SQL Server local DB


Controller, Service Layer, Repository Layer are using **Dependency Injection (Autofac)** for the loosely coupled logic and components

**Why we are using Autofac** > Service Layer, Repository Layer, Log files logging is automatically taken care by the interceptor


**AutoMapper** > Is used to map the Domain Model and View Model and Vice Versa

**nlog** > Is used for Log files logging
