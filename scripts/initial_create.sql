IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [AccountTypes] (
    [AccountTypeId] int NOT NULL,
    [IsActive] bit NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_AccountTypes] PRIMARY KEY ([AccountTypeId])
);

GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Currencies] (
    [CurrencyId] int NOT NULL,
    [IsActive] bit NOT NULL,
    [CurrencyCode] nvarchar(15) NOT NULL,
    [LongName] nvarchar(150) NOT NULL,
    [DisplayName] nvarchar(15) NOT NULL,
    CONSTRAINT [PK_Currencies] PRIMARY KEY ([CurrencyId])
);

GO

CREATE TABLE [UserInternalTokens] (
    [Id] nvarchar(450) NOT NULL,
    [UserId] nvarchar(max) NOT NULL,
    [Issuer] nvarchar(max) NOT NULL,
    [CreatingIp] nvarchar(max) NULL,
    [ValidFrom] datetime2 NOT NULL,
    [ValidTo] datetime2 NOT NULL,
    [LastUsedDate] datetime2 NOT NULL,
    [IsValid] bit NOT NULL,
    [TokenString] nvarchar(max) NOT NULL DEFAULT ((N'')),
    [Device] nvarchar(max) NULL,
    CONSTRAINT [PK_UserInternalTokens] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Users] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NOT NULL,
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
    [IsActive] bit NOT NULL,
    [InsertUserId] nvarchar(450) NULL,
    [InsertTime] datetime2 NOT NULL,
    [UpdateUserId] nvarchar(450) NULL,
    [UpdateTime] datetime2 NULL,
    [ActiveBudgetId] int NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Users_Users_InsertUserId] FOREIGN KEY ([InsertUserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Users_Users_UpdateUserId] FOREIGN KEY ([UpdateUserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Budgets] (
    [BudgetId] int NOT NULL IDENTITY,
    [IsActive] bit NOT NULL,
    [InsertUserId] nvarchar(450) NULL,
    [InsertTime] datetime2 NOT NULL,
    [UpdateUserId] nvarchar(450) NULL,
    [UpdateTime] datetime2 NULL,
    [Name] nvarchar(max) NOT NULL,
    [CurrencyId] int NOT NULL,
    CONSTRAINT [PK_Budgets] PRIMARY KEY ([BudgetId]),
    CONSTRAINT [FK_Budgets_Currencies_CurrencyId] FOREIGN KEY ([CurrencyId]) REFERENCES [Currencies] ([CurrencyId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Budgets_Users_InsertUserId] FOREIGN KEY ([InsertUserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Budgets_Users_UpdateUserId] FOREIGN KEY ([UpdateUserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Accounts] (
    [AccountId] int NOT NULL IDENTITY,
    [IsActive] bit NOT NULL,
    [InsertUserId] nvarchar(450) NULL,
    [InsertTime] datetime2 NOT NULL,
    [UpdateUserId] nvarchar(450) NULL,
    [UpdateTime] datetime2 NULL,
    [Name] nvarchar(max) NOT NULL,
    [StartingBalance] money NOT NULL,
    [CurrentBalance] money NOT NULL,
    [AccountTypeId] int NOT NULL,
    [BudgetId] int NOT NULL,
    CONSTRAINT [PK_Accounts] PRIMARY KEY ([AccountId]),
    CONSTRAINT [FK_Accounts_AccountTypes_AccountTypeId] FOREIGN KEY ([AccountTypeId]) REFERENCES [AccountTypes] ([AccountTypeId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Accounts_Budgets_BudgetId] FOREIGN KEY ([BudgetId]) REFERENCES [Budgets] ([BudgetId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Accounts_Users_InsertUserId] FOREIGN KEY ([InsertUserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Accounts_Users_UpdateUserId] FOREIGN KEY ([UpdateUserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [BudgetPlans] (
    [BudgetPlanId] int NOT NULL IDENTITY,
    [IsActive] bit NOT NULL,
    [InsertUserId] nvarchar(450) NULL,
    [InsertTime] datetime2 NOT NULL,
    [UpdateUserId] nvarchar(450) NULL,
    [UpdateTime] datetime2 NULL,
    [Month] int NOT NULL,
    [Year] int NOT NULL,
    [BudgetId] int NOT NULL,
    CONSTRAINT [PK_BudgetPlans] PRIMARY KEY ([BudgetPlanId]),
    CONSTRAINT [FK_BudgetPlans_Budgets_BudgetId] FOREIGN KEY ([BudgetId]) REFERENCES [Budgets] ([BudgetId]) ON DELETE CASCADE,
    CONSTRAINT [FK_BudgetPlans_Users_InsertUserId] FOREIGN KEY ([InsertUserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_BudgetPlans_Users_UpdateUserId] FOREIGN KEY ([UpdateUserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [BudgetUsers] (
    [BudgetUserId] int NOT NULL IDENTITY,
    [IsActive] bit NOT NULL,
    [InsertUserId] nvarchar(450) NULL,
    [InsertTime] datetime2 NOT NULL,
    [UpdateUserId] nvarchar(450) NULL,
    [UpdateTime] datetime2 NULL,
    [BudgetId] int NOT NULL,
    [UserId] nvarchar(450) NULL,
    [CanDelete] bit NOT NULL DEFAULT ((CONVERT([bit],(0)))),
    [CanRead] bit NOT NULL DEFAULT ((CONVERT([bit],(0)))),
    [CanWrite] bit NOT NULL DEFAULT ((CONVERT([bit],(0)))),
    [IsAdmin] bit NOT NULL DEFAULT ((CONVERT([bit],(0)))),
    [IsOwner] bit NOT NULL DEFAULT ((CONVERT([bit],(0)))),
    CONSTRAINT [PK_BudgetUsers] PRIMARY KEY ([BudgetUserId]),
    CONSTRAINT [FK_BudgetUsers_Budgets_BudgetId] FOREIGN KEY ([BudgetId]) REFERENCES [Budgets] ([BudgetId]) ON DELETE CASCADE,
    CONSTRAINT [FK_BudgetUsers_Users_InsertUserId] FOREIGN KEY ([InsertUserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_BudgetUsers_Users_UpdateUserId] FOREIGN KEY ([UpdateUserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_BudgetUsers_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Categories] (
    [CategoryId] int NOT NULL IDENTITY,
    [IsActive] bit NOT NULL,
    [InsertUserId] nvarchar(450) NULL,
    [InsertTime] datetime2 NOT NULL,
    [UpdateUserId] nvarchar(450) NULL,
    [UpdateTime] datetime2 NULL,
    [Name] nvarchar(max) NOT NULL,
    [IsIncomeCategory] bit NOT NULL,
    [ParentCategoryId] int NULL,
    [Order] int NOT NULL,
    [BudgetId] int NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([CategoryId]),
    CONSTRAINT [FK_Categories_Budgets_BudgetId] FOREIGN KEY ([BudgetId]) REFERENCES [Budgets] ([BudgetId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Categories_Users_InsertUserId] FOREIGN KEY ([InsertUserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Categories_Categories_ParentCategoryId] FOREIGN KEY ([ParentCategoryId]) REFERENCES [Categories] ([CategoryId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Categories_Users_UpdateUserId] FOREIGN KEY ([UpdateUserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [BudgetPlanCategories] (
    [BudgetPlanCategoryId] int NOT NULL IDENTITY,
    [IsActive] bit NOT NULL,
    [InsertUserId] nvarchar(450) NULL,
    [InsertTime] datetime2 NOT NULL,
    [UpdateUserId] nvarchar(450) NULL,
    [UpdateTime] datetime2 NULL,
    [PlannedAmount] money NOT NULL,
    [BudgetPlanId] int NOT NULL,
    [CategoryId] int NOT NULL,
    CONSTRAINT [PK_BudgetPlanCategories] PRIMARY KEY ([BudgetPlanCategoryId]),
    CONSTRAINT [FK_BudgetPlanCategories_BudgetPlans_BudgetPlanId] FOREIGN KEY ([BudgetPlanId]) REFERENCES [BudgetPlans] ([BudgetPlanId]) ON DELETE CASCADE,
    CONSTRAINT [FK_BudgetPlanCategories_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([CategoryId]) ON DELETE CASCADE,
    CONSTRAINT [FK_BudgetPlanCategories_Users_InsertUserId] FOREIGN KEY ([InsertUserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_BudgetPlanCategories_Users_UpdateUserId] FOREIGN KEY ([UpdateUserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Transactions] (
    [TransactionId] int NOT NULL IDENTITY,
    [IsActive] bit NOT NULL,
    [InsertUserId] nvarchar(450) NULL,
    [InsertTime] datetime2 NOT NULL,
    [UpdateUserId] nvarchar(450) NULL,
    [UpdateTime] datetime2 NULL,
    [Date] datetime2 NOT NULL,
    [Amount] money NOT NULL,
    [Description] nvarchar(max) NULL,
    [CategoryId] int NULL,
    [SourceAccountId] int NOT NULL,
    [TargetAccountId] int NULL,
    CONSTRAINT [PK_Transactions] PRIMARY KEY ([TransactionId]),
    CONSTRAINT [FK_Transactions_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([CategoryId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Transactions_Users_InsertUserId] FOREIGN KEY ([InsertUserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Transactions_Accounts_SourceAccountId] FOREIGN KEY ([SourceAccountId]) REFERENCES [Accounts] ([AccountId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Transactions_Accounts_TargetAccountId] FOREIGN KEY ([TargetAccountId]) REFERENCES [Accounts] ([AccountId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Transactions_Users_UpdateUserId] FOREIGN KEY ([UpdateUserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [TransactionTemplates] (
    [Id] int NOT NULL IDENTITY,
    [IsActive] bit NOT NULL,
    [InsertUserId] nvarchar(450) NULL,
    [InsertTime] datetime2 NOT NULL,
    [UpdateUserId] nvarchar(450) NULL,
    [UpdateTime] datetime2 NULL,
    [Name] nvarchar(250) NULL,
    [Amount] money NULL,
    [Description] nvarchar(max) NULL,
    [CategoryId] int NULL,
    [SourceAccountId] int NULL,
    [TargetAccountId] int NULL,
    [BudgetId] int NOT NULL,
    [UserId] nvarchar(450) NULL,
    CONSTRAINT [PK_TransactionTemplates] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TransactionTemplates_Budgets_BudgetId] FOREIGN KEY ([BudgetId]) REFERENCES [Budgets] ([BudgetId]) ON DELETE CASCADE,
    CONSTRAINT [FK_TransactionTemplates_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([CategoryId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_TransactionTemplates_Users_InsertUserId] FOREIGN KEY ([InsertUserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_TransactionTemplates_Accounts_SourceAccountId] FOREIGN KEY ([SourceAccountId]) REFERENCES [Accounts] ([AccountId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_TransactionTemplates_Accounts_TargetAccountId] FOREIGN KEY ([TargetAccountId]) REFERENCES [Accounts] ([AccountId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_TransactionTemplates_Users_UpdateUserId] FOREIGN KEY ([UpdateUserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_TransactionTemplates_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [TransactionItem] (
    [TransactionItemId] int NOT NULL IDENTITY,
    [TransactionId] int NOT NULL,
    [Amount] money NOT NULL,
    [Description] nvarchar(max) NULL,
    [CategoryId] int NOT NULL,
    CONSTRAINT [PK_TransactionItem] PRIMARY KEY ([TransactionItemId]),
    CONSTRAINT [FK_TransactionItem_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([CategoryId]) ON DELETE CASCADE,
    CONSTRAINT [FK_TransactionItem_Transactions_TransactionId] FOREIGN KEY ([TransactionId]) REFERENCES [Transactions] ([TransactionId]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Accounts_AccountTypeId] ON [Accounts] ([AccountTypeId]);

GO

CREATE INDEX [IX_Accounts_BudgetId] ON [Accounts] ([BudgetId]);

GO

CREATE INDEX [IX_Accounts_InsertUserId] ON [Accounts] ([InsertUserId]);

GO

CREATE INDEX [IX_Accounts_UpdateUserId] ON [Accounts] ([UpdateUserId]);

GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);

GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE ([NormalizedName] IS NOT NULL);

GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);

GO

CREATE INDEX [IX_BudgetPlanCategories_BudgetPlanId] ON [BudgetPlanCategories] ([BudgetPlanId]);

GO

CREATE INDEX [IX_BudgetPlanCategories_CategoryId] ON [BudgetPlanCategories] ([CategoryId]);

GO

CREATE INDEX [IX_BudgetPlanCategories_InsertUserId] ON [BudgetPlanCategories] ([InsertUserId]);

GO

CREATE INDEX [IX_BudgetPlanCategories_UpdateUserId] ON [BudgetPlanCategories] ([UpdateUserId]);

GO

CREATE INDEX [IX_BudgetPlans_BudgetId] ON [BudgetPlans] ([BudgetId]);

GO

CREATE INDEX [IX_BudgetPlans_InsertUserId] ON [BudgetPlans] ([InsertUserId]);

GO

CREATE INDEX [IX_BudgetPlans_UpdateUserId] ON [BudgetPlans] ([UpdateUserId]);

GO

CREATE INDEX [IX_Budgets_CurrencyId] ON [Budgets] ([CurrencyId]);

GO

CREATE INDEX [IX_Budgets_InsertUserId] ON [Budgets] ([InsertUserId]);

GO

CREATE INDEX [IX_Budgets_UpdateUserId] ON [Budgets] ([UpdateUserId]);

GO

CREATE INDEX [IX_BudgetUsers_BudgetId] ON [BudgetUsers] ([BudgetId]);

GO

CREATE INDEX [IX_BudgetUsers_InsertUserId] ON [BudgetUsers] ([InsertUserId]);

GO

CREATE INDEX [IX_BudgetUsers_UpdateUserId] ON [BudgetUsers] ([UpdateUserId]);

GO

CREATE INDEX [IX_BudgetUsers_UserId] ON [BudgetUsers] ([UserId]);

GO

CREATE INDEX [IX_Categories_BudgetId] ON [Categories] ([BudgetId]);

GO

CREATE INDEX [IX_Categories_InsertUserId] ON [Categories] ([InsertUserId]);

GO

CREATE INDEX [IX_Categories_ParentCategoryId] ON [Categories] ([ParentCategoryId]);

GO

CREATE INDEX [IX_Categories_UpdateUserId] ON [Categories] ([UpdateUserId]);

GO

CREATE INDEX [IX_TransactionItem_CategoryId] ON [TransactionItem] ([CategoryId]);

GO

CREATE INDEX [IX_TransactionItem_TransactionId] ON [TransactionItem] ([TransactionId]);

GO

CREATE INDEX [IX_Transactions_CategoryId] ON [Transactions] ([CategoryId]);

GO

CREATE INDEX [IX_Transactions_InsertUserId] ON [Transactions] ([InsertUserId]);

GO

CREATE INDEX [IX_Transactions_SourceAccountId] ON [Transactions] ([SourceAccountId]);

GO

CREATE INDEX [IX_Transactions_TargetAccountId] ON [Transactions] ([TargetAccountId]);

GO

CREATE INDEX [IX_Transactions_UpdateUserId] ON [Transactions] ([UpdateUserId]);

GO

CREATE INDEX [IX_TransactionTemplates_CategoryId] ON [TransactionTemplates] ([CategoryId]);

GO

CREATE INDEX [IX_TransactionTemplates_InsertUserId] ON [TransactionTemplates] ([InsertUserId]);

GO

CREATE INDEX [IX_TransactionTemplates_SourceAccountId] ON [TransactionTemplates] ([SourceAccountId]);

GO

CREATE INDEX [IX_TransactionTemplates_TargetAccountId] ON [TransactionTemplates] ([TargetAccountId]);

GO

CREATE INDEX [IX_TransactionTemplates_UpdateUserId] ON [TransactionTemplates] ([UpdateUserId]);

GO

CREATE INDEX [IX_TransactionTemplates_UserId] ON [TransactionTemplates] ([UserId]);

GO

CREATE UNIQUE INDEX [IX_TemplateName_User_Budget] ON [TransactionTemplates] ([BudgetId], [UserId], [Name]) WHERE ([UserId] IS NOT NULL AND [Name] IS NOT NULL);

GO

CREATE UNIQUE INDEX [IX_Users_Email] ON [Users] ([Email]);

GO

CREATE INDEX [IX_Users_InsertUserId] ON [Users] ([InsertUserId]);

GO

CREATE INDEX [EmailIndex] ON [Users] ([NormalizedEmail]);

GO

CREATE UNIQUE INDEX [UserNameIndex] ON [Users] ([NormalizedUserName]) WHERE ([NormalizedUserName] IS NOT NULL);

GO

CREATE INDEX [IX_Users_UpdateUserId] ON [Users] ([UpdateUserId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200815161912_Test', N'3.1.7');

GO

