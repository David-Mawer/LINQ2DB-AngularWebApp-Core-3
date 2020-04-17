using IdentityServer4.Models;
using LinqToDB.Mapping;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using static AngularWebApp.Extensions.Linq2Db;

namespace AngularWebApp.Auth.DB
{
    [Table("AspNetRoleClaims")]
    public partial class AspNetRoleClaims : IdentityRoleClaim<string>
    {
        [PrimaryKey, Identity] public override int Id { get => base.Id; set => base.Id = value; } // int
        [Column, NotNull] public override string RoleId { get => base.RoleId; set => base.RoleId = value; } // nvarchar(450)
        [Column, Nullable] public override string ClaimType { get => base.ClaimType; set => base.ClaimType = value; } // nvarchar(max)
        [Column, Nullable] public override string ClaimValue { get => base.ClaimValue; set => base.ClaimValue = value; } // nvarchar(max)

        #region Associations

        /// <summary>
        /// FK_AspNetRoleClaims_AspNetRoles_RoleId
        /// </summary>
        [Association(ThisKey = "RoleId", OtherKey = "Id", CanBeNull = false, Relationship = Relationship.ManyToOne, KeyName = "FK_AspNetRoleClaims_AspNetRoles_RoleId", BackReferenceName = "AspNetRoleClaimsRoleIds")]
        public AspNetRoles Role { get; set; }

        #endregion
    }

    [Table("AspNetRoles")]
    public partial class AspNetRoles : IdentityRole<string>
    {
        [PrimaryKey, NotNull] public override string Id { get => base.Id; set => base.Id = value; } // nvarchar(450)
        [Column, Nullable] public override string Name { get => base.Name; set => base.Name = value; } // nvarchar(256)
        [Column, Nullable] public override string NormalizedName { get => base.NormalizedName; set => base.NormalizedName = value; } // nvarchar(256)
        [Column, Nullable] public override string ConcurrencyStamp { get => base.ConcurrencyStamp; set => base.ConcurrencyStamp = value; } // nvarchar(max)
        [Column, Nullable] public string Description { get; set; } // nvarchar(max)
        [Column, Nullable] public string CreatedBy { get; set; } // nvarchar(max)
        [Column, Nullable] public string UpdatedBy { get; set; } // nvarchar(max)
        [Column, NotNull] public DateTime CreatedDate { get; set; } // datetime2(7)
        [Column, NotNull] public DateTime UpdatedDate { get; set; } // datetime2(7)

        #region Associations

        /// <summary>
        /// FK_AspNetRoleClaims_AspNetRoles_RoleId_BackReference
        /// </summary>
        [Association(ThisKey = "Id", OtherKey = "RoleId", CanBeNull = true, Relationship = Relationship.OneToMany, IsBackReference = true)]
        public IEnumerable<AspNetRoleClaims> AspNetRoleClaimsRoleIds { get; set; }

        /// <summary>
        /// FK_AspNetUserRoles_AspNetRoles_RoleId_BackReference
        /// </summary>
        [Association(ThisKey = "Id", OtherKey = "RoleId", CanBeNull = true, Relationship = Relationship.OneToMany, IsBackReference = true)]
        public IEnumerable<AspNetUserRoles> AspNetUserRolesRoleIds { get; set; }

        #endregion
    }

    [Table("AspNetUserClaims")]
    public partial class AspNetUserClaims : IdentityUserClaim<string>
    {
        [PrimaryKey, Identity] public override int Id { get => base.Id; set => base.Id = value; } // int
        [Column, NotNull] public override string UserId { get => base.UserId; set => base.UserId = value; } // nvarchar(450)
        [Column, Nullable] public override string ClaimType { get => base.ClaimType; set => base.ClaimType = value; } // nvarchar(max)
        [Column, Nullable] public override string ClaimValue { get => base.ClaimValue; set => base.ClaimValue = value; } // nvarchar(max)

        #region Associations

        /// <summary>
        /// FK_AspNetUserClaims_AspNetUsers_UserId
        /// </summary>
        [Association(ThisKey = "UserId", OtherKey = "Id", CanBeNull = false, Relationship = Relationship.ManyToOne, KeyName = "FK_AspNetUserClaims_AspNetUsers_UserId", BackReferenceName = "AspNetUserClaimsUserIds")]
        public AspNetUsers User { get; set; }

        #endregion
    }

    [Table("AspNetUserLogins")]
    public partial class AspNetUserLogins : IdentityUserLogin<string>
    {
        [PrimaryKey(1), NotNull] public override string LoginProvider { get => base.LoginProvider; set => base.LoginProvider = value; } // nvarchar(128)
        [PrimaryKey(2), NotNull] public override string ProviderKey { get => base.ProviderKey; set => base.ProviderKey = value; } // nvarchar(128)
        [Microsoft.AspNetCore.Identity.PersonalData] [Column, Nullable] public override string ProviderDisplayName { get => base.ProviderDisplayName; set => base.ProviderDisplayName = value; } // nvarchar(max)
        [Column, NotNull] public override string UserId { get => base.UserId; set => base.UserId = value; } // nvarchar(450)

        #region Associations

        /// <summary>
        /// FK_AspNetUserLogins_AspNetUsers_UserId
        /// </summary>
        [Association(ThisKey = "UserId", OtherKey = "Id", CanBeNull = false, Relationship = Relationship.ManyToOne, KeyName = "FK_AspNetUserLogins_AspNetUsers_UserId", BackReferenceName = "AspNetUserLoginsUserIds")]
        public AspNetUsers User { get; set; }

        #endregion
    }

    [Table("AspNetUserRoles")]
    public partial class AspNetUserRoles : IdentityUserRole<string>
    {
        [PrimaryKey(1), NotNull] public override string UserId { get => base.UserId; set => base.UserId = value; } // nvarchar(450)
        [PrimaryKey(2), NotNull] public override string RoleId { get => base.RoleId; set => base.RoleId = value; } // nvarchar(450)

        #region Associations

        /// <summary>
        /// FK_AspNetUserRoles_AspNetRoles_RoleId
        /// </summary>
        [Association(ThisKey = "RoleId", OtherKey = "Id", CanBeNull = false, Relationship = Relationship.ManyToOne, KeyName = "FK_AspNetUserRoles_AspNetRoles_RoleId", BackReferenceName = "AspNetUserRolesRoleIds")]
        public AspNetRoles Role { get; set; }

        /// <summary>
        /// FK_AspNetUserRoles_AspNetUsers_UserId
        /// </summary>
        [Association(ThisKey = "UserId", OtherKey = "Id", CanBeNull = false, Relationship = Relationship.ManyToOne, KeyName = "FK_AspNetUserRoles_AspNetUsers_UserId", BackReferenceName = "AspNetUserRolesUserIds")]
        public AspNetUsers User { get; set; }

        #endregion
    }

    [Table("AspNetUsers")]
    public partial class AspNetUsers : IdentityUser, IConcurrency<string>
    {
        [PrimaryKey, NotNull] public override string Id { get => base.Id; set => base.Id = value; } // nvarchar(450)
        [Microsoft.AspNetCore.Identity.PersonalData] [Column, Nullable] public override string UserName { get => base.UserName; set => base.UserName = value; } // nvarchar(256)
        [Column, Nullable] public override string NormalizedUserName { get => base.NormalizedUserName; set => base.NormalizedUserName = value; } // nvarchar(256)
        [Microsoft.AspNetCore.Identity.PersonalData] [Column, Nullable] public override string Email { get => base.Email; set => base.Email = value; } // nvarchar(256)
        [Column, Nullable] public override string NormalizedEmail { get => base.NormalizedEmail; set => base.NormalizedEmail = value; } // nvarchar(256)
        [Column, NotNull] public override bool EmailConfirmed { get => base.EmailConfirmed; set => base.EmailConfirmed = value; } // bit
        [Column, Nullable] public override string PasswordHash { get => base.PasswordHash; set => base.PasswordHash = value; } // nvarchar(max)
        [Column, Nullable] public override string SecurityStamp { get => base.SecurityStamp; set => base.SecurityStamp = value; } // nvarchar(max)
        [Column, Nullable] public override string ConcurrencyStamp { get => base.ConcurrencyStamp; set => base.ConcurrencyStamp = value; } // nvarchar(max)
        [Microsoft.AspNetCore.Identity.PersonalData] [Column, Nullable] public override string PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; } // nvarchar(max)
        [Column, NotNull] public override bool PhoneNumberConfirmed { get => base.PhoneNumberConfirmed; set => base.PhoneNumberConfirmed = value; } // bit
        [Column, NotNull] public override bool TwoFactorEnabled { get => base.TwoFactorEnabled; set => base.TwoFactorEnabled = value; } // bit
        [Column, Nullable] public override DateTimeOffset? LockoutEnd { get => base.LockoutEnd; set => base.LockoutEnd = value; } // datetimeoffset(7)
        [Column, NotNull] public override bool LockoutEnabled { get => base.LockoutEnabled; set => base.LockoutEnabled = value; } // bit
        [Column, NotNull] public override int AccessFailedCount { get => base.AccessFailedCount; set => base.AccessFailedCount = value; } // int


        #region Associations

        /// <summary>
        /// FK_AspNetUserClaims_AspNetUsers_UserId_BackReference
        /// </summary>
        [Association(ThisKey = "Id", OtherKey = "UserId", CanBeNull = true, Relationship = Relationship.OneToMany, IsBackReference = true)]
        public IEnumerable<AspNetUserClaims> AspNetUserClaimsUserIds { get; set; }

        /// <summary>
        /// FK_AspNetUserLogins_AspNetUsers_UserId_BackReference
        /// </summary>
        [Association(ThisKey = "Id", OtherKey = "UserId", CanBeNull = true, Relationship = Relationship.OneToMany, IsBackReference = true)]
        public IEnumerable<AspNetUserLogins> AspNetUserLoginsUserIds { get; set; }

        /// <summary>
        /// FK_AspNetUserRoles_AspNetUsers_UserId_BackReference
        /// </summary>
        [Association(ThisKey = "Id", OtherKey = "UserId", CanBeNull = true, Relationship = Relationship.OneToMany, IsBackReference = true)]
        public IEnumerable<AspNetUserRoles> AspNetUserRolesUserIds { get; set; }

        /// <summary>
        /// FK_AspNetUserTokens_AspNetUsers_UserId_BackReference
        /// </summary>
        [Association(ThisKey = "Id", OtherKey = "UserId", CanBeNull = true, Relationship = Relationship.OneToMany, IsBackReference = true)]
        public IEnumerable<AspNetUserTokens> AspNetUserTokensUserIds { get; set; }

        #endregion
    }

    [Table("AspNetUserTokens")]
    public partial class AspNetUserTokens : IdentityUserToken<string>
    {
        [PrimaryKey(1), NotNull] public override string UserId { get => base.UserId; set => base.UserId = value; }
        [PrimaryKey(3), NotNull] public override string LoginProvider { get => base.LoginProvider; set => base.LoginProvider = value; }
        [PrimaryKey(2), NotNull] public override string Name { get => base.Name; set => base.Name = value; }
        [Column, Nullable] public override string Value { get => base.Value; set => base.Value = value; }
    }

    [Table("DeviceCodes")]
    public partial class DeviceCodes : DeviceCode
    {
        [PrimaryKey, NotNull] public string UserCode { get; set; } // nvarchar(200)
        [Column, NotNull] public string DeviceCode { get; set; } // nvarchar(200)
        [Column, Nullable] public string SubjectId { get; set; } // nvarchar(200)
        [Column, NotNull] public new string ClientId { get { return base.ClientId; } set { base.ClientId = value; } } // nvarchar(200)
        [Column, NotNull] public new DateTime CreationTime { get { return base.CreationTime; } set { base.CreationTime = value; } } // datetime2(7)
        [Column, NotNull] public DateTime Expiration { get; set; } // datetime2(7)
        [Column, NotNull] public string Data { get; set; } // nvarchar(max)
    }

    [Table("PersistedGrants")]
    public partial class PersistedGrants : PersistedGrant
    {
        [PrimaryKey, NotNull] public new string Key { get { return base.Key; } set { base.Key = value; } } // nvarchar(200)
        [Column, NotNull] public new string Type { get { return base.Type; } set { base.Type = value; } } // nvarchar(50)
        [Column, Nullable] public new string SubjectId { get { return base.SubjectId; } set { base.SubjectId = value; } } // nvarchar(200)
        [Column, NotNull] public new string ClientId { get { return base.ClientId; } set { base.ClientId = value; } } // nvarchar(200)
        [Column, NotNull] public new DateTime CreationTime { get { return base.CreationTime; } set { base.CreationTime = value; } } // datetime2(7)
        [Column, Nullable] public new DateTime? Expiration { get { return base.Expiration; } set { base.Expiration = value; } } // datetime2(7)
        [Column, NotNull] public new string Data { get { return base.Data; } set { base.Data = value; } } // nvarchar(max)
    }

}
