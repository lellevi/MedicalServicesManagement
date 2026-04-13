USE [MedicalServicesManagementDB]
GO
-- admin user
INSERT [dbo].[[User]]] ([Id], [AuthUserId], [MedSpecialityId], [MedInfo], [Surname], [Name], [MiddleName], [BirthDate], [Telephone]) VALUES 
(N'314d5a1e-90d8-4380-a1a4-9f37976c29b0', N'61c72f51-7bf6-45df-a7a9-5aef85fae492', NULL, NULL, N'Главный', N'Администратор', NULL, CAST(N'2000-11-11T00:00:00.0000000' AS DateTime2), '')
GO

-- medSpecialities
INSERT [dbo].[MedSpeciality] ([Id], [Name]) VALUES (N'4b125b35-b793-4e6a-bc3f-93e82d04de28', N'Офтальмолог')
GO
INSERT [dbo].[MedSpeciality] ([Id], [Name]) VALUES (N'6069a53c-179f-448d-b5de-c886f64c2f92', N'Педиатр')
GO
INSERT [dbo].[MedSpeciality] ([Id], [Name]) VALUES (N'a35b3c15-3eaf-4506-b76c-89778b4c7de4', N'Терапевт')
GO

-- users
INSERT [dbo].[[User]]] ([Id], [AuthUserId], [MedSpecialityId], [MedInfo], [Surname], [Name], [MiddleName], [BirthDate], [Telephone]) VALUES (N'4721c6fe-50ce-4cfd-a189-76217c04de7b', N'04858889-5740-4271-a291-57133cefa36b', NULL, NULL, N'myuser@mail.com', N'myuser@mail.com', N'myuser@mail.com', CAST(N'2000-11-11T00:00:00.0000000' AS DateTime2), N'12345678900')
GO
INSERT [dbo].[[User]]] ([Id], [AuthUserId], [MedSpecialityId], [MedInfo], [Surname], [Name], [MiddleName], [BirthDate], [Telephone]) VALUES (N'4b5e293c-b295-4527-b728-26c0720075e9', N'31a4a7a9-d625-4e65-bc4a-d69396f7ca96', N'a35b3c15-3eaf-4506-b76c-89778b4c7de4', NULL, N'Петров', N'Пётр', N'Петрович', CAST(N'2000-12-12T00:00:00.0000000' AS DateTime2), N'+12345678900')
GO
INSERT [dbo].[[User]]] ([Id], [AuthUserId], [MedSpecialityId], [MedInfo], [Surname], [Name], [MiddleName], [BirthDate], [Telephone]) VALUES (N'a8bff4ef-3708-4980-8ceb-a975a5da1f00', N'90888c97-3fe6-49fc-8656-4b6877f39af4', N'4b125b35-b793-4e6a-bc3f-93e82d04de28', N'Специалист высшей квалификации', N'Иванов', N'Иван', N'Иванович', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'+123456789000')
GO

-- services
INSERT [dbo].[Service] ([Id], [Name], [ForAdults], [MedSpecialityId], [Cost], [Comment]) VALUES (N'238acd6a-9045-4b34-ac70-07bb0a681d3c', N'Консультация', 1, N'4b125b35-b793-4e6a-bc3f-93e82d04de28', CAST(12.00 AS Decimal(18, 2)), NULL)
GO
INSERT [dbo].[Service] ([Id], [Name], [ForAdults], [MedSpecialityId], [Cost], [Comment]) VALUES (N'ea40555a-4562-4f78-824a-c70d8380d71f', N'Консультация', 0, N'4b125b35-b793-4e6a-bc3f-93e82d04de28', CAST(10.00 AS Decimal(18, 2)), NULL)
GO
