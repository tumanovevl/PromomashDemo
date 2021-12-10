
declare @promomashDemoDevDb varchar(255) = N'PromomashDemoDev';

IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = @promomashDemoDevDb)
BEGIN
    SELECT 'Database ' + @promomashDemoDevDb + ' already Exist' AS Message
END
ELSE
BEGIN
    EXEC('CREATE DATABASE '+ @promomashDemoDevDb)
    SELECT 'Database ' + @promomashDemoDevDb + ' is Creates' AS Message
END