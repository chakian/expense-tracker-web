using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EFEntities
{
    public partial class ExpenseTrackerContext : DbContext
    {
        public ExpenseTrackerContext()
        {
        }

        public ExpenseTrackerContext(DbContextOptions<ExpenseTrackerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountTypes> AccountTypes { get; set; }
        public virtual DbSet<Accounts> Accounts { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<BudgetPlanCategories> BudgetPlanCategories { get; set; }
        public virtual DbSet<BudgetPlans> BudgetPlans { get; set; }
        public virtual DbSet<BudgetUsers> BudgetUsers { get; set; }
        public virtual DbSet<Budgets> Budgets { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Currencies> Currencies { get; set; }
        public virtual DbSet<TransactionItem> TransactionItem { get; set; }
        public virtual DbSet<TransactionTemplates> TransactionTemplates { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }
        public virtual DbSet<UserInternalTokens> UserInternalTokens { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=localhost\\SQL2017; Initial Catalog=ExpenseTracker; Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountTypes>(entity =>
            {
                entity.HasKey(e => e.AccountTypeId);

                entity.Property(e => e.AccountTypeId).ValueGeneratedNever();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Accounts>(entity =>
            {
                entity.HasKey(e => e.AccountId);

                entity.HasIndex(e => e.AccountTypeId);

                entity.HasIndex(e => e.BudgetId);

                entity.HasIndex(e => e.InsertUserId);

                entity.HasIndex(e => e.UpdateUserId);

                entity.Property(e => e.CurrentBalance).HasColumnType("money");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.StartingBalance).HasColumnType("money");

                entity.HasOne(d => d.AccountType)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.AccountTypeId);

                entity.HasOne(d => d.Budget)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.BudgetId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.InsertUser)
                    .WithMany(p => p.AccountsInsertUser)
                    .HasForeignKey(d => d.InsertUserId);

                entity.HasOne(d => d.UpdateUser)
                    .WithMany(p => p.AccountsUpdateUser)
                    .HasForeignKey(d => d.UpdateUserId);
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<BudgetPlanCategories>(entity =>
            {
                entity.HasKey(e => e.BudgetPlanCategoryId);

                entity.HasIndex(e => e.BudgetPlanId);

                entity.HasIndex(e => e.CategoryId);

                entity.HasIndex(e => e.InsertUserId);

                entity.HasIndex(e => e.UpdateUserId);

                entity.Property(e => e.PlannedAmount).HasColumnType("money");

                entity.HasOne(d => d.BudgetPlan)
                    .WithMany(p => p.BudgetPlanCategories)
                    .HasForeignKey(d => d.BudgetPlanId);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.BudgetPlanCategories)
                    .HasForeignKey(d => d.CategoryId);

                entity.HasOne(d => d.InsertUser)
                    .WithMany(p => p.BudgetPlanCategoriesInsertUser)
                    .HasForeignKey(d => d.InsertUserId);

                entity.HasOne(d => d.UpdateUser)
                    .WithMany(p => p.BudgetPlanCategoriesUpdateUser)
                    .HasForeignKey(d => d.UpdateUserId);
            });

            modelBuilder.Entity<BudgetPlans>(entity =>
            {
                entity.HasKey(e => e.BudgetPlanId);

                entity.HasIndex(e => e.BudgetId);

                entity.HasIndex(e => e.InsertUserId);

                entity.HasIndex(e => e.UpdateUserId);

                entity.HasOne(d => d.Budget)
                    .WithMany(p => p.BudgetPlans)
                    .HasForeignKey(d => d.BudgetId);

                entity.HasOne(d => d.InsertUser)
                    .WithMany(p => p.BudgetPlansInsertUser)
                    .HasForeignKey(d => d.InsertUserId);

                entity.HasOne(d => d.UpdateUser)
                    .WithMany(p => p.BudgetPlansUpdateUser)
                    .HasForeignKey(d => d.UpdateUserId);
            });

            modelBuilder.Entity<BudgetUsers>(entity =>
            {
                entity.HasKey(e => e.BudgetUserId);

                entity.HasIndex(e => e.BudgetId);

                entity.HasIndex(e => e.InsertUserId);

                entity.HasIndex(e => e.UpdateUserId);

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.CanDelete)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.CanRead)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.CanWrite)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.IsAdmin)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.IsOwner)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.HasOne(d => d.Budget)
                    .WithMany(p => p.BudgetUsers)
                    .HasForeignKey(d => d.BudgetId);

                entity.HasOne(d => d.InsertUser)
                    .WithMany(p => p.BudgetUsersInsertUser)
                    .HasForeignKey(d => d.InsertUserId);

                entity.HasOne(d => d.UpdateUser)
                    .WithMany(p => p.BudgetUsersUpdateUser)
                    .HasForeignKey(d => d.UpdateUserId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BudgetUsersUser)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Budgets>(entity =>
            {
                entity.HasKey(e => e.BudgetId);

                entity.HasIndex(e => e.CurrencyId);

                entity.HasIndex(e => e.InsertUserId);

                entity.HasIndex(e => e.UpdateUserId);

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Budgets)
                    .HasForeignKey(d => d.CurrencyId);

                entity.HasOne(d => d.InsertUser)
                    .WithMany(p => p.BudgetsInsertUser)
                    .HasForeignKey(d => d.InsertUserId);

                entity.HasOne(d => d.UpdateUser)
                    .WithMany(p => p.BudgetsUpdateUser)
                    .HasForeignKey(d => d.UpdateUserId);
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.HasIndex(e => e.BudgetId);

                entity.HasIndex(e => e.InsertUserId);

                entity.HasIndex(e => e.ParentCategoryId);

                entity.HasIndex(e => e.UpdateUserId);

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Budget)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.BudgetId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.InsertUser)
                    .WithMany(p => p.CategoriesInsertUser)
                    .HasForeignKey(d => d.InsertUserId);

                entity.HasOne(d => d.ParentCategory)
                    .WithMany(p => p.InverseParentCategory)
                    .HasForeignKey(d => d.ParentCategoryId);

                entity.HasOne(d => d.UpdateUser)
                    .WithMany(p => p.CategoriesUpdateUser)
                    .HasForeignKey(d => d.UpdateUserId);
            });

            modelBuilder.Entity<Currencies>(entity =>
            {
                entity.HasKey(e => e.CurrencyId);

                entity.Property(e => e.CurrencyId).ValueGeneratedNever();

                entity.Property(e => e.CurrencyCode)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.LongName)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<TransactionItem>(entity =>
            {
                entity.HasIndex(e => e.CategoryId);

                entity.HasIndex(e => e.TransactionId);

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TransactionItem)
                    .HasForeignKey(d => d.CategoryId);

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.TransactionItem)
                    .HasForeignKey(d => d.TransactionId);
            });

            modelBuilder.Entity<TransactionTemplates>(entity =>
            {
                entity.HasIndex(e => e.CategoryId);

                entity.HasIndex(e => e.InsertUserId);

                entity.HasIndex(e => e.SourceAccountId);

                entity.HasIndex(e => e.TargetAccountId);

                entity.HasIndex(e => e.UpdateUserId);

                entity.HasIndex(e => e.UserId);

                entity.HasIndex(e => new { e.BudgetId, e.UserId, e.Name })
                    .HasName("IX_TemplateName_User_Budget")
                    .IsUnique()
                    .HasFilter("([UserId] IS NOT NULL AND [Name] IS NOT NULL)");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.HasOne(d => d.Budget)
                    .WithMany(p => p.TransactionTemplates)
                    .HasForeignKey(d => d.BudgetId);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TransactionTemplates)
                    .HasForeignKey(d => d.CategoryId);

                entity.HasOne(d => d.InsertUser)
                    .WithMany(p => p.TransactionTemplatesInsertUser)
                    .HasForeignKey(d => d.InsertUserId);

                entity.HasOne(d => d.SourceAccount)
                    .WithMany(p => p.TransactionTemplatesSourceAccount)
                    .HasForeignKey(d => d.SourceAccountId);

                entity.HasOne(d => d.TargetAccount)
                    .WithMany(p => p.TransactionTemplatesTargetAccount)
                    .HasForeignKey(d => d.TargetAccountId);

                entity.HasOne(d => d.UpdateUser)
                    .WithMany(p => p.TransactionTemplatesUpdateUser)
                    .HasForeignKey(d => d.UpdateUserId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TransactionTemplatesUser)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Transactions>(entity =>
            {
                entity.HasKey(e => e.TransactionId);

                entity.HasIndex(e => e.CategoryId);

                entity.HasIndex(e => e.InsertUserId);

                entity.HasIndex(e => e.SourceAccountId);

                entity.HasIndex(e => e.TargetAccountId);

                entity.HasIndex(e => e.UpdateUserId);

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.CategoryId);

                entity.HasOne(d => d.InsertUser)
                    .WithMany(p => p.TransactionsInsertUser)
                    .HasForeignKey(d => d.InsertUserId);

                entity.HasOne(d => d.SourceAccount)
                    .WithMany(p => p.TransactionsSourceAccount)
                    .HasForeignKey(d => d.SourceAccountId);

                entity.HasOne(d => d.TargetAccount)
                    .WithMany(p => p.TransactionsTargetAccount)
                    .HasForeignKey(d => d.TargetAccountId);

                entity.HasOne(d => d.UpdateUser)
                    .WithMany(p => p.TransactionsUpdateUser)
                    .HasForeignKey(d => d.UpdateUserId);
            });

            modelBuilder.Entity<UserInternalTokens>(entity =>
            {
                entity.Property(e => e.Issuer).IsRequired();

                entity.Property(e => e.TokenString)
                    .IsRequired()
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.UserId).IsRequired();
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .IsUnique();

                entity.HasIndex(e => e.InsertUserId);

                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.HasIndex(e => e.UpdateUserId);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasOne(d => d.InsertUser)
                    .WithMany(p => p.InverseInsertUser)
                    .HasForeignKey(d => d.InsertUserId);

                entity.HasOne(d => d.UpdateUser)
                    .WithMany(p => p.InverseUpdateUser)
                    .HasForeignKey(d => d.UpdateUserId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
