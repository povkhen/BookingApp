USE master;
GO

-- Creating a Database -- 

IF DB_ID('BookingDB') IS NULL
BEGIN	
	CREATE DATABASE BookingDB
	ON							  
	(
		NAME = 'BookingDB',            
		FILENAME = 'C:\databases\BookingDB.mdf', 
		SIZE = 125MB,                 
		MAXSIZE = 500MB,				
		FILEGROWTH = 25MB				
	)
	LOG ON						  
	( 
		NAME = 'LogBookingDB',            
		FILENAME = 'C:\databases\LogBookingDB.ldf', 
		SIZE = 5MB,                        
		MAXSIZE = 50MB,                    
		FILEGROWTH = 5MB                   
	)   
	COLLATE Cyrillic_General_CI_AS 
END
GO

USE BookingDB;
GO

-- Creating Schemes --

IF SCHEMA_ID('route') IS NULL
BEGIN
	EXEC ('CREATE SCHEMA route')
END
GO

IF SCHEMA_ID('train') IS NULL
BEGIN
	EXEC ('CREATE SCHEMA train')
END
GO

IF SCHEMA_ID('customer') IS NULL
BEGIN
   EXEC ('CREATE SCHEMA customer')
END
GO

IF SCHEMA_ID('schedule') IS NULL
BEGIN
   EXEC ('CREATE SCHEMA schedule')
END
GO


-- Creating of Tables -- 

IF OBJECT_ID('train.TypeSeat') IS NULL
BEGIN
	CREATE TABLE train.TypeSeat
	(	
		Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY NOT NULL,
		[Name] varchar(15) UNIQUE NOT NULL,		
	)
END
GO

IF OBJECT_ID('train.TypeCar') IS NULL
BEGIN
	CREATE TABLE train.TypeCar
	(	
		Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY NOT NULL,
		[Name] varchar(15) UNIQUE NOT NULL,
	)
END
GO

IF OBJECT_ID('route.Route') IS NULL
BEGIN
	CREATE TABLE [route].[Route]
	(	
		Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY NOT NULL ,
		[Name] NVARCHAR(100) NULL,
	)
	IF NOT EXISTS (SELECT name FROM sysindexes WHERE name = 'ix_routes_name')
	BEGIN
		CREATE NONCLUSTERED INDEX ix_routes_name
		ON [route].[Route]([Name]);
	END
END
GO

IF OBJECT_ID('train.Train') IS NULL
BEGIN
	CREATE TABLE train.Train
	(	
		Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY NOT NULL,
		RouteId UNIQUEIDENTIFIER NOT NULL,
		[Name] NVARCHAR(50) UNIQUE NOT NULL,
		AvarageSpeed FLOAT NOT NULL CHECK (AvarageSpeed > 0),

		CONSTRAINT FK_Train_RouteId FOREIGN KEY (RouteId)
		REFERENCES [route].[Route](Id) ON DELETE CASCADE ON UPDATE CASCADE,
	)
END
GO

IF OBJECT_ID('train.Car') IS NULL
BEGIN
	CREATE TABLE train.Car
	(	
		Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY NOT NULL ,
		TrainId UNIQUEIDENTIFIER NOT NULL,
		TypeCarId UNIQUEIDENTIFIER NULL,
		Max_Capasity SMALLINT,
		PriceCoefficient FLOAT NOT NULL DEFAULT 1.0,
		Number SMALLINT,

		CONSTRAINT UQ_Num_TrainId UNIQUE NONCLUSTERED (TrainId, Number), 

		CONSTRAINT FK_Car_TrainId FOREIGN KEY (TrainId)
		REFERENCES train.Train(Id) ON DELETE CASCADE ON UPDATE CASCADE,

		CONSTRAINT FK_Car_TypeCarId FOREIGN KEY (TypeCarId)
		REFERENCES train.TypeCar(Id) ON DELETE SET NULL ON UPDATE CASCADE,
	)
END
GO

IF OBJECT_ID('train.Seat') IS NULL
BEGIN
	CREATE TABLE train.Seat
	(	
		Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY NOT NULL ,
		CarId UNIQUEIDENTIFIER NOT NULL,
		TypeSeatId UNIQUEIDENTIFIER NULL,
		Number SMALLINT NOT NULL

		CONSTRAINT UQ_Num_CarId UNIQUE NONCLUSTERED (CarId, Number), 

		CONSTRAINT FK_Seat_TypeSeatId FOREIGN KEY (TypeSeatId)
		REFERENCES train.TypeSeat(Id) ON DELETE SET NULL ON UPDATE CASCADE,

		CONSTRAINT FK_Seat_CarId FOREIGN KEY (CarId)
		REFERENCES train.Car(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	)
END
GO

IF OBJECT_ID('route.Adress') IS NULL
BEGIN
	CREATE TABLE [route].Adress
	(	
		Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY NOT NULL ,
		Town NVARCHAR(100) NOT NULL,
		Region NVARCHAR(100) NOT NULL,
		Street NVARCHAR(100) NOT NULL,
		Apartment VARCHAR(5) NOT NULL,
		Country NVARCHAR(100) NOT NULL,
		PostalCode VARCHAR(5) NULL,
	)
END
GO

IF OBJECT_ID('route.Station') IS NULL
BEGIN
	CREATE TABLE [route].Station
	(	
		Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY NOT NULL ,
		AdressId UNIQUEIDENTIFIER UNIQUE NOT NULL,
		[Name] NVARCHAR(100) UNIQUE NOT NULL,

		CONSTRAINT FK_Station_AdressId FOREIGN KEY (AdressId)
		REFERENCES [route].Adress(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	)
END
GO

IF OBJECT_ID('route.Railway') IS NULL
BEGIN
	CREATE TABLE [route].Railway
	(	
		Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY NOT NULL ,
		Station1Id UNIQUEIDENTIFIER NOT NULL,
		Station2Id UNIQUEIDENTIFIER NOT NULL,	
		Distance DECIMAL(18,3) NOT NULL,

		CONSTRAINT FK_RailWay_Station1Id FOREIGN KEY (Station1Id)
		REFERENCES [route].Station(Id) ON DELETE NO ACTION,
		
		CONSTRAINT FK_RailWay_Station2Id FOREIGN KEY (Station2Id)
		REFERENCES [route].Station(Id) ON DELETE NO ACTION,
	)
END
GO

IF OBJECT_ID('route.RailwayToRoute') IS NULL
BEGIN
	CREATE TABLE [route].RailwayToRoute
	(	
		Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY NOT NULL ,
		RailwayId UNIQUEIDENTIFIER NOT NULL,
		RouteId UNIQUEIDENTIFIER NOT NULL,
		SequenceNumber SMALLINT NOT NULL,
		StopTime FLOAT NOT NULL,

		CONSTRAINT FK_RailwayToRoute_RailwayId FOREIGN KEY (RailwayId)
		REFERENCES [route].Railway(Id) ON DELETE CASCADE ON UPDATE CASCADE,

		CONSTRAINT FK_RailwayToRoute_RouteId FOREIGN KEY (RouteId)
		REFERENCES [route].[Route](Id) ON DELETE CASCADE ON UPDATE CASCADE,
	)
END
GO

IF OBJECT_ID('schedule.Day') IS NULL
BEGIN
	CREATE TABLE schedule.[Day]
	(	
		Number int PRIMARY KEY NOT NULL,
		[DayName] varchar(15) UNIQUE NOT NULL
	)
END
GO


IF OBJECT_ID('schedule.RecurrenceDay') IS NULL
BEGIN
	CREATE TABLE schedule.RecurrenceDay
	(	
		Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY NOT NULL ,
		TrainId UNIQUEIDENTIFIER NOT NULL,
		NumberDay INT NOT NULL,

		CONSTRAINT FK_RecurrenceDay_TrainId FOREIGN KEY (TrainId)
		REFERENCES [train].[Train](Id) ON DELETE CASCADE ON UPDATE CASCADE,

		CONSTRAINT FK_RecurrenceDay_NumberDay FOREIGN KEY (NumberDay)
		REFERENCES [schedule].[Day](Number) ON DELETE CASCADE ON UPDATE CASCADE,
	)
END
GO 

IF OBJECT_ID('schedule.Time') IS NULL
BEGIN
	CREATE TABLE schedule.[Time]
	(	
		Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY NOT NULL,
		RecurrenceDayId UNIQUEIDENTIFIER NOT NULL,
		[Time] TIME NOT NULL,

		CONSTRAINT FK_Time_RecurrenceDayId FOREIGN KEY (RecurrenceDayId)
		REFERENCES schedule.RecurrenceDay(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	)
END
GO

IF OBJECT_ID('schedule.Trip') IS NULL
BEGIN
	CREATE TABLE schedule.Trip
	(	
		Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY NOT NULL,
		TrainId UNIQUEIDENTIFIER NOT NULL,
		[Datetime] DATETIME NOT NULL,

		CONSTRAINT UQ_Datetime_TrainId UNIQUE NONCLUSTERED (TrainId, [Datetime]), 
		
		CONSTRAINT FK_Trip_TrainId FOREIGN KEY (TrainId)
		REFERENCES [train].[Train](Id) ON DELETE CASCADE ON UPDATE CASCADE
	)
END
GO 

IF OBJECT_ID('customer.TypeCustomer') IS NULL
BEGIN
	CREATE TABLE customer.TypeCustomer
	(
		Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY NOT NULL,
	    Name varchar(15) UNIQUE NOT NULL,
	)
END
GO

IF OBJECT_ID('customer.Customer') IS NULL 
BEGIN
	CREATE TABLE customer.Customer
	(
		Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY NOT NULL,
		TypeCustomerId UNIQUEIDENTIFIER NOT NULL,
		FirstName NVARCHAR(100) NOT NULL,
		LastName NVARCHAR (100) NOT NULL,
	
		CONSTRAINT FK_TypeCustomerId FOREIGN KEY (TypeCustomerId)
		REFERENCES customer.TypeCustomer(Id) ON DELETE SET NULL ON UPDATE CASCADE,

	)

END
GO

IF OBJECT_ID('customer.Ticket') IS NULL 
BEGIN
	CREATE TABLE customer.Ticket
	(
		Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY NOT NULL ,
		CustomerId UNIQUEIDENTIFIER NOT NULL,
		SeatId UNIQUEIDENTIFIER NOT NULL,
		TripId UNIQUEIDENTIFIER NOT NULL,
		ArrivalStationId UNIQUEIDENTIFIER NOT NULL,
		DepartureStationId UNIQUEIDENTIFIER NOT NULL,
		ArrivalTime DATETIME NOT NULL,
		DepartureTime DATETIME NULL,
		Price FLOAT NULL,

		CONSTRAINT FK_Ticket_CustomerId FOREIGN KEY (CustomerId)
		REFERENCES customer.Customer(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_Ticket_SeatId FOREIGN KEY (SeatId)
		REFERENCES train.Seat(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_Ticket_TripId FOREIGN KEY (TripId)
		REFERENCES schedule.Trip(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_Ticket_ArrivalStationId FOREIGN KEY (ArrivalStationId)
		REFERENCES [route].Station(Id) ON DELETE NO ACTION,

		CONSTRAINT FK_Ticket_DepartureStationId FOREIGN KEY (DepartureStationId)
		REFERENCES [route].Station(Id) ON DELETE NO ACTION
	)
END
GO

