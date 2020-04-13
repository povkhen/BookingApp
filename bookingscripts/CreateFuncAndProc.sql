CREATE PROC [customer].[NEWCUSTOMER](
     @FirstName NVARCHAR(100),
	 @LastName NVARCHAR(100),
	 @TypeCustomer VARCHAR(15),
	 @CustomerId UNIQUEIDENTIFIER OUTPUT
	 )
AS
BEGIN
	DECLARE @SearchId UNIQUEIDENTIFIER = (SELECT Id FROM [customer].Customer 
				   WHERE TypeCustomerId = (SELECT Id FROM [customer].[TypeCustomer] WHERE Name = @TypeCustomer) AND
				         FirstName = @FirstName AND LastName = @LastName)

	IF @SearchId IS NULL
	BEGIN 
		DECLARE @TypeCustomerId UNIQUEIDENTIFIER
		SET @CustomerId = NEWID();
		SET @TypeCustomerId = (SELECT Id FROM [customer].[TypeCustomer] WHERE Name = @TypeCustomer)
	
		INSERT INTO [customer].[Customer]
		(
			Id,
			[TypeCustomerId],
			[FirstName],
			[LastName]
		)
		VALUES
		(@CustomerId, @TypeCustomerId, @FirstName, @LastName)
	END
	ELSE BEGIN
		SET @CustomerId = @SearchId
	END 
END

CREATE PROCEDURE [route].[ORDERED_STATIONS](
     @Route AS nvarchar(100)
	 )
AS
BEGIN
	SELECT * FROM [route].[GET_ORDERED_STATIONS](@Route) ORDER BY SequenceNumber
END

CREATE PROC [route].[SEARCHPROC]
(     
	@StartStation nvarchar(100),
	@EndStation nvarchar(100),
	@Date Date
) AS
BEGIN
	SELECT 
		   Sub.TripId as Id,
		   TRA.Name AS Train,
		   R.Name AS [Route],
		   Sub.DepartureTime,
		   Sub.ArrivalTime,
		   [route].[GET_TIME_DIFF](Sub.DepartureTime, Sub.ArrivalTime) as Duration

	FROM [route].[SEARCHINGTRIP](@StartStation, @EndStation, @Date) AS Sub
	INNER JOIN schedule.Trip AS TRI ON TRI.Id = Sub.TripId
	INNER JOIN train.Train as TRA ON TRA.Id = TRI.TrainId
    INNER JOIN route.Route as R ON TRA.RouteId = R.Id 
END;

CREATE PROCEDURE [schedule].[GET_ALL_SEATS](
		 @From NVARCHAR(100),
		 @TO NVARCHAR(100),
		 @TripId UNIQUEIDENTIFIER,
		 @TypeCar NVARCHAR(100))
	AS
	BEGIN
		SELECT
			S.Id as SeatId,
			S.CarNumber as CarNumber,
			S.TypeCar as CarType,
			S.SeatNumber as SeatNumber,
			S.PriceCoeff as PriceCoeff,
			S.Free as Free
		FROM [schedule].[GET_ALLSEATSOFTRIP](@From, @To, @TripId) as S
		WHERE S.TypeCar = @TypeCar 
	END

CREATE PROCEDURE [schedule].[GET_GROUPING_SEATS](
	 @From NVARCHAR(100),
	 @TO NVARCHAR(100),
     @TripId UNIQUEIDENTIFIER )
AS
BEGIN
	SELECT
		S.TypeCar as Car,
        Count(S.Id) as [Count]
    FROM [schedule].[GET_ALLSEATSOFTRIP](@From, @To, @TripId) as S
	WHERE S.Free = '1'
	GROUP BY S.TypeCar
END

ALTER PROC [schedule].[NEWTRIP]
(     
	@DateFrom AS DATE,
	@DateTo AS DATE
) AS
BEGIN 
	BEGIN TRAN new_trip
		INSERT INTO schedule.Trip(Id,TrainId, Datetime) 
		SELECT 
		    NEWID() AS Id,
			TrainId,		    
			CONVERT(DATETIME, CONVERT(varchar(20), [Day],101)  + ' ' + CONVERT(varchar(8), [Time], 108)) as [Datetime]
		FROM 

			(SELECT Tr.Id as TrainId, Gen.[Day] as [Day], Ti.[Time] as [Time]
			FROM [schedule].[RecurrenceDay] as Re
			CROSS APPLY schedule.GENDAY(@DateFrom, @DateTo, Re.NumberDay ) as Gen
			INNER JOIN [train].[Train] as Tr on Tr.Id = Re.TrainId
			INNER JOIN [schedule].[Time] as Ti on Ti.RecurrenceDayId = Re.Id) as Temp

		WHERE NOT EXISTS
		    ( SELECT * FROM schedule.Trip as [Trip] 
		        WHERE [Trip].TrainId = Temp.TrainId 
		            AND [Trip].Datetime = CONVERT(DATETIME,
					CONVERT(varchar(20), Temp.[Day],101)  + ' ' + CONVERT(varchar(8), Temp.[Time], 108)))
	COMMIT TRAN new_trip
END;

CREATE FUNCTION [route].[GET_ORDERED_STATIONS](
     @Route AS nvarchar(100)
	 )
RETURNS @stations TABLE
(    
	Name nvarchar(100),
	SequenceNumber INT
)
AS
BEGIN

	DECLARE @RouteId UNIQUEIDENTIFIER
	SET @RouteId = (SELECT Id FROM [route].[Route] WHERE Name = @Route)
	
	INSERT INTO @stations
	SELECT  S.Name, RTR.SequenceNumber 
	from [route].Station as S
	INNER JOIN [route].Railway AS R ON S.Id = R.Station1Id
	INNER JOIN [route].RailwayToRoute AS RTR ON RTR.RailwayId = R.Id
	WHERE (RTR.SequenceNumber = 1 AND RTR.RouteId = @RouteId);
	
	INSERT INTO @stations
	SELECT S.Name, (RTR.SequenceNumber+1) 
	from [route].Station as S
	INNER JOIN [route].Railway AS R ON S.Id = R.Station2Id
	INNER JOIN [route].RailwayToRoute AS RTR ON RTR.RailwayId = R.Id
	WHERE (RTR.SequenceNumber >= 1 AND RTR.RouteId= @RouteId);
RETURN
END

CREATE FUNCTION [route].[GET_ROUTES_BY_STATIONS](
     @StartStationId AS UNIQUEIDENTIFIER,
     @EndStationId AS UNIQUEIDENTIFIER
	 )
RETURNS @routes TABLE
(    
	[Id] UNIQUEIDENTIFIER
)
AS
BEGIN
	;WITH RouteWithStartStation AS
	(
		SELECT DISTINCT RO.Id FROM [route].[Route] as RO
		INNER JOIN [route].RailwayToRoute AS RTR ON RTR.RouteId = RO.Id
		INNER JOIN [route].Railway as RA ON RA.ID = RTR.RailwayId 
		WHERE RA.Station1Id = @StartStationId
	),RouteWithEndStation AS
	(
		SELECT DISTINCT RO.Id FROM [route].[Route] as RO
		INNER JOIN [route].RailwayToRoute AS RTR ON RTR.RouteId = RO.Id
		INNER JOIN [route].Railway as RA ON RA.ID = RTR.RailwayId 
		WHERE RA.Station2Id = @EndStationId
	)
	INSERT INTO @routes
	SELECT St.Id FROM RouteWithStartStation as St
	INNER JOIN RouteWithEndStation as En on St.Id = En.Id
RETURN
END

CREATE  FUNCTION [route].[GET_STATIONS_BETWEEN_2STATIONS](
	 @TripId AS UNIQUEIDENTIFIER,
     @FromId AS UNIQUEIDENTIFIER,
	 @ToId AS UNIQUEIDENTIFIER
	 )
RETURNS @stations TABLE
(    
	Name nvarchar(100)
)
AS
BEGIN
	DECLARE @RouteId UNIQUEIDENTIFIER
	DECLARE @FromOrder INT, @ToOrder INT
	
	SET @RouteId = (SELECT RouteId FROM [train].[Train] as Train
					INNER JOIN schedule.Trip as Trip ON Trip.TrainId = Train.Id
					WHERE Trip.Id = @TripId)

	SET @FromOrder = (SELECT RTR.SequenceNumber from [route].RailwayToRoute as RTR
				   INNER JOIN [route].Railway as R ON RTR.RailwayId = R.Id
				   WHERE RTR.RouteId = @RouteId AND R.Station1Id = @FromId);

	SET @ToOrder = (SELECT RTR.SequenceNumber from [route].RailwayToRoute as RTR
				   INNER JOIN [route].Railway as R ON RTR.RailwayId = R.Id
				   WHERE RTR.RouteId = @RouteId AND R.Station2Id = @ToId);
	
	
	INSERT INTO @stations
	SELECT  S.Name from [route].Station as S
	INNER JOIN [route].Railway AS R ON S.Id = R.Station1Id
	INNER JOIN [route].RailwayToRoute as RTR ON RTR.RailwayId = R.Id
	WHERE RTR.RouteId = @RouteId AND RTR.SequenceNumber BETWEEN @FromOrder AND @ToOrder
	ORDER BY RTR.SequenceNumber
RETURN
END

CREATE   FUNCTION [route].[SEARCHINGTRIP]
(     
	@StartStation nvarchar(100),
	@EndStation nvarchar(100),
	@Date Date
) 
RETURNS @trips TABLE
(    
	[TripId] UNIQUEIDENTIFIER,
	DepartureTime DATETIME,
	ArrivalTime DATETIME
)
AS
BEGIN
	DECLARE @StartStationId AS UNIQUEIDENTIFIER, @EndStationId AS UNIQUEIDENTIFIER
	SELECT @StartStationId = (SELECT Id from route.Station WHERE Name = @StartStation) 
	SELECT @EndStationId = (SELECT Id from route.Station WHERE Name = @EndStation) 

	;WITH Temp
	AS
	(
		SELECT T.Id FROM [schedule].[GET_TRIPS](@StartStationId, @EndStationId, @Date) as G
		INNER JOIN schedule.Trip as T ON G.Id = T.Id
	)
	INSERT INTO @trips
	SELECT 
		   T.Id as Id,
		   (schedule.GET_DEPARTURE_TIME(T.TrainId, T.[Datetime], @StartStationId)) as DepartureTime,
		   (schedule.GET_ARRIVAL_TIME(T.TrainId, T.[Datetime], @EndStationId)) as ArrivalTime
		   FROM schedule.Trip as T
		   INNER JOIN Temp ON T.Id = Temp.Id
		   WHERE T.Datetime >= GETDATE()
RETURN	
END;

CREATE FUNCTION [schedule].[GENDAY](
     @DateFrom AS DATE,
	 @DateTo AS DATE,
	 @Day AS INT
	 )
RETURNS @days TABLE
(    
	[Day] DATE   
)
AS
BEGIN
	WITH CTE(DateOfDay)
	AS
	(
	      SELECT @DateFrom
	      UNION ALL
	      SELECT DATEADD(day, 1, DateOfDay) FROM CTE
	      WHERE DateOfDay < @DateTo
	)
	INSERT INTO @days
	SELECT DateOfDay FROM CTE where datepart("dw", DateOfDay) = @Day;	
RETURN
END

CREATE FUNCTION [schedule].[GET_ALLSEATSOFTRIP](
	 @From AS NVARCHAR(100),
	 @To AS NVARCHAR(100),
     @TripId AS UNIQUEIDENTIFIER
	 )
RETURNS @seats TABLE
(   
	[Id] UNIQUEIDENTIFIER,
	[TypeCar] VARCHAR(15),
	CarNumber SMALLINT,
	SeatNumber SMALLINT,
	PriceCoeff FLOAT,
	Free BIT
)
AS
BEGIN
	DECLARE @FromId UNIQUEIDENTIFIER = (SELECT Id FROM route.Station WHERE Name = @From);
	DECLARE @ToId UNIQUEIDENTIFIER = (SELECT Id FROM route.Station WHERE Name = @To);
	
	DECLARE @AllSeat TABLE
	(
		Id UNIQUEIDENTIFIER,
		[Name] NVARCHAR(100),
		TripId UNIQUEIDENTIFIER,
		CarNumber SMALLINT,
		SeatNumber SMALLINT,
		PriceCoeff FLOAT
	)

	DECLARE @SealdSeat TABLE
	(
		TripId UNIQUEIDENTIFIER,
		Id UNIQUEIDENTIFIER,
		[Name] NVARCHAR(100),
		[From] NVARCHAR(100),
		[To] NVARCHAR(100),
		CarNumber SMALLINT,
		SeatNumber SMALLINT,
		PriceCoeff FLOAT
	)

	INSERT INTO @AllSeat
	SELECT SE.Id as Id, TC.Name as [Name], Trip.Id as TripId,
	CAR.Number as CarNumber, SE.Number as SeatNumber, CAR.PriceCoefficient as PriceCoeff
	FROM train.Seat AS SE
	INNER JOIN train.Car AS CAR  
	ON SE.CarId = CAR.Id
	INNER JOIN train.TypeCar AS TC
	ON TC.Id = CAR.TypeCarId
	INNER JOIN train.Train AS TR
	ON TR.Id = CAR.TrainId
	INNER JOIN schedule.Trip AS Trip
	ON Trip.TrainId = Tr.Id
	WHERE Trip.Id = @TripId
    
	INSERT INTO @SealdSeat
	SELECT S.TripId, S.Id, S.[Name], TI.ArrivalStationId, TI.DepartureStationId,
	S.CarNumber as CarNumber, S.SeatNumber as SeatNumber, S.PriceCoeff as PriceCoeff
	FROM @AllSeat as S
	INNER JOIN customer.Ticket AS TI
	ON TI.TripId = S.TripId
	
	;WITH INTERSECTSEAT([Name])
	AS
	(
		(SELECT [Name] FROM [route].[GET_STATIONS_BETWEEN_2STATIONS] (@TripId, @FromId, @ToId))
		INTERSECT 
		(SELECT temp.[Name] FROM @SealdSeat as S
		 CROSS APPLY [route].[GET_STATIONS_BETWEEN_2STATIONS] (@TripId, S.[From], S.[To]) as temp)
	)
	INSERT INTO @seats
	SELECT SEALD.Id, SEALD.[Name], SEALD.CarNumber,
		   SEALD.SeatNumber, SEALD.PriceCoeff,
		   'True'
	FROM @SealdSeat AS SEALD
	WHERE ((SELECT * FROM INTERSECTSEAT) IS NULL)	
	
	;WITH FREESEAT(Id, [Name], CarNumber, SeatNumber, PriceCoeff)
	AS
	(
		SELECT S.Id, S.[Name], S.CarNumber, S.SeatNumber, S.PriceCoeff 
		FROM @AllSeat as S
		LEFT JOIN @SealdSeat as seald
		ON S.Id = seald.Id
		WHERE seald.Id IS NULL
	)
	INSERT INTO @seats
	SELECT FREE.Id, FREE.[Name], FREE.CarNumber, FREE.SeatNumber, FREE.PriceCoeff, 'True' FROM FREESEAT AS FREE

	;WITH OTHERSEAT(Id, [Name], CarNumber, SeatNumber, PriceCoeff)
	AS
	(
		SELECT S.Id, S.[Name], S.CarNumber, S.SeatNumber, S.PriceCoeff 
		FROM @AllSeat as S
		LEFT JOIN @seats as free
		ON S.Id = free.Id
		WHERE free.Id IS NULL
	)
	INSERT INTO @seats
	SELECT OTHER.Id, OTHER.[Name], OTHER.CarNumber, OTHER.SeatNumber, OTHER.PriceCoeff, 'False' FROM OTHERSEAT AS OTHER

RETURN
END

CREATE FUNCTION [schedule].[GET_TRIPS](
     @StartStationId AS UNIQUEIDENTIFIER,
     @EndStationId AS UNIQUEIDENTIFIER,
	 @Date AS DATE
	 )
RETURNS @trips TABLE
(    
	[Id] UNIQUEIDENTIFIER
)
AS
BEGIN
	INSERT INTO @trips
	SELECT TRIP.Id FROM schedule.Trip AS TRIP
	INNER JOIN train.Train AS TRAIN 
	ON TRAIN.Id = TRIP.TrainId
	INNER JOIN [route].[GET_ROUTES_BY_STATIONS](@StartStationId, @EndStationId) as R
	ON TRAIN.RouteId = R.Id
	WHERE CONVERT(DATE, TRIP.Datetime) = @Date
RETURN
END

CREATE FUNCTION [route].[GET_DELAY_BETWEEN_STATIONS](
     @StartStationId AS UNIQUEIDENTIFIER,
     @EndStationId UNIQUEIDENTIFIER,
	 @RouteId UNIQUEIDENTIFIER
	 )
RETURNS FLOAT
AS
BEGIN
	DECLARE @StartNumRail INT, @EndNumRail INT
	SET @StartNumRail = (SELECT RTR.SequenceNumber
						 FROM route.Railway as R
						 INNER JOIN route.RailwayToRoute as RTR
						 ON R.Id = RTR.RailwayId 
						 WHERE (RTR.RouteId = @RouteId AND R.Station1Id = @StartStationId))
	
	SET @EndNumRail = (SELECT RTR.SequenceNumber
						 FROM route.Railway as R
						 INNER JOIN route.RailwayToRoute as RTR
						 ON R.Id = RTR.RailwayId 
						 WHERE (RTR.RouteId = @RouteId AND R.Station2Id = @EndStationId))
	
     RETURN (SELECT CAST(SUM(RTR.StopTime) as FLOAT) FROM [route].Railway as R
			INNER JOIN route.RailwayToRoute as RTR 
			ON R.Id = RTR.RailwayId
			WHERE RTR.SequenceNumber BETWEEN @StartNumRail and (@EndNumRail-1) 
			AND RTR.RouteId = @RouteId )
END

CREATE FUNCTION [route].[GET_DISTANCE_BETWEEN_STATIONS](
     @StartStationId AS UNIQUEIDENTIFIER,
     @EndStationId AS UNIQUEIDENTIFIER,
	 @RouteId AS UNIQUEIDENTIFIER
	 )
RETURNS FLOAT
AS
BEGIN
	DECLARE @StartNumRail INT, @EndNumRail INT
	
	SET @StartNumRail = (SELECT RTR.SequenceNumber
						 FROM route.Railway as R
						 INNER JOIN route.RailwayToRoute as RTR
						 ON R.Id = RTR.RailwayId 
						 WHERE (RTR.RouteId = @RouteId AND R.Station1Id = @StartStationId))
	
	SET @EndNumRail = (SELECT RTR.SequenceNumber
						 FROM route.Railway as R
						 INNER JOIN route.RailwayToRoute as RTR
						 ON R.Id = RTR.RailwayId 
						 WHERE (RTR.RouteId = @RouteId AND R.Station2Id = @EndStationId))

	RETURN (SELECT CAST(SUM(R.Distance) as FLOAT) FROM [route].Railway as R
			INNER JOIN route.RailwayToRoute as RTR 
			ON R.Id = RTR.RailwayId
			WHERE RTR.SequenceNumber BETWEEN @StartNumRail and @EndNumRail
			AND RTR.RouteId = @RouteId )
END

CREATE FUNCTION [route].[GET_NEWDATETIME](
     @OldDateTime AS DATETIME,
     @Time AS FLOAT
	 )
RETURNS DATETIME
AS
BEGIN
	DECLARE @Hours INT, @Minutes INT, @WithMinutes DATETIME
	SELECT @Hours = CAST(@Time * 60 AS int)/60
	SELECT @Minutes = CAST(RIGHT(100 + CAST(@Time * 60 AS int) % 60, 2) as INT)
	SELECT @WithMinutes = dateadd(MINUTE, @Minutes, @OldDateTime)
	RETURN (SELECT dateadd(HOUR, @Hours, @WithMinutes)) 
END

CREATE FUNCTION [route].[GET_TIME_DIFF](
     @First DATETIME,
	 @Second DATETIME
	 )
RETURNS NVARCHAR(10)
AS
BEGIN
	DECLARE @Seconds INT
	SET @Seconds = DATEDIFF(SECOND, @First, @Second)

	RETURN	CONVERT(VARCHAR(5), @Seconds/60/60)
	  + ':' + RIGHT('0' + CONVERT(VARCHAR(2), @Seconds/60%60), 2)
	  + ':' + RIGHT('0' + CONVERT(VARCHAR(2), @Seconds % 60), 2)
END

CREATE FUNCTION [schedule].[GET_ARRIVAL_TIME](
     @TrainId UNIQUEIDENTIFIER,
	 @StartDateTime DATETIME,
	 @StationId UNIQUEIDENTIFIER
	 )
RETURNS DATETIME AS
BEGIN
	DECLARE @Speed FLOAT, @StartStationId UNIQUEIDENTIFIER, @RouteId UNIQUEIDENTIFIER
	DECLARE @Distance FLOAT, @Delay FLOAT, @Time FLOAT

	SET @Speed = (SELECT AvarageSpeed FROM train.Train WHERE Id = @TrainId)
	SET @RouteId = (SELECT RouteId from train.Train WHERE Id = @TrainId)
	SET @StartStationId = (SELECT R.Station1Id FROM route.Railway as R
						   INNER JOIN route.RailwayToRoute as RTR 
						   ON R.Id = RTR.RailwayId 							 
						   WHERE (RTR.SequenceNumber = 1 AND 
						   RTR.RouteId = @RouteId))
	
	SET @Distance = ISNULL([route].[GET_DISTANCE_BETWEEN_STATIONS](@StartStationId, @StationId, @RouteId),0.0)
	SET @Delay = ISNULL([route].[GET_DELAY_BETWEEN_STATIONS](@StartStationId, @StationId, @RouteId),0.0)
	SET @Time = ((@Distance / @Speed) + (@Delay / 60))
	RETURN ([route].[GET_NEWDATETIME](@StartDateTime, @Time))
END

CREATE FUNCTION [schedule].[GET_DEPARTURE_TIME](
     @TrainId UNIQUEIDENTIFIER,
	 @StartDateTime DATETIME,
	 @StationId UNIQUEIDENTIFIER
	 )
RETURNS DATETIME AS
BEGIN
	DECLARE @RouteId AS UNIQUEIDENTIFIER
	DECLARE @Delay AS FLOAT
	SET @Delay = ISNULL((SELECT RTR.[StopTime] 
				 FROM [route].[RailwayToRoute] as RTR
				 INNER JOIN route.Railway as R
				 ON R.Id = RTR.RailwayId
				 WHERE RTR.RouteId = @RouteId AND R.Station2Id = @StationId),0)


	SET @RouteId = (SELECT RouteId from train.Train WHERE Id = @TrainId)
	RETURN ([route].[GET_NEWDATETIME]([schedule].[GET_ARRIVAL_TIME](@TrainId, @StartDateTime, @StationId), @Delay/60))
END

CREATE FUNCTION [train].[GETSEATID] (
    @TripId AS UNIQUEIDENTIFIER,
	@NumberOfCar INT,
	@NumberOfSeat INT
)
RETURNS UNIQUEIDENTIFIER
AS
BEGIN
RETURN (
	SELECT 
        S.Id
    FROM
        train.Seat as S
	INNER JOIN train.Car AS C ON S.CarId = C.Id
	INNER JOIN train.Train as Tra ON C.TrainId = Tra.Id
	INNER JOIN schedule.Trip as Tri ON Tri.TrainId = Tra.Id
    WHERE
        Tri.Id = @TripId AND C.Number = @NumberOfCar AND S.Number = @NumberOfSeat )
END
