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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229020256_deletedallmigrations')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229020256_deletedallmigrations')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [FirstName] nvarchar(256) NULL,
        [LastName] nvarchar(256) NULL,
        [FullName] nvarchar(256) NULL,
        [TravelerEmail] nvarchar(256) NULL,
        [Mobile_Number] nvarchar(128) NULL,
        [DateJoined] datetime2 NULL,
        [DefaultTravelGroupKey] nvarchar(450) NULL,
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
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229020256_deletedallmigrations')
BEGIN
    CREATE TABLE [Destinations] (
        [Id] int NOT NULL IDENTITY,
        [City] nvarchar(450) NOT NULL,
        [Country] nvarchar(450) NOT NULL,
        [TravelGroupId] nvarchar(max) NULL,
        [CreatedDate] datetime2 NULL,
        [ModifiedDate] datetime2 NULL,
        CONSTRAINT [PK_Destinations] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229020256_deletedallmigrations')
BEGIN
    CREATE TABLE [Trips] (
        [Id] int NOT NULL IDENTITY,
        [TripName] nvarchar(max) NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NOT NULL,
        [TravelGroupId] nvarchar(max) NULL,
        [CreatedDate] datetime2 NULL,
        [ModifiedDate] datetime2 NULL,
        CONSTRAINT [PK_Trips] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229020256_deletedallmigrations')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229020256_deletedallmigrations')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229020256_deletedallmigrations')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(128) NOT NULL,
        [ProviderKey] nvarchar(128) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229020256_deletedallmigrations')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229020256_deletedallmigrations')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(128) NOT NULL,
        [Name] nvarchar(128) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229020256_deletedallmigrations')
BEGIN
    CREATE TABLE [TravelGroups] (
        [Id] int NOT NULL IDENTITY,
        [GroupName] nvarchar(450) NOT NULL,
        [InvitationKey] nvarchar(450) NULL,
        [SubscriberId] nvarchar(450) NULL,
        CONSTRAINT [PK_TravelGroups] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TravelGroups_AspNetUsers_SubscriberId] FOREIGN KEY ([SubscriberId]) REFERENCES [AspNetUsers] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229020256_deletedallmigrations')
BEGIN
    CREATE TABLE [Travelers] (
        [Id] int NOT NULL IDENTITY,
        [FullName] nvarchar(256) NULL,
        [EmailAddress] nvarchar(256) NULL,
        [TravelGroupId] int NULL,
        [TripId] int NULL,
        CONSTRAINT [PK_Travelers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Travelers_TravelGroups_TravelGroupId] FOREIGN KEY ([TravelGroupId]) REFERENCES [TravelGroups] ([Id]),
        CONSTRAINT [FK_Travelers_Trips_TripId] FOREIGN KEY ([TripId]) REFERENCES [Trips] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229020256_deletedallmigrations')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229020256_deletedallmigrations')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229020256_deletedallmigrations')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229020256_deletedallmigrations')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229020256_deletedallmigrations')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229020256_deletedallmigrations')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229020256_deletedallmigrations')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229020256_deletedallmigrations')
BEGIN
    CREATE INDEX [IX_Travelers_TravelGroupId] ON [Travelers] ([TravelGroupId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229020256_deletedallmigrations')
BEGIN
    CREATE INDEX [IX_Travelers_TripId] ON [Travelers] ([TripId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229020256_deletedallmigrations')
BEGIN
    CREATE INDEX [IX_TravelGroups_SubscriberId] ON [TravelGroups] ([SubscriberId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229020256_deletedallmigrations')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20221229020256_deletedallmigrations', N'6.0.13');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229181242_changes to travelgroup and traveler')
BEGIN
    ALTER TABLE [Travelers] DROP CONSTRAINT [FK_Travelers_TravelGroups_TravelGroupId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229181242_changes to travelgroup and traveler')
BEGIN
    ALTER TABLE [Travelers] DROP CONSTRAINT [FK_Travelers_Trips_TripId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229181242_changes to travelgroup and traveler')
BEGIN
    DROP INDEX [IX_Travelers_TravelGroupId] ON [Travelers];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229181242_changes to travelgroup and traveler')
BEGIN
    EXEC sp_rename N'[Travelers].[TripId]', N'TravelGroupId1', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229181242_changes to travelgroup and traveler')
BEGIN
    EXEC sp_rename N'[Travelers].[IX_Travelers_TripId]', N'IX_Travelers_TravelGroupId1', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229181242_changes to travelgroup and traveler')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Trips]') AND [c].[name] = N'TripName');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Trips] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Trips] ALTER COLUMN [TripName] nvarchar(max) NOT NULL;
    ALTER TABLE [Trips] ADD DEFAULT N'' FOR [TripName];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229181242_changes to travelgroup and traveler')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Trips]') AND [c].[name] = N'TravelGroupId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Trips] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Trips] ALTER COLUMN [TravelGroupId] int NOT NULL;
    ALTER TABLE [Trips] ADD DEFAULT 0 FOR [TravelGroupId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229181242_changes to travelgroup and traveler')
BEGIN
    ALTER TABLE [TravelGroups] ADD [OwnerId] nvarchar(450) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229181242_changes to travelgroup and traveler')
BEGIN
    ALTER TABLE [TravelGroups] ADD [TypeId] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229181242_changes to travelgroup and traveler')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Travelers]') AND [c].[name] = N'TravelGroupId');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Travelers] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Travelers] ALTER COLUMN [TravelGroupId] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229181242_changes to travelgroup and traveler')
BEGIN
    ALTER TABLE [Travelers] ADD [CreatedDate] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229181242_changes to travelgroup and traveler')
BEGIN
    ALTER TABLE [Travelers] ADD [ModifiedDate] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229181242_changes to travelgroup and traveler')
BEGIN
    CREATE INDEX [IX_Trips_TravelGroupId] ON [Trips] ([TravelGroupId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229181242_changes to travelgroup and traveler')
BEGIN
    ALTER TABLE [Travelers] ADD CONSTRAINT [FK_Travelers_TravelGroups_TravelGroupId1] FOREIGN KEY ([TravelGroupId1]) REFERENCES [TravelGroups] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229181242_changes to travelgroup and traveler')
BEGIN
    ALTER TABLE [Trips] ADD CONSTRAINT [FK_Trips_TravelGroups_TravelGroupId] FOREIGN KEY ([TravelGroupId]) REFERENCES [TravelGroups] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229181242_changes to travelgroup and traveler')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20221229181242_changes to travelgroup and traveler', N'6.0.13');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229181501_Removed travelgroupid reference')
BEGIN
    ALTER TABLE [Travelers] DROP CONSTRAINT [FK_Travelers_TravelGroups_TravelGroupId1];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229181501_Removed travelgroupid reference')
BEGIN
    DROP INDEX [IX_Travelers_TravelGroupId1] ON [Travelers];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229181501_Removed travelgroupid reference')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Travelers]') AND [c].[name] = N'TravelGroupId1');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Travelers] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Travelers] DROP COLUMN [TravelGroupId1];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221229181501_Removed travelgroupid reference')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20221229181501_Removed travelgroupid reference', N'6.0.13');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221230164100_changed the baseentity travelgroupid to a int')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Travelers]') AND [c].[name] = N'TravelGroupId');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Travelers] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Travelers] ALTER COLUMN [TravelGroupId] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221230164100_changed the baseentity travelgroupid to a int')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Destinations]') AND [c].[name] = N'TravelGroupId');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Destinations] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [Destinations] ALTER COLUMN [TravelGroupId] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221230164100_changed the baseentity travelgroupid to a int')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20221230164100_changed the baseentity travelgroupid to a int', N'6.0.13');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230114154628_Updated destinations, trips. Added onboarding and onboardingsteps')
BEGIN
    ALTER TABLE [Trips] DROP CONSTRAINT [FK_Trips_TravelGroups_TravelGroupId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230114154628_Updated destinations, trips. Added onboarding and onboardingsteps')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Trips]') AND [c].[name] = N'TravelGroupId');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Trips] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [Trips] ALTER COLUMN [TravelGroupId] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230114154628_Updated destinations, trips. Added onboarding and onboardingsteps')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Trips]') AND [c].[name] = N'StartDate');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Trips] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [Trips] ALTER COLUMN [StartDate] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230114154628_Updated destinations, trips. Added onboarding and onboardingsteps')
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Trips]') AND [c].[name] = N'EndDate');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Trips] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [Trips] ALTER COLUMN [EndDate] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230114154628_Updated destinations, trips. Added onboarding and onboardingsteps')
BEGIN
    ALTER TABLE [Trips] ADD [OwnerId] nvarchar(450) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230114154628_Updated destinations, trips. Added onboarding and onboardingsteps')
BEGIN
    ALTER TABLE [Destinations] ADD [ArrivalDate] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230114154628_Updated destinations, trips. Added onboarding and onboardingsteps')
BEGIN
    ALTER TABLE [Destinations] ADD [DepartureDate] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230114154628_Updated destinations, trips. Added onboarding and onboardingsteps')
BEGIN
    CREATE TABLE [OnBoardings] (
        [Id] int NOT NULL IDENTITY,
        [SubscriberId] nvarchar(max) NOT NULL,
        [CurrentStep] int NOT NULL,
        [isComplete] bit NOT NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NOT NULL,
        CONSTRAINT [PK_OnBoardings] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230114154628_Updated destinations, trips. Added onboarding and onboardingsteps')
BEGIN
    CREATE TABLE [OnBoardingSteps] (
        [Id] int NOT NULL IDENTITY,
        [Step] int NOT NULL,
        [StepName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_OnBoardingSteps] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230114154628_Updated destinations, trips. Added onboarding and onboardingsteps')
BEGIN
    ALTER TABLE [Trips] ADD CONSTRAINT [FK_Trips_TravelGroups_TravelGroupId] FOREIGN KEY ([TravelGroupId]) REFERENCES [TravelGroups] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230114154628_Updated destinations, trips. Added onboarding and onboardingsteps')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230114154628_Updated destinations, trips. Added onboarding and onboardingsteps', N'6.0.13');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230115160310_removed the FK to travelgroup from subscriber')
BEGIN
    ALTER TABLE [TravelGroups] DROP CONSTRAINT [FK_TravelGroups_AspNetUsers_SubscriberId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230115160310_removed the FK to travelgroup from subscriber')
BEGIN
    DROP INDEX [IX_TravelGroups_SubscriberId] ON [TravelGroups];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230115160310_removed the FK to travelgroup from subscriber')
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TravelGroups]') AND [c].[name] = N'SubscriberId');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [TravelGroups] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [TravelGroups] DROP COLUMN [SubscriberId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230115160310_removed the FK to travelgroup from subscriber')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230115160310_removed the FK to travelgroup from subscriber', N'6.0.13');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230115183614_added newtriptemplate and modified destinations')
BEGIN
    ALTER TABLE [Destinations] ADD [TripId] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230115183614_added newtriptemplate and modified destinations')
BEGIN
    CREATE TABLE [NewTripTemplates] (
        [Id] int NOT NULL IDENTITY,
        [TripId] int NOT NULL,
        [TripName] nvarchar(max) NULL,
        [TravelGroupId] int NOT NULL,
        [DestinationId] int NOT NULL,
        [DestinationName] nvarchar(max) NULL,
        [isComplete] bit NOT NULL,
        CONSTRAINT [PK_NewTripTemplates] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230115183614_added newtriptemplate and modified destinations')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230115183614_added newtriptemplate and modified destinations', N'6.0.13');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230115185215_made all properties nullable in newtriptemplate')
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[NewTripTemplates]') AND [c].[name] = N'isComplete');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [NewTripTemplates] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [NewTripTemplates] ALTER COLUMN [isComplete] bit NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230115185215_made all properties nullable in newtriptemplate')
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[NewTripTemplates]') AND [c].[name] = N'TripId');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [NewTripTemplates] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [NewTripTemplates] ALTER COLUMN [TripId] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230115185215_made all properties nullable in newtriptemplate')
BEGIN
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[NewTripTemplates]') AND [c].[name] = N'TravelGroupId');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [NewTripTemplates] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [NewTripTemplates] ALTER COLUMN [TravelGroupId] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230115185215_made all properties nullable in newtriptemplate')
BEGIN
    DECLARE @var13 sysname;
    SELECT @var13 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[NewTripTemplates]') AND [c].[name] = N'DestinationId');
    IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [NewTripTemplates] DROP CONSTRAINT [' + @var13 + '];');
    ALTER TABLE [NewTripTemplates] ALTER COLUMN [DestinationId] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230115185215_made all properties nullable in newtriptemplate')
BEGIN
    ALTER TABLE [NewTripTemplates] ADD [UserId] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230115185215_made all properties nullable in newtriptemplate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230115185215_made all properties nullable in newtriptemplate', N'6.0.13');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230115195058_added email and fullname to newtriptemplate')
BEGIN
    ALTER TABLE [NewTripTemplates] ADD [EmailAddress] nvarchar(256) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230115195058_added email and fullname to newtriptemplate')
BEGIN
    ALTER TABLE [NewTripTemplates] ADD [FullName] nvarchar(256) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230115195058_added email and fullname to newtriptemplate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230115195058_added email and fullname to newtriptemplate', N'6.0.13');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230115203146_added more bools to track the create trip process made isComplete nullable')
BEGIN
    ALTER TABLE [Trips] DROP CONSTRAINT [FK_Trips_TravelGroups_TravelGroupId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230115203146_added more bools to track the create trip process made isComplete nullable')
BEGIN
    DROP INDEX [IX_Trips_TravelGroupId] ON [Trips];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230115203146_added more bools to track the create trip process made isComplete nullable')
BEGIN
    ALTER TABLE [NewTripTemplates] ADD [isDestinationComplete] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230115203146_added more bools to track the create trip process made isComplete nullable')
BEGIN
    ALTER TABLE [NewTripTemplates] ADD [isTravelersComplete] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230115203146_added more bools to track the create trip process made isComplete nullable')
BEGIN
    ALTER TABLE [NewTripTemplates] ADD [isTripComplete] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230115203146_added more bools to track the create trip process made isComplete nullable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230115203146_added more bools to track the create trip process made isComplete nullable', N'6.0.13');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230117020013_updated triptemplate')
BEGIN
    ALTER TABLE [NewTripTemplates] ADD [DepartureDate] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230117020013_updated triptemplate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230117020013_updated triptemplate', N'6.0.13');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230117021415_updated triptemplate with all date properties nullable and arrivaldate')
BEGIN
    ALTER TABLE [NewTripTemplates] ADD [ArrivalDate] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230117021415_updated triptemplate with all date properties nullable and arrivaldate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230117021415_updated triptemplate with all date properties nullable and arrivaldate', N'6.0.13');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230122233343_added the DestinationTraveler data model')
BEGIN
    CREATE TABLE [DestinationTravelers] (
        [Id] int NOT NULL IDENTITY,
        [FullName] nvarchar(256) NULL,
        [EmailAddress] nvarchar(256) NULL,
        [DestinationId] int NOT NULL,
        CONSTRAINT [PK_DestinationTravelers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230122233343_added the DestinationTraveler data model')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230122233343_added the DestinationTraveler data model', N'6.0.13');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230123140950_changed the name of the destination travelers')
BEGIN
    DROP TABLE [DestinationTravelers];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230123140950_changed the name of the destination travelers')
BEGIN
    CREATE TABLE [TravelerDestinations] (
        [Id] int NOT NULL IDENTITY,
        [FullName] nvarchar(256) NULL,
        [EmailAddress] nvarchar(256) NULL,
        [DestinationId] int NOT NULL,
        CONSTRAINT [PK_TravelerDestinations] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230123140950_changed the name of the destination travelers')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230123140950_changed the name of the destination travelers', N'6.0.13');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230125024217_updated the TravelerDestinations table')
BEGIN
    DECLARE @var14 sysname;
    SELECT @var14 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TravelerDestinations]') AND [c].[name] = N'EmailAddress');
    IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [TravelerDestinations] DROP CONSTRAINT [' + @var14 + '];');
    ALTER TABLE [TravelerDestinations] DROP COLUMN [EmailAddress];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230125024217_updated the TravelerDestinations table')
BEGIN
    DECLARE @var15 sysname;
    SELECT @var15 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TravelerDestinations]') AND [c].[name] = N'FullName');
    IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [TravelerDestinations] DROP CONSTRAINT [' + @var15 + '];');
    ALTER TABLE [TravelerDestinations] DROP COLUMN [FullName];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230125024217_updated the TravelerDestinations table')
BEGIN
    ALTER TABLE [TravelerDestinations] ADD [TravelerId] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230125024217_updated the TravelerDestinations table')
BEGIN
    ALTER TABLE [TravelerDestinations] ADD [TripId] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230125024217_updated the TravelerDestinations table')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230125024217_updated the TravelerDestinations table', N'6.0.13');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230128194207_added transports and loadgings')
BEGIN
    CREATE TABLE [Lodgings] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(512) NULL,
        [DestinationId] int NOT NULL,
        [CheckInDate] datetime2 NULL,
        [CheckOutDate] datetime2 NULL,
        [MaxOccupancy] int NULL,
        [Nights] int NOT NULL,
        [WebLink] nvarchar(max) NULL,
        [TravelGroupId] int NULL,
        [CreatedDate] datetime2 NULL,
        [ModifiedDate] datetime2 NULL,
        CONSTRAINT [PK_Lodgings] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230128194207_added transports and loadgings')
BEGIN
    CREATE TABLE [Transports] (
        [Id] int NOT NULL IDENTITY,
        [DestinationId] int NOT NULL,
        [DepartureDestinationId] int NULL,
        [FromAddress] nvarchar(max) NULL,
        [ToAddress] nvarchar(max) NULL,
        [DepartureDatetime] datetime2 NOT NULL,
        [PreferredAirport] nvarchar(max) NULL,
        [ArrivalDestinationId] int NOT NULL,
        [ArrivalDatetime] datetime2 NOT NULL,
        [TransportType] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [Quantity] int NULL,
        [TravelGroupId] int NULL,
        [CreatedDate] datetime2 NULL,
        [ModifiedDate] datetime2 NULL,
        CONSTRAINT [PK_Transports] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230128194207_added transports and loadgings')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230128194207_added transports and loadgings', N'6.0.13');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230131125921_Changes to transport')
BEGIN
    DECLARE @var16 sysname;
    SELECT @var16 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Transports]') AND [c].[name] = N'Quantity');
    IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [Transports] DROP CONSTRAINT [' + @var16 + '];');
    ALTER TABLE [Transports] DROP COLUMN [Quantity];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230131125921_Changes to transport')
BEGIN
    DECLARE @var17 sysname;
    SELECT @var17 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Transports]') AND [c].[name] = N'DestinationId');
    IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [Transports] DROP CONSTRAINT [' + @var17 + '];');
    ALTER TABLE [Transports] ALTER COLUMN [DestinationId] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230131125921_Changes to transport')
BEGIN
    DECLARE @var18 sysname;
    SELECT @var18 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Transports]') AND [c].[name] = N'ArrivalDestinationId');
    IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [Transports] DROP CONSTRAINT [' + @var18 + '];');
    ALTER TABLE [Transports] ALTER COLUMN [ArrivalDestinationId] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230131125921_Changes to transport')
BEGIN
    ALTER TABLE [Transports] ADD [CostPerTraveler] decimal(18,2) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230131125921_Changes to transport')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230131125921_Changes to transport', N'6.0.13');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230201141150_added type tables')
BEGIN
    DECLARE @var19 sysname;
    SELECT @var19 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Lodgings]') AND [c].[name] = N'Nights');
    IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [Lodgings] DROP CONSTRAINT [' + @var19 + '];');
    ALTER TABLE [Lodgings] ALTER COLUMN [Nights] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230201141150_added type tables')
BEGIN
    ALTER TABLE [Lodgings] ADD [CostPerNight] decimal(18,2) NOT NULL DEFAULT 0.0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230201141150_added type tables')
BEGIN
    ALTER TABLE [Lodgings] ADD [LodgingType] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230201141150_added type tables')
BEGIN
    ALTER TABLE [Lodgings] ADD [TotalCost] decimal(18,2) NOT NULL DEFAULT 0.0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230201141150_added type tables')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230201141150_added type tables', N'6.0.13');
END;
GO

COMMIT;
GO

