CREATE PROCEDURE [dbo].[spUser_Insert]
	@Id nvarchar(128),
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Email nvarchar(256)

AS
	insert into dbo.[User](Id, FirstName, LastName, Email)
	values (@Id, @FirstName, @LastName, @Email);

RETURN 0
