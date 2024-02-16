USE [CatSalon];
DROP TABLE IF EXISTS [dbo].[Appointment_Service];
DROP TABLE IF EXISTS [dbo].[Appointment];
DROP TABLE IF EXISTS [dbo].[Cat];
DROP TABLE IF EXISTS [dbo].[Service];
DROP TABLE IF EXISTS [dbo].[Owner];
DROP TABLE IF EXISTS [dbo].[Employee];


CREATE TABLE [dbo].[Owner] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (50)  NOT NULL,
    [Phone]    NVARCHAR (12)  NOT NULL,
    [Email]    NVARCHAR (100) NOT NULL,
    [Password] NVARCHAR (75)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[Cat] (
    [Id]               INT  IDENTITY (1, 1) NOT NULL,
    [Owner_Id]         INT            NOT NULL,
    [Name]             NVARCHAR (50)  NOT NULL,
    [Breed]            NVARCHAR (50)  NOT NULL,
    [Birth_Date]       DATE           NOT NULL,
    [Health_Condition] NVARCHAR (250) NULL,
    [Fixed]            BIT            DEFAULT ((0)) NOT NULL,
    [Declawed]         BIT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ToOwner] FOREIGN KEY ([Owner_Id]) REFERENCES [dbo].[Owner] ([Id]) ON DELETE CASCADE
);


CREATE TABLE [dbo].[Service] (
    [Id]               INT      IDENTITY (1, 1)  NOT NULL,
    [Title]       NVARCHAR (50)  NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [Duration]    DECIMAL (18)   NULL,
    [Price]       DECIMAL (18)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[Employee] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[Appointment] (
    [Id]             INT      IDENTITY (1, 1) NOT NULL,
    [Cat_Id]         INT      NOT NULL,
    [Employee_Id]    INT      NOT NULL,
    [Scheduled_Date] DATETIME NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[Appointment_Service] (
    [Id]            INT IDENTITY (1, 1) NOT NULL,
    [AppointmentId] INT NOT NULL,
    [ServiceId]     INT NOT NULL,
    PRIMARY KEY CLUSTERED ([AppointmentId] ASC, [ServiceId] ASC),
    CONSTRAINT [FK_AppointmentService_AppointmentId] FOREIGN KEY ([AppointmentId]) REFERENCES [dbo].[Appointment] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AppointmentService_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [dbo].[Service] ([Id]) ON DELETE CASCADE
);

-- Populate Employee Table
DELETE FROM [dbo].[Employee];
INSERT INTO [dbo].[Employee] (Name) VALUES
('Alex'),
('James'),
('Vanya'),
('Henry');

-- Populate Service Table
DELETE FROM [dbo].[Service];
INSERT INTO [dbo].[Service] (Title, Description, Duration, Price) VALUES
('Paw Treatment', 'Trim claw length down and clean paws', 0.5, 20.99),
('Hair Trim', 'hair trim', 0.5, 15.99),
('Bath', 'Bath scrubbing with special shampoo followed by premium brushification', 0.75, 49.99),
('Lions Mane Styling', 'Remove hair length around the torso and tail, save for the tip. Leaving a full mane and tail plume!', 0.75, 33.99),
('Hygiene Treatment', 'Ear and teeth cleaning', 0.5, 45.99);
