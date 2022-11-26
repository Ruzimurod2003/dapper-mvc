CREATE PROCEDURE ToFullDateTime @dateTime nvarchar(30)
AS
    DECLARE @date DATETIME;
    SET @date = CONVERT(DATETIME, @dateTime, 121)
    SELECT DATENAME(YEAR, @date) AS 'Year',        
    DATENAME(QUARTER, @date)     AS 'Quarter',     
    DATENAME(MONTH, @date)       AS 'MonthName',       
    DATENAME(DAYOFYEAR, @date)   AS 'DayOfYear',   
    DATENAME(DAY, @date)         AS 'Day',         
    DATENAME(WEEK, @date)        AS 'Week',        
    DATENAME(WEEKDAY, @date)     AS 'DayOfTheWeek',     
    DATENAME(HOUR, @date)        AS 'Hour',        
    DATENAME(MINUTE, @date)      AS 'Minute',      
    DATENAME(SECOND, @date)      AS 'Second',      
    DATENAME(MILLISECOND, @date) AS 'MilliSecond', 
    DATENAME(MICROSECOND, @date) AS 'MicroSecond', 
    DATENAME(NANOSECOND, @date)  AS 'NanoSecond',  
    DATENAME(ISO_WEEK, @date)    AS 'ISO_WEEK'
