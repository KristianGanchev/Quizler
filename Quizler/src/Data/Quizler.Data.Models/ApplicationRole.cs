﻿//// ReSharper disable VirtualMemberCallInConstructor
//namespace Quizler.Data.Models
//{
//    using System;

//    using Microsoft.AspNetCore.Identity;
//    using Quizler.Data.Common.Models;

//    public class ApplicationRole : IdentityRole, IAuditInfo, IDeletableEntity
//    {
//        public ApplicationRole()
//            : this(null)
//        {
//        }

//        public ApplicationRole(string name)
//            : base(name)
//        {
//            this.Id = Guid.NewGuid().ToString();
//        }

//        public DateTime CreatedOn { get; set; }

//        public DateTime? ModifiedOn { get; set; }

//        public bool IsDeleted { get; set; }

//        public DateTime? DeletedOn { get; set; }
//    }
//}
