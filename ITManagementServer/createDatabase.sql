CREATE DATABASE `ITManagementDb`;

CREATE TABLE `ITManagementDb`.`Departments` (
	`Id` int NOT NULL AUTO_INCREMENT,
    `Title` nvarchar(250) NULL,
    `WorkerDuties` nvarchar(1500) NULL,
	PRIMARY KEY (`Id`)
);

CREATE TABLE `ITManagementDb`.`Workers` (
	`Id` int NOT NULL AUTO_INCREMENT,
    `Name` nvarchar(200) NULL,
    `Position` int NOT NULL,
    `Salary` decimal(10, 2) NULL,
    `BirthDate` date NULL,
    `HireDate` date NULL,
    `Active` bool NOT NULL default true,
    `EnglishLevel` nvarchar(50) NULL,
    `DepartmentId` int NULL,
    `Login` nvarchar(500) NOT NULL,
    `Password` nvarchar(500) NOT NULL,
    `Salt` nvarchar(500) NOT NULL,
	PRIMARY KEY (`Id`),
    FOREIGN KEY (`DepartmentId`) REFERENCES `ITManagementDb`.`Departments`(`Id`) ON DELETE RESTRICT
);

CREATE TABLE `ITManagementDb`.`Requests` (
	`Id` int NOT NULL AUTO_INCREMENT,
    `CreateTime` datetime NOT NULL default CURRENT_TIMESTAMP,
    `Type` int NOT NULL,
    `RequestDescription` nvarchar(500) NOT NULL,
    `Resolved` bool default false,
    `ResolveNote` nvarchar(500) NULL,
    `WorkerId` int NOT NULL,
    PRIMARY KEY (`Id`),
    FOREIGN KEY (`WorkerId`) REFERENCES `ITManagementDb`.`Workers`(`Id`) ON DELETE RESTRICT
);

CREATE TABLE `ITManagementDb`.`Projects` (
	`Id` int NOT NULL AUTO_INCREMENT,
    `Title` nvarchar(500) NULL,
    `Description` nvarchar(1000) NULL,
    `TechnologiesStack` nvarchar(500) NULL,
    `StartDate` datetime NULL,
    `Active` bool NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `ITManagementDb`.`WorkerProjects` (
	`WorkerId` int NOT NULL,
    `ProjectId` int NOT NULL,
    `Cost` decimal(10, 2) NULL,
    FOREIGN KEY (`WorkerId`) REFERENCES `ITManagementDb`.`Workers`(`Id`) 
			ON UPDATE CASCADE
            ON DELETE RESTRICT,
    FOREIGN KEY (`ProjectId`) REFERENCES `ITManagementDb`.`Projects`(`Id`) 
			ON UPDATE CASCADE
            ON DELETE RESTRICT
);

CREATE TABLE `ITManagementDb`.`TimeOffs` (
	`Id` int NOT NULL AUTO_INCREMENT,
    `DateFrom` date NOT NULL,
    `DateBefore` date NOT NULL,
    `Paid` bool NOT NULL,
    `Reason` int NULL,
    `Comment` nvarchar(500) NULL,
    `WorkerId` int NOT NULL,
    PRIMARY KEY (`Id`),
    FOREIGN KEY (`WorkerId`) REFERENCES `ITManagementDb`.`Workers`(`Id`) ON DELETE RESTRICT
);