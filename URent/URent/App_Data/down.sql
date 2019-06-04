-- This script is used to empty out the database so we can start fresh
-- FOR USE ON DEVELOPMENT DATABASE ONLY

-- Drops our tables
DROP TABLE [dbo].[SUPUserReviews]
DROP TABLE [dbo].[SUPItemReviews]
DROP TABLE [dbo].[SUPTransactions]
DROP TABLE [dbo].[SUPRequests]
DROP TABLE [dbo].[SUPImages]
DROP TABLE [dbo].[SUPItems]
DROP TABLE [dbo].[SUPUsers]

-- Drops ASP.NET tables
DROP TABLE [dbo].[AspNetUserRoles]
DROP TABLE [dbo].[AspNetUserClaims]
DROP TABLE [dbo].[AspNetUserLogins]
DROP TABLE [dbo].[AspNetRoles]
DROP TABLE [dbo].[AspNetUsers]