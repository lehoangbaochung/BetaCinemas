IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE TABLE [Member] (
        [Id] nvarchar(450) NOT NULL,
        [FullName] nvarchar(max) NULL,
        [Pass] nvarchar(max) NULL,
        [Birthday] datetime2 NOT NULL,
        [Gender] bit NOT NULL,
        [RegisterDate] datetime2 NOT NULL,
        [HomeAddress] nvarchar(max) NULL,
        [CardNumber] nvarchar(max) NULL,
        [UserName] nvarchar(max) NULL,
        [NormalizedUserName] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        [NormalizedEmail] nvarchar(max) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_Member] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE TABLE [Movie] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(50) NOT NULL,
        [About] ntext NULL,
        [Duration] int NULL,
        [Director] nvarchar(50) NULL,
        [Actor] ntext NULL,
        [Country] nvarchar(50) NULL,
        [Lang] nvarchar(20) NULL,
        [Genre] nvarchar(50) NULL,
        [ReleaseDate] date NULL,
        [PosterUrl] text NULL,
        [TrailerUrl] text NULL,
        [IsShowing] bit NULL,
        CONSTRAINT [PK_Movie] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE TABLE [Post] (
        [Id] int NOT NULL IDENTITY,
        [PostTime] datetime NOT NULL,
        [Content] ntext NOT NULL,
        [AttachedUrl] text NULL,
        [ImageUrl] text NULL,
        CONSTRAINT [PK_Post] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE TABLE [Roles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE TABLE [Room] (
        [Id] int NOT NULL IDENTITY,
        [RowTotal] int NOT NULL,
        [ColumnTotal] int NOT NULL,
        [About] ntext NULL,
        CONSTRAINT [PK_Room] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE TABLE [TicketPrice] (
        [Id] int NOT NULL IDENTITY,
        [Weekdays] int NOT NULL,
        [StartTime] time NULL,
        [EndTime] time NULL,
        [Price] int NOT NULL,
        [IsPriority] bit NOT NULL,
        [Is2D] bit NOT NULL,
        CONSTRAINT [PK_TicketPrice] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE TABLE [Users] (
        [Id] nvarchar(450) NOT NULL,
        [FullName] nvarchar(max) NULL,
        [Pass] nvarchar(max) NULL,
        [Birthday] datetime2 NOT NULL,
        [Gender] bit NOT NULL,
        [RegisterDate] datetime2 NOT NULL,
        [HomeAddress] nvarchar(max) NULL,
        [CardNumber] nvarchar(max) NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE TABLE [RoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_RoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_RoleClaims_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE TABLE [Seat] (
        [Id] int NOT NULL,
        [RoomId] int NOT NULL,
        [RowIndex] char(2) NOT NULL,
        [ColumnIndex] int NOT NULL,
        [IsEmpty] bit NOT NULL,
        CONSTRAINT [PK_Seat] PRIMARY KEY ([Id]),
        CONSTRAINT [FK__Seat__Id__7720AD13] FOREIGN KEY ([Id]) REFERENCES [Room] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE TABLE [Showtime] (
        [Id] int NOT NULL,
        [RoomId] int NOT NULL,
        [ShowTime] datetime NOT NULL,
        [Is2D] bit NOT NULL,
        CONSTRAINT [PK_Showtime] PRIMARY KEY ([Id]),
        CONSTRAINT [FK__Showtime__Id__7BE56230] FOREIGN KEY ([Id]) REFERENCES [Room] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE TABLE [Bill] (
        [Id] int NOT NULL IDENTITY,
        [MemberId] nvarchar(450) NOT NULL,
        [SoldTime] datetime NULL,
        [Total] int NOT NULL,
        [IsSold] bit NOT NULL,
        [About] ntext NULL,
        [UserId] nvarchar(450) NULL,
        CONSTRAINT [PK_Bill] PRIMARY KEY ([Id]),
        CONSTRAINT [FK__Bill__MemberId__084B3915] FOREIGN KEY ([MemberId]) REFERENCES [Member] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Bill_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE TABLE [Contact] (
        [Id] int NOT NULL IDENTITY,
        [MemberId] nvarchar(450) NOT NULL,
        [SentTime] datetime NOT NULL,
        [Content] ntext NOT NULL,
        [IsReplied] bit NOT NULL,
        [UserId] nvarchar(450) NULL,
        CONSTRAINT [PK_Contact] PRIMARY KEY ([Id]),
        CONSTRAINT [FK__Contact__MemberI__725BF7F6] FOREIGN KEY ([MemberId]) REFERENCES [Member] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Contact_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE TABLE [UserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_UserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserClaims_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE TABLE [UserLogins] (
        [LoginProvider] nvarchar(128) NOT NULL,
        [ProviderKey] nvarchar(128) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_UserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_UserLogins_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE TABLE [UserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_UserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE TABLE [UserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(128) NOT NULL,
        [Name] nvarchar(128) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_UserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_UserTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE TABLE [Ticket] (
        [Id] int NOT NULL IDENTITY,
        [MemberId] nvarchar(450) NOT NULL,
        [MovieId] int NOT NULL,
        [ShowtimeId] int NOT NULL,
        [TicketPriceId] int NOT NULL,
        [SoldTime] datetime NOT NULL,
        [UserId] nvarchar(450) NULL,
        CONSTRAINT [PK_Ticket] PRIMARY KEY ([Id]),
        CONSTRAINT [FK__Ticket__MemberId__00AA174D] FOREIGN KEY ([MemberId]) REFERENCES [Member] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK__Ticket__MovieId__019E3B86] FOREIGN KEY ([MovieId]) REFERENCES [Movie] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK__Ticket__Showtime__02925FBF] FOREIGN KEY ([ShowtimeId]) REFERENCES [Showtime] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK__Ticket__TicketPr__038683F8] FOREIGN KEY ([TicketPriceId]) REFERENCES [TicketPrice] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Ticket_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE TABLE [BillDetail] (
        [Id] int NOT NULL IDENTITY,
        [BillId] int NOT NULL,
        [TicketId] int NOT NULL,
        CONSTRAINT [PK_BillDetail] PRIMARY KEY ([Id]),
        CONSTRAINT [FK__BillDetai__BillI__0D0FEE32] FOREIGN KEY ([BillId]) REFERENCES [Bill] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK__BillDetai__Ticke__0E04126B] FOREIGN KEY ([TicketId]) REFERENCES [Ticket] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE INDEX [IX_Bill_MemberId] ON [Bill] ([MemberId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE INDEX [IX_Bill_UserId] ON [Bill] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE INDEX [IX_BillDetail_BillId] ON [BillDetail] ([BillId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE INDEX [IX_BillDetail_TicketId] ON [BillDetail] ([TicketId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE INDEX [IX_Contact_MemberId] ON [Contact] ([MemberId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE INDEX [IX_Contact_UserId] ON [Contact] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE INDEX [IX_RoleClaims_RoleId] ON [RoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [Roles] ([NormalizedName]) WHERE ([NormalizedName] IS NOT NULL)');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE INDEX [IX_Ticket_MemberId] ON [Ticket] ([MemberId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE INDEX [IX_Ticket_MovieId] ON [Ticket] ([MovieId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE INDEX [IX_Ticket_ShowtimeId] ON [Ticket] ([ShowtimeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE INDEX [IX_Ticket_TicketPriceId] ON [Ticket] ([TicketPriceId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE INDEX [IX_Ticket_UserId] ON [Ticket] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE INDEX [IX_UserClaims_UserId] ON [UserClaims] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE INDEX [IX_UserLogins_UserId] ON [UserLogins] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE INDEX [IX_UserRoles_RoleId] ON [UserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    CREATE INDEX [EmailIndex] ON [Users] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [Users] ([NormalizedUserName]) WHERE ([NormalizedUserName] IS NOT NULL)');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525100838_CinemaMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210525100838_CinemaMigration', N'5.0.6');
END;
GO

COMMIT;
GO
