﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcTest.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class intissuesEntities2 : DbContext
    {
        public intissuesEntities2()
            : base("name=intissuesEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Bill_Det_1500Form> Bill_Det_1500Form { get; set; }
        public virtual DbSet<Bill_Det_EOBFile> Bill_Det_EOBFile { get; set; }
        public virtual DbSet<Bill_Mst_1500Form> Bill_Mst_1500Form { get; set; }
        public virtual DbSet<dtproperty> dtproperties { get; set; }
        public virtual DbSet<EmailBilling> EmailBillings { get; set; }
        public virtual DbSet<EmailSupport> EmailSupports { get; set; }
        public virtual DbSet<Employee_Mst_Personal> Employee_Mst_Personal { get; set; }
        public virtual DbSet<Gen_Custom_Ctrl> Gen_Custom_Ctrl { get; set; }
        public virtual DbSet<Gen_Custom_Number> Gen_Custom_Number { get; set; }
        public virtual DbSet<Gen_Custom_Seq_Ctrl> Gen_Custom_Seq_Ctrl { get; set; }
        public virtual DbSet<Gen_Det_MedazFollowup> Gen_Det_MedazFollowup { get; set; }
        public virtual DbSet<Gen_DET_UserPrefer> Gen_DET_UserPrefer { get; set; }
        public virtual DbSet<Gen_Mst_Appsettings> Gen_Mst_Appsettings { get; set; }
        public virtual DbSet<Gen_Mst_Client> Gen_Mst_Client { get; set; }
        public virtual DbSet<Gen_Mst_ClientInfo> Gen_Mst_ClientInfo { get; set; }
        public virtual DbSet<Gen_Mst_ClientMachines> Gen_Mst_ClientMachines { get; set; }
        public virtual DbSet<Gen_Mst_Components> Gen_Mst_Components { get; set; }
        public virtual DbSet<Gen_Mst_DevTraining> Gen_Mst_DevTraining { get; set; }
        public virtual DbSet<Gen_Mst_DocumentType> Gen_Mst_DocumentType { get; set; }
        public virtual DbSet<Gen_Mst_ErrLog> Gen_Mst_ErrLog { get; set; }
        public virtual DbSet<Gen_Mst_Files> Gen_Mst_Files { get; set; }
        public virtual DbSet<Gen_Mst_GeneralTestCases> Gen_Mst_GeneralTestCases { get; set; }
        public virtual DbSet<Gen_Mst_HowDoI> Gen_Mst_HowDoI { get; set; }
        public virtual DbSet<Gen_Mst_IssuePriority> Gen_Mst_IssuePriority { get; set; }
        public virtual DbSet<Gen_Mst_IssueStatus> Gen_Mst_IssueStatus { get; set; }
        public virtual DbSet<Gen_Mst_IssueType> Gen_Mst_IssueType { get; set; }
        public virtual DbSet<Gen_Mst_MedazErrLog> Gen_Mst_MedazErrLog { get; set; }
        public virtual DbSet<Gen_Mst_MedazFollowup> Gen_Mst_MedazFollowup { get; set; }
        public virtual DbSet<Gen_Mst_Module> Gen_Mst_Module { get; set; }
        public virtual DbSet<Gen_Mst_MyTask> Gen_Mst_MyTask { get; set; }
        public virtual DbSet<Gen_Mst_PRDSTDTestCases> Gen_Mst_PRDSTDTestCases { get; set; }
        public virtual DbSet<Gen_Mst_Product> Gen_Mst_Product { get; set; }
        public virtual DbSet<Gen_Mst_Screen> Gen_Mst_Screen { get; set; }
        public virtual DbSet<Gen_Mst_Table> Gen_Mst_Table { get; set; }
        public virtual DbSet<Gen_Mst_Teams> Gen_Mst_Teams { get; set; }
        public virtual DbSet<Gen_Mst_Technology> Gen_Mst_Technology { get; set; }
        public virtual DbSet<Gen_Mst_TestCases> Gen_Mst_TestCases { get; set; }
        public virtual DbSet<Gen_Mst_TestCaseType> Gen_Mst_TestCaseType { get; set; }
        public virtual DbSet<Gen_mst_TestingProcess> Gen_mst_TestingProcess { get; set; }
        public virtual DbSet<Gen_Mst_UserPrefer> Gen_Mst_UserPrefer { get; set; }
        public virtual DbSet<Gen_Mst_version> Gen_Mst_version { get; set; }
        public virtual DbSet<Gen_Mst_VersionUpdateDetail> Gen_Mst_VersionUpdateDetail { get; set; }
        public virtual DbSet<ISO_MST_Heading> ISO_MST_Heading { get; set; }
        public virtual DbSet<Issue_Defects> Issue_Defects { get; set; }
        public virtual DbSet<Issue_Defects_History> Issue_Defects_History { get; set; }
        public virtual DbSet<Issue_Testcases> Issue_Testcases { get; set; }
        public virtual DbSet<IssueDetail> IssueDetails { get; set; }
        public virtual DbSet<IssueMaster> IssueMasters { get; set; }
        public virtual DbSet<Rights_Det_Role_Access> Rights_Det_Role_Access { get; set; }
        public virtual DbSet<Rights_DET_Role_Actions> Rights_DET_Role_Actions { get; set; }
        public virtual DbSet<Rights_DET_Role_Menu> Rights_DET_Role_Menu { get; set; }
        public virtual DbSet<Rights_DET_User_Actions> Rights_DET_User_Actions { get; set; }
        public virtual DbSet<Rights_DET_User_Menu> Rights_DET_User_Menu { get; set; }
        public virtual DbSet<Rights_MST_Action> Rights_MST_Action { get; set; }
        public virtual DbSet<Rights_MST_Menu> Rights_MST_Menu { get; set; }
        public virtual DbSet<Rights_MST_Role> Rights_MST_Role { get; set; }
        public virtual DbSet<Rights_MST_Screen> Rights_MST_Screen { get; set; }
        public virtual DbSet<Rights_MST_UserAccounts> Rights_MST_UserAccounts { get; set; }
        public virtual DbSet<Rights_MST_UserMaster> Rights_MST_UserMaster { get; set; }
        public virtual DbSet<SysMZ_KeyActions> SysMZ_KeyActions { get; set; }
        public virtual DbSet<SysMZ_KeyCodes> SysMZ_KeyCodes { get; set; }
        public virtual DbSet<Sysmz_mst_reports> Sysmz_mst_reports { get; set; }
        public virtual DbSet<SysMZ_MST_RepParamType> SysMZ_MST_RepParamType { get; set; }
        public virtual DbSet<SysMZ_Tables_Tags> SysMZ_Tables_Tags { get; set; }
        public virtual DbSet<User_det_DailyTask> User_det_DailyTask { get; set; }
        public virtual DbSet<Client_Deliverables> Client_Deliverables { get; set; }
        public virtual DbSet<Client_Meetings> Client_Meetings { get; set; }
        public virtual DbSet<Client_Mst_Versions> Client_Mst_Versions { get; set; }
        public virtual DbSet<EmailBilling_Detail> EmailBilling_Detail { get; set; }
        public virtual DbSet<EmailSupport_backup> EmailSupport_backup { get; set; }
        public virtual DbSet<EmailSupport_Detail> EmailSupport_Detail { get; set; }
        public virtual DbSet<Gen_Mst_BillingEmails> Gen_Mst_BillingEmails { get; set; }
        public virtual DbSet<Gen_Mst_BusLogic> Gen_Mst_BusLogic { get; set; }
        public virtual DbSet<Gen_Mst_Client_EmailID> Gen_Mst_Client_EmailID { get; set; }
        public virtual DbSet<Gen_Mst_ClientMachines_BACKUP> Gen_Mst_ClientMachines_BACKUP { get; set; }
        public virtual DbSet<Gen_Mst_Emails> Gen_Mst_Emails { get; set; }
        public virtual DbSet<GEN_MST_EMAILS_5312013> GEN_MST_EMAILS_5312013 { get; set; }
        public virtual DbSet<Gen_Mst_Employee> Gen_Mst_Employee { get; set; }
        public virtual DbSet<Gen_Mst_Requirements> Gen_Mst_Requirements { get; set; }
        public virtual DbSet<Gen_Mst_TabPage> Gen_Mst_TabPage { get; set; }
        public virtual DbSet<Gen_Mst_VersionUpdate> Gen_Mst_VersionUpdate { get; set; }
        public virtual DbSet<Gen_Mst_VersionUpdate_BACKUP> Gen_Mst_VersionUpdate_BACKUP { get; set; }
        public virtual DbSet<Issue_Defects_backup_11012012> Issue_Defects_backup_11012012 { get; set; }
        public virtual DbSet<Issue_Defects_backup_11062012> Issue_Defects_backup_11062012 { get; set; }
        public virtual DbSet<Issue_Testcases_backup_11012012> Issue_Testcases_backup_11012012 { get; set; }
        public virtual DbSet<IssueDetails_backup_11012012> IssueDetails_backup_11012012 { get; set; }
        public virtual DbSet<IssueMaster_backup_11012012> IssueMaster_backup_11012012 { get; set; }
        public virtual DbSet<IssueMasterOld> IssueMasterOlds { get; set; }
        public virtual DbSet<MyTask_Details> MyTask_Details { get; set; }
        public virtual DbSet<SysMZ_Static_Values> SysMZ_Static_Values { get; set; }
        public virtual DbSet<SysMZ_Static_Values_Temp> SysMZ_Static_Values_Temp { get; set; }
        public virtual DbSet<TEMP_SATHISH> TEMP_SATHISH { get; set; }
        public virtual DbSet<SysMZ_Static_Values1> SysMZ_Static_Values1 { get; set; }
        public virtual DbSet<SysMZ_Static_Values_status> SysMZ_Static_Values_status { get; set; }
        public virtual DbSet<User_det_DailyTask_oLD> User_det_DailyTask_oLD { get; set; }
        public virtual DbSet<UserRegistration> UserRegistrations { get; set; }
    }
}
