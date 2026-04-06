USE [MedAuthDb]
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'0', N'Гость', N'ГОСТЬ', NULL)
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'1', N'Администратор', N'АДМИНИСТРАТОР', NULL)
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'2', N'Пациент', N'ПАЦИЕНТ', NULL)
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'3', N'Врач', N'ВРАЧ', NULL)
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'4', N'Регистратор', N'РЕГИСТРАТОР', NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'61c72f51-7bf6-45df-a7a9-5aef85fae492', N'admin@mail.com', N'admin@mail.com', N'admin@mail.com', N'admin@mail.com', 1, N'AQAAAAIAAYagAAAAENzM+3fDLcjnYoWg3auThUK2oQarHXTjk6K/aXXPU2VBuqFAlu3S5pjWvATJup2V2g==', N'930504e7-8145-4446-9da2-a7ba80f8cdbd', N'4580af35-c273-40a3-8aa3-ff58fb73cb04', NULL, 0, 0, NULL, 0, 0)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'61c72f51-7bf6-45df-a7a9-5aef85fae492', N'1')
GO