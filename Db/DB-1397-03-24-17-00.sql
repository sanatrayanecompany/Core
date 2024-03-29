USE [MockTestDB]
GO
/****** Object:  StoredProcedure [UserAccess].[SP_Authentication]    Script Date: 24/03/1397 01:02:07 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [UserAccess].[SP_GetMenu]
    @UserId int
AS
BEGIN

    select  m.[Id]
		   ,m.[ParentId]
		   ,m.[TypeId]
		   ,m.[Name]
		   ,m.[Title]
		   ,m.[Parameter]
		   ,m.[Icon]
		   ,m.[Order]
		   ,m.[IsActive]
	from [UserAccess].[tbUser] u
	inner join [UserAccess].[tbUserRole] ur on u.id = ur.UserId

	inner join [UserAccess].[tbRole] r on r.Id = ur.RoleId
	inner join [UserAccess].[tbRolePermission] rp on rp.RoleId = r.Id
	inner join [UserAccess].[tbMenu] m on m.Id = rp.MenuId
	where u.Id = 1 --@UserId 

END;