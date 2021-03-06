--{"Id":7020,"FirstName":"Gabriel","MiddleName":"Antonio","LastName":"Vallejo"}
CREATE   PROCEDURE [dbo].[PERSONAS_INSERTAR]	
	@Personas varchar(MAX)
AS
BEGIN
	SET NOCOUNT ON;   
	DECLARE @Descripcion varchar(20)
	declare @countAfectadas int
	BEGIN TRY
		--insertar el nuevo comercio
		INSERT INTO Person.Person
           (BusinessEntityID,
			   FirstName,
			   MiddleName,
			   LastName,
			   PersonType)
		SELECT Id,FirstName,MiddleName,LastName,'EM'
		FROM OPENJSON(@Personas)
		WITH(Id INT,
			 FirstName VARCHAR(100),
			 MiddleName VARCHAR(100),
			 LastName VARCHAR(100)
			 )  
	set @countAfectadas = @@ROWCOUNT
	END TRY
	BEGIN CATCH
	    set @countAfectadas = 0
		SELECT (SELECT * FROM (SELECT  ERROR_NUMBER() AS statusCode ,ERROR_MESSAGE() AS statusDesc, @countAfectadas AS Afectados) as x FOR JSON AUTO) AS 'Respuesta'
		RETURN 1

	END CATCH

	SELECT (SELECT * FROM (SELECT  0 AS statusCode ,'Transaccion Exitosa' AS statusDesc, @countAfectadas AS Afectados) as x FOR JSON AUTO) AS 'Respuesta'
END