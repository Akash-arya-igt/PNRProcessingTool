
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/27/2017 17:06:27
-- Generated from EDMX file: D:\Projects\IGT.PNRProcessingTool\PNRProcessingTool\SourceCode\IGT.PNRProcessing.AdminTool\IGT.PNRProcessing.DataAccessLayer\PNRProcessingDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [PNRProcessingDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Queue_Robot]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Queue] DROP CONSTRAINT [FK_Queue_Robot];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Queue]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Queue];
GO
IF OBJECT_ID(N'[dbo].[Robot]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Robot];
GO
IF OBJECT_ID(N'[dbo].[tblPCCConfiguration]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblPCCConfiguration];
GO
IF OBJECT_ID(N'[dbo].[tblPNRProcessingTrace]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblPNRProcessingTrace];
GO
IF OBJECT_ID(N'[dbo].[tblTicketingFlowConfiguration]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblTicketingFlowConfiguration];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Queues'
/*
CREATE TABLE [dbo].[Queues] (
    [QueueId] int IDENTITY(1,1) NOT NULL,
    [QueueRefName] nvarchar(100)  NULL,
    [GDS] nvarchar(20)  NULL,
    [PCC] nvarchar(20)  NULL,
    [QueueNoCategory] nvarchar(100)  NULL,
    [ScanFreq] int  NULL,
    [QueueFunction] nvarchar(100)  NULL,
    [RobotId] int  NULL,
    [HAP] varchar(20)  NOT NULL,
    [UserID] varchar(20)  NOT NULL,
    [Password] varchar(20)  NOT NULL
);
GO

-- Creating table 'Robots'
CREATE TABLE [dbo].[Robots] (
    [RobotId] int IDENTITY(1,1) NOT NULL,
    [RobotName] nvarchar(100)  NULL,
    [Remarks] nvarchar(100)  NULL,
    [CommRemarks] nvarchar(100)  NULL,
    [AvailAVL] bit  NULL,
    [FOP] nvarchar(100)  NULL,
    [Fare] nvarchar(100)  NULL,
    [MCTError] bit  NULL,
    [SuccessQueueNo] int  NULL,
    [SuccessMsg] nvarchar(100)  NULL,
    [FailureQueueNo] int  NULL,
    [FailureMsg] nvarchar(100)  NULL
);
GO
*/
-- Creating table 'tblPNRProcessingTraces'
CREATE TABLE [dbo].[tblPNRProcessingTraces] (
    [PNRProcessingTraceId] varchar(50)  NOT NULL,
    [Recloc] varchar(10)  NOT NULL,
    [PCC] varchar(5)  NOT NULL,
    [Status] varchar(10)  NULL,
    [IsProcessComplete] bit  NULL,
    [NoOfFares] int  NOT NULL,
    [NoOfTicketedFares] int  NOT NULL,
    [Error] varchar(100)  NULL,
    [PNRCaptureDate] datetime  NULL,
    [LastModified] datetime  NULL
);
GO

-- Creating table 'tblPCCConfigurations'
CREATE TABLE [dbo].[tblPCCConfigurations] (
    [PCCID] int IDENTITY(1,1) NOT NULL,
    [PCC] varchar(10)  NOT NULL,
    [HAP] varchar(50)  NOT NULL,
    [UserId] varchar(50)  NOT NULL,
    [Password] varchar(50)  NOT NULL,
    [GDS] varchar(20)  NOT NULL,
    [URL] varchar(100)  NOT NULL
);
GO

-- Creating table 'tblTicketingFlowConfigurations'
CREATE TABLE [dbo].[tblTicketingFlowConfigurations] (
    [TicketingflowId] int IDENTITY(1,1) NOT NULL,
    [TicketingflowName] varchar(50)  NOT NULL,
    [PCCID] int  NOT NULL,
    [PreFormatedRemark] varchar(100)  NULL,
    [CommissionRemarkFormat] varchar(100)  NULL,
    [CommissionPct] int  NULL,
    [AllowedFOP] varchar(50)  NULL,
    [AllowedFareType] varchar(50)  NULL,
    [IsAVLCheck] bit  NOT NULL,
    [IsMCTCheck] bit  NOT NULL,
    [SuccessQNo] int  NOT NULL,
    [SuccessMsg] varchar(100)  NULL,
    [FailQNo] int  NOT NULL,
    [FailMsg] varchar(100)  NULL,
    [TargetQNo] int  NOT NULL,
    [ScanFrequency] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------
/*
-- Creating primary key on [QueueId] in table 'Queues'
ALTER TABLE [dbo].[Queues]
ADD CONSTRAINT [PK_Queues]
    PRIMARY KEY CLUSTERED ([QueueId] ASC);
GO

-- Creating primary key on [RobotId] in table 'Robots'
ALTER TABLE [dbo].[Robots]
ADD CONSTRAINT [PK_Robots]
    PRIMARY KEY CLUSTERED ([RobotId] ASC);
GO
*/

-- Creating primary key on [PNRProcessingTraceId] in table 'tblPNRProcessingTraces'
ALTER TABLE [dbo].[tblPNRProcessingTraces]
ADD CONSTRAINT [PK_tblPNRProcessingTraces]
    PRIMARY KEY CLUSTERED ([PNRProcessingTraceId] ASC);
GO

-- Creating primary key on [PCCID] in table 'tblPCCConfigurations'
ALTER TABLE [dbo].[tblPCCConfigurations]
ADD CONSTRAINT [PK_tblPCCConfigurations]
    PRIMARY KEY CLUSTERED ([PCCID] ASC);
GO

-- Creating primary key on [TicketingflowId] in table 'tblTicketingFlowConfigurations'
ALTER TABLE [dbo].[tblTicketingFlowConfigurations]
ADD CONSTRAINT [PK_tblTicketingFlowConfigurations]
    PRIMARY KEY CLUSTERED ([TicketingflowId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------
/*
-- Creating foreign key on [RobotId] in table 'Queues'
ALTER TABLE [dbo].[Queues]
ADD CONSTRAINT [FK_Queue_Robot]
    FOREIGN KEY ([RobotId])
    REFERENCES [dbo].[Robots]
        ([RobotId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Queue_Robot'
CREATE INDEX [IX_FK_Queue_Robot]
ON [dbo].[Queues]
    ([RobotId]);
GO
*/
-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------