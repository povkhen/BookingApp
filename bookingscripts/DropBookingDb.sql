 IF DB_ID('BookingDB') IS NOT NULL
	USE BookingDB;


 -- Deleting Tables --

DROP TABLE IF EXISTS customer.Ticket;
DROP TABLE IF EXISTS customer.Customer;
DROP TABLE IF EXISTS customer.TypeCustomer;
DROP TABLE IF EXISTS train.Seat;
DROP TABLE IF EXISTS train.Car;
DROP TABLE IF EXISTS train.TypeSeat;
DROP TABLE IF EXISTS train.TypeCar;
DROP TABLE IF EXISTS schedule.Trip;
DROP TABLE IF EXISTS schedule.RecurrenceDay;
DROP TABLE IF EXISTS schedule.[Day];
DROP TABLE IF EXISTS schedule.[Time];
DROP TABLE IF EXISTS train.Train;
DROP TABLE IF EXISTS [route].RailwayToRoute;
DROP TABLE IF EXISTS [route].Railway;
DROP TABLE IF EXISTS [route].Station;
DROP TABLE IF EXISTS [route].Adress;
DROP TABLE IF EXISTS [route].[Route];
GO

 -- Deleting Schemas --

--DROP SCHEMA IF EXISTS [route];
--DROP SCHEMA IF EXISTS [train];
--DROP SCHEMA IF EXISTS [schedule];
--DROP SCHEMA IF EXISTS [customer];
--GO
