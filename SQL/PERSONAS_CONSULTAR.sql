

CREATE PROCEDURE [dbo].[PERSONAS_CONSULTAR]
@Id as int
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
			IF @Id =0
			BEGIN
				SELECT 
					(SELECT BusinessEntityID AS Id,
								FirstName,
								MiddleName,
								LastName
						FROM Person.Person WITH(NOLOCK)FOR JSON AUTO) AS  'Respuesta'
				END
			ELSE
			BEGIN 
			SELECT
				(SELECT BusinessEntityID AS Id,
								FirstName,
								MiddleName,
								LastName
				  FROM Person.Person WITH(NOLOCK)  WHERE  businessEntityID = @Id  
					FOR JSON AUTO) AS  'Respuesta';
			END
	END TRY
	BEGIN CATCH
	    SELECT NULL AS 'Respuesta'
	END CATCH
	
END
